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
    public class StateRepository : IStateRepository
    {
        private readonly AppDbContext _dbContext;

        public StateRepository(AppDbContext db)
        {
            _dbContext = db;
        }

        public async Task<ICollection<State>> GetAllStates()
        {
            return await _dbContext.Statements.ToListAsync();
        }

        public async Task<State> GetState(Guid Id)
        {
            return await _dbContext.Statements.Include(s => s.Cities).SingleOrDefaultAsync(s => s.Id == Id);
        }

        public async Task<State> GetState(string Name)
        {
            return await _dbContext.Statements.Include(s => s.Cities).SingleOrDefaultAsync(s => s.Name == Name);
        }
    }
}
