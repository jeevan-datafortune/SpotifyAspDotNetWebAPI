using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface IAuthService
    {
         UserModel? ValidateLogin(LoginModel loginModel);
    }
}
