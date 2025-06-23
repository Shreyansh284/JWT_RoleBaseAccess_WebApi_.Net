using Application.DTOs.UserDTOs;
using Core.Entities;

namespace Application.Interfaces;

public interface IAuthRepository
{
    Task<User> RegisterUserAsync(User user);
    Task<string> LoginUserAsync(UserLoginDTO  userLoginDTO);
}