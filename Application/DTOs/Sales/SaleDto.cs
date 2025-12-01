namespace Application.DTOs.Sales
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string IncomeDate { get; set; } = string.Empty;
        public double TotalIncome { get; set; }
        public string? Observations { get; set; }

        public List<SaleDetailDto> Details { get; set; } = new();
    }
}
