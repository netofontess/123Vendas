using _123Vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.Infrastructure.Context
{
    public class SalesDbContext(DbContextOptions<SalesDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>()
                .HasMany(s => s.SaleItems)
                .WithOne(si => si.Sale)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItem { get; set; }
    }
}
