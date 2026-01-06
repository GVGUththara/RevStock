namespace InventoryService.DTOs.SparePart
{
    public class SparePartDto
    {
        public Guid SparePartId { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public Guid CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
