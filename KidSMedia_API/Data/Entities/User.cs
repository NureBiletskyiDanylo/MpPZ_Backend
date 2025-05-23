﻿using System.Data;
using KidSMedia_API.Enums;

namespace KidSMedia_API.Data.Entities;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public byte[] PasswordSalt { get; set; } = [];
    public byte[] PasswordHash { get; set; } = [];
    public Roles Role { get; set; } = Roles.User;
}
