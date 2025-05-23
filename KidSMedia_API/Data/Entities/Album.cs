﻿namespace KidSMedia_API.Data.Entities;

public class Album
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public DateOnly ChildDateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;
}
