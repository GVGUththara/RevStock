using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.DTOs.Role;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("revstock/api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet("getAllRoles")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("getRoleById/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var role = await _service.GetByIdAsync(id);
            return role == null ? NotFound() : Ok(role);
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> Create([FromBody] CreateRoleDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpDelete("deleteRoleById/{id}")]
        public async Task<IActionResult> Delete(Guid id)
            => await _service.DeleteAsync(id) ? Ok() : NotFound();
    }
}
