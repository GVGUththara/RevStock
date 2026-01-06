namespace InventoryService.DTOs.GRN
{
    public class GrnResponseDto
    {
        public Guid GRNId { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public Guid ReceivedBy { get; set; }
        public string Status { get; set; }

        public List<GrnItemResponseDto> Items { get; set; }
    }
}
