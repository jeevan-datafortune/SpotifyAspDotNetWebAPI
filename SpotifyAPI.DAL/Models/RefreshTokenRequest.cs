using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAPI.DAL.Models
{
    public class RefreshTokenRequest
    {
        public string? UserName {  get; set; }
        public string? RefreshToken {  get; set; }
    }
}
