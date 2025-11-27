using Application.DTOs.Users;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user == null)
        {
            return NotFound(new { message = "Usuario no encontrado" });
        }
        return Ok(user);
    }

    [HttpGet("role/{roleId}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetByRole(Guid roleId)
    {
        var users = await _mediator.Send(new GetUsersByRoleQuery(roleId));
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
    {
        try
        {
            var user = await _mediator.Send(new CreateUserCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto dto)
    {
        try
        {
            var user = await _mediator.Send(new UpdateUserCommand(id, dto));
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/change-password")]
    public async Task<ActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordDto dto)
    {
        try
        {
            await _mediator.Send(new ChangePasswordCommand(id, dto));
            return Ok(new { message = "Contraseña actualizada correctamente" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
