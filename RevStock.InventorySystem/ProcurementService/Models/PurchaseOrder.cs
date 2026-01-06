using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProcurementService.Models
{
    [Index(nameof(PurchaseOrderNumber), IsUnique = true)]
    public class PurchaseOrder
    {
        [Key]
        public Guid PurchaseOrderId { get; set; }

        [Required, MaxLength(50)]
        public string PurchaseOrderNumber { get; set; }

        [Required]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PurchaseOrderItem> Items { get; set; }
    }
}
