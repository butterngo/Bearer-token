namespace OAuthIdentity.Models
{
    using OAuthIdentity.Enum;

    public class Client: IEntity<string>
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public OAuthGrant AllowedGrant { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public int TokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}