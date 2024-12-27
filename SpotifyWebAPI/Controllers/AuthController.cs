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
                response.Token = this._authService.GetToken(model.UserName);
                response.RefreshToken = this._authService.GenerateRefreshToken();
                _refreshTokens[model.UserName] = response.RefreshToken;
                response.ExpiresIn = DateTime.Now.AddHours(1);
                return Ok(response);
            }
            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken(RefreshTokenRequest model)
        {
            if (_refreshTokens.ContainsKey(model.UserName) && _refreshTokens[model.UserName] == model.RefreshToken)
            {
                var token = this._authService.GetToken(model.UserName);
                var refreshToken = this._authService.GenerateRefreshToken();
                _refreshTokens[model.UserName] = refreshToken;
                var response = new AuthResponse
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresIn = DateTime.Now.AddHours(1)
                };
                return Ok(response);
            }
            return Ok(null);
        }
    }
}
