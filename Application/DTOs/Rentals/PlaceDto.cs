using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Rentals;

// DTO para crear un nuevo lugar
public class CreatePlaceDto
{
    [Required(ErrorMessage = "El ID de la ubicación es requerido")]
    public Guid LocationId { get; set; }
    
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El área es requerida")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El área debe tener entre 1 y 100 caracteres")]
    public string Area { get; set; } = string.Empty;
    
    [StringLength(40000, ErrorMessage = "La URL de imagen no debe exceder 2048 caracteres")]
    public string? ImagenUrl { get; set; }
}

// DTO para actualizar un lugar existente
public class UpdatePlaceDto
{
    public Guid? LocationId { get; set; }
    
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
    public string? Name { get; set; }
    
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El área debe tener entre 1 y 100 caracteres")]
    public string? Area { get; set; }
    
    [StringLength(2048, ErrorMessage = "La URL de imagen no debe exceder 2048 caracteres")]
    public string? ImagenUrl { get; set; }
}

// DTO de respuesta para lugar
public class PlaceDto
{
    public Guid Id { get; set; }
    public Guid LocationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string? ImagenUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public LocationDto? Location { get; set; }
}
