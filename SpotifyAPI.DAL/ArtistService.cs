using SpotifyAPI.DAL.Data;
using SpotifyAPI.DAL.Data.Models;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL
{
    public class ArtistService : IArtistService
    {
        private readonly AppDbContext _dbContext;
        public ArtistService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ArtistModel? Create(ArtistModel artist)
        {
            var model = new Artist
            {
                Id = artist.Id,
                Name = artist.Name,
            };
            _dbContext.Artist.Add(model);
            _dbContext.SaveChanges();
            return new ArtistModel
            {
                Id = artist.Id,
                Name = artist.Name
            };
        }

        public bool Delete(int id)
        {
            _dbContext.Song_Artists.RemoveRange(_dbContext.Song_Artists.Where(x => x.ArtistId == id));
            var artist = _dbContext.Artist.Where(x => x.Id == id).FirstOrDefault();
            if (artist != null)
            {
                _dbContext.Artist.Remove(artist);
            }
            _dbContext.SaveChanges(true);
            return true;
        }

        public List<ArtistModel> GetAll()
        {
            return _dbContext.Artist
                .OrderBy(x=>x.Name)
                .Select(z => new ArtistModel
                {
                    Id = z.Id,
                    Name = z.Name,
                }).ToList();
        }

        public ArtistModel? GetById(int id)
        {
            return _dbContext.Artist
                .Where(x => x.Id == id)
                .Select(z => new ArtistModel
                {
                    Id = z.Id,
                    Name = z.Name,
                }).FirstOrDefault();
        }

        public ArtistModel? Update(ArtistModel artist)
        {
            var model = new Artist
            {
                Id = artist.Id,
                Name = artist.Name,
            };
            _dbContext.Artist.Update(model);
            _dbContext.SaveChanges();
            return new ArtistModel
            {
                Id = artist.Id,
                Name = artist.Name
            };
        }
    }
}
