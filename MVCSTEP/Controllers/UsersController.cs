using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Commands;
using MVCSTEP.Models;

namespace MVCSTEP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
 
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = await _mediator.Send(new GetUsersQuery());
        return Ok(users);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var command = new GetUserByIdQuery(id);
 
        // Отправка команды через Mediator для получения пользователя по Id
        var user = await _mediator.Send(command);
 
        if (user == null)
        {
            return NotFound("User not found.");
        }
 
        return Ok(user);
    }
 
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid user data.");
        }
 
        // Отправка команды через Mediator для добавления пользователя
        var user = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }
 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var command = new DeleteUserCommand(id);
 
        // Отправка команды через Mediator для удаления пользователя
        var success = await _mediator.Send(command);
 
        if (!success)
        {
            return NotFound("User not found.");
        }
 
        return NoContent();
    }
 
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("User ID mismatch.");
        }
 
        // Отправка команды через Mediator для обновления пользователя
        var updatedUser = await _mediator.Send(command);
 
        if (updatedUser == null)
        {
            return NotFound("User not found.");
        }
 
        return Ok(updatedUser);
    }
}