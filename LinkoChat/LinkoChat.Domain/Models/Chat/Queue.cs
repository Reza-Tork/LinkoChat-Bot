using LinkoChat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{
    public class Queue
    {
        [Key, ForeignKey(nameof(User))]
        public long UserId { get; set; }

        public Gender RequestedGender { get; set; }
        public Gender Gender { get; set; }
        public DateTime SearchingFrom { get; set; } = DateTime.Now;
        public bool IsSearching { get; set; } = false;

        public virtual User User { get; set; }
    }
}
