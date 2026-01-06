using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models
{
    public class StockLedger
    {
        [Key]
        public Guid StockLedgerId { get; set; }

        public Guid SparePartId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TransactionType { get; set; }
        // "GRN", "ISSUE", "ADJUST"

        public int Quantity { get; set; }

        public decimal UnitCost { get; set; }

        [MaxLength(100)]
        public string Reference { get; set; }
        // Example: "GRN:1234-5678"

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
