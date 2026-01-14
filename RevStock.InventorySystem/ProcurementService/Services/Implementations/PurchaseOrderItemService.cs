using Microsoft.EntityFrameworkCore;
using ProcurementService.Data;
using ProcurementService.DTOs.PurchaseOrderItem;
using ProcurementService.Models;
using ProcurementService.Services.Interfaces;

namespace ProcurementService.Services.Implementations
{
    public class PurchaseOrderItemService : IPurchaseOrderItemService
    {
        private readonly ProcurementDbContext _context;

        public PurchaseOrderItemService(ProcurementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemAsync(CreatePurchaseOrderItemDto dto)
        {
            var po = await _context.PurchaseOrders
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.PurchaseOrderId == dto.PurchaseOrderId);

            if (po == null || po.Status != "DRAFT")
                return false;

            po.Items.Add(new PurchaseOrderItem
            {
                POItemId = Guid.NewGuid(),
                PurchaseOrderId = po.PurchaseOrderId,
                SparePartId = dto.SparePartId,
                OrderedQty = dto.OrderedQty,
                UnitCost = dto.UnitCost
            });

            RecalculateTotal(po);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateItemAsync(Guid poItemId, UpdatePurchaseOrderItemDto dto)
        {
            var item = await _context.PurchaseOrderItems
                .Include(i => i.PurchaseOrder)
                .ThenInclude(p => p.Items)
                .FirstOrDefaultAsync(i => i.POItemId == poItemId);

            if (item == null || item.PurchaseOrder.Status != "DRAFT")
                return false;

            item.OrderedQty = dto.OrderedQty;
            item.UnitCost = dto.UnitCost;

            RecalculateTotal(item.PurchaseOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveItemAsync(Guid poItemId)
        {
            var item = await _context.PurchaseOrderItems
                .Include(i => i.PurchaseOrder)
                .ThenInclude(p => p.Items)
                .FirstOrDefaultAsync(i => i.POItemId == poItemId);

            if (item == null || item.PurchaseOrder.Status != "DRAFT")
                return false;

            _context.PurchaseOrderItems.Remove(item);
            RecalculateTotal(item.PurchaseOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        private void RecalculateTotal(PurchaseOrder po)
        {
            po.TotalAmount = po.Items.Sum(i => i.OrderedQty * i.UnitCost);
            po.UpdatedAt = DateTime.UtcNow;
        }
    }
}
