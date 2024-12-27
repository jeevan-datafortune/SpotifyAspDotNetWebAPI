using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private static readonly Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var response = this._authService.ValidateLogin(model);
            if (response.IsSuccess)
            {
                var token = this._authService.GetToken(model.UserName);
                var refreshToken = this._authService.GenerateRefreshToken();
                _refreshTokens[model.UserName] = refreshToken;
                return Ok(new { Token = token, RefreshToken = refreshToken });
            }
            return Unauthorized();
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken(RefreshTokenRequest model)
        {
            if (_refreshTokens.ContainsKey(model.UserName) && _refreshTokens[model.UserName] == model.RefreshToken)
            {
                var token = this._authService.GetToken(model.UserName);
                var refreshToken = this._authService.GenerateRefreshToken();
                _refreshTokens[model.UserName] = refreshToken;
                return Ok(new { Token = token, RefreshToken = refreshToken });
            }
            return Ok(new { Token = "", RefreshToken = "" });
        }
    }
}
