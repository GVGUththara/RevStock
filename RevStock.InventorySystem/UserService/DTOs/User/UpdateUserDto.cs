namespace UserService.DTOs.User
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }
    }
}
