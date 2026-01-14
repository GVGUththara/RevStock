namespace SalesService.Models
{
    public class SalesInvoice
    {
        public Guid SalesInvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }

        public string Status { get; set; } // DRAFT, POSTED, CANCELLED

        public Guid CreatedBy { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<SalesInvoiceItem> Items { get; set; }
    }
}
