using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface ISongService
    {
        SongModel? Create(SongModel song);
        SongModel? Update(SongModel song);
        bool Delete(int id);
        SongModel? Get(int? id);
        bool AddToPlayList(SongModel song);
        bool RemoveFromPlayList(int id);
        List<SongModel> GetAll(int playListId);
        bool UpdateImage(int? id, string imageName);
    }
}
