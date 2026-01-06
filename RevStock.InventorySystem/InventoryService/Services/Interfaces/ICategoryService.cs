using InventoryService.DTOs.SparePartCategory;

namespace InventoryService.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<SparePartCategoryDto>> GetAllAsync();
        Task<SparePartCategoryDto> GetByIdAsync(Guid id);
        Task<SparePartCategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
