using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Models
{
    [Index(nameof(SparePartId), IsUnique = true)]
    public class Stock
    {
        [Key]
        public Guid StockId { get; set; }

        public Guid SparePartId { get; set; }
        public SparePart SparePart { get; set; }

        public int AvailableQty { get; set; }
        public int ReservedQty { get; set; }

        public int MinimumStockLevel { get; set; }
        public int MaximumStockLevel { get; set; }
        public int ReorderLevel { get; set; }

        public DateTime? LastStockTakeDate { get; set; }
    }

}
