using KidSMedia_API.Data.Entities;
using KidSMedia_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KidSMedia_API.Data.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<bool> AddUserAsync(User user)
    {
        context.Users.Add(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        User? user = await context.Users
            .FirstOrDefaultAsync(a => a.Email == email);
        return user;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        if (user == null) throw new ArgumentNullException("User with specified Id was not found");
        return user;
    }

    public async Task<bool> RemoveUserAsync(User user)
    {
        context.Users.Remove(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        context.Users.Update(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }
}
