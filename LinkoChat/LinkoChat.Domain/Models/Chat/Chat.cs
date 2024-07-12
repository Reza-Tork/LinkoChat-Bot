using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{
    public class Chat
    {
        public Chat()
        {
            Messages = new HashSet<Message>();
        }
        [Key]
        public Guid ChatId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public long ConnectedTo { get; set; }

        public bool IsEnded { get; set; } = false;

        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
