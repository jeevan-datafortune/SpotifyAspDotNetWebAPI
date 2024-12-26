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
            _dbContext.Artists.Add(model);
            _dbContext.SaveChanges();
            return new ArtistModel
            {
                Id = artist.Id,
                Name = artist.Name
            };
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ArtistModel> GetAll()
        {
            return _dbContext.Artists
                .Select(z => new ArtistModel
                {
                    Id = z.Id,
                    Name = z.Name,
                }).ToList();
        }

        public ArtistModel? GetById(int id)
        {
            return _dbContext.Artists
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
            _dbContext.Artists.Update(model);
            _dbContext.SaveChanges();
            return new ArtistModel
            {
                Id = artist.Id,
                Name = artist.Name
            };
        }
    }
}
