using InventoryService.DTOs.Stock;
using InventoryService.Models;

namespace InventoryService.Services.Interfaces
{
    public interface IStockService
    {
        Task<IEnumerable<StockResponseDto>> GetAllAsync();
        Task<StockResponseDto?> GetByIdAsync(Guid id);
        Task<StockResponseDto> CreateAsync(CreateStockDto dto);
        Task<bool> UpdateAsync(Guid id, CreateStockDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
