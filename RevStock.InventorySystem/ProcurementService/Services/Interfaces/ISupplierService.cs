using ProcurementService.DTOs.Supplier;

namespace ProcurementService.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierResponseDto>> GetAllAsync();
        Task<SupplierResponseDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreateSupplierDto dto);
        Task<bool> UpdateAsync(Guid id, CreateSupplierDto dto);
        Task<bool> DeactivateAsync(Guid id);
    }
}
