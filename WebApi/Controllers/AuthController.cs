using Application.Commands.AuthCommands;
using Application.DTOs.UserDTOs;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : Controller
{
   [HttpPost("Login")]

   public async Task<IActionResult> LoginAsync(UserLoginDTO userLoginDTO)
   {
      var token = await sender.Send(new LoginCommand(userLoginDTO));
      if (token == null)
      {
         return Unauthorized("Invalid username or password");
      }
      return Ok(token);
   }

   [HttpPost("Register")]
   public async Task<IActionResult> RegisterAsync([FromBody] User user)
   {
      var result = await sender.Send(new RegisterCommand(user));
      if (result == null)
         return BadRequest("User already exists");
      return Ok(result);
   }
}