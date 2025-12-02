using Application.DTOs.Museum;
using Application.UseCases.Museum.TypePersons.Commands;
using Application.UseCases.Museum.TypePersons.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Museum;

// Controlador para gestionar los tipos de persona del museo
[ApiController]
[Route("api/museum/type-persons")]
[Tags("Museum")]
public class TypePersonsController : ControllerBase
{
    private readonly CreateTypePersonUseCase _createTypePerson;
    private readonly UpdateTypePersonUseCase _updateTypePerson;
    private readonly DeleteTypePersonUseCase _deleteTypePerson;
    private readonly GetAllTypePersonsUseCase _getAllTypePersons;
    private readonly GetTypePersonByIdUseCase _getTypePersonById;

    public TypePersonsController(
        CreateTypePersonUseCase createTypePerson,
        UpdateTypePersonUseCase updateTypePerson,
        DeleteTypePersonUseCase deleteTypePerson,
        GetAllTypePersonsUseCase getAllTypePersons,
        GetTypePersonByIdUseCase getTypePersonById)
    {
        _createTypePerson = createTypePerson;
        _updateTypePerson = updateTypePerson;
        _deleteTypePerson = deleteTypePerson;
        _getAllTypePersons = getAllTypePersons;
        _getTypePersonById = getTypePersonById;
    }

    // Controlador para gestionar los tipos de persona del museo
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var typePersons = await _getAllTypePersons.ExecuteAsync();
            return Ok(typePersons);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los tipos de persona del museo
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var typePerson = await _getTypePersonById.ExecuteAsync(id);
            return Ok(typePerson);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los tipos de persona del museo
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTypePersonDto dto)
    {
        try
        {
            var typePerson = await _createTypePerson.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = typePerson.Id }, typePerson);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los tipos de persona del museo
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTypePersonDto dto)
    {
        try
        {
            var typePerson = await _updateTypePerson.ExecuteAsync(id, dto);
            return Ok(typePerson);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los tipos de persona del museo
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteTypePerson.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
