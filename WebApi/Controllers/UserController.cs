using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
public class UserController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddUser ([FromBody]User user)
    {
        var addUser = await sender.Send(new AddUserCommand(user));

        return Ok(addUser);
    }
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await sender.Send(new GetAllUsersQuery());

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await sender.Send(new GetUserByIdQuery(id));
        if (user is null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
    {
        var userToUpdate=await sender.Send(new EditUserCommand(id, user));

        if (userToUpdate is null)
        {
            return NotFound("User not found");
        }
        return Ok(userToUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var userToDelete = await sender.Send(new DeleteUserCommand(id));
        if (userToDelete)
        {
            return Ok(userToDelete);
        }

        return NotFound("User not found");

    }
}