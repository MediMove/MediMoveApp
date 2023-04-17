using MediMove.Server.Data;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class ParamedicRepository : IParamedicRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public ParamedicRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Paramedic>> GetParamedics()
        {
            return await _dbContext.Paramedics.ToListAsync();
        }

        public async Task<Paramedic> GetParamedic(int id)
        {
            return await _dbContext.Paramedics.FindAsync(id);
        }
    }
}
