using Microsoft.EntityFrameworkCore;
using SpotifyAPI.DAL.Data;
using SpotifyAPI.DAL.Data.Models;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;
using System.Runtime.CompilerServices;

namespace SpotifyAPI.DAL
{
    public class SongService : ISongService
    {
        private readonly AppDbContext _dbContext;
        public SongService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddToPlayList(SongModel song)
        {
            var songToPlayList = new PlaylistSong()
            {
                PlaylistId = song.PlayListId,
                SongId = song.Id
            };

            _dbContext.PlaylistSongs.Add(songToPlayList);
            _dbContext.SaveChanges();
            return true;
        }

        public SongModel? Create(SongModel song)
        {
            var model = new Song
            {
                Duration = song.Duration,
                Name = song.Name,
                Uri = song.Uri
            };

            _dbContext.Songs.Add(model);
            _dbContext.SaveChanges();
            if (song.Artists != null)
            {
                foreach (var artist in song.Artists)
                {
                    _dbContext.SongArtists.Add(new SongArtist { ArtistId = artist.Id, SongId = model.Id });
                }
                _dbContext.SaveChanges(true);
            }           
            return this.Get(model.Id);
        }

        public bool Delete(int id)
        {
            var song = _dbContext.Songs.Where(x => x.Id == id).FirstOrDefault();
            if (song != null)
            {
                _dbContext.SongArtists.RemoveRange(_dbContext.SongArtists.Where(x => x.SongId == id));
                _dbContext.PlaylistSongs.RemoveRange(_dbContext.PlaylistSongs.Where(x => x.SongId == id));
                _dbContext.SaveChanges(true);
                return true;
            }
            return false;
        }

        public SongModel? Get(int? id)
        {
            var song = _dbContext.Songs.Where(x => x.Id == id).FirstOrDefault();
            if (song != null)
            {

                var songModel = new SongModel
                {
                    Id = song.Id,
                    Name = song.Name,
                    Uri = song.Uri,
                    Duration = song.Duration,
                    Image = song.Image
                };
                var artists = from sa in _dbContext.SongArtists.Where(x => x.SongId == id)
                              join a in _dbContext.Artists on sa.ArtistId equals a.Id
                              orderby a.Name ascending
                              select new ArtistModel
                              {
                                  Id = a.Id,
                                  Name = a.Name
                              };

                if (artists != null && artists.Any())
                    songModel.Artists = artists.ToList();

                var playList = from s in _dbContext.Songs
                               join ps in _dbContext.PlaylistSongs.Where(x => x.SongId == id) on s.Id equals ps.SongId
                               join p in _dbContext.Playlists on ps.PlaylistId equals p.Id
                               select new PlaylistModel
                               {
                                   Id = p.Id,
                                   Name = p.Name,
                                   Description = p.Description,
                                   IsPublic = p.IsPublic
                               };
                if (playList != null && playList.Any())
                    songModel.Playlist = playList.FirstOrDefault();

                return songModel;
            }
            return null;
        }

        public List<SongModel> GetAll(int playListId)
        {
            var songs = (from ps in _dbContext.PlaylistSongs.Where(x => x.PlaylistId == playListId)
                         join s in _dbContext.Songs on ps.SongId equals s.Id
                         select new SongModel
                         {
                             Id = s.Id,
                             Name = s.Name,
                             Duration = s.Duration,
                             Image = s.Image,
                             Uri = s.Uri,
                             AddedOn = ps.AddedOn
                         }).ToList();
            for (int i = 0; i < songs.Count; i++)
            {
                songs[i].Artists = (from sa in _dbContext.SongArtists.Where(x => x.SongId == songs[i].Id)
                                    join a in _dbContext.Artists on sa.ArtistId equals a.Id
                                    orderby a.Name ascending
                                    select new ArtistModel
                                    {
                                        Id = a.Id,
                                        Name = a.Name
                                    }).ToList();
                songs[i].Playlist = (from s in _dbContext.Songs
                                     join ps in _dbContext.PlaylistSongs.Where(x => x.SongId == songs[i].Id) on s.Id equals ps.SongId
                                     join p in _dbContext.Playlists on ps.PlaylistId equals p.Id
                                     select new PlaylistModel
                                     {
                                         Id = p.Id,
                                         Name = p.Name,
                                         Description = p.Description,
                                         IsPublic = p.IsPublic
                                     }).FirstOrDefault();
            }
            return songs;
        }

        public bool RemoveFromPlayList(int id)
        {
            var playListSong = _dbContext.PlaylistSongs.FirstOrDefault(x => x.Id == id);
            if (playListSong != null)
            {
                _dbContext.PlaylistSongs.Remove(playListSong);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateImage(int? id,string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                var song = _dbContext.Songs.Where(x => x.Id == id).FirstOrDefault();
                if (song != null)
                {
                    song.Image = imageName;
                    _dbContext.Songs.Update(song);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public SongModel? Update(SongModel song)
        {
            var model = new Song
            {
                Id= song.Id,
                Duration = song.Duration,
                Name = song.Name,
                Uri = song.Uri
            };

            _dbContext.Songs.Update(model);
            _dbContext.SaveChanges();
            _dbContext.SongArtists.RemoveRange(_dbContext.SongArtists.Where(x=>x.SongId==song.Id));
            _dbContext.SaveChanges();
            if (song.Artists != null)
            {
                foreach (var artist in song.Artists)
                {
                    _dbContext.SongArtists.Add(new SongArtist { ArtistId = artist.Id, SongId = model.Id });
                }
                _dbContext.SaveChanges(true);
            }
            return this.Get(model.Id);
        }
    }
}
