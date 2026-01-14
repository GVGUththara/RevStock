using Microsoft.EntityFrameworkCore;
using SalesService.Data;
using SalesService.DTOs;
using SalesService.Models;
using SalesService.Services.Interfaces;

namespace SalesService.Services.Implementations
{
    public class SalesService : ISalesService
    {
        private readonly SalesDbContext _context;
        private readonly InventoryClient _inventory;

        public SalesService(SalesDbContext context, InventoryClient inventory)
        {
            _context = context;
            _inventory = inventory;
        }

        public async Task<Guid> CreateInvoiceAsync(CreateSalesInvoiceDto dto, Guid userId)
        {
            var invoice = new SalesInvoice
            {
                SalesInvoiceId = Guid.NewGuid(),
                InvoiceNumber = $"INV-{DateTime.UtcNow.Ticks}",
                InvoiceDate = DateTime.UtcNow,
                CustomerName = dto.CustomerName,
                CustomerPhone = dto.CustomerPhone,
                Status = "DRAFT",
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TotalAmount = 0
            };

            _context.SalesInvoices.Add(invoice);
            await _context.SaveChangesAsync();

            return invoice.SalesInvoiceId;
        }

        public async Task<bool> AddItemAsync(AddSalesItemDto dto)
        {
            var invoice = await _context.SalesInvoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == dto.SalesInvoiceId);

            if (invoice == null || invoice.Status != "DRAFT")
                return false;

            var item = new SalesInvoiceItem
            {
                SalesItemId = Guid.NewGuid(),
                SalesInvoiceId = invoice.SalesInvoiceId,
                SparePartId = dto.SparePartId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                LineTotal = dto.Quantity * dto.UnitPrice
            };

            invoice.Items.Add(item);
            invoice.TotalAmount = invoice.Items.Sum(i => i.LineTotal);
            invoice.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PostInvoiceAsync(Guid invoiceId, string token)
        {
            var invoice = await _context.SalesInvoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == invoiceId);

            if (invoice == null || invoice.Status != "DRAFT")
                return false;

            foreach (var item in invoice.Items)
            {
                var success = await _inventory.DeductStock(
                    item.SparePartId, item.Quantity, token);

                if (!success) return false;
            }

            invoice.Status = "POSTED";
            invoice.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
