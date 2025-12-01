using System;

namespace Application.DTOs.Modules;

/// <summary>
/// DTO para representar un módulo en las respuestas
/// </summary>
public class ModuleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// DTO para crear un nuevo módulo
/// </summary>
public class CreateModuleDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

/// <summary>
/// DTO para actualizar un módulo existente
/// </summary>
public class UpdateModuleDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
