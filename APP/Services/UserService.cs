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
            if (DbQuery().Any(u => u.UserName == request.UserName.Trim()))
                return Error("User with the same user name exists!");
            var entity = new User
            {
                BirthDate = request.BirthDate,
                FirstName = request.FirstName?.Trim(),
                LastName = request.LastName?.Trim(),
                Gender = request.Gender,
                Password = request.Password.Trim(),
                RegistrationDate = DateTime.Now,
                UserName = request.UserName.Trim(),
                StatusId = request.StatusId ?? 0,
                Score = request.Score,
                RoleIds = request.RoleIds,
            };
            DbAdd(entity);
            return Success(entity.Id, "User created successfully.");
        }

        public UserRequest Edit(int id)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<User> DbQuery()
        {
            return base.DbQuery().Include(u => u.Status)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .OrderByDescending(u => u.RegistrationDate).ThenByDescending(u => u.Score)
                .ThenBy(u => u.UserName);
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
                    : string.Empty,
                StatusTitleR = u.Status.Title,
                RoleNamesR = string.Join("<br>", u.UserRoles.Select(ur => ur.Role.Name))
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
