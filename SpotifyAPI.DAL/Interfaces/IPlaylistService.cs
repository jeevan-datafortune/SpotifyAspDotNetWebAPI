using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface IPlaylistService
    {
        PlaylistModel? Create (PlaylistModel playlist);
        PlaylistModel? Update (PlaylistModel playlist);
        bool Delete(int id);
        List<PlaylistModel> GetAll (int userId);
        PlaylistModel? Get(int id);
    }
}
