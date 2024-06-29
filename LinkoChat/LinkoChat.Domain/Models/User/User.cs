using System.ComponentModel.DataAnnotations;

namespace LinkoChat.Domain.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Follower> Followers { get; set; } = [];
        public virtual ICollection<Follower> Followees { get; set; } = [];
    }
}
