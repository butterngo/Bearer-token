namespace OAuthIdentity.Repository
{
    using Microsoft.AspNet.Identity;
    using OAuthIdentity.Models;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IOAuthIdentityRefreshRepository : IDisposable
    {
        Task<RefreshToken> AddAsync(RefreshToken model);

        Task<RefreshToken> EditAsync(RefreshToken model);

        Task DeleteAsync(string id);

        Task DeleteAsync(Expression<Func<RefreshToken, bool>> pression);

        Task DeleteAsync(RefreshToken model);

        Task<RefreshToken> FindByAsync(string id);

        IQueryable<RefreshToken> Find(Expression<Func<RefreshToken, bool>> pression = null);
    }
}
