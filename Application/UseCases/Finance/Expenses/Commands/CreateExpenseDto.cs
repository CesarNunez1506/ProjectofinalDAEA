using System;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Finance.Expenses.Commands
{
    public class CreateExpenseDto
    {
        public Guid ModuleId { get; set; }

        [Required(ErrorMessage = "El tipo de gasto es requerido")]
        [StringLength(100, ErrorMessage = "El tipo de gasto no debe exceder los 100 caracteres")]
        public string ExpenseType { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "La descripción no debe exceder los 255 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El monto es requerido")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Guid? ReportId { get; set; }
    }
}
