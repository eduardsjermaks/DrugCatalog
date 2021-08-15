using DrugCatalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrugCatalog.Data
{
    public class DrugCatalogContext: DbContext
    {
        public DrugCatalogContext(DbContextOptions<DrugCatalogContext> options) : base(options)
        {
        }

        public DbSet<Drug> Drugs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drug>()
                    .Property(s => s.Code)
                    .HasMaxLength(30)
                    .IsRequired();

            modelBuilder.Entity<Drug>()
                    .Property(s => s.Label)
                    .HasMaxLength(100)
                    .IsRequired();
        }
    }
}
