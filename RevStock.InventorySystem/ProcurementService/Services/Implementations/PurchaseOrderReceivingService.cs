using Microsoft.EntityFrameworkCore;
using ProcurementService.Data;
using ProcurementService.DTOs.PurchaseOrder;
using ProcurementService.Services.Interfaces;

namespace ProcurementService.Services.Implementations
{
    public class PurchaseOrderReceivingService : IPurchaseOrderReceivingService
    {
        private readonly ProcurementDbContext _context;

        public PurchaseOrderReceivingService(ProcurementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ReceiveAsync(ReceivePoItemDto dto)
        {
            var po = await _context.PurchaseOrders
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.PurchaseOrderId == dto.PurchaseOrderId);

            if (po == null || po.Status == "DRAFT" || po.Status == "SUBMITTED")
                return false;

            var item = po.Items.FirstOrDefault(i => i.SparePartId == dto.SparePartId);
            if (item == null)
                return false;

            item.ReceivedQty += dto.ReceivedQty;

            po.Status = po.Items.All(i => i.ReceivedQty >= i.OrderedQty)
                ? "COMPLETED"
                : "PARTIALLY_RECEIVED";

            po.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
