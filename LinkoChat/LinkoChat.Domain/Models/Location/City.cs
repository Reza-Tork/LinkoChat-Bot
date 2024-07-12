using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        [ForeignKey(nameof(State))]
        public Guid StateId { get; set; }
        public virtual State State { get; set; }
    }
}
