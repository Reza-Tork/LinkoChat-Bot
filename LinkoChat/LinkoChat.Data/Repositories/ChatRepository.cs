using LinkoChat.Application.Interfaces;
using LinkoChat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _dbContext;

        public ChatRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task AddMessage(Message message)
        {
            await _dbContext.Messages.AddAsync(message);
        }

        public async Task AddQueue(Queue queue)
        {
            await _dbContext.Queues.AddAsync(queue);
        }

        public void DeleteQueue(Queue queue)
        {
            _dbContext.Queues.Remove(queue);
        }

        public async Task<Queue> GetQueue(long userId)
        {
            return await _dbContext.Queues.FirstOrDefaultAsync(q => q.UserId == userId);
        }

        public async Task<ICollection<Queue>> GetQueues()
        {
            return await _dbContext.Queues.ToListAsync();
        }

        public async Task<ICollection<Chat>> GetChats(long userId)
        {
            return await _dbContext.Chats.ToListAsync();
        }

        public async Task<Chat> GetLastChat(long userId)
        {
            return await _dbContext.Chats.OrderBy(x => x.StartDate).FirstOrDefaultAsync(c => c.UserId == userId && c.IsEnded == false);
        }

        public async Task AddChat(Chat chat)
        {
            await _dbContext.Chats.AddAsync(chat);
        }
    }
}
