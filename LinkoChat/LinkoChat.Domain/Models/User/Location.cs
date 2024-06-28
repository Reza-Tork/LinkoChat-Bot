using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{
    public class Location
    {
        public int UserId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual User User { get; set; }
    }
}
