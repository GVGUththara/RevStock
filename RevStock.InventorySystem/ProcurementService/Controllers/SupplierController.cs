using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcurementService.DTOs.Supplier;
using ProcurementService.Services.Interfaces;

namespace ProcurementService.Controllers
{
    [ApiController]
    [Route("revstock/api/suppliers")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,ProcurementOfficer,Manager")]
        [HttpGet("getAllSuppliers")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [Authorize(Roles = "Admin")]
        [HttpPost("createSupplier")]
        public async Task<IActionResult> Create(CreateSupplierDto dto)
            => Ok(await _service.CreateAsync(dto));

        [Authorize(Roles = "Admin")]
        [HttpPut("updateSupplier/{id}")]
        public async Task<IActionResult> Update(Guid id, CreateSupplierDto dto)
            => await _service.UpdateAsync(id, dto) ? Ok() : NotFound();

        [Authorize(Roles = "Admin")]
        [HttpDelete("deactivateSupplier/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
            => await _service.DeactivateAsync(id) ? Ok() : NotFound();
    }
}
