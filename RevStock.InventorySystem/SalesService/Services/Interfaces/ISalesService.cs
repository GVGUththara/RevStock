using SalesService.DTOs;

namespace SalesService.Services.Interfaces
{
    public interface ISalesService
    {
        Task<Guid> CreateInvoiceAsync(CreateSalesInvoiceDto dto, Guid userId);
        Task<bool> AddItemAsync(AddSalesItemDto dto);
        Task<bool> PostInvoiceAsync(Guid invoiceId, string token);
    }
}
