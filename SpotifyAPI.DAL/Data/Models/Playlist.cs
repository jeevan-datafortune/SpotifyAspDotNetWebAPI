using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.DAL.Data.Models
{
    public class Playlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsPublic { get; set; }    
        public int? UserID { get; set; }
        public string? Image {  get; set; }
        public User? User { get; set; }   
        public ICollection<PlaylistSong>? Songs { get; set; }
    }
}
