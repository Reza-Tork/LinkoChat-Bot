using LinkoChat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{
    public class Profile
    {
        public int UserId { get; set; }

        public DateTime LastActivity { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public string Username { get; set; }

        public string? Picture { get; set; }

        [MaxLength(14)]
        public string? Name { get; set; } = "❓";

        [Range(12, 99)]
        public int? Age { get; set; }

        public MaritalStatus? MaritalStatus { get; set; } = Enums.MaritalStatus.None;

        public bool SameAgeSearch { get; set; } = false;

        public bool SameStateSearch { get; set; } = false;

        public bool IsCompletedProfile { get; set; } = false;

        public virtual User User { get; set; }
    }
}
