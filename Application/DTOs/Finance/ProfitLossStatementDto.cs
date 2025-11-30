using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Finance
{
    public class ProfitLossStatementDto
    {
        [Required]
        public decimal TotalIncome { get; set; }

        [Required]
        public decimal TotalExpenses { get; set; }

        [Required]
        public decimal NetProfit { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Campos estándar para consistencia con otros módulos
        public bool? Status { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
