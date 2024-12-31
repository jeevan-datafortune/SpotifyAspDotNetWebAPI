using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PlaylistController(IPlaylistService playlistService, 
            IWebHostEnvironment webHostEnvironment) { 
            _playlistService = playlistService;
            _webHostEnvironment = webHostEnvironment;
        }

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
            var playList = _playlistService.Get(id);
            if (playList==null)
                throw new PlaylistException("Playlist not found");
            var result = this._playlistService.Delete(id);
            if (!string.IsNullOrEmpty(playList.Image))
            {
                string webRootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
                string filePath = Path.Combine(webRootPath, playList.Image);
                System.IO.File.Delete(filePath);
            }

            return Ok(result);
        }

        [HttpPost("Uplaod/{id}")]
        public async Task<IActionResult> UploadImage(IFormFile file,int id)
        {
            if (file == null || file.Length == 0)
                throw new PlaylistException("No image found");
            PlaylistModel _playlist = _playlistService.Get(id);
            if (_playlist != null)
            {
                string webRootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
                string filePath = Path.Combine(webRootPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
               
                if (!string.IsNullOrEmpty(_playlist.Image))
                {
                    System.IO.File.Delete(Path.Combine(webRootPath, _playlist.Image));
                }
                _playlist.Image = fileName;
                _playlistService.Update(_playlist);
                return Ok(new NotificationModel { SuccessMessage="Image uploaded"});
            }
            throw new PlaylistException("Playlist is not exists");
        }
    }
}
