using APP.Authentication.Models;
using APP.Domain;
using CORE.Authentication.Services;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Authentication.Services
{
    public class AuthService : DbService<User>, IAuthService
    {
        private readonly ICookieAuthService _cookieAuthService;

        public AuthService(DbContext db, ICookieAuthService cookieAuthService) : base(db)
        {
            _cookieAuthService = cookieAuthService;
        }

        public async Task<CommandResponse> Login(LoginRequest request)
        {
            var userEntity = DbSingle(u => u.UserName == request.UserName
                && u.Password == request.Password && u.StatusId == 1);
            if (userEntity is null)
                return Error("User not found!");
            await _cookieAuthService.SignIn(userEntity.Id, userEntity.UserName,
                userEntity.UserRoles.Select(ur => ur.Role.Name));
            return Success(userEntity.Id, "User logged in successfully.");
        }

        public async Task Logout()
        {
            await _cookieAuthService.SignOut();
        }
    }
}
