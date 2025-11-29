using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class MuseoConfiguration : IEntityTypeConfiguration<Museo>
{
    public void Configure(EntityTypeBuilder<Museo> builder)
    {
        // Nombre de la tabla
        builder.ToTable("museos");

        // Primary Key
        builder.HasKey(m => m.Id);

        // Propiedades
        builder.Property(m => m.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(m => m.Nombre)
            .HasColumnName("nombre")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(m => m.Direccion)
            .HasColumnName("direccion")
            .HasMaxLength(255);

        builder.Property(m => m.Ciudad)
            .HasColumnName("ciudad")
            .HasMaxLength(255);

        builder.Property(m => m.HorarioAtencion)
            .HasColumnName("horario_atencion")
            .HasMaxLength(255);

        builder.Property(m => m.Activo)
            .HasColumnName("activo")
            .HasDefaultValue(true);

        builder.Property(m => m.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(m => m.UpdatedAt)
            .HasColumnName("updated_at");
    }
}
