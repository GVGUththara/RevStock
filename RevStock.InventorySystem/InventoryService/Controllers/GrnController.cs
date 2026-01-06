using InventoryService.DTOs.GRN;
using InventoryService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("revstock/api/grn")]
    public class GrnController : ControllerBase
    {
        private readonly IGrnService _service;

        public GrnController(IGrnService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,InventoryOfficer,Manager")]
        [HttpGet("getAllGrns")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [Authorize(Roles = "Admin,InventoryOfficer,Manager")]
        [HttpGet("getGrnById/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var grn = await _service.GetByIdAsync(id);
            return grn == null ? NotFound() : Ok(grn);
        }

        [Authorize(Roles = "InventoryOfficer")]
        [HttpPost("createGrn")]
        public async Task<IActionResult> Create(CreateGrnDto dto)
            => Ok(await _service.CreateAsync(dto));

        [Authorize(Roles = "InventoryOfficer")]
        [HttpPost("addGrnItem")]
        public async Task<IActionResult> AddItem(CreateGrnItemDto dto)
        {
            var result = await _service.AddItemAsync(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("postGrn/{id}")]
        public async Task<IActionResult> Post(Guid id)
            => await _service.PostAsync(id) ? Ok() : BadRequest();
    }
}
