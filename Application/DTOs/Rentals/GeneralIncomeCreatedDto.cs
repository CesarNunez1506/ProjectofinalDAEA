namespace Application.DTOs.Rentals;

// DTO de respuesta para ingresos generales creados
public class GeneralIncomeCreatedDto
{
    public Guid Id { get; set; }
    public string ModuleName { get; set; } = string.Empty;
    public string IncomeType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
}
