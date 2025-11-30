using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases;

public interface IMuseoUseCases
{
    Task<IEnumerable<MuseoDto>> GetAllAsync();
    Task<MuseoDto?> GetByIdAsync(Guid id);
    Task<MuseoDto> CreateAsync(CreateMuseoDto dto);
    Task<MuseoDto?> UpdateAsync(Guid id, UpdateMuseoDto dto);
    Task<bool> DeleteAsync(Guid id);
}

public class MuseoUseCases : IMuseoUseCases
{
    private readonly IMuseoRepository _repo;

    public MuseoUseCases(IMuseoRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<MuseoDto>> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();
        return data.Select(ToDto);
    }

    public async Task<MuseoDto?> GetByIdAsync(Guid id)
    {
        var museo = await _repo.GetByIdAsync(id);
        return museo == null ? null : ToDto(museo);
    }

    public async Task<MuseoDto> CreateAsync(CreateMuseoDto dto)
    {
        var museo = new Museo
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Direccion = dto.Direccion,
            Ciudad = dto.Ciudad,
            HorarioAtencion = dto.HorarioAtencion,
            Activo = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(museo);

        return ToDto(museo);
    }

    public async Task<MuseoDto?> UpdateAsync(Guid id, UpdateMuseoDto dto)
    {
        var museo = await _repo.GetByIdAsync(id);
        if (museo == null) return null;

        museo.Nombre = dto.Nombre;
        museo.Direccion = dto.Direccion;
        museo.Ciudad = dto.Ciudad;
        museo.HorarioAtencion = dto.HorarioAtencion;
        museo.Activo = dto.Activo;
        museo.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(museo);

        return ToDto(museo);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var museo = await _repo.GetByIdAsync(id);
        if (museo == null) return false;

        await _repo.DeleteAsync(museo);
        return true;
    }

    private static MuseoDto ToDto(Museo m) => new()
    {
        Id = m.Id,
        Nombre = m.Nombre,
        Direccion = m.Direccion,
        Ciudad = m.Ciudad,
        HorarioAtencion = m.HorarioAtencion,
        Activo = m.Activo,
        CreatedAt = m.CreatedAt,
        UpdatedAt = m.UpdatedAt
    };
}
