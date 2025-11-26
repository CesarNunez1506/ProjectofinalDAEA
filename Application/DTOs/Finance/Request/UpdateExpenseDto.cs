namespace Application.DTOs.Finance.Request
{
    public class UpdateExpenseDto
    {
        public System.Guid Id { get; set; }
        public System.Guid ModuleId { get; set; }
        public string ExpenseType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
        public string? Description { get; set; }
        public System.Guid? ReportId { get; set; }
    }
}
