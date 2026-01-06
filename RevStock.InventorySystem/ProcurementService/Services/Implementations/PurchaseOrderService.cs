using Microsoft.EntityFrameworkCore;
using ProcurementService.Data;
using ProcurementService.DTOs.PurchaseOrder;
using ProcurementService.Models;
using ProcurementService.Services.Interfaces;

namespace ProcurementService.Services.Implementations
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly ProcurementDbContext _db;

        public PurchaseOrderService(ProcurementDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PurchaseOrderResponseDto>> GetAllAsync()
            => await _db.PurchaseOrders
                .Select(po => new PurchaseOrderResponseDto
                {
                    PurchaseOrderId = po.PurchaseOrderId,
                    PurchaseOrderNumber = po.PurchaseOrderNumber,
                    Status = po.Status,
                    TotalAmount = po.TotalAmount
                })
                .ToListAsync();

        public async Task<PurchaseOrderResponseDto?> GetByIdAsync(Guid id)
        {
            var po = await _db.PurchaseOrders.FindAsync(id);
            return po == null ? null : new PurchaseOrderResponseDto
            {
                PurchaseOrderId = po.PurchaseOrderId,
                PurchaseOrderNumber = po.PurchaseOrderNumber,
                Status = po.Status,
                TotalAmount = po.TotalAmount
            };
        }

        public async Task<Guid> CreateAsync(CreatePurchaseOrderDto dto)
        {
            var po = new PurchaseOrder
            {
                PurchaseOrderId = Guid.NewGuid(),
                PurchaseOrderNumber = $"PO-{DateTime.UtcNow:yyyyMMddHHmmss}",
                SupplierId = dto.SupplierId,
                OrderDate = DateTime.UtcNow,
                ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
                Status = "DRAFT",
                CreatedBy = dto.CreatedBy
            };

            _db.PurchaseOrders.Add(po);
            await _db.SaveChangesAsync();
            return po.PurchaseOrderId;
        }

        public async Task<bool> SubmitAsync(Guid id)
        {
            var po = await _db.PurchaseOrders.FindAsync(id);
            if (po == null || po.Status != "DRAFT") return false;

            po.Status = "SUBMITTED";
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveAsync(Guid id, Guid approverId)
        {
            var po = await _db.PurchaseOrders.FindAsync(id);
            if (po == null || po.Status != "SUBMITTED") return false;

            po.Status = "APPROVED";
            po.ApprovedBy = approverId;
            po.ApprovedDate = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            var po = await _db.PurchaseOrders.FindAsync(id);
            if (po == null) return false;

            po.Status = "CANCELLED";
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
