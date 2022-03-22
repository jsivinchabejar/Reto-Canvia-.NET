using Canvia.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Canvia.Infrastructure.Data.Configurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> entity)
        {
            entity.HasKey(e => e.IdPersona)
                   .HasName("PK__Persona__2EC8D2AC5B353136");

            entity.ToTable("Persona");

            entity.Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.FechaHoraActualizacion)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.FechaHoraRegistro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Nombres)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.NumeroDni)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NumeroDNI");
        }
    }
}
