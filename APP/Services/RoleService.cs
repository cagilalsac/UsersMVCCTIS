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

        protected override IQueryable<Role> DbQuery()
        {
            return base.DbQuery().Include(r => r.UserRoles);
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
            var entity = DbSingle(id);
            if (entity is null)
                return null;
            return new RoleRequest
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public CommandResponse Update(RoleRequest request)
        {
            if (DbQuery().Any(role => role.Id != request.Id && role.Name == request.Name.Trim()))
                return Error("Role with the same name exists!");
            var entity = DbSingle(request.Id);
            if (entity is null)
                return Error("Role not found!");
            entity.Name = request.Name.Trim();
            DbUpdate(entity);
            return Success(entity.Id, "Role updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var entity = DbSingle(id);
            if (entity is null)
                return Error("Role not found!");
            DbRemove(entity.UserRoles);
            DbRemove(entity);
            return Success(entity.Id, "Role deleted successfully.");
        }
    }
}
