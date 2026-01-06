namespace InventoryService.DTOs.SparePartCategory
{
    public class SparePartCategoryDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
