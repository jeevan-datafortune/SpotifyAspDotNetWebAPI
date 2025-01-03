using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.DAL.Data.Models
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Uri { get; set; }
        public int? Duration { get; set; }
        public string? Image { get; set; }

        public PlaylistSong? PlaylistSong { get; set; }
      
    }
}
