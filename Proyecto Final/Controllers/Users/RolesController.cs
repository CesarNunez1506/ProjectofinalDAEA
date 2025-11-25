using Application.DTOs.Users;
using Application.UseCases.Roles.Commands;
using Application.UseCases.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
    {
        var roles = await _mediator.Send(new GetAllRolesQuery());
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDto>> GetById(Guid id)
    {
        var role = await _mediator.Send(new GetRoleByIdQuery(id));
        if (role == null)
        {
            return NotFound(new { message = "Rol no encontrado" });
        }
        return Ok(role);
    }

    [HttpGet("{id}/permissions")]
    public async Task<ActionResult<IEnumerable<PermissionDto>>> GetPermissions(Guid id)
    {
        var permissions = await _mediator.Send(new GetRolePermissionsQuery(id));
        return Ok(permissions);
    }

    [HttpPost]
    public async Task<ActionResult<RoleDto>> Create([FromBody] CreateRoleDto dto)
    {
        try
        {
            var role = await _mediator.Send(new CreateRoleCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RoleDto>> Update(Guid id, [FromBody] UpdateRoleDto dto)
    {
        try
        {
            var role = await _mediator.Send(new UpdateRoleCommand(id, dto));
            return Ok(role);
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
            await _mediator.Send(new DeleteRoleCommand(id));
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
