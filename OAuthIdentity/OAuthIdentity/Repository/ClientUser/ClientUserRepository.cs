namespace OAuthIdentity.Repository
{
    using OAuthIdentity.Models;

    public class ClientUserRepository : Repository<ClientUser, string, OAuthIdentityContext>, IClientUserRepository
    {
        public ClientUserRepository(): this(new OAuthIdentityContext()) { }

        public ClientUserRepository(OAuthIdentityContext context) : base(context)
        {
        }

        public static ClientUserRepository Create()
        {
            return new ClientUserRepository();
        }
    }
}