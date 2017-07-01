namespace OAuthIdentity.Repository
{
    using OAuthIdentity.Models;

    public class ClientRepository : Repository<Client, string, OAuthIdentityContext>, IClientRepository
    {
        public ClientRepository() : this(new OAuthIdentityContext()) { }

        public ClientRepository(OAuthIdentityContext context) : base(context)
        {
        }

        public static ClientRepository Create()
        {
            return new ClientRepository();
        }
    }
}