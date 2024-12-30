using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface IUserService
    {
        NotificationModel Create(UserModel user);
        NotificationModel Update(UserModel user);
        UserModel? GetUser(int id);
        IEnumerable<UserModel> GetAllUsers();
        bool IsUserExists(string email, int id);
    }
}
