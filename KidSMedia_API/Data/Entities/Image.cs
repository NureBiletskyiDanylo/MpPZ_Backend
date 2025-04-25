namespace KidSMedia_API.Data.Entities;

// DO NOT DO REPOSITORY FOR THIS
public class Image
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? PublicId { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}
