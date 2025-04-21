namespace KidSMedia_API.Data.Entities;

public class Album
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;
}
