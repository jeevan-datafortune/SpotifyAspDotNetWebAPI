using SpotifyAPI.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAPI.DAL.Models
{
    public class PlaylistSongModel
    {
        public int? PlayListId { get; set; }
        public int? SongId { get; set; }
       
    }
}
