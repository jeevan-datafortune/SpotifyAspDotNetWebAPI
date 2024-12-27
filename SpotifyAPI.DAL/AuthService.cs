using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpotifyAPI.DAL.Data;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpotifyAPI.DAL
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;


        public AuthService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public AuthResponse ValidateLogin(LoginModel loginModel)
        {
            var response = new AuthResponse();
            var user = _dbContext.User.Where(x => x.Email == loginModel.UserName && x.Password == loginModel.Password && x.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                response.IsSuccess = true;
                response.User = new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    IsActive = user.IsActive
                };
            }
            else
            {
                response.IsSuccess = false;

            }
            return response;
        }
        public string GetToken(string userName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "User") // Add roles or other claims as needed
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
    }
}
