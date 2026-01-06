using InventoryService.DTOs.SparePartCategory;
using InventoryService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("revstock/api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,InventoryOfficer,Manager,SalesPerson,ProcurementOfficer")]
        [HttpGet("getAllCategories")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [Authorize(Roles = "Admin,InventoryOfficer,Manager,SalesPerson,ProcurementOfficer")]
        [HttpGet("getCategoryById/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var cat = await _service.GetByIdAsync(id);
            return cat == null ? NotFound() : Ok(cat);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createCategory")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
            => Ok(await _service.CreateAsync(dto));

        [Authorize(Roles = "Admin")]
        [HttpPut("updateCategory/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
            => await _service.UpdateAsync(id, dto) ? Ok() : NotFound();

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteCategory/{id}")]
        public async Task<IActionResult> Delete(Guid id)
            => await _service.DeleteAsync(id) ? Ok() : NotFound();
    }
}
