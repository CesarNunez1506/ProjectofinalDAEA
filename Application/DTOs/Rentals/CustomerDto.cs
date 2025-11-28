using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Rentals;

// DTO para crear un nuevo cliente
public class CreateCustomerDto
{
    [Required(ErrorMessage = "El nombre completo es requerido")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El nombre completo debe tener entre 1 y 255 caracteres")]
    public string FullName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El DNI es requerido")]
    [Range(10000000, 99999999, ErrorMessage = "El DNI debe ser un número de 8 dígitos")]
    public int Dni { get; set; }
    
    [Required(ErrorMessage = "El teléfono es requerido")]
    [StringLength(20, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 20 caracteres")]
    public string Phone { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    [StringLength(255, ErrorMessage = "El email no debe exceder 255 caracteres")]
    public string Email { get; set; } = string.Empty;
}

// DTO para actualizar un cliente existente
public class UpdateCustomerDto
{
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El nombre completo debe tener entre 1 y 255 caracteres")]
    public string? FullName { get; set; }
    
    [Range(10000000, 99999999, ErrorMessage = "El DNI debe ser un número de 8 dígitos")]
    public int? Dni { get; set; }
    
    [StringLength(20, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 20 caracteres")]
    public string? Phone { get; set; }
    
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    [StringLength(255, ErrorMessage = "El email no debe exceder 255 caracteres")]
    public string? Email { get; set; }
}

// DTO de respuesta para cliente
public class CustomerDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Dni { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
