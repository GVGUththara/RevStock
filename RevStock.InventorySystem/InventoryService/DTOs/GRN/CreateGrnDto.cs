namespace InventoryService.DTOs.GRN
{
    public class CreateGrnDto
    {
        public Guid PurchaseOrderId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public Guid ReceivedBy { get; set; }
    }
}
