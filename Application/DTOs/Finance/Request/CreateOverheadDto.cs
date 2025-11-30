using System;

namespace Application.DTOs.Finance.Request
{
    public class CreateOverheadDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; } = true;
    }
}
