using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Modules;
using Application.UseCases.Modules.Commands;
using Application.UseCases.Modules.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers;

/// <summary>
/// Controlador REST API para gestión de módulos del sistema
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize] // Requiere autenticación JWT
public class ModulesController : ControllerBase
{
    private readonly GetAllModulesQuery _getAllModulesQuery;
    private readonly GetModuleByIdQuery _getModuleByIdQuery;
    private readonly SearchModulesByNameQuery _searchModulesByNameQuery;
    private readonly CreateModuleCommand _createModuleCommand;
    private readonly UpdateModuleCommand _updateModuleCommand;
    private readonly DeleteModuleCommand _deleteModuleCommand;

    public ModulesController(
        GetAllModulesQuery getAllModulesQuery,
        GetModuleByIdQuery getModuleByIdQuery,
        SearchModulesByNameQuery searchModulesByNameQuery,
        CreateModuleCommand createModuleCommand,
        UpdateModuleCommand updateModuleCommand,
        DeleteModuleCommand deleteModuleCommand)
    {
        _getAllModulesQuery = getAllModulesQuery;
        _getModuleByIdQuery = getModuleByIdQuery;
        _searchModulesByNameQuery = searchModulesByNameQuery;
        _createModuleCommand = createModuleCommand;
        _updateModuleCommand = updateModuleCommand;
        _deleteModuleCommand = deleteModuleCommand;
    }

    /// <summary>
    /// Obtiene todos los módulos del sistema
    /// </summary>
    /// <returns>Lista de módulos</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ModuleDto>), 200)]
    public async Task<ActionResult<IEnumerable<ModuleDto>>> GetAll()
    {
        try
        {
            var modules = await _getAllModulesQuery.ExecuteAsync();
            return Ok(modules);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener los módulos", error = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene un módulo por su ID
    /// </summary>
    /// <param name="id">ID del módulo</param>
    /// <returns>Módulo encontrado</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ModuleDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ModuleDto>> GetById(Guid id)
    {
        try
        {
            var module = await _getModuleByIdQuery.ExecuteAsync(id);

            if (module == null)
            {
                return NotFound(new { message = $"No se encontró el módulo con ID {id}" });
            }

            return Ok(module);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener el módulo", error = ex.Message });
        }
    }

    /// <summary>
    /// Busca módulos por nombre (búsqueda parcial)
    /// </summary>
    /// <param name="name">Término de búsqueda</param>
    /// <returns>Lista de módulos que coinciden</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<ModuleDto>), 200)]
    public async Task<ActionResult<IEnumerable<ModuleDto>>> Search([FromQuery] string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "El parámetro 'name' es requerido" });
            }

            var modules = await _searchModulesByNameQuery.ExecuteAsync(name);
            return Ok(modules);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al buscar módulos", error = ex.Message });
        }
    }

    /// <summary>
    /// Crea un nuevo módulo
    /// </summary>
    /// <param name="dto">Datos del módulo a crear</param>
    /// <returns>Módulo creado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ModuleDto), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ModuleDto>> Create([FromBody] CreateModuleDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var module = await _createModuleCommand.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = module.Id }, module);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al crear el módulo", error = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza un módulo existente
    /// </summary>
    /// <param name="id">ID del módulo a actualizar</param>
    /// <param name="dto">Nuevos datos del módulo</param>
    /// <returns>Módulo actualizado</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ModuleDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ModuleDto>> Update(Guid id, [FromBody] UpdateModuleDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var module = await _updateModuleCommand.ExecuteAsync(id, dto);
            return Ok(module);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al actualizar el módulo", error = ex.Message });
        }
    }

    /// <summary>
    /// Elimina un módulo
    /// </summary>
    /// <param name="id">ID del módulo a eliminar</param>
    /// <returns>Confirmación de eliminación</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteModuleCommand.ExecuteAsync(id);
            return Ok(new { message = "Módulo eliminado exitosamente" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar el módulo", error = ex.Message });
        }
    }
}
