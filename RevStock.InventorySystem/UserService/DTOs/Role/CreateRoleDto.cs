using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs.Role
{
    public class CreateRoleDto
    {
        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }
    }
}
