namespace InventoryService.DTOs.GRN
{
    public class GrnItemResponseDto
    {
        public Guid GRNItemId { get; set; }
        public Guid GRNId { get; set; }
        public Guid SparePartId { get; set; }
        public int ReceivedQty { get; set; }
        public decimal UnitCost { get; set; }
    }
}
