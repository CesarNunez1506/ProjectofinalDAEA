namespace Application.DTOs.Museum;

// DTO para la entidad PaymentMethod (Método de Pago)
public class PaymentMethodDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

// DTO para crear PaymentMethod
public class CreatePaymentMethodDto
{
    public string Name { get; set; } = null!;
}

// DTO para actualizar PaymentMethod
public class UpdatePaymentMethodDto
{
    public string? Name { get; set; }
}
