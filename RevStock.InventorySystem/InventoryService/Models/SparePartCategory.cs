using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models
{
    public class SparePartCategory
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string CategoryName { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<SparePart> SpareParts { get; set; }
    }
}
