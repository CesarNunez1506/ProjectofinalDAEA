using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Finance;

/// <summary>
/// DTO para crear un nuevo overhead (cierre de gastos del monasterio)
/// </summary>
public class CreateOverheadDto
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(255, ErrorMessage = "El nombre no debe exceder los 255 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "El monto es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
    public decimal Amount { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres")]
    public string? Description { get; set; }
}

/// <summary>
/// DTO para actualizar un overhead
/// </summary>
public class UpdateOverheadDto
{
    [StringLength(255, ErrorMessage = "El nombre no debe exceder los 255 caracteres")]
    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
    public decimal? Amount { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres")]
    public string? Description { get; set; }

    public bool? Status { get; set; }
}

/// <summary>
/// DTO de respuesta para overhead
/// </summary>
public class OverheadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<MonasteryExpenseDto> MonasteryExpenses { get; set; } = new List<MonasteryExpenseDto>();
}
