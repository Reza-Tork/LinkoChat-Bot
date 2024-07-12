using LinkoChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Interfaces
{
    public interface IStateService
    {
        Task<ICollection<State>> GetAllStates();

        Task<State> GetState(Guid Id);

        Task<State> GetState(string Name);
    }
}
