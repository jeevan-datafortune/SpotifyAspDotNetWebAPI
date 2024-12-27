using SpotifyAPI.DAL.Data;
using SpotifyAPI.DAL.Data.Models;
using SpotifyAPI.DAL.Interfaces;
using SpotifyAPI.DAL.Models;

namespace SpotifyAPI.DAL
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext) => _dbContext = dbContext;
        public UserModel? Create(UserModel user)
        {
            var usr = new User
            {
                CreatedDate = DateTime.Now,
                Name = user.Name,
                Email = user.Email,
                IsActive = user.IsActive
            };
            _dbContext.User.Add(usr);
            _dbContext.SaveChanges();

            return new UserModel
            {
                Id = usr.Id,
                Name = usr.Name,
                Email = usr.Email,
                IsActive = user.IsActive,
                CreatedDate = usr.CreatedDate
            };
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return this._dbContext.User
                .OrderBy(x => x.Name)
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                }).ToList();
        }

        public UserModel? GetUser(int id)
        {
            User? usr = _dbContext.User.Where(u => u.Id == id).FirstOrDefault();
            if (usr == null) return null;
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
                Id = user.Id,
                CreatedDate = DateTime.Now,
                Name = user.Name,
                Email = user.Email,
                IsActive = user.IsActive
            };
            _dbContext.User.Update(usr);
            return user;
        }
        public bool IsUserExists(string email, int id)
        {
            var user =this._dbContext.User.Where(x=>x.Email.ToLower()==email.ToLower() && x.Id!=id).FirstOrDefault();
            return user != null;
        }
    }
}
