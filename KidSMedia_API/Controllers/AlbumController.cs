using KidSMedia_API.Data.Entities;
using KidSMedia_API.DTOs;
using KidSMedia_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KidSMedia_API.Controllers
{
    public class AlbumController(IAlbumRepository albumRepository) : BaseApiController
    {
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAlbum(int id)
        {
            var album = await albumRepository.GetAlbumByIdAsync(id);
            return Ok(album);
        }

        [HttpPost("")]
        public async Task<ActionResult> CreateAlbum(CreateAlbumDto model)
        {
            if (model == null) return BadRequest("Data was not received");
            bool success = await albumRepository.CreateAlbumAsync(model);
            if (success) return Ok();
            return BadRequest("Album was not created due to internal problems. Try again later");
        }

        [HttpPut("")]
        public async Task<ActionResult> UpdateAlbum(AlbumDto model)
        {
            if (model == null) return BadRequest("Data was not received");
            bool result = await albumRepository.EditAlbumAsync(model);
            if (result) return Ok();
            return BadRequest("Due to some internal error, album was not updated. Try again later");
        }

        [HttpGet("my-albums")]
        public async Task<ActionResult> GetMyAlbums()
        {
            var userId = GetUserId();
            var albums = await albumRepository.GetAlbumsByUserIdAsync(userId);
            return Ok(albums);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAlbum(int id)
        {
            bool success = await albumRepository.DeleteAlbumByIdAsync(id);
            if (success) return Ok();
            return BadRequest("Album was not deleted due to some internal error");
        }

        private int GetUserId()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception("Cannot get id from token"));
            return userId;
        }
    }
}
