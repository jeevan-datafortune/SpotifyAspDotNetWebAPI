using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.DAL;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;

        [HttpPost("GetAllUsers")]       
        public IActionResult GetAllUsers()
        {
            return Ok(this._userService.GetAllUsers());
        }

        [HttpGet("GetById/{id}")]      
        public IActionResult GetUser(int id)
        {
            return Ok(this._userService.GetUser(id));
        }

        [HttpPost("Create")]       
        public IActionResult Create(UserModel model)
        {
            model.CreatedDate = DateTime.Now;
            return Ok(this._userService.Create(model));
        }

        [HttpPost("Update")]       
        public IActionResult Update(UserModel model)
        {
            model.CreatedDate = DateTime.Now;
            return Ok(this._userService.Update(model));
        }
    }
}
