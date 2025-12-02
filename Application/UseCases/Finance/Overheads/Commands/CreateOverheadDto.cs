using System;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Finance.Overheads.Commands
{
    public class CreateOverheadDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public bool? Status { get; set; } = true;
    }
}
