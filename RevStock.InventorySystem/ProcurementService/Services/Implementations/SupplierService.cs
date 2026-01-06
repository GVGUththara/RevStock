using Microsoft.EntityFrameworkCore;
using ProcurementService.Data;
using ProcurementService.DTOs.Supplier;
using ProcurementService.Models;
using ProcurementService.Services.Interfaces;

namespace ProcurementService.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ProcurementDbContext _db;

        public SupplierService(ProcurementDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<SupplierResponseDto>> GetAllAsync()
            => await _db.Suppliers
                .Where(s => s.IsActive)
                .Select(s => new SupplierResponseDto
                {
                    SupplierId = s.SupplierId,
                    SupplierCode = s.SupplierCode,
                    SupplierName = s.SupplierName,
                    Country = s.Country,
                    Currency = s.Currency,
                    IsActive = s.IsActive
                })
                .ToListAsync();

        public async Task<SupplierResponseDto?> GetByIdAsync(Guid id)
        {
            var s = await _db.Suppliers.FindAsync(id);
            return s == null ? null : new SupplierResponseDto
            {
                SupplierId = s.SupplierId,
                SupplierCode = s.SupplierCode,
                SupplierName = s.SupplierName,
                Country = s.Country,
                Currency = s.Currency,
                IsActive = s.IsActive
            };
        }

        public async Task<Guid> CreateAsync(CreateSupplierDto dto)
        {
            var supplier = new Supplier
            {
                SupplierId = Guid.NewGuid(),
                SupplierCode = dto.SupplierCode,
                SupplierName = dto.SupplierName,
                Country = dto.Country,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                Currency = dto.Currency,
                LeadTimeDays = dto.LeadTimeDays
            };

            _db.Suppliers.Add(supplier);
            await _db.SaveChangesAsync();
            return supplier.SupplierId;
        }

        public async Task<bool> UpdateAsync(Guid id, CreateSupplierDto dto)
        {
            var s = await _db.Suppliers.FindAsync(id);
            if (s == null) return false;

            s.SupplierName = dto.SupplierName;
            s.Country = dto.Country;
            s.Email = dto.Email;
            s.Phone = dto.Phone;
            s.Address = dto.Address;
            s.Currency = dto.Currency;
            s.LeadTimeDays = dto.LeadTimeDays;
            s.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var s = await _db.Suppliers.FindAsync(id);
            if (s == null) return false;

            s.IsActive = false;
            s.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
