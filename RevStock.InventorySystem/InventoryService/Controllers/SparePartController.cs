using InventoryService.DTOs.SparePart;
using InventoryService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("revstock/api/spareparts")]
    public class SparePartController : ControllerBase
    {
        private readonly ISparePartService _service;

        public SparePartController(ISparePartService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,InventoryOfficer,Manager,SalesPerson,ProcurementOfficer")]
        [HttpGet("getAllSpareParts")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [Authorize(Roles = "Admin,InventoryOfficer,Manager,SalesPerson,ProcurementOfficer")]
        [HttpGet("getSparePartById/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var part = await _service.GetByIdAsync(id);
            return part == null ? NotFound() : Ok(part);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createSparePart")]
        public async Task<IActionResult> Create([FromBody] CreateSparePartDto dto)
            => Ok(await _service.CreateAsync(dto));

        [Authorize(Roles = "Admin")]
        [HttpPut("updateSparePart/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSparePartDto dto)
            => await _service.UpdateAsync(id, dto) ? Ok() : NotFound();

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteSparePart/{id}")]
        public async Task<IActionResult> Delete(Guid id)
            => await _service.DeleteAsync(id) ? Ok() : NotFound();
    }
}
