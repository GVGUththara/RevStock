namespace ProcurementService.DTOs.PurchaseOrder
{
    public class ReceivePoItemDto
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid SparePartId { get; set; }
        public int ReceivedQty { get; set; }
    }
}
