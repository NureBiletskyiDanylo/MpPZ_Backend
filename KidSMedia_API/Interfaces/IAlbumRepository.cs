using KidSMedia_API.DTOs;

namespace KidSMedia_API.Interfaces;

public interface IAlbumRepository
{
    Task<bool> CreateAlbumAsync(CreateAlbumDto model);
    Task<bool> EditAlbumAsync(AlbumDto updatedAlbum);
    Task<bool> DeleteAlbumByIdAsync(int albumId);
    Task<AlbumDto> GetAlbumByIdAsync(int id);
    Task<List<AlbumDto>> GetAlbumsByUserIdAsync(int userId);
}
