using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesService.Models
{
    public class SalesInvoiceItem
    {
        [Key]
        public Guid SalesItemId { get; set; }

        [Required]
        public Guid SalesInvoiceId { get; set; }
        public SalesInvoice SalesInvoice { get; set; }

        // Reference to Inventory Service (NO FK constraint)
        [Required]
        public Guid SparePartId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LineTotal { get; set; } // Quantity * UnitPrice (stored)
    }
}
