namespace OAuthIdentity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    
    public class Role : IdentityRole
    {
        public static readonly string Administrator = "Administrator";
        public static readonly string User = "User";
        
        public static IEnumerable<string> FindAll()
        {
            yield return Role.Administrator;
            yield return Role.User;
        }
    }
}