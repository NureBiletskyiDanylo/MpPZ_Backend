namespace KidSMedia_API.DTOs;

public class CreatePostDto
{
    public string? Text { get; set; }
    public int AuthorId { get; set; }
    public List<IFormFile> Images { get; set; } = [];
}
