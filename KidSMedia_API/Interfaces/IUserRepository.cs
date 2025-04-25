using KidSMedia_API.Data.Entities;

namespace KidSMedia_API.Interfaces;

public interface IUserRepository
{
    Task<bool> AddUserAsync(User user);
    Task<bool> RemoveUserAsync(User user);
    Task<User> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> UserExistsAsync(string email);
}
