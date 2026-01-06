namespace ProcurementService.DTOs.Supplier
{
    public class CreateSupplierDto
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Currency { get; set; }
        public int LeadTimeDays { get; set; }
    }
}
