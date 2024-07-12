using LinkoChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Interfaces
{
    public interface IChatRepository
    {
        Task AddQueue(Queue queue);
        void DeleteQueue(Queue queue);
        Task<ICollection<Queue>> GetQueues();
        Task<Queue> GetQueue(long userId);

        Task AddChat(Chat chat);
        Task<ICollection<Chat>> GetChats(long userId);
        Task<Chat> GetLastChat(long userId);

        Task AddMessage(Message message);
    }
}
