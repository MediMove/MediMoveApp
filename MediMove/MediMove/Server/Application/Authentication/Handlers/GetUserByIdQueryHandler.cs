using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Authentication.Queries;
using MediMove.Server.Data;
using MediMove.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Authentication.Handlers
{
    public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, ErrorOr<User>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetUserByIdQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<ErrorOr<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == request.Id,
                cancellationToken: cancellationToken);//.FindAsync(new object?[] { request.Id },cancellationToken: cancellationToken);
            
            if (user is null)
                return Errors.Errors.EntityNotFoundError;

            return user;
        }
    }
}
