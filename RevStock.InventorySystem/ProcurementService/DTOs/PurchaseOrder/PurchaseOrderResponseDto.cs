namespace ProcurementService.DTOs.PurchaseOrder
{
    public class PurchaseOrderResponseDto
    {
        public Guid PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
