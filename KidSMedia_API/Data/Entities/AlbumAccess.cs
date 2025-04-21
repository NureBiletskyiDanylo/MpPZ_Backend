using KidSMedia_API.Enums;

namespace KidSMedia_API.Data.Entities;

public class AlbumAccess
{
    public int UserId { get; set; }
    public int AlbumId { get; set; }
    public AlbumAccessModifiers AccessModifier { get; set; }
}
