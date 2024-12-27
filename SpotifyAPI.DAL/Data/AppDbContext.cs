using Microsoft.EntityFrameworkCore;
using SpotifyAPI.DAL.Data.Models;

namespace SpotifyAPI.DAL.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Playlist> Playlist { get; set; }  
        public DbSet<Song> Song { get; set; }
        public DbSet<SongArtist> Song_Artists { get; set; }
        public DbSet<PlaylistSong> Playlist_Songs { get; set; }
        
    }
}
