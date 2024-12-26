using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.DAL.Data.Models
{
    public class SongArtist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }    

        public int? SongId { get; set; }
        public int? ArtistId { get; set; }
    }
}
