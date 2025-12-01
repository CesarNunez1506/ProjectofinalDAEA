using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

/// <summary>
/// Interfaz específica para el repositorio de Módulos
/// Extiende el repositorio genérico y añade métodos específicos del dominio
/// </summary>
public interface IModuleRepository : IRepository<Module>
{
    /// <summary>
    /// Obtiene un módulo por su nombre
    /// </summary>
    Task<Module?> GetByNameAsync(string name);
    
    /// <summary>
    /// Verifica si existe un módulo con el nombre dado (útil para validaciones)
    /// </summary>
    Task<bool> ExistsByNameAsync(string name);
    
    /// <summary>
    /// Obtiene todos los módulos con sus permisos relacionados
    /// </summary>
    Task<IEnumerable<Module>> GetAllWithPermissionsAsync();
    
    /// <summary>
    /// Obtiene un módulo con todos sus permisos
    /// </summary>
    Task<Module?> GetByIdWithPermissionsAsync(Guid id);
}
