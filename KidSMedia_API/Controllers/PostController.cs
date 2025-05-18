using KidSMedia_API.Data.Entities;
using KidSMedia_API.DTOs;
using KidSMedia_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KidSMedia_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepo;
    private readonly IWebHostEnvironment _env;

    public PostsController(IPostRepository postRepo, IWebHostEnvironment env)
    {
        _postRepo = postRepo;
        _env = env;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPost(int id)
    {
        var post = await _postRepo.GetPostByIdAsync(id);
        if (post == null) return NotFound();

        return Ok(post);
    }

    [HttpGet("album/{albumId}")]
    public async Task<ActionResult<List<PostDto>>> GetPostsByAlbum(int albumId)
    {
        var posts = await _postRepo.GetPostsByAlbumIdAsync(albumId);
        return Ok(posts);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostDto createPostDto)
    {
        var result = await _postRepo.CreatePostAsync(createPostDto);
        if (!result) return BadRequest("Failed to create record");

        return Ok("Record created successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var result = await _postRepo.RemovePostByIdAsync(id);
        if (!result) return NotFound("Record not found or could not be deleted");

        return NoContent();
    }

    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdatePost(int id, [FromForm] CreatePostDto model)
    {
        var savedImages = new List<Image>();

        if (model.Images.Any())
        {
            var uploadDir = Path.Combine(_env.WebRootPath, "image");
            Directory.CreateDirectory(uploadDir);

            foreach (var file in model.Images)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadDir, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                savedImages.Add(new Image
                {
                    ImageUrl = $"/image/{fileName}"
                });
            }
        }

        var post = new Post
        {
            Id = id,
            Text = model.Text,
            AuthorId = model.AuthorId,
            AlbumId = 1,
            Images = savedImages
        };

        var result = await _postRepo.UpdatePostAsync(post);
        if (!result) return NotFound("Record not found or not updated");

        return Ok("Record updated successfully");
    }
}
