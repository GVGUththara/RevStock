using ProcurementService.DTOs.PurchaseOrder;

namespace ProcurementService.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrderResponseDto>> GetAllAsync();
        Task<PurchaseOrderResponseDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreatePurchaseOrderDto dto);
        Task<bool> SubmitAsync(Guid id);
        Task<bool> ApproveAsync(Guid id, Guid approverId);
        Task<bool> CancelAsync(Guid id);
    }
}
