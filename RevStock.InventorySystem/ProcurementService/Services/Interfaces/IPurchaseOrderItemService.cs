using ProcurementService.DTOs.PurchaseOrderItem;

namespace ProcurementService.Services.Interfaces
{
    public interface IPurchaseOrderItemService
    {
        Task<bool> AddItemAsync(CreatePurchaseOrderItemDto dto);
        Task<bool> UpdateItemAsync(Guid poItemId, UpdatePurchaseOrderItemDto dto);
        Task<bool> RemoveItemAsync(Guid poItemId);
    }
}
