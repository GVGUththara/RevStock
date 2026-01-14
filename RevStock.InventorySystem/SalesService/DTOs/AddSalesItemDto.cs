namespace SalesService.DTOs
{
    public class AddSalesItemDto
    {
        public Guid SalesInvoiceId { get; set; }
        public Guid SparePartId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
