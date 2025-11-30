namespace Application.DTOs.Rentals;

// DTO de respuesta para usuario en el contexto de Rentals
public class RentalUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
