using LinkoChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(long userId);
        Task<User> CheckUser(long userId);
        Task<bool> IsUserExist(string username);
        Task<bool> IsUserExist(long userId);
        Task<User> GetUser(long userId);
        Task<User> GetUser(string username);
        Task UpdateUser(User user);
        Task SaveChanges();
        Task DeleteUser(long userId);
    }
}
