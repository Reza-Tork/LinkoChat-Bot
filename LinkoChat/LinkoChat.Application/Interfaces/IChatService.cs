using LinkoChat.Domain.Enums;
using LinkoChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Interfaces
{
    public interface IChatService
    {
        Task StartSearch(User user, Gender gender);
        Task<Queue> GetCurrentSearch(long userId);
        Task StopSearch(long userId);

        Task StartChat(User user, User targetUser);
        Task<Chat> GetCurrentChat(long userId);
        Task<ICollection<Chat>> GetAllChats(long userId);
        Task EndChat(User user, Chat chat);

        Task SendMessage(long From, long To, int messageId);
    }
}
