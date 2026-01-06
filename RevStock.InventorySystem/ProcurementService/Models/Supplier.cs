using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProcurementService.Models
{
    [Index(nameof(SupplierCode), IsUnique = true)]
    public class Supplier
    {
        [Key]
        public Guid SupplierId { get; set; }

        [Required, MaxLength(50)]
        public string SupplierCode { get; set; }

        [Required, MaxLength(150)]
        public string SupplierName { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; }

        public int LeadTimeDays { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
