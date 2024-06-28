using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Models
{ 
    public class UserFollower
    {
        public int FollowerId { get; set; }
        public virtual User Follower { get; set; }

        public int FolloweeId { get; set; }
        public virtual User Followee { get; set; }
    }
}
