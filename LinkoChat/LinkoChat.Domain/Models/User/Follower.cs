using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{ 
    public class Follower
    {
        public int FollowerId { get; set; }
        public virtual User UserFollower { get; set; }

        public int FolloweeId { get; set; }
        public virtual User UserFollowee { get; set; }
    }
}
