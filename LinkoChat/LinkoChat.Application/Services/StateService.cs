using LinkoChat.Application.Interfaces;
using LinkoChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            this.stateRepository = stateRepository;
        }
        public async Task<ICollection<State>> GetAllStates()
        {
            return await stateRepository.GetAllStates();
        }

        public async Task<State> GetState(Guid Id)
        {
            return await stateRepository.GetState(Id);
        }

        public async Task<State> GetState(string Name)
        {
            return await stateRepository.GetState(Name);
        }
    }
}
