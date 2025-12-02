namespace Application.DTOs;

public class MuseoDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? HorarioAtencion { get; set; }
    public bool Activo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateMuseoDto
{
    public string Nombre { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? HorarioAtencion { get; set; }
}

public class UpdateMuseoDto
{
    public string Nombre { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? HorarioAtencion { get; set; }
    public bool Activo { get; set; }
}
