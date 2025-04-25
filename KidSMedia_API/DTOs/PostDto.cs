namespace KidSMedia_API.DTOs;

public class PostDto
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public int AuthorId { get; set; }
    public List<ImageDto> Images { get; set; } = [];
}
