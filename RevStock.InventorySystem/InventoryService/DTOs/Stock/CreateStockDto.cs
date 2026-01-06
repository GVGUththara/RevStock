namespace InventoryService.DTOs.Stock
{
    public class CreateStockDto
    {
        public Guid SparePartId { get; set; }
        public int AvailableQty { get; set; }
        public int ReservedQty { get; set; }
        public int MinimumStockLevel { get; set; }
        public int MaximumStockLevel { get; set; }
        public int ReorderLevel { get; set; }
    }
}
