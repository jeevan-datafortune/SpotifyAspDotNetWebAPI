using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.DAL.Data.Models;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        public PlaylistController(IPlaylistService playlistService) => _playlistService = playlistService;

        [HttpGet("GetUserPlayLists/{userid}")]
        public IActionResult GetUserPlayLists(int userid)
        {
            if (userid <= 0)
                throw new PlaylistException("User not found");

            return Ok(this._playlistService.GetAll(userid));
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                throw new PlaylistException("Playlist not found");

            return Ok(this._playlistService.Get(id));
        }

        [HttpPost("Create")]
        public IActionResult Create(PlaylistModel model)
        {
            return Ok(this._playlistService.Create(model));
        }

        [HttpPut("Update")]
        public IActionResult Update(PlaylistModel model)
        {
            return Ok(this._playlistService.Update(model));
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0 || this.Get(id) == null)
                throw new PlaylistException("Playlist not found");

            if (this._playlistService.Delete(id))
                return Ok(new NotificationModel { SuccessMessage = "Playlist deleted successfully" });
            else
                throw new ArtistException($"An error occured while deleting playlist {id}");
        }

    }
}
