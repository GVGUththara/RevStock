namespace ProcurementService.DTOs.Supplier
{
    public class SupplierResponseDto
    {
        public Guid SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
    }
}
