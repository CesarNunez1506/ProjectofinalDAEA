using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Modules;

/// <summary>
/// Repositorio específico para Módulos
/// Hereda de GenericRepository y añade lógica específica del dominio
/// </summary>
public class ModuleRepository : GenericRepository<Module>, IModuleRepository
{
    public ModuleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Module?> GetByNameAsync(string name)
    {
        // Usando el método genérico FirstOrDefaultAsync con expresión lambda
        return await FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        // Usando el método genérico AnyAsync con expresión lambda
        return await AnyAsync(m => m.Name.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<Module>> GetAllWithPermissionsAsync()
    {
        // Usando el método genérico GetAsync con Include
        return await GetAsync(includeProperties: "Permissions");
    }

    public async Task<Module?> GetByIdWithPermissionsAsync(Guid id)
    {
        // Usando el método genérico GetAsync con filtro e Include
        var modules = await GetAsync(filter: m => m.Id == id, includeProperties: "Permissions");
        return modules.FirstOrDefault();
    }

    // Podemos sobrescribir métodos del genérico si necesitamos lógica específica
    public override async Task<IEnumerable<Module>> GetAllAsync()
    {
        // Por ejemplo, ordenar por nombre por defecto
        return await _dbSet
            .AsNoTracking()
            .OrderBy(m => m.Name)
            .ToListAsync();
    }
}
