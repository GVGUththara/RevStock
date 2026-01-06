using AuthService.DTOs;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("revstock/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JwtTokenService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthController(
            IHttpClientFactory httpClientFactory,
            JwtTokenService jwtService,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var userServiceBaseUrl = _configuration["Services:UserService:BaseUrl"];

            var response = await client.PostAsJsonAsync(
                $"{userServiceBaseUrl}/revstock/api/validateUsers",
                new ValidateUserRequest
                {
                    Identifier = dto.Identifier,
                    Password = dto.Password
                });

            if (!response.IsSuccessStatusCode)
                return Unauthorized("Invalid credentials");

            var user = await response.Content
                .ReadFromJsonAsync<ValidateUserResponse>();

            var token = _jwtService.GenerateToken(
                user!.UserId,
                user.Username,
                user.Role
            );

            return Ok(new
            {
                token,
                expiresIn = 3600,
                role = user.Role
            });
        }
    }

}
