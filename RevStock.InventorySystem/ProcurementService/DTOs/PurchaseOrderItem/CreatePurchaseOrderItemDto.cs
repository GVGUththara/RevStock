namespace ProcurementService.DTOs.PurchaseOrderItem
{
    public class CreatePurchaseOrderItemDto
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid SparePartId { get; set; }
        public int OrderedQty { get; set; }
        public decimal UnitCost { get; set; }
    }
}
