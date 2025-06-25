using Application.Interfaces;
using Core.Entities;
using MediatR;

namespace Application.Commands.AuthCommands;

public record RegisterCommand(User user):IRequest<User> {};

public class RegisterCommandHandler(IAuthRepository authRepository,IEmailService emailService) : IRequestHandler<RegisterCommand, User>
{
    public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await authRepository.RegisterUserAsync(request.user);
        if (user != null)
        {
            await emailService.SendEmailAsync(
                user.Email,
                "Welcome to IRCTC!",
                $"Hi {user.Name}, your registration was successful!"
            );
        }

        return user;
    }
}