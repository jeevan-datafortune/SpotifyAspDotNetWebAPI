using Microsoft.EntityFrameworkCore;
using SpotifyAPI.DAL.Data.Models;

namespace SpotifyAPI.DAL.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Playlist> Playlists { get; set; }  
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongArtist>SongArtists { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("OurConnectionString");
        }

    }
}
