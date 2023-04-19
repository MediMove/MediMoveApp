﻿using MediMove.Server.Data;
using MediMove.Server.Entities;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public BillingRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Billing>> GetBillings()
        {
            return await _dbContext.Billings.ToListAsync();
        }

        public async Task<Billing> GetBilling(int id)
        {
            return await _dbContext.Billings.FindAsync(id);
        }
    }
}
