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
                SuccessMessage = "Playlist created",
                id=model.Id
            };
        }

        public NotificationModel Delete(int id)
        {
            var notification = new NotificationModel { };
            var playList = _dbContext.Playlist.Where(x => x.Id == id).FirstOrDefault();
            if (playList != null)
            {
                if (_dbContext.Playlist_Songs.Where(x => x.PlaylistId == id).Count()>0)
                {
                    _dbContext.Playlist_Songs.RemoveRange(_dbContext.Playlist_Songs.Where(x => x.PlaylistId == id));                   
                }
                _dbContext.Playlist.Remove(playList);
                _dbContext.SaveChanges(true);
                notification.SuccessMessage="Playlist removed successfully";
            }
            else
            {
                notification.ErrorMessage = "Playlist not found";
            }
            return notification;
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
                    UserID = x.UserID,
                    Owner = new UserModel()
                    {
                        Id = x.Id,
                        Name = x.Name
                    },
                    Image = x.Image,
                    SongsCount=x.Songs.Count(),
                    Duration=x.Songs.Sum(s=>s.Song.Duration)
                }).FirstOrDefault();
            
            //if (playlist != null)
            //{
                
            //    var songs = from s in _dbContext.Song.AsEnumerable()
            //                join ps in _dbContext.Playlist_Songs.AsEnumerable()
            //                on s.Id equals ps.SongId
            //                where ps.PlaylistId == id
            //                select s;
            //    if (songs != null && songs.Count() > 0)
            //    {
            //        playlist.SongsCount = songs.Count();
            //        playlist.Duration = songs.Sum(x => x.Duration);
            //        playlist.Images = new List<ImageModel>();
            //        if (!string.IsNullOrEmpty(playlist.Image))
            //        {
            //            playlist.Images.Add(new ImageModel { Uri= playlist.Image });
            //        }
            //        playlist.Images.AddRange(songs.OrderBy(x => x.Name).Select(x => new ImageModel { Uri = x.Image }).ToList());
                    
            //    }
            //    else
            //    {
            //        playlist.SongsCount = 0;
            //        playlist.Duration = 0;
            //    }

            //}
            return playlist;
        }

        public List<PlaylistModel> GetAll(int userId)
        {
           var playLists= _dbContext.Playlist.Where(x => x.UserID == userId && x.IsPublic == true)
                 .Select(x => new PlaylistModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     IsPublic = x.IsPublic,
                     UserID = x.UserID,
                     Owner = new UserModel()
                     {
                         Id=x.Id,
                         Name=x.Name
                     },
                     Image = x.Image,
                     SongsCount= x.Songs.Count(),
                     Duration = x.Songs.Sum(s => s.Song.Duration)
                 }).ToList();
          
            return playLists;
        }

        public NotificationModel Update(PlaylistModel playlist)
        {
            var model = new Playlist
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                IsPublic = playlist.IsPublic,
                UserID = playlist.UserID,
                Image = playlist.Image
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
