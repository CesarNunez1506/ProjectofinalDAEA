namespace Application.DTOs.Museum;

// DTO para la entidad TypePerson (Tipo de Persona)
public class TypePersonDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public double BasePrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

// DTO para crear TypePerson
public class CreateTypePersonDto
{
    public string Name { get; set; } = null!;
    public double BasePrice { get; set; }
}

// DTO para actualizar TypePerson
public class UpdateTypePersonDto
{
    public string? Name { get; set; }
    public double? BasePrice { get; set; }
}
