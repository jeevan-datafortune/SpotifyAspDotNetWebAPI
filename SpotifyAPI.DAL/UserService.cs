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
        public NotificationModel Create(UserModel user)
        {
            var usr = new User
            {
                CreatedDate = DateTime.Now,
                Name = user.Name,
                Email = user.Email,
                IsActive = true
            };
            var notification = new NotificationModel { };
            if (_dbContext.User.Where(x => x.Email.ToLower() == usr.Email.ToLower()).Count() == 0)
            {
                _dbContext.User.Add(usr);
                _dbContext.SaveChanges();
                notification.SuccessMessage = $"User {usr.Name} created successfully";
            }
            else
            {
                notification.ErrorMessage = "Email already exists";
            }
            return notification;
           
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return this._dbContext.User
                .OrderBy(x => x.Name)
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    Name = x.Name,
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

        public NotificationModel Update(UserModel user)
        {
            var usr = _dbContext.User.Where(x => x.Id == user.Id).FirstOrDefault();
            usr.Name = user.Name;
            usr.Email = user.Email;
            var notification = new NotificationModel { };
            if (_dbContext.User.Where(x => x.Email.ToLower() == usr.Email.ToLower() && x.Id!=usr.Id).Count() == 0)
            {
                _dbContext.User.Update(usr);
                _dbContext.SaveChanges();
                notification.SuccessMessage = "User details updated successfully";
            }
            else
            {
                notification.ErrorMessage = "Email already exists";
            }
            return notification;
        }
        public bool IsUserExists(string email, int id)
        {
            var user =this._dbContext.User.Where(x=>x.Email.ToLower()==email.ToLower() && x.Id!=id).FirstOrDefault();
            return user != null;
        }
    }
}
