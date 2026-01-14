namespace SalesService.Models
{
    public class SalesInvoiceItem
    {
        public Guid SalesItemId { get; set; }
        public Guid SalesInvoiceId { get; set; }

        public Guid SparePartId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        public SalesInvoice SalesInvoice { get; set; }
    }
}
