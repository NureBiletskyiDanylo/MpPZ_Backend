using KidSMedia_API.DTOs;
using KidSMedia_API.Interfaces;

namespace KidSMedia_API.Data.Repositories;

public class AlbumRepository : IAlbumRepository
{
    public Task<bool> CreateAlbumAsync(CreateAlbumDto model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAlbumByIdAsync(int albumId)
    {
        throw new NotImplementedException();
    }

    public Task<AlbumDto> EditAlbumAsync(AlbumDto updatedAlbum)
    {
        throw new NotImplementedException();
    }

    public Task<AlbumDto> GetAlbumByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<AlbumDto>> GetAlbumsByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}
