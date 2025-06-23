using Application.DTOs.UserDTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Commands.AuthCommands;

public record LoginCommand(UserLoginDTO userLoginDto) : IRequest<string>{};

public class LoginCommandHandler(IAuthRepository authRepository) : IRequestHandler<LoginCommand,string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authRepository.LoginUserAsync(request.userLoginDto);
    }
}