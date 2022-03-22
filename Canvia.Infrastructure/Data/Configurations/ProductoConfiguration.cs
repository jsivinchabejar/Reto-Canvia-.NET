using Canvia.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Canvia.Infrastructure.Data.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> entity)
        {
            entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__09889210015DA576");

            entity.ToTable("Producto");

            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.FechaHoraActualizacion)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.FechaHoraRegistro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.PrecioOriginal).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.PrecioPromocional)
                .HasColumnType("decimal(18, 2)")
                .HasDefaultValueSql("((0))");
        }
    }
}
