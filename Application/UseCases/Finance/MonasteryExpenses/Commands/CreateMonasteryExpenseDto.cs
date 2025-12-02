using System;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Finance.MonasteryExpenses.Commands
{
    public class CreateMonasteryExpenseDto
    {
        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        public double Amount { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        public string? Descripcion { get; set; }
    }
}
