using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Queries.UserQueries;

public record GetUserByIdQuery(Guid userId):IRequest<User>;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetUserByIdAsync(request.userId);
    }
}