using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{
    public class Message
    {
        public Guid Id { get; set; }

        public int MessageId { get; set; }

        public long From { get; set; }

        public long To { get; set; }

        [ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
