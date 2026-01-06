using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UserService.Data;
using UserService.Models;
using UserService.DTOs.Auth;

namespace UserService.Controllers
{
    [ApiController]
    [Route("revstock/api")]
    public class AuthInternalController : ControllerBase
    {
        private readonly UserDbContext _context;

        public AuthInternalController(UserDbContext context)
        {
            _context = context;
        }

        [HttpPost("validateUsers")]
        public async Task<ActionResult<ValidateUserResponse>> ValidateUser(
        ValidateUserRequest request)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Identifier || u.Email == request.Identifier);

            if (user == null)
                return Unauthorized();

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized();

            return Ok(new ValidateUserResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.RoleName,
                IsActive = user.IsActive
            });
        }
    }
}
