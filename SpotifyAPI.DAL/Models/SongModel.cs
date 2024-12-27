using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAPI.DAL.Models
{
    public class SongModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Uri { get; set; }
        public int? Duration { get; set; }
        public int? PlayListId { get; set; }
        public string? Image { get; set; }
        public PlaylistModel? Playlist { get; set; }
        public DateTime? AddedOn { get; set; }
        public List<ArtistModel>? Artists { get; set; }

    }
    public class SongException : Exception
    {
        public SongException(string message) : base(message) { }
    }
}
