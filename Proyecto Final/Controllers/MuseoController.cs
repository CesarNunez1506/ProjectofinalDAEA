using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MuseoController : ControllerBase
{
    private readonly IMuseoUseCases _useCases;

    public MuseoController(IMuseoUseCases useCases)
    {
        _useCases = useCases;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _useCases.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _useCases.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMuseoDto dto)
    {
        var result = await _useCases.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateMuseoDto dto)
    {
        var result = await _useCases.UpdateAsync(id, dto);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _useCases.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
