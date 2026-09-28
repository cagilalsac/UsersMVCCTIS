using CORE.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CORE.Services
{
    public abstract class DbService<TEntity> : Service where TEntity : Record, new()
    {
        private readonly DbContext _db;

        protected DbService(DbContext db)
        {
            _db = db;
        }

        protected virtual IQueryable<TEntity> DbQuery()
        {
            return _db.Set<TEntity>().AsNoTracking();
        }

        protected TEntity DbSingle(int id)
            => DbQuery().AsTracking().SingleOrDefault(entity => entity.Id == id);

        protected TEntity DbSingle(Expression<Func<TEntity, bool>> predicate) 
            => DbQuery().AsTracking().SingleOrDefault(predicate);
    }
}
