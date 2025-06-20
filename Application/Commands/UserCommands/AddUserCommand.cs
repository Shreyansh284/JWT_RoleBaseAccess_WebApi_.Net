using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Commands.UserCommands;

public record AddUserCommand(User user) : IRequest<User>;

public class AddUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AddUserCommand, User>
{
    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        return await userRepository.AddUserAsync(request.user);
    }
}