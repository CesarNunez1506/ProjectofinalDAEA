using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Modules;
using Domain.Interfaces.Repositories;

namespace Application.UseCases.Modules.Queries;

/// <summary>
/// Query para obtener todos los módulos
/// </summary>
public class GetAllModulesQuery
{
    private readonly IModuleRepository _moduleRepository;

    public GetAllModulesQuery(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<IEnumerable<ModuleDto>> ExecuteAsync()
    {
        var modules = await _moduleRepository.GetAllAsync();

        return modules.Select(m => new ModuleDto
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description,
            CreatedAt = m.CreatedAt,
            UpdatedAt = m.UpdatedAt
        });
    }
}

/// <summary>
/// Query para obtener un módulo por ID
/// </summary>
public class GetModuleByIdQuery
{
    private readonly IModuleRepository _moduleRepository;

    public GetModuleByIdQuery(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<ModuleDto?> ExecuteAsync(Guid id)
    {
        var module = await _moduleRepository.GetByIdAsync(id);

        if (module == null)
            return null;

        return new ModuleDto
        {
            Id = module.Id,
            Name = module.Name,
            Description = module.Description,
            CreatedAt = module.CreatedAt,
            UpdatedAt = module.UpdatedAt
        };
    }
}

/// <summary>
/// Query para buscar módulos por nombre (búsqueda parcial)
/// Ejemplo de uso de expresiones para búsquedas dinámicas
/// </summary>
public class SearchModulesByNameQuery
{
    private readonly IModuleRepository _moduleRepository;

    public SearchModulesByNameQuery(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<IEnumerable<ModuleDto>> ExecuteAsync(string searchTerm)
    {
        // Usando expresiones lambda para búsqueda dinámica
        var modules = await _moduleRepository.FindAsync(
            m => m.Name.ToLower().Contains(searchTerm.ToLower())
        );

        return modules.Select(m => new ModuleDto
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description,
            CreatedAt = m.CreatedAt,
            UpdatedAt = m.UpdatedAt
        });
    }
}
