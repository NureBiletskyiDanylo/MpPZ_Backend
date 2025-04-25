namespace KidSMedia_API.DTOs;

public class CreateAlbumDto
{
    public required string Title { get; set; }
    public DateOnly ChildDateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
    public int OwnerId { get; set; }
}
