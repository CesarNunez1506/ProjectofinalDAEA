using System;
using System.Collections.Generic;

namespace Application.DTOs.Finance;

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
