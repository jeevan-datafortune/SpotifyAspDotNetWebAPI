using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)=>_artistService = artistService;

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(this._artistService.GetAll());
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(this._artistService.GetById(id));
        }

        [HttpPost("Create")]
        public IActionResult Create(ArtistModel model)
        {
            return Ok(this._artistService.Create(model));
        }

        [HttpPut("Update")]
        public IActionResult Update(ArtistModel model)
        {
            return Ok(this._artistService.Update(model));
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(this._artistService.Delete(id));
        }
    }
}
