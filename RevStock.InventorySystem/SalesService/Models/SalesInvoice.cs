using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SalesService.Models
{
    [Index(nameof(InvoiceNumber), IsUnique = true)]
    public class SalesInvoice
    {
        [Key]
        public Guid SalesInvoiceId { get; set; }

        [Required, MaxLength(50)]
        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required, MaxLength(150)]
        public string CustomerName { get; set; }

        [MaxLength(50)]
        public string CustomerPhone { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }  // DRAFT / POSTED / CANCELLED

        [Required]
        public Guid CreatedBy { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<SalesInvoiceItem> Items { get; set; }
    }
}
