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

        public NotificationModel Create(PlaylistModel playlist)
        {
            var model = new Playlist
            {
                Name = playlist.Name,
                Description = playlist.Description,
                IsPublic = playlist.IsPublic,
                UserID = playlist.UserID
            };
            _dbContext.Playlist.Add(model);
            _dbContext.SaveChanges();
            return new NotificationModel
            {
                SuccessMessage="Playlist created"
            };
        }

        public NotificationModel Delete(int id)
        {
            var playList = _dbContext.Playlist.Where(x => x.Id == id).FirstOrDefault();
            if (playList != null)
            {
                if (_dbContext.Playlist_Songs.Any(x => x.PlaylistId == id))
                {
                    _dbContext.Playlist_Songs.RemoveRange(_dbContext.Playlist_Songs.Where(x => x.PlaylistId == id));
                    _dbContext.SaveChanges();
                }
                _dbContext.Playlist.Remove(playList);
                _dbContext.SaveChanges();
                return new NotificationModel
                {
                    SuccessMessage = "Playlist removed"
                };
            }
            return new NotificationModel
            {
                ErrorMessage = "Playlist not found"
            };
        }

        public PlaylistModel? Get(int id)
        {
            var playlist = _dbContext.Playlist.Where(x => x.Id == id)
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
                var songs = from s in _dbContext.Song
                            join ps in _dbContext.Playlist_Songs.Where(x => x.PlaylistId == id).ToList()
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
            return _dbContext.Playlist.Where(x => x.UserID == userId && x.IsPublic == true)
                 .Select(x => new PlaylistModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     IsPublic = x.IsPublic,
                     UserID = x.UserID
                 }).ToList();
        }

        public NotificationModel Update(PlaylistModel playlist)
        {
            var model = new Playlist
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                IsPublic = playlist.IsPublic,
                UserID = playlist.UserID
            };
            _dbContext.Playlist.Update(model);
            _dbContext.SaveChanges();
            return new NotificationModel
            {
                SuccessMessage = "Playlist updated"
            };
        }
    }
}
