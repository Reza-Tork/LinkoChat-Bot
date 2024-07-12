using LinkoChat.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkoChat.Domain.Models
{
    public class User
    {
        public User()
        {
            Profile = new Profile();
            Location = new Location();
            Queue = new Queue();
            Chats = new HashSet<Chat>();
        }
        [Key]
        public long UserId { get; set; }
        public int Coin { get; set; } = 0;
        public StepStatus StepStatus { get; set; } = StepStatus.Start;
        public bool IsRegistered { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        public virtual Profile Profile { get; set; }
        public virtual Location Location { get; set; }
        public virtual Queue? Queue { get; set; }
        public virtual ICollection<Chat>? Chats { get; set; }
    }
}
