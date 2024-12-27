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

        [HttpPost("GetAll")]       
        public IActionResult GetAll()
        {
            return Ok(this._userService.GetAllUsers());
        }

        [HttpGet("Get/{id}")]      
        public IActionResult Get(int id)
        {
            if (id <= 0) { throw new UserException("User not found"); }
            return Ok(this._userService.GetUser(id));
        }

        [HttpPost("Create")]       
        public IActionResult Create(UserModel model)
        {
            if (this._userService.IsUserExists(model.Email, model.Id))
                throw new UserException("Email already exists");
            model.CreatedDate = DateTime.Now;            
            return Ok(this._userService.Create(model));
        }

        [HttpPost("Update")]       
        public IActionResult Update(UserModel model)
        {
            if (this._userService.IsUserExists(model.Email, model.Id))
                throw new UserException("Email already exists");

            model.CreatedDate = DateTime.Now;
            return Ok(this._userService.Update(model));
        }
    }
   
}
