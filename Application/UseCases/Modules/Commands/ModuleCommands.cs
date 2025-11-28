using System;
using System.Threading.Tasks;
using Application.DTOs.Modules;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases.Modules.Commands;

/// <summary>
/// Comando para crear un nuevo módulo
/// </summary>
public class CreateModuleCommand
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateModuleCommand(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
    {
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ModuleDto> ExecuteAsync(CreateModuleDto dto)
    {
        // Validar que no exista un módulo con el mismo nombre
        if (await _moduleRepository.ExistsByNameAsync(dto.Name))
        {
            throw new InvalidOperationException($"Ya existe un módulo con el nombre '{dto.Name}'");
        }

        var module = new Module
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _moduleRepository.AddAsync(module);
        await _unitOfWork.SaveChangesAsync();

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
/// Comando para actualizar un módulo existente
/// </summary>
public class UpdateModuleCommand
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateModuleCommand(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
    {
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ModuleDto> ExecuteAsync(Guid id, UpdateModuleDto dto)
    {
        var module = await _moduleRepository.GetByIdAsync(id);

        if (module == null)
        {
            throw new InvalidOperationException($"No se encontró el módulo con ID {id}");
        }

        // Validar que no exista otro módulo con el mismo nombre
        var existingModule = await _moduleRepository.GetByNameAsync(dto.Name);
        if (existingModule != null && existingModule.Id != id)
        {
            throw new InvalidOperationException($"Ya existe otro módulo con el nombre '{dto.Name}'");
        }

        module.Name = dto.Name;
        module.Description = dto.Description;
        module.UpdatedAt = DateTime.UtcNow;

        await _moduleRepository.UpdateAsync(module);
        await _unitOfWork.SaveChangesAsync();

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
/// Comando para eliminar un módulo
/// </summary>
public class DeleteModuleCommand
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteModuleCommand(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
    {
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        // Obtener el módulo con sus permisos para validar
        var module = await _moduleRepository.GetByIdWithPermissionsAsync(id);

        if (module == null)
        {
            throw new InvalidOperationException($"No se encontró el módulo con ID {id}");
        }

        // Validar que no tenga permisos asociados (integridad referencial)
        if (module.Permissions.Any())
        {
            throw new InvalidOperationException(
                $"No se puede eliminar el módulo '{module.Name}' porque tiene {module.Permissions.Count} permisos asociados"
            );
        }

        await _moduleRepository.DeleteAsync(module);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
