using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Core.Entities;
using Infrastructure.Data;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories;

public class AuthRepository(AppDbContext appDbContext,IConfiguration configuration):IAuthRepository
{
    public async Task<User> RegisterUserAsync(User user)
    {
        if (await appDbContext.Users.AnyAsync(u => u.Email == user.Email || u.Name == user.Name))
        {
            return null;
        }
        // create object if error
        var hashedPassword= new PasswordHasher<User>().HashPassword(user,user.Password);
        user.Password = hashedPassword;
        await appDbContext.Users.AddAsync(user);
        await appDbContext.SaveChangesAsync();
        return user;
    }
    public async Task<string> LoginUserAsync(UserLoginDTO userLoginDTO)
    {
        var user=await appDbContext.Users.SingleOrDefaultAsync(u=>u.Email==userLoginDTO.UserName || u.Name==userLoginDTO.UserName);
        if(user==null)
        {
            return null;
        }

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, userLoginDTO.Password) ==
            PasswordVerificationResult.Failed)
        {
            return null;
        }

        string token = CreateToken(user);
        return token;
    }

    private string CreateToken(User user)
    {
        var claims=new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Role,user.Role.ToString())
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor=new JwtSecurityToken(
            issuer:configuration.GetValue<string>("AppSettings:Issuer"),
            audience:configuration.GetValue<string>("AppSettings:Audience"),
            claims:claims,
            expires:DateTime.Now,
            signingCredentials:creds);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    public async Task<User?> GetUserByUsernameOrEmailAsync(string input)
    {
        return await appDbContext.Users
            .FirstOrDefaultAsync(u => u.Email == input || u.Name == input);
    }

}