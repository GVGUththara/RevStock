using Microsoft.EntityFrameworkCore;
using InventoryService.Data;
using InventoryService.DTOs.Stock;
using InventoryService.Models;
using InventoryService.Services.Interfaces;

namespace InventoryService.Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly InventoryDbContext _context;

        public StockService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockResponseDto>> GetAllAsync()
        {
            return await _context.Stocks
                .Select(s => new StockResponseDto
                {
                    StockId = s.StockId,
                    SparePartId = s.SparePartId,
                    AvailableQty = s.AvailableQty,
                    ReservedQty = s.ReservedQty,
                    MinimumStockLevel = s.MinimumStockLevel,
                    MaximumStockLevel = s.MaximumStockLevel,
                    ReorderLevel = s.ReorderLevel
                })
                .ToListAsync();
        }

        public async Task<StockResponseDto?> GetByIdAsync(Guid id)
        {
            return await _context.Stocks
                .Where(s => s.StockId == id)
                .Select(s => new StockResponseDto
                {
                    StockId = s.StockId,
                    SparePartId = s.SparePartId,
                    AvailableQty = s.AvailableQty,
                    ReservedQty = s.ReservedQty,
                    MinimumStockLevel = s.MinimumStockLevel,
                    MaximumStockLevel = s.MaximumStockLevel,
                    ReorderLevel = s.ReorderLevel
                })
                .FirstOrDefaultAsync();
        }

        public async Task<StockResponseDto> CreateAsync(CreateStockDto dto)
        {
            var stock = new Stock
            {
                StockId = Guid.NewGuid(),
                SparePartId = dto.SparePartId,
                AvailableQty = dto.AvailableQty,
                ReservedQty = dto.ReservedQty,
                MinimumStockLevel = dto.MinimumStockLevel,
                MaximumStockLevel = dto.MaximumStockLevel,
                ReorderLevel = dto.ReorderLevel
            };

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return new StockResponseDto
            {
                StockId = stock.StockId,
                SparePartId = stock.SparePartId,
                AvailableQty = stock.AvailableQty,
                ReservedQty = stock.ReservedQty,
                MinimumStockLevel = stock.MinimumStockLevel,
                MaximumStockLevel = stock.MaximumStockLevel,
                ReorderLevel = stock.ReorderLevel
            };
        }

        public async Task<bool> UpdateAsync(Guid id, CreateStockDto dto)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return false;

            stock.AvailableQty = dto.AvailableQty;
            stock.ReservedQty = dto.ReservedQty;
            stock.MinimumStockLevel = dto.MinimumStockLevel;
            stock.MaximumStockLevel = dto.MaximumStockLevel;
            stock.ReorderLevel = dto.ReorderLevel;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return false;

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
