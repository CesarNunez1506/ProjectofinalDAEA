using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Finance
{
    public class OverheadDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es requerido")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "El tipo debe tener entre 1 y 50 caracteres")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "El monto es requerido")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(255, ErrorMessage = "La descripción no debe exceder los 255 caracteres")]
        public string? Description { get; set; }

        // Campos estándar para consistencia
        public bool? Status { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
