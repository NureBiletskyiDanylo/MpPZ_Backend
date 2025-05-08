namespace KidSMedia_API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Role { get; set; } = "User";
    }
}
