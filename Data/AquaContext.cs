using AquaMonitor.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AquaMonitor.Api.Data
{
    public class AquaContext : DbContext
    {
        public AquaContext(DbContextOptions<AquaContext> options)
            : base(options)
        {
        }

        public DbSet<ConsumoAgua> ConsumosAgua { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsumoAgua>()
                .Property(c => c.LitrosConsumidos)
                .HasPrecision(10, 2); // <-- Ajuste recomendado pelo Oracle

            base.OnModelCreating(modelBuilder);
        }
    }
}
