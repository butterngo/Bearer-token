namespace OAuthIdentity.Repository
{
    using OAuthIdentity.Models;

    public interface IRefreshTokenRepository : IRepository<RefreshToken, string>
    {
    }
}
