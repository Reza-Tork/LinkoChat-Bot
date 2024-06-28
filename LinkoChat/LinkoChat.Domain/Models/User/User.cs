using System.ComponentModel.DataAnnotations;

namespace LinkoChat.Domain.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<UserFollower> Followers { get; set; } = [];
        public virtual ICollection<UserFollower> Followees { get; set; } = [];
    }
}
