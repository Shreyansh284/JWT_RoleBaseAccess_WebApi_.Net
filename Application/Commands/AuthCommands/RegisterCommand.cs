using Application.Interfaces;
using Core.Entities;
using MediatR;

namespace Application.Commands.AuthCommands;

public record RegisterCommand(User user):IRequest<User> {};

public class RegisterCommandHandler(IAuthRepository authRepository) : IRequestHandler<RegisterCommand, User>
{
    public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await authRepository.RegisterUserAsync(request.user);
    }
}