using System;

namespace Application.UseCases.Finance.Overheads.Commands
{
    public class UpdateOverheadDto
    {
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }
}
