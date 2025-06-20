using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Commands.UserCommands;

public record EditUserCommand(Guid userId,User user) : IRequest<User>;

public class EditUserCommandHandler(IUserRepository userRepository) : IRequestHandler<EditUserCommand, User>
{
    public async Task<User> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        return await userRepository.UpdateUserAsync(request.userId, request.user);
    }
}