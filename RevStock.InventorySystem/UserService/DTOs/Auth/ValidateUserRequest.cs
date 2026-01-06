using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs.Auth
{
    public class ValidateUserRequest
    {
        [Required]
        public string Identifier { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
