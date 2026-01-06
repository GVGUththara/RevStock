using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models
{
    public class GRN
    {
        [Key]
        public Guid GRNId { get; set; }

        public Guid PurchaseOrderId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public Guid ReceivedBy { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public ICollection<GRNItem> Items { get; set; }
    }
}
