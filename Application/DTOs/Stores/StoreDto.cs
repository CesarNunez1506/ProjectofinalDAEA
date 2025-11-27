namespace Application.DTOs.Stores
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Observations { get; set; }
    }
}
