using KidSMedia_API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KidSMedia_API.Data;

public class DataContext : DbContext
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AlbumAccess>()
            .HasKey(k => new { k.UserId, k.AlbumId });
    }
}
