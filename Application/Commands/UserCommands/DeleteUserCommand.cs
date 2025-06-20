using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Commands.UserCommands;

public record DeleteUserCommand(Guid userId) : IRequest<bool>;

public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand,bool>
{
   public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
   {
      return await userRepository.DeleteUserAsync(request.userId);
   }
}