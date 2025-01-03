using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.DAL;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SongsController(ISongService songService,
             IWebHostEnvironment webHostEnvironment)
        {
            _songService = songService;
            _webHostEnvironment = webHostEnvironment;
        }

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
           SongModel song = this._songService.Get(id);
            if (song == null)
                throw new SongException("Song not found");

            var result = this._songService.Delete(id);
            if (!string.IsNullOrEmpty(song.Image))
            {
                string webRootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
                string filePath = Path.Combine(webRootPath, song.Image);
                System.IO.File.Delete(filePath);
            }

            return Ok(result);
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

        [HttpPost("Uplaod/{id}")]
        public async Task<IActionResult> UploadImage(IFormFile file, int id)
        {
            if (file == null || file.Length == 0)
                throw new SongException("No image found");
            SongModel _song = _songService.Get(id);
            if (_song != null)
            {
                string webRootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
                string filePath = Path.Combine(webRootPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                if (!string.IsNullOrEmpty(_song.Image))
                {
                    System.IO.File.Delete(Path.Combine(webRootPath, _song.Image));
                }
                _song.Image = fileName;
                _songService.Update(_song);
                return Ok(new NotificationModel { SuccessMessage = "Image uploaded" });
            }
            throw new SongException("Song is not found");
        }

    }
}
