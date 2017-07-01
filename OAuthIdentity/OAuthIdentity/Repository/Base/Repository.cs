namespace OAuthIdentity.Repository
{
    using OAuthIdentity.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web;

    public abstract class Repository<TEntity, TId, TContext> : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TContext: DbContext
    {
        protected readonly TContext _context;

        protected Repository(TContext context)
        {
            _context = context;
        }

        private TId Id => (TId)Convert.ChangeType(Guid.NewGuid().ToString("n"), typeof(TId));

        public async Task<TEntity> AddAsync(TEntity model)
        {
            model.Id = Id;

            _context.Entry(model).State = EntityState.Added;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await FindByAsync(id);

            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> pression)
        {
            var entities = await Find(pression).ToListAsync();

            if (entities.Count() > 0)
            {
                foreach (var entity in entities)
                {
                    await DeleteAsync(entity);
                }
            }
        }

        public async Task DeleteAsync(TEntity model)
        {
            _context.Entry(model).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> EditAsync(TEntity model)
        {
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return model;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> pression = null)
        {
            if (pression == null)
            {
                return _context.Set<TEntity>().AsNoTracking();
            }
            return _context.Set<TEntity>().AsNoTracking().Where(pression);
        }

        public async Task<TEntity> FindByAsync(string id)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}