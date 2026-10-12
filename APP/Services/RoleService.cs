using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class RoleService : DbService<Role>, IService<RoleRequest, RoleResponse>
    {
        public RoleService(DbContext db) : base(db)
        {
        }

        public IQueryable<RoleResponse> Query()
        {
            var roleEntityQuery = DbQuery(); // select * from Roles
            var roleResponseQuery = roleEntityQuery.Select(roleEntity => new RoleResponse
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name
            });
            return roleResponseQuery;
        }

        public RoleRequest Edit(int id)
        {
            throw new NotImplementedException();
        }

        public CommandResponse Add(RoleRequest request)
        {
            throw new NotImplementedException();
        }

        public CommandResponse Update(RoleRequest request)
        {
            throw new NotImplementedException();
        }

        public CommandResponse Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
