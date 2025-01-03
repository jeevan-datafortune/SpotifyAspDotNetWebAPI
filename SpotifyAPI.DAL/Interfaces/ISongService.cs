using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface ISongService
    {
        NotificationModel Create(SongModel song);
        NotificationModel Update(SongModel song);
        NotificationModel Delete(int id);
        SongModel? Get(int? id);
        bool AddToPlayList(PlaylistSongModel song);
        bool RemoveFromPlayList(PlaylistSongModel song);
        bool CheckSongExistsinPlayList(PlaylistSongModel song);
        List<SongModel> GetAll();
        List<SongModel> GetSongsByPlayList(int playListId);
        bool UpdateImage(int? id, string imageName);
    }
}
