using Canvia.Core.Entities;
using Canvia.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

#nullable disable

namespace Canvia.Infrastructure.Data
{
    public partial class RetoCanviaContext : DbContext
    {
        public RetoCanviaContext()
        {
        }

        public RetoCanviaContext(DbContextOptions<RetoCanviaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=RetoCanvia;uid=sa;pwd=SQL2017!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.ApplyConfiguration(new PersonaConfiguration());
                modelBuilder.ApplyConfiguration(new ProductoConfiguration());

                var foundProperty = entityType.FindProperty("Eliminado");

                if (foundProperty != null && foundProperty.ClrType == typeof(bool))
                {
                    var newParam = Expression.Parameter(entityType.ClrType);
                    var filter = Expression.Lambda(Expression.Equal(Expression.Property(newParam, "Eliminado"), Expression.Constant(false)), newParam);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
