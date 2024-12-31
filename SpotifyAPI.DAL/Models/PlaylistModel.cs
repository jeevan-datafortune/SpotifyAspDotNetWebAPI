using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAPI.DAL.Models
{
    public class PlaylistModel : ErrorModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsPublic { get; set; }
        public int? UserID { get; set; }
        public UserModel?  Owner { get; set; }
        public List<ImageModel>? Images { get; set; } 
        public string? Image { get; set; }
        public int? SongsCount { get; set; }
        public int? Duration { get; set; }

    }
    public class PlaylistException : Exception
    {
        public PlaylistException(string message) : base(message) { }
    }
}
