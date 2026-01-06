using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs.User;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("revstock/api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpPut("updateUserById/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
            => await _service.UpdateAsync(id, dto) ? Ok() : NotFound();

        [HttpDelete("deleteUserById/{id}")]
        public async Task<IActionResult> Delete(Guid id)
            => await _service.DeleteAsync(id) ? Ok() : NotFound();
    }
}
