using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface IUserService
    {
        UserModel? Create(UserModel user);
        UserModel? Update(UserModel user);
        UserModel? GetUser(int id);
        IEnumerable<UserModel> GetAllUsers();
    }
}
