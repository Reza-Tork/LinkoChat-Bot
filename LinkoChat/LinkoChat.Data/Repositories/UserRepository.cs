using LinkoChat.Application.Interfaces;
using LinkoChat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkoChat.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task Delete(long id)
        {
            var user = await GetUser(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
            }
        }

        public async Task<User> GetUser(long userId)
        {
            return await _dbContext.Users
                .Include(x => x.Profile)
                .Include(x => x.Location)
                    .ThenInclude(loc => loc.State)
                .Include(x => x.Location)
                    .ThenInclude(loc => loc.City)
                .Include(x => x.Queue)
                .Include(x => x.Chats)
                .ThenInclude(x => x.Messages)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User> GetUser(string username)
        {
            return await _dbContext.Users
                .Include(x => x.Profile)
                .Include(x => x.Location)
                    .ThenInclude(loc => loc.State)
                .Include(x => x.Location)
                    .ThenInclude(loc => loc.City)
                .Include(x => x.Queue)
                .Include(x => x.Chats)
                .ThenInclude(x => x.Messages)
                .FirstOrDefaultAsync(x => x.Profile.Username == username);
        }

        public async Task<bool> IsExist(Expression<Func<User, bool>> predicate)
        {
            return await _dbContext.Users
                .Include(x => x.Profile)
                .AnyAsync(predicate);
        }

        public async Task Update(User user)
        {
            var dbUser = await IsExist(x => x.UserId == user.UserId);
            if (dbUser)
            {
                _dbContext.Users.Update(user);

                await SaveChanges();
            }
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
