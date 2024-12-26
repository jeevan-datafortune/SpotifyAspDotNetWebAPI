using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.DAL.Data.Models
{
    public class PlaylistSong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public int? SongId { get; set; }

        [Required]
        public int? PlaylistId { get; set; }

        [Required]
        public DateTime? AddedOn { get; set; }
    }
}
