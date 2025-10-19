using API_Veterinaria.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace API_Veterinaria.Data
{
    public class VeterinariaDbContext : IdentityDbContext<Usuario>
    {
        public VeterinariaDbContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IAuditable>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.FechaCreacion = DateTime.UtcNow;
                    entry.Entity.Activo = true;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.FechaActualizacion = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.FechaBaja = DateTime.UtcNow;
                    entry.Entity.Activo = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Veterinaria> Veterinarias { get; set; }
    }
}
