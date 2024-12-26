using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface IArtistService
    {
        ArtistModel? Create(ArtistModel artist);
        ArtistModel? GetById(int id);
        ArtistModel? Update(ArtistModel artist);
        bool Delete(int id);
        List<ArtistModel> GetAll();
    }
}
