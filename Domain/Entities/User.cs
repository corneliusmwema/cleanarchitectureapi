using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        [Key]

        public Guid Id { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string UserName { get; set; }


        public string? ProfilePhoto { get; set; }

        [JsonIgnore]
        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}