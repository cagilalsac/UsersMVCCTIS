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

        public CommandResponse Add(RoleRequest request)
        {
            // Way 1:
            //var entity = DbSingle(role => role.Name == request.Name);
            //if (entity is not null)
            //    return Error("Role with the same name exists!");
            // Way 2:
            if (DbQuery().Any(role => role.Name == request.Name.Trim()))
                return Error("Role with the same name exists!");
            var entity = new Role
            {
                Name = request.Name.Trim()
            };
            DbAdd(entity);
            return Success(entity.Id, "Role added successfully.");
        }

        public RoleRequest Edit(int id)
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
