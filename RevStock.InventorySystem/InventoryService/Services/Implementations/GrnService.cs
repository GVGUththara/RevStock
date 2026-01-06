using Microsoft.EntityFrameworkCore;
using InventoryService.Data;
using InventoryService.DTOs.GRN;
using InventoryService.Models;
using InventoryService.Services.Interfaces;

namespace InventoryService.Services.Implementations
{
    public class GrnService : IGrnService
    {
        private readonly InventoryDbContext _context;

        public GrnService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GrnResponseDto>> GetAllAsync()
        {
            return await _context.GRNs
                .Include(g => g.Items)
                .Select(g => new GrnResponseDto
                {
                    GRNId = g.GRNId,
                    PurchaseOrderId = g.PurchaseOrderId,
                    ReceivedDate = g.ReceivedDate,
                    ReceivedBy = g.ReceivedBy,
                    Status = g.Status,
                    Items = g.Items.Select(i => new GrnItemResponseDto
                    {
                        GRNItemId = i.GRNItemId,
                        SparePartId = i.SparePartId,
                        ReceivedQty = i.ReceivedQty,
                        UnitCost = i.UnitCost
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<GrnResponseDto?> GetByIdAsync(Guid id)
        {
            return await _context.GRNs
                .Include(g => g.Items)
                .Where(g => g.GRNId == id)
                .Select(g => new GrnResponseDto
                {
                    GRNId = g.GRNId,
                    PurchaseOrderId = g.PurchaseOrderId,
                    ReceivedDate = g.ReceivedDate,
                    ReceivedBy = g.ReceivedBy,
                    Status = g.Status,
                    Items = g.Items.Select(i => new GrnItemResponseDto
                    {
                        GRNItemId = i.GRNItemId,
                        SparePartId = i.SparePartId,
                        ReceivedQty = i.ReceivedQty,
                        UnitCost = i.UnitCost
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<GRN> CreateAsync(CreateGrnDto dto)
        {
            var grn = new GRN
            {
                GRNId = Guid.NewGuid(),
                PurchaseOrderId = dto.PurchaseOrderId,
                ReceivedDate = dto.ReceivedDate,
                ReceivedBy = dto.ReceivedBy,
                Status = "DRAFT",
                Items = new List<GRNItem>()
            };

            _context.GRNs.Add(grn);
            await _context.SaveChangesAsync();
            return grn;
        }

        public async Task<GrnItemResponseDto> AddItemAsync(CreateGrnItemDto dto)
        {
            var grn = await _context.GRNs.FindAsync(dto.GRNId);
            if (grn == null || grn.Status != "DRAFT")
                throw new Exception("GRN not found or already posted");

            var item = new GRNItem
            {
                GRNItemId = Guid.NewGuid(),
                GRNId = dto.GRNId,
                SparePartId = dto.SparePartId,
                ReceivedQty = dto.ReceivedQty,
                UnitCost = dto.UnitCost
            };

            _context.GRNItems.Add(item);
            await _context.SaveChangesAsync();

            return new GrnItemResponseDto
            {
                GRNItemId = item.GRNItemId,
                GRNId = item.GRNId,
                SparePartId = item.SparePartId,
                ReceivedQty = item.ReceivedQty,
                UnitCost = item.UnitCost
            };
        }

        public async Task<bool> PostAsync(Guid grnId)
        {
            var grn = await _context.GRNs
                .Include(g => g.Items)
                .FirstOrDefaultAsync(g => g.GRNId == grnId);

            if (grn == null || grn.Status == "POSTED")
                return false;

            if (!grn.Items.Any())
                throw new Exception("Cannot post GRN without items");

            foreach (var item in grn.Items)
            {
                // 1️⃣ Update Stock
                var stock = await _context.Stocks
                    .FirstOrDefaultAsync(s => s.SparePartId == item.SparePartId);

                if (stock == null)
                {
                    stock = new Stock
                    {
                        StockId = Guid.NewGuid(),
                        SparePartId = item.SparePartId,
                        AvailableQty = item.ReceivedQty,
                        ReservedQty = 0
                    };
                    _context.Stocks.Add(stock);
                }
                else
                {
                    stock.AvailableQty += item.ReceivedQty;
                }

                // 2️⃣ Stock Ledger entry
                _context.StockLedgers.Add(new StockLedger
                {
                    StockLedgerId = Guid.NewGuid(),
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = "GRN",
                    Quantity = item.ReceivedQty,
                    UnitCost = item.UnitCost,
                    Reference = $"GRN:{grn.GRNId}"
                });
            }

            grn.Status = "POSTED";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
