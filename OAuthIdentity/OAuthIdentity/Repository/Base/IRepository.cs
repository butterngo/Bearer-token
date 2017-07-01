namespace OAuthIdentity.Repository
{
    using OAuthIdentity.Models;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, TId>: IDisposable
        where TEntity : class, IEntity<TId>
    {
        Task<TEntity> AddAsync(TEntity model);

        Task<TEntity> EditAsync(TEntity model);

        Task DeleteAsync(string id);

        Task DeleteAsync(Expression<Func<TEntity, bool>> pression);

        Task DeleteAsync(TEntity model);

        Task<TEntity> FindByAsync(string id);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> pression = null);
    }
}
