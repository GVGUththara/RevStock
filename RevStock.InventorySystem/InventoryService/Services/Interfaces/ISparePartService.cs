using InventoryService.DTOs.SparePart;

namespace InventoryService.Services.Interfaces
{
    public interface ISparePartService
    {
        Task<IEnumerable<SparePartDto>> GetAllAsync();
        Task<SparePartDto> GetByIdAsync(Guid id);
        Task<SparePartDto> CreateAsync(CreateSparePartDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateSparePartDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
