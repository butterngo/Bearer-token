namespace OAuthIdentity.Models
{
    using System.ComponentModel.DataAnnotations;

    public interface IEntity<T>
    {
        [Key]
        T Id { get; set; }
    }
}
