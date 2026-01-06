using Microsoft.EntityFrameworkCore;
using InventoryService.Data;
using InventoryService.DTOs.SparePart;
using InventoryService.Models;
using InventoryService.Services.Interfaces;

namespace InventoryService.Services.Implementations
{
    public class SparePartService : ISparePartService
    {
        private readonly InventoryDbContext _context;

        public SparePartService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SparePartDto>> GetAllAsync()
        {
            return await _context.SpareParts
                .Select(p => new SparePartDto
                {
                    SparePartId = p.SparePartId,
                    PartCode = p.PartCode,
                    PartName = p.PartName,
                    VehicleBrand = p.VehicleBrand,
                    VehicleModel = p.VehicleModel,
                    CategoryId = p.CategoryId,
                    UnitPrice = p.UnitPrice,
                    IsActive = p.IsActive
                }).ToListAsync();
        }

        public async Task<SparePartDto> GetByIdAsync(Guid id)
        {
            var part = await _context.SpareParts.FindAsync(id);
            if (part == null) return null;

            return new SparePartDto
            {
                SparePartId = part.SparePartId,
                PartCode = part.PartCode,
                PartName = part.PartName,
                VehicleBrand = part.VehicleBrand,
                VehicleModel = part.VehicleModel,
                CategoryId = part.CategoryId,
                UnitPrice = part.UnitPrice,
                IsActive = part.IsActive
            };
        }

        public async Task<SparePartDto> CreateAsync(CreateSparePartDto dto)
        {
            var part = new SparePart
            {
                SparePartId = Guid.NewGuid(),
                PartCode = dto.PartCode,
                PartName = dto.PartName,
                VehicleBrand = dto.VehicleBrand,
                VehicleModel = dto.VehicleModel,
                CategoryId = dto.CategoryId,
                UnitPrice = dto.UnitPrice
            };

            _context.SpareParts.Add(part);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(part.SparePartId);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateSparePartDto dto)
        {
            var part = await _context.SpareParts.FindAsync(id);
            if (part == null) return false;

            part.PartName = dto.PartName ?? part.PartName;
            part.VehicleBrand = dto.VehicleBrand;
            part.VehicleModel = dto.VehicleModel;
            part.CategoryId = dto.CategoryId;
            part.UnitPrice = dto.UnitPrice;
            part.IsActive = dto.IsActive;
            part.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var part = await _context.SpareParts.FindAsync(id);
            if (part == null) return false;

            _context.SpareParts.Remove(part);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
