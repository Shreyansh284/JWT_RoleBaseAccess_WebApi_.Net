using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Queries.UserQueries;

public record GetAllUsersQuery() : IRequest<IEnumerable<User>>;

public class GetAllUserQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetAllUsersAsync();
    }
}