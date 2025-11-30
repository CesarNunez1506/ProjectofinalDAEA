using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Finance
{
    public class FinancialReportDto
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public decimal TotalIncome { get; set; }

        [Required]
        public decimal TotalExpenses { get; set; }

        [Required]
        public decimal NetProfit { get; set; }

        [StringLength(500, ErrorMessage = "Las notas no deben exceder los 500 caracteres")]
        public string? Observations { get; set; }

        // Campos para consistencia con otros módulos
        public bool? Status { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
