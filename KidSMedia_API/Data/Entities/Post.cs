namespace KidSMedia_API.Data.Entities;

public class Post
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;
    public int AlbumId { get; set; }
    public Album Album { get; set; } = null!;
    public List<Image> Images { get; set; } = [];
}
