using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface IPlaylistService
    {
        NotificationModel Create (PlaylistModel playlist);
        NotificationModel Update (PlaylistModel playlist);
        NotificationModel Delete(int id);
        List<PlaylistModel> GetAll (int userId);
        PlaylistModel? Get(int id);
    }
}
