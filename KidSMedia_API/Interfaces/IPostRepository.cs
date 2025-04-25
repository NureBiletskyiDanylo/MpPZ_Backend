using KidSMedia_API.DTOs;

namespace KidSMedia_API.Interfaces;

public interface IPostRepository
{
    Task<PostDto> GetPostByIdAsync(int postId);
    Task<List<PostDto>> GetPostsByAlbumIdAsync(int albumId);
    Task<bool> CreatePostAsync(CreatePostDto model);
    Task<bool> RemovePostByIdAsync(int postId);
}
