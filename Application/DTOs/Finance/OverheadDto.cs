namespace Application.DTOs.Finance
{
    public class OverheadDto
    {
        public System.Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public System.DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
    }
}
