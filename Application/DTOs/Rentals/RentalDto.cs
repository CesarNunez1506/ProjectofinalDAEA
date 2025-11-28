using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Rentals;

// DTO para crear un nuevo alquiler
public class CreateRentalDto
{
    [Required(ErrorMessage = "El ID del cliente es requerido")]
    public Guid CustomerId { get; set; }
    
    [Required(ErrorMessage = "El ID del lugar es requerido")]
    public Guid PlaceId { get; set; }
    
    [Required(ErrorMessage = "El ID del usuario es requerido")]
    public Guid UserId { get; set; }
    
    [Required(ErrorMessage = "La fecha de inicio es requerida")]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "La fecha de fin es requerida")]
    public DateTime EndDate { get; set; }
    
    [Required(ErrorMessage = "El monto es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0")]
    public decimal Amount { get; set; }
}

// DTO para actualizar un alquiler existente
public class UpdateRentalDto
{
    public Guid? CustomerId { get; set; }
    
    public Guid? PlaceId { get; set; }
    
    public Guid? UserId { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0")]
    public decimal? Amount { get; set; }
    
    public bool? Status { get; set; }
}

// DTO de respuesta para alquiler
public class RentalDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid PlaceId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Amount { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public CustomerDto? Customer { get; set; }
    public PlaceDto? Place { get; set; }
    public UserDto? User { get; set; }
}

// DTO de respuesta para alquiler creado con ingreso generado
public class RentalCreatedResponseDto
{
    public Guid Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Lugar { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public decimal Monto { get; set; }
    public bool Status { get; set; }
    public GeneralIncomeCreatedDto? IngresoGenerado { get; set; }
}
