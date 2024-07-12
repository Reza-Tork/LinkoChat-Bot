using LinkoChat.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkoChat.Domain.Models
{
    public class Profile
    {
        [Key, ForeignKey("User")]
        public long UserId { get; set; }

        public DateTime LastActivity { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public string? CallerUsername { get; set; }

        public bool IsFirstStart { get; set; } = true;

        public string Username { get; set; }

        public string? Picture { get; set; }

        [MaxLength(14)]
        public string? Name { get; set; } = "❓";

        [Range(12, 99)]
        public int? Age { get; set; }

        public Gender Gender { get; set; } = Gender.Unknown;

        public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.None;

        public bool SameAgeSearch { get; set; } = false;

        public bool SameStateSearch { get; set; } = false;

        public bool IsCompletedProfile { get; set; } = false;

        public virtual User User { get; set; }
    }
}
