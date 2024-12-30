using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL.Interfaces
{
    public interface IArtistService
    {
        NotificationModel Create(ArtistModel artist);
        ArtistModel? GetById(int id);
        NotificationModel Update(ArtistModel artist);
        NotificationModel Delete(int id);
        List<ArtistModel> GetAll();
    }
}
