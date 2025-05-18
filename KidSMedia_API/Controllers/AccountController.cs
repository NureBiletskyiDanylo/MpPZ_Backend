using AutoMapper;
using KidSMedia_API.Data.Entities;
using KidSMedia_API.DTOs;
using KidSMedia_API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace KidSMedia_API.Controllers;

public class AccountController(IUserRepository repository, ITokenService tokenService, IMapper mapper) : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        User? user = await repository.GetUserByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized("Invalid credentials");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid credentials");
        }

        return new UserDto
        {
            Username = user.Username,
            Token = tokenService.CreateToken(user),
            Role = user.Role.ToString()
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        User? existingUser = await repository.GetUserByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            return BadRequest("This email is already taken");
        }

        using var hmac = new HMACSHA512();

        var user = new User
        {
            Username = registerDto.Username.ToLower(),
            Email = registerDto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        bool result = await repository.AddUserAsync(user);
        if (!result)
        {
            return BadRequest("Unfortunately, user was not created. Try again later...");
        }

        return new UserDto
        {
            Username = user.Username,
            Token = tokenService.CreateToken(user),
            Role = "User"
        };
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUser(int id, AccountDto dto)
    {
        var user = await repository.GetUserByIdAsync(id);
        if (user == null) return BadRequest("User was not found");

        user.Username = dto.Username;
        user.Email = dto.Email;
        if (user.Role == Enums.Roles.User)
        {
            user.Role = dto.Role;
        }

        bool success = await repository.UpdateUserAsync(user);
        if (success) return Ok();
        return BadRequest("User was not updated");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await repository.GetUserByIdAsync(id);
        if (user == null) return BadRequest("User was not found");

        bool success = await repository.RemoveUserAsync(user);
        if (success) return Ok();
        return BadRequest("User was not deleted");
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetUser(int id)
    {
        var user = await repository.GetUserByIdAsync(id);
        if (user == null) return BadRequest("User was not found");

        AccountDto? account = mapper.Map<AccountDto>(user);
        return Ok(account);
    }

}
