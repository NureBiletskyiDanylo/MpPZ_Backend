using KidSMedia_API.Data;
using KidSMedia_API.Data.Entities;
using KidSMedia_API.DTOs;
using KidSMedia_API.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace KidSMedia_API.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _env;

    public PostRepository(DataContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<PostDto> GetPostByIdAsync(int postId)
    {
        var post = await _context.Posts
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (post == null) return null!;

        return new PostDto
        {
            Id = post.Id,
            Text = post.Text,
            AuthorId = post.AuthorId,
            Images = post.Images?.Select(img => new ImageDto
            {
                Id = img.Id,
                ImageUrl = img.ImageUrl
            }).ToList() ?? []
        };
    }

    public async Task<List<PostDto>> GetPostsByAlbumIdAsync(int albumId)
    {
        var posts = await _context.Posts
            .Where(p => p.AlbumId == albumId)
            .Include(p => p.Images)
            .ToListAsync();

        return posts.Select(post => new PostDto
        {
            Id = post.Id,
            Text = post.Text,
            AuthorId = post.AuthorId,
            Images = post.Images?.Select(img => new ImageDto
            {
                Id = img.Id,
                ImageUrl = img.ImageUrl
            }).ToList() ?? []
        }).ToList();
    }

    public async Task<bool> CreatePostAsync(CreatePostDto model)
    {
        var savedImages = new List<Image>();

        if (model.Images.Any())
        {
            var uploadDir = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(uploadDir);

            foreach (var file in model.Images)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadDir, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                savedImages.Add(new Image
                {
                    ImageUrl = $"/images/{fileName}",
                    PublicId = null
                });
            }
        }

        var post = new Post
        {
            Text = model.Text,
            AuthorId = model.AuthorId,
            AlbumId = 1,
            Images = savedImages
        };

        _context.Posts.Add(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemovePostByIdAsync(int postId)
    {
        var post = await _context.Posts
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (post == null) return false;

        foreach (var image in post.Images)
        {
            var fullPath = Path.Combine(_env.WebRootPath, image.ImageUrl.TrimStart('/'));
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }

        _context.Posts.Remove(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdatePostAsync(Post post)
    {
        var existingPost = await _context.Posts
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == post.Id);

        if (existingPost == null) return false;

        existingPost.Text = post.Text;
        _context.Images.RemoveRange(existingPost.Images);
        existingPost.Images = post.Images;

        _context.Posts.Update(existingPost);
        return await _context.SaveChangesAsync() > 0;
    }

}
