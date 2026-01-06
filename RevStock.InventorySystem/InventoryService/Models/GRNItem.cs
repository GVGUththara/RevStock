using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Models
{
    public class GRNItem
    {
        [Key]
        public Guid GRNItemId { get; set; }

        public Guid GRNId { get; set; }
        public GRN GRN { get; set; }

        public Guid SparePartId { get; set; }
        public SparePart SparePart { get; set; }

        public int ReceivedQty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitCost { get; set; }
    }
}
