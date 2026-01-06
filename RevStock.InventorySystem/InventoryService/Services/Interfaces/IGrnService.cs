using InventoryService.DTOs.GRN;
using InventoryService.Models;

namespace InventoryService.Services.Interfaces
{
    public interface IGrnService
    {
        Task<IEnumerable<GrnResponseDto>> GetAllAsync();
        Task<GrnResponseDto?> GetByIdAsync(Guid id);

        Task<GRN> CreateAsync(CreateGrnDto dto);
        Task<GrnItemResponseDto> AddItemAsync(CreateGrnItemDto dto);

        Task<bool> PostAsync(Guid grnId);
    }
}
