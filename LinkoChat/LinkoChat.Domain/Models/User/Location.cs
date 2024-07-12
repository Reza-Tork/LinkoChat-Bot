using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{
    public class Location
    {
        [Key, ForeignKey("User")]
        public long UserId { get; set; }
        public City? City { get; set; }
        public State? State { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public virtual User User { get; set; }
    }
}
