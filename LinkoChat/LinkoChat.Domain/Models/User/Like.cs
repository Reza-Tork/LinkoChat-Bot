using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{
    public class Like
    {
        public int LikerId { get; set; }
        public virtual User LikerUser { get; set; }

        public int LikeeId { get; set; }
        public virtual User LikeeUser { get; set; }
    }
}
