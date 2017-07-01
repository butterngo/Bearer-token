namespace OAuthIdentity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ClientUser : IEntity<string>
    {
        public string Id { get; set; }
       
        public string ClientId { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
      
        public string UserId { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}