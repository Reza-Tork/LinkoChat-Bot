using LinkoChat.Application.Interfaces;
using LinkoChat.Application.Utils;
using LinkoChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LinkoChat.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Domain.Models.User> CheckUser(long userId)
        {
            var user = await GetUser(userId);
            if (user != null)
                return user;

            return await CreateUser(userId);
        }

        public async Task<Domain.Models.User> CreateUser(long userId)
        {
            if(!await IsUserExist(userId))
            {
                var user = new Domain.Models.User()
                {
                    UserId = userId,
                    Profile = new Profile()
                    {
                        Username = StringUtils.GenerateUsername()
                    },
                    Location = new Domain.Models.Location(),
                    Chats = new List<Domain.Models.Chat>()
                };
                await userRepository.Add(user);
                return user;
            }

            return await GetUser(userId);
        }

        public async Task DeleteUser(long userId)
        {
            await userRepository.Delete(userId);
        }

        public async Task<Domain.Models.User> GetUser(long userId)
        {
            return await userRepository.GetUser(userId);
        }

        public async Task<Domain.Models.User> GetUser(string username)
        {
            return await userRepository.GetUser(username);
        }

        public async Task<bool> IsUserExist(string username)
        {
            return await userRepository.IsExist(x => x.Profile.Username == username);
        }

        public async Task<bool> IsUserExist(long userId)
        {
            return await userRepository.IsExist(x => x.UserId == userId);
        }

        public async Task UpdateUser(Domain.Models.User user)
        {
            await userRepository.Update(user);
        }

        public async Task SaveChanges()
        {
            await userRepository.SaveChanges();
        }
    }
}
