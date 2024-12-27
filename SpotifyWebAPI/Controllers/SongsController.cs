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
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songService;
        public SongsController(ISongService songService) => _songService = songService;

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(this._songService.GetAll());
        }

        [HttpGet("GetSongsByPlayList/{playlistId}")]
        public IActionResult GetSongsByPlayList(int playlistId)
        {
            return Ok(this._songService.GetSongsByPlayList(playlistId));
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                throw new SongException("Song not found");
            return Ok(this._songService.Get(id));
        }

        [HttpPost("Create")]
        public IActionResult Create(SongModel song)
        {
            return Ok(this._songService.Create(song));
        }

        [HttpPut("Update")]
        public IActionResult Update(SongModel song)
        {
            return Ok(this._songService.Update(song));
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0 || this.Get(id) == null)
                throw new SongException("Song not found");
            if (this._songService.Delete(id))
                return Ok(new NotificationModel { SuccessMessage = "Song deleted successfully" });
            else
                throw new ArtistException($"An error occured while deleting song {id}");
        }

        [HttpPost("AddToPlayList")]
        public IActionResult AddToPlayList(PlaylistSongModel song)
        {
            bool isExists = this._songService.CheckSongExistsinPlayList(song);
            if (isExists)
                throw new SongException("Song already exists in playlist");

            return Ok(this._songService.AddToPlayList(song));
        }

        [HttpPost("RemoveFromPlayList")]
        public IActionResult RemoveFromPlayList(PlaylistSongModel song)
        {
            bool isExists = this._songService.CheckSongExistsinPlayList(song);
            if (!isExists)
                throw new SongException("Song is not exists in playlist");
            return Ok(this._songService.RemoveFromPlayList(song));
        }

      

    }
}
