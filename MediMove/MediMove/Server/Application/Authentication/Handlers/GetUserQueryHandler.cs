using AutoMapper;
using ErrorOr;
using MediMove.Server.Application.Authentication.Queries;
using MediMove.Server.Application.Availabilities.Queries.GetAvailabilityQuery;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Authentication.Handlers
{
    public class GetUserQueryHandler
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetUserQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            var user = await _dbContext.Users.FindAsync(new object?[] { request.Id },
                cancellationToken: cancellationToken);

            if (user is null)
                return Errors.Errors.EntityNotFoundError;

            return user;
        }
    }
}
