using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Enums
{
    public enum Command
    {
        Start,
        Connect,
        NearPeople,
        SearchPeople,
        Help,
        Profile,
        Coin,
        InviteFriends,
        InChatProfile,
        InChatPrivate,
        InChatEnd
    }

    public enum CommandType
    {
        CompleteProfile,
        RandomSearch,
        CancelSearch,
        NearPeople,
        SearchPeople,
        MyProfile,
        UserProfile
    }

    public enum SearchPeopleCallbackCommand
    {
        SameState,
        SameAge,
        AdvancedSearch,
        NewUsers,
        NoChats,
        MyLastChats
    }

}
