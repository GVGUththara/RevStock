using Microsoft.EntityFrameworkCore;
using InventoryService.Data;
using InventoryService.DTOs.SparePartCategory;
using InventoryService.Models;
using InventoryService.Services.Interfaces;

namespace InventoryService.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly InventoryDbContext _context;

        public CategoryService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SparePartCategoryDto>> GetAllAsync()
        {
            return await _context.SparePartCategories
                .Select(c => new SparePartCategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    IsActive = c.IsActive
                }).ToListAsync();
        }

        public async Task<SparePartCategoryDto> GetByIdAsync(Guid id)
        {
            var category = await _context.SparePartCategories.FindAsync(id);
            if (category == null) return null;

            return new SparePartCategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                IsActive = category.IsActive
            };
        }

        public async Task<SparePartCategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new SparePartCategory
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = dto.CategoryName
            };

            _context.SparePartCategories.Add(category);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(category.CategoryId);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _context.SparePartCategories.FindAsync(id);
            if (category == null) return false;

            category.CategoryName = dto.CategoryName ?? category.CategoryName;
            category.IsActive = dto.IsActive;
            category.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _context.SparePartCategories.FindAsync(id);
            if (category == null) return false;

            _context.SparePartCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
