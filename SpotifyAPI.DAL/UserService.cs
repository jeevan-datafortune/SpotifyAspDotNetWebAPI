using SpotifyAPI.DAL.Data;
using SpotifyAPI.DAL.Data.Models;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext)=>_dbContext = dbContext;
        public UserModel? Create(UserModel user)
        {
            var usr = new User
            {
                CreatedDate = DateTime.Now,
                Name = user.Name,
                Email = user.Email,
                IsActive = user.IsActive
            };
            _dbContext.Users.Add(usr);
            _dbContext.SaveChanges();

            return new UserModel { 
              Id = usr.Id,
              Name = usr.Name,
              Email = usr.Email,
              IsActive = user.IsActive,
              CreatedDate=usr.CreatedDate
            };
        }

        public UserModel? GetUser(int id)
        {
            User? usr = _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if(usr == null) return null;
            return new UserModel
            {
                Id = usr.Id,
                Name = usr.Name,
                Email = usr.Email,
                IsActive = usr.IsActive,
                CreatedDate = usr.CreatedDate
            };
        }

        public UserModel? Update(UserModel user)
        {
            var usr = new User
            {
                Id= user.Id,
                CreatedDate = DateTime.Now,
                Name = user.Name,
                Email = user.Email,
                IsActive = user.IsActive
            };
            _dbContext.Users.Update(usr);
            return user;
        }
    }
}
