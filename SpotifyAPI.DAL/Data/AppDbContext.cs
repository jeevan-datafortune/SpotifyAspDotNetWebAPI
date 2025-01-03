using Microsoft.EntityFrameworkCore;
using SpotifyAPI.DAL.Data.Models;
using System;

namespace SpotifyAPI.DAL.Data
{
    public class AppDbContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>().HasOne(p => p.User).WithMany(p => p.Playlists).HasForeignKey(p => p.UserID);
            modelBuilder.Entity<PlaylistSong>().HasOne(p => p.Playlist).WithMany(p => p.Songs).HasForeignKey(p => p.PlaylistId);
            modelBuilder.Entity<PlaylistSong>().HasOne(p => p.Song).WithOne(p => p.PlaylistSong).HasForeignKey<PlaylistSong>(p => p.SongId);
        }
    }
}
