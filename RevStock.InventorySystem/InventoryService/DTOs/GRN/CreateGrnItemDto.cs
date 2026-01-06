namespace InventoryService.DTOs.GRN
{
    public class CreateGrnItemDto
    {
        public Guid GRNId { get; set; }
        public Guid SparePartId { get; set; }
        public int ReceivedQty { get; set; }
        public decimal UnitCost { get; set; }
    }
}
