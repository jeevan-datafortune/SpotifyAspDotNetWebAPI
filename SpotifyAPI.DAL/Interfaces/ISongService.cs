using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface ISongService
    {
        SongModel? Create(SongModel song);
        SongModel? Update(SongModel song);
        bool Delete(int id);
        SongModel? Get(int? id);
        bool AddToPlayList(PlaylistSongModel song);
        bool RemoveFromPlayList(PlaylistSongModel song);
        bool CheckSongExistsinPlayList(PlaylistSongModel song);
        List<SongModel> GetAll();
        List<SongModel> GetSongsByPlayList(int playListId);
        bool UpdateImage(int? id, string imageName);
    }
}
