using LinkoChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Update(User user);
        Task<User> GetUser(long userId);
        Task<User> GetUser(string username);
        Task<bool> IsExist(Expression<Func<User, bool>> predicate);
        Task SaveChanges();
        Task Delete(long id);
    }
}
