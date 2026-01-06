using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcurementService.Models
{
    public class PurchaseOrderItem
    {
        [Key]
        public Guid POItemId { get; set; }

        [Required]
        public Guid PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        // Reference to Inventory Service (NO FK constraint)
        [Required]
        public Guid SparePartId { get; set; }

        public int OrderedQty { get; set; }

        public int ReceivedQty { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitCost { get; set; }
    }
}
