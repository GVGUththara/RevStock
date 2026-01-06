namespace AuthService.DTOs
{
    public class ValidateUserRequest
    {
        public string Identifier { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
