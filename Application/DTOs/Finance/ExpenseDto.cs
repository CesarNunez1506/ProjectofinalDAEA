using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Finance
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }

        [Required(ErrorMessage = "El tipo de gasto es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El tipo de gasto debe tener entre 1 y 100 caracteres")]
        public string ExpenseType { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "La descripción no debe exceder los 255 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El monto es requerido")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Guid? ReportId { get; set; }

        // Campos para consistencia con otros módulos
        public bool? Status { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
