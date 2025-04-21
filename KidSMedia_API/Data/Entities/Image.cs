namespace KidSMedia_API.Data.Entities;

public class Image
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}
