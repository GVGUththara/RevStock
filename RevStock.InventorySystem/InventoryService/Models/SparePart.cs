using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Models
{
    [Index(nameof(PartCode), IsUnique = true)]
    public class SparePart
    {
        [Key]
        public Guid SparePartId { get; set; }

        [Required, MaxLength(50)]
        public string PartCode { get; set; }

        [Required, MaxLength(150)]
        public string PartName { get; set; }

        [MaxLength(100)]
        public string VehicleBrand { get; set; }

        [MaxLength(100)]
        public string VehicleModel { get; set; }

        public Guid CategoryId { get; set; }
        public SparePartCategory Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Stock Stock { get; set; }
    }
}
