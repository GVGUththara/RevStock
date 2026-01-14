using ProcurementService.DTOs.PurchaseOrder;

namespace ProcurementService.Services.Interfaces
{
    public interface IPurchaseOrderReceivingService
    {
        Task<bool> ReceiveAsync(ReceivePoItemDto dto);
    }
}
