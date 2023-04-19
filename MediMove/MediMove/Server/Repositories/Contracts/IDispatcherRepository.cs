﻿using MediMove.Server.Entities;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IDispatcherRepository
    {
        Task<IEnumerable<Dispatcher>> GetDispatchers();
        Task<Dispatcher> GetDispatcher(int id);
    }
}
