using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}
