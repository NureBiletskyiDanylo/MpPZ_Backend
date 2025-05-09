using KidSMedia_API.Enums;

namespace KidSMedia_API.DTOs;

public class AccountDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public Roles Role { get; set; } = Roles.User;
}
