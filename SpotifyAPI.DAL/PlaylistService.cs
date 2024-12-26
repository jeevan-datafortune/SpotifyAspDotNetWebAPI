using SpotifyAPI.DAL.Data;
using SpotifyAPI.DAL.Data.Models;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL
{
    public class PlaylistService : IPlaylistService
    {
        private readonly AppDbContext _dbContext;
        public PlaylistService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PlaylistModel? Create(PlaylistModel playlist)
        {
            var model = new Playlist
            {
                Name = playlist.Name,
                Description = playlist.Description,
                IsPublic = playlist.IsPublic,
                UserID = playlist.UserID
            };
            _dbContext.Playlists.Add(model);
            _dbContext.SaveChanges();
            return new PlaylistModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsPublic = model.IsPublic,
                UserID = model.UserID
            };
        }

        public bool Delete(int id)
        {
            var playList = _dbContext.Playlists.Where(x => x.Id == id).FirstOrDefault();
            if (playList != null)
            {
                _dbContext.Playlists.Remove(playList);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public PlaylistModel? Get(int id)
        {
            var playlist = _dbContext.Playlists.Where(x => x.Id == id)
                .Select(x => new PlaylistModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IsPublic = x.IsPublic,
                    UserID = x.UserID
                }).FirstOrDefault();

            if (playlist != null)
            {
                var songs = from s in _dbContext.Songs
                            join ps in _dbContext.PlaylistSongs.Where(x => x.PlaylistId == id).ToList()
                            on s.Id equals ps.SongId
                            select s;
                if (songs != null && songs.Any())
                {
                    playlist.SongsCount = songs.Count();
                    playlist.Duration = songs.Sum(x => x.Duration);
                }
                else
                {
                    playlist.SongsCount = 0;
                    playlist.Duration = 0;
                }

            }
            return playlist;
        }

        public List<PlaylistModel> GetAll(int userId)
        {
            return _dbContext.Playlists.Where(x => x.UserID == userId && x.IsPublic == true)
                 .Select(x => new PlaylistModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     IsPublic = x.IsPublic,
                     UserID = x.UserID
                 }).ToList();
        }

        public PlaylistModel? Update(PlaylistModel playlist)
        {
            var model = new Playlist
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                IsPublic = playlist.IsPublic,
                UserID = playlist.UserID
            };
            _dbContext.Playlists.Add(model);
            _dbContext.SaveChanges();
            return playlist;
        }
    }
}
