using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Rentals;

// DTO para crear una nueva ubicación
public class CreateLocationDto
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 255 caracteres")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La dirección es requerida")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "La dirección debe tener entre 1 y 500 caracteres")]
    public string Address { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La capacidad es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor que 0")]
    public int Capacity { get; set; }
    
    [Required(ErrorMessage = "El estado es requerido")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El estado debe tener entre 1 y 50 caracteres")]
    public string Status { get; set; } = string.Empty;
}

// DTO para actualizar una ubicación existente
public class UpdateLocationDto
{
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 255 caracteres")]
    public string? Name { get; set; }
    
    [StringLength(500, MinimumLength = 1, ErrorMessage = "La dirección debe tener entre 1 y 500 caracteres")]
    public string? Address { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor que 0")]
    public int? Capacity { get; set; }
    
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El estado debe tener entre 1 y 50 caracteres")]
    public string? Status { get; set; }
}

// DTO de respuesta para ubicación
public class LocationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
