namespace OAuthIdentity.Repository
{
    using OAuthIdentity.Models;
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Data.Entity;

    public class OAuthIdentityRefreshRepository: IOAuthIdentityRefreshRepository
    {
        private readonly OAuthIdentityContext _context;

        public OAuthIdentityRefreshRepository():this(new OAuthIdentityContext()) { }

        public OAuthIdentityRefreshRepository(OAuthIdentityContext context)
        {
            _context = context;
        }

        public static OAuthIdentityRefreshRepository Create()
        {
            return new OAuthIdentityRefreshRepository();
        }

        public async Task<RefreshToken> AddAsync(RefreshToken model)
        {
            _context.Entry(model).State = EntityState.Added;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await FindByAsync(id);

            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(Expression<Func<RefreshToken, bool>> pression)
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

        public async Task DeleteAsync(RefreshToken model)
        {
            _context.Entry(model).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> EditAsync(RefreshToken model)
        {
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return model;
        }

        public IQueryable<RefreshToken> Find(Expression<Func<RefreshToken, bool>> pression = null)
        {
            if (pression == null)
            {
                return _context.RefreshTokens.AsNoTracking();
            }
            return _context.RefreshTokens.AsNoTracking().Where(pression);
        }

        public async Task<RefreshToken> FindByAsync(string id)
        {
            return await _context.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}