# 🚀 GUÍA RÁPIDA DE IMPLEMENTACIÓN

## Para completar los componentes faltantes, sigue estos templates:

---

## 1️⃣ CREAR UN REPOSITORIO

### Template base (copiar y adaptar):

```csharp
using Domain.Entities;
using Domain.Interfaces.Repositories.Production;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Production;

public class [Entity]Repository : I[Entity]Repository
{
    private readonly LocalDbContext _context;

    public [Entity]Repository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<[Entity]>> GetAllAsync()
    {
        return await _context.[Entities]
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<[Entity]?> GetByIdAsync(Guid id)
    {
        return await _context.[Entities]
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<[Entity]> CreateAsync([Entity] entity)
    {
        _context.[Entities].Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<[Entity]> UpdateAsync([Entity] entity)
    {
        _context.[Entities].Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.[Entities].FindAsync(id);
        if (entity == null) return false;

        _context.[Entities].Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.[Entities].AnyAsync(e => e.Id == id);
    }
}
```

### Ejemplos de uso de Include (para relaciones):

```csharp
// Un nivel
public async Task<Product?> GetByIdWithCategoryAsync(Guid id)
{
    return await _context.Products
        .Include(p => p.Category)
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);
}

// Múltiples niveles
public async Task<Production?> GetByIdWithRelationsAsync(Guid id)
{
    return await _context.Productions
        .Include(p => p.Product)
            .ThenInclude(pr => pr.Category)
        .Include(p => p.Plant)
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);
}
```

---

## 2️⃣ CREAR UN CASO DE USO

### Template para CREATE:

```csharp
using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.[Module];

public class Create[Entity]UseCase
{
    private readonly I[Entity]Repository _repository;
    // Agregar otros repositorios si es necesario

    public Create[Entity]UseCase(I[Entity]Repository repository)
    {
        _repository = repository;
    }

    public async Task<[Entity]Dto> ExecuteAsync(Create[Entity]Dto dto)
    {
        // 1. Validaciones de negocio
        // Ejemplo: verificar duplicados, existencia de FK, etc.

        // 2. Crear entidad
        var entity = new [Entity]
        {
            Id = Guid.NewGuid(),
            // ... mapear propiedades desde dto
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // 3. Guardar
        var created = await _repository.CreateAsync(entity);

        // 4. Retornar DTO
        return new [Entity]Dto
        {
            Id = created.Id,
            // ... mapear propiedades
        };
    }
}
```

### Template para GET ALL:

```csharp
public class GetAll[Entities]UseCase
{
    private readonly I[Entity]Repository _repository;

    public GetAll[Entities]UseCase(I[Entity]Repository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<[Entity]Dto>> ExecuteAsync()
    {
        var entities = await _repository.GetAllAsync();

        return entities.Select(e => new [Entity]Dto
        {
            Id = e.Id,
            // ... mapear propiedades
        });
    }
}
```

### Template para GET BY ID:

```csharp
public class Get[Entity]ByIdUseCase
{
    private readonly I[Entity]Repository _repository;

    public Get[Entity]ByIdUseCase(I[Entity]Repository repository)
    {
        _repository = repository;
    }

    public async Task<[Entity]Dto?> ExecuteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        
        if (entity == null)
            return null;

        return new [Entity]Dto
        {
            Id = entity.Id,
            // ... mapear propiedades
        };
    }
}
```

### Template para UPDATE:

```csharp
public class Update[Entity]UseCase
{
    private readonly I[Entity]Repository _repository;

    public Update[Entity]UseCase(I[Entity]Repository repository)
    {
        _repository = repository;
    }

    public async Task<[Entity]Dto> ExecuteAsync(Guid id, Update[Entity]Dto dto)
    {
        // 1. Verificar que existe
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"[Entity] con ID {id} no encontrada");
        }

        // 2. Actualizar solo campos proporcionados
        if (dto.Property1 != null)
            entity.Property1 = dto.Property1;
        
        if (dto.Property2.HasValue)
            entity.Property2 = dto.Property2.Value;

        entity.UpdatedAt = DateTime.UtcNow;

        // 3. Guardar
        var updated = await _repository.UpdateAsync(entity);

        // 4. Retornar DTO
        return new [Entity]Dto
        {
            Id = updated.Id,
            // ... mapear propiedades
        };
    }
}
```

### Template para DELETE:

```csharp
public class Delete[Entity]UseCase
{
    private readonly I[Entity]Repository _repository;

    public Delete[Entity]UseCase(I[Entity]Repository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        // 1. Verificar que existe
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"[Entity] con ID {id} no encontrada");
        }

        // 2. Validaciones de negocio
        // Ejemplo: verificar que no tenga registros dependientes

        // 3. Eliminar (físico o lógico según caso)
        // Soft delete:
        return await _repository.SoftDeleteAsync(id);
        
        // Hard delete:
        // return await _repository.DeleteAsync(id);
    }
}
```

---

## 3️⃣ CREAR UN CONTROLADOR

### Template completo:

```csharp
using Application.DTOs.Production;
using Application.UseCases.Production.[Module];
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Production;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class [Entities]Controller : ControllerBase
{
    private readonly Create[Entity]UseCase _createUseCase;
    private readonly GetAll[Entities]UseCase _getAllUseCase;
    private readonly Get[Entity]ByIdUseCase _getByIdUseCase;
    private readonly Update[Entity]UseCase _updateUseCase;
    private readonly Delete[Entity]UseCase _deleteUseCase;
    private readonly ILogger<[Entities]Controller> _logger;

    public [Entities]Controller(
        Create[Entity]UseCase createUseCase,
        GetAll[Entities]UseCase getAllUseCase,
        Get[Entity]ByIdUseCase getByIdUseCase,
        Update[Entity]UseCase updateUseCase,
        Delete[Entity]UseCase deleteUseCase,
        ILogger<[Entities]Controller> logger)
    {
        _createUseCase = createUseCase;
        _getAllUseCase = getAllUseCase;
        _getByIdUseCase = getByIdUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los [entities]
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<[Entity]Dto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<[Entity]Dto>>> GetAll()
    {
        try
        {
            var result = await _getAllUseCase.ExecuteAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener [entities]");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene un [entity] por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof([Entity]Dto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<[Entity]Dto>> GetById(Guid id)
    {
        try
        {
            var result = await _getByIdUseCase.ExecuteAsync(id);
            if (result == null)
                return NotFound(new { error = $"[Entity] con ID {id} no encontrado" });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener [entity] {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea un nuevo [entity]
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof([Entity]Dto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<[Entity]Dto>> Create([FromBody] Create[Entity]Dto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _createUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error de validación");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear [entity]");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza un [entity]
    /// </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof([Entity]Dto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<[Entity]Dto>> Update(Guid id, [FromBody] Update[Entity]Dto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _updateUseCase.ExecuteAsync(id, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "[Entity] no encontrado: {Id}", id);
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error de validación");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar [entity] {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina un [entity]
    /// </summary>
    [HttpDelete("{id}")]
    // O [HttpPut("{id}")] para soft delete (como en el original)
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteUseCase.ExecuteAsync(id);
            return Ok(new { message = "[Entity] eliminado exitosamente" });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "[Entity] no encontrado: {Id}", id);
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "No se puede eliminar [entity] {Id}", id);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar [entity] {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }
}
```

---

## 4️⃣ CONTROLADOR CON UPLOAD DE ARCHIVOS (Para Products)

```csharp
[HttpPost]
[Consumes("multipart/form-data")]
public async Task<ActionResult<ProductDto>> Create([FromForm] CreateProductDto dto)
{
    try
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _createUseCase.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error al crear producto");
        return StatusCode(500, new { error = "Error interno del servidor" });
    }
}
```

### Uso de IFormFile en el caso de uso:

```csharp
public async Task<ProductDto> ExecuteAsync(CreateProductDto dto)
{
    string? imageUrl = null;

    // Manejar carga de imagen
    if (dto.ImageFile != null && dto.ImageFile.Length > 0)
    {
        using var memoryStream = new MemoryStream();
        await dto.ImageFile.CopyToAsync(memoryStream);
        
        imageUrl = await _fileStorageService.SaveFileAsync(
            memoryStream.ToArray(),
            dto.ImageFile.FileName,
            "products"
        );
    }

    var product = new Product
    {
        Id = Guid.NewGuid(),
        Name = dto.Name,
        CategoryId = dto.CategoryId,
        Price = dto.Price,
        Description = dto.Description,
        ImagenUrl = imageUrl,
        Status = true,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    var created = await _productRepository.CreateAsync(product);

    return new ProductDto
    {
        Id = created.Id,
        Name = created.Name,
        // ... resto de propiedades
    };
}
```

---

## 5️⃣ REGISTRAR EN PROGRAM.CS

### Para cada componente creado, agregar:

```csharp
// Repositorio
builder.Services.AddScoped<I[Entity]Repository, [Entity]Repository>();

// Casos de uso
builder.Services.AddScoped<Create[Entity]UseCase>();
builder.Services.AddScoped<GetAll[Entities]UseCase>();
builder.Services.AddScoped<Get[Entity]ByIdUseCase>();
builder.Services.AddScoped<Update[Entity]UseCase>();
builder.Services.AddScoped<Delete[Entity]UseCase>();
```

---

## 6️⃣ CHECKLIST POR MÓDULO

Para cada entidad (Product, Recipe, PlantProduction, etc.):

- [ ] ✅ Interfaz de repositorio existe (en Domain/Interfaces)
- [ ] ✅ DTOs existen (en Application/DTOs)
- [ ] 🔶 Implementar repositorio (en Infrastructure/Repositories)
- [ ] 🔶 Crear 5 casos de uso (Create, GetAll, GetById, Update, Delete)
- [ ] 🔶 Crear controlador con 5 endpoints
- [ ] 🔶 Registrar en Program.cs
- [ ] 🔶 Probar con Swagger/Postman

---

## 7️⃣ ORDEN SUGERIDO DE IMPLEMENTACIÓN

1. **Products** (prioridad alta, tiene upload de imágenes)
2. **Recipes** (prioridad alta, depende de productos)
3. **PlantProductions** (prioridad media)
4. **Productions** (prioridad alta, caso complejo ya está)
5. **Losts** (prioridad baja)

---

## 🎯 RECUERDA

- Usar `AsNoTracking()` en queries de solo lectura
- Usar `Include()` para cargar relaciones
- Validar ModelState en controllers
- Manejar excepciones apropiadamente
- Loggear errores y advertencias
- Documentar con comentarios XML (///)
- Seguir el patrón establecido

---

**¡Copia estos templates y adapta los nombres! La estructura ya está definida.** 🚀
