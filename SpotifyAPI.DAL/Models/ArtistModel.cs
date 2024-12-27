using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAPI.DAL.Models
{
    public class ArtistModel: ErrorModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        
    }

    public class ArtistException : Exception
    {
        public ArtistException(string message) : base(message) { }
    }
}
