namespace Application.DTOs.Sales
{
    public class SaleDetailDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double Mount { get; set; }
    }
}
