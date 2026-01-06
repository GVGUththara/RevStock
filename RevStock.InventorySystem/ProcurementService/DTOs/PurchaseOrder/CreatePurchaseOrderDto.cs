namespace ProcurementService.DTOs.PurchaseOrder
{
    public class CreatePurchaseOrderDto
    {
        public Guid SupplierId { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
