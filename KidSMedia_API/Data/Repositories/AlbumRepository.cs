using AutoMapper;
using KidSMedia_API.Data.Entities;
using KidSMedia_API.DTOs;
using KidSMedia_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KidSMedia_API.Data.Repositories;

public class AlbumRepository(DataContext context, IMapper mapper) : IAlbumRepository
{
    public async Task<bool> CreateAlbumAsync(CreateAlbumDto model)
    {
        var album = mapper.Map<Album>(model);
        if (album == null) throw new ArgumentException("Album has corrupted data. Cannot be created");

        await context.Albums.AddAsync(album);
        return await context.SaveChangesAsync() > 0; 

    }

    public async Task<bool> DeleteAlbumByIdAsync(int albumId)
    {
        var album = await context.Albums.FindAsync(albumId);
        if (album == null) return false;

        context.Albums.Remove(album);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EditAlbumAsync(AlbumDto updatedAlbum)
    {
        var album = await context.Albums.FindAsync(updatedAlbum.Id);
        if (album == null) throw new ArgumentException("Updated album does not exist");

        album.Title = updatedAlbum.Title;
        album.ChildDateOfBirth = updatedAlbum.ChildDateOfBirth;
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<AlbumDto> GetAlbumByIdAsync(int id)
    {
        var album = await context.Albums.FindAsync(id);
        if (album == null) throw new ArgumentNullException("Album was not found");
        return mapper.Map<AlbumDto>(album);
    }

    public async Task<List<AlbumDto>> GetAlbumsByUserIdAsync(int userId)
    {
        List<Album> albums = await context.Albums.Where(a => a.OwnerId == userId).ToListAsync();
        return albums == null ? new List<AlbumDto>() : mapper.Map<List<AlbumDto>>(albums);
    }
}
