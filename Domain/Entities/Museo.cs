using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Museo
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? HorarioAtencion { get; set; }

    public bool Activo { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    

    // → Si tu proyecto usa relaciones, las agregamos aquí
    // public virtual ICollection<ObraArte> ObrasArte { get; set; } = new List<ObraArte>();
}
