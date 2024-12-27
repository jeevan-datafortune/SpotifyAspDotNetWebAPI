using SpotifyAPI.DAL.Models;
using System.Security.Claims;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface IAuthService
    {
         AuthResponse ValidateLogin(LoginModel loginModel);
        string GetToken(string userName);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
