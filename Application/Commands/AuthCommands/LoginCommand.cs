using Application.DTOs.UserDTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace Application.Commands.AuthCommands;

public record LoginCommand(UserLoginDTO userLoginDto) : IRequest<string>{};

public class LoginCommandHandler(IAuthRepository authRepository,IHttpContextAccessor httpContextAccessor,IEmailService emailService) : IRequestHandler<LoginCommand,string>
{
    // public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    // {
    //     return await authRepository.LoginUserAsync(request.userLoginDto);
    // }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var token = await authRepository.LoginUserAsync(request.userLoginDto);

        if (token == null)
            return null;

        var context = httpContextAccessor.HttpContext;
        var ip = context?.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        var userAgent = context?.Request.Headers["User-Agent"].ToString() ?? "Unknown Device";
        var loginTime = DateTime.UtcNow.ToString("f");

        var user = await authRepository.GetUserByUsernameOrEmailAsync(request.userLoginDto.UserName);

        if (user != null)
        {
            string subject = "New Login Detected";
            string body = $"""
                               <html>
                               <body style="font-family: Arial, sans-serif; color: #333;">
                                   <h2 style="color: #2E86C1;">Hi {user.Name},</h2>

                                   <p>Your account was just logged in:</p>

                                   <ul style="line-height: 1.6;">
                                       <li><strong>📍 IP Address:</strong> {ip}</li>
                                       <li><strong>🕒 Time (UTC):</strong> {loginTime}</li>
                                       <li><strong>🧭 Device:</strong> {userAgent}</li>
                                   </ul>

                                   <p>If this was <strong>you</strong>, no action is needed.</p>
                                   <p>If this was <strong>not you</strong>, we recommend resetting your password immediately.</p>

                                   <br />
                                   <p>Regards,</p>
                                   <p><strong>Security Team</strong></p>
                               </body>
                               </html>
                           """;


            await emailService.SendEmailAsync(user.Email, subject, body);
        }

        return token;
    }

}