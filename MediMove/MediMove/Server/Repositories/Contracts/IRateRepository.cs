﻿using MediMove.Server.Models;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IRateRepository
    {
        Task<IEnumerable<Rate>> GetRates();
        Task<Rate> GetRate(int id);
    }
}
