using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class UserService : DbService<User>, IService<UserRequest, UserResponse>
    {
        public UserService(DbContext db) : base(db)
        {
        }

        public CommandResponse Add(UserRequest request)
        {
            throw new NotImplementedException();
        }

        public UserRequest Edit(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserResponse> Query()
        {
            return DbQuery().Select(u => new UserResponse
            {
                BirthDate = u.BirthDate,
                FirstName = u.FirstName,
                Gender = u.Gender,
                Id = u.Id,
                IsOnline = u.IsOnline,
                UserName = u.UserName,
                LastName = u.LastName,
                Password = u.Password,
                RegistrationDate = u.RegistrationDate,
                RoleIds = u.RoleIds,
                Score = u.Score,
                StatusId = u.StatusId,

                FullNameR = u.FirstName + " " + u.LastName,
                ScoreR = u.Score.ToString("N1"),
                PasswordR = new string('*', u.Password.Length),
                IsOnlineR = u.IsOnline ? "Online" : "Offline",
                GenderR = u.Gender.ToString(),
                RegistrationDateR = u.RegistrationDate.ToString("MM/dd/yyyy HH:mm:ss"),
                BirthDateR = u.BirthDate.HasValue ? u.BirthDate.Value.ToShortDateString()
                    : string.Empty
            });
        }

        public CommandResponse Remove(int id)
        {
            throw new NotImplementedException();
        }

        public CommandResponse Update(UserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
