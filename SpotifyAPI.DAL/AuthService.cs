using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL
{
    public class AuthService : IAuthService
    {
        public UserModel? ValidateLogin(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
