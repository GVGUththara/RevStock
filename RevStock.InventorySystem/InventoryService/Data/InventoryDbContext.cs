using Microsoft.EntityFrameworkCore;
using InventoryService.Models;

namespace InventoryService.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options) { }

        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<SparePartCategory> SparePartCategories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<GRN> GRNs { get; set; }
        public DbSet<GRNItem> GRNItems { get; set; }
        public DbSet<StockLedger> StockLedgers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Category → SpareParts (1–Many)
            modelBuilder.Entity<SparePart>()
                .HasOne(p => p.Category)
                .WithMany(c => c.SpareParts)
                .HasForeignKey(p => p.CategoryId);

            // SparePart → Stock (1–1)
            modelBuilder.Entity<SparePart>()
                .HasOne(p => p.Stock)
                .WithOne(s => s.SparePart)
                .HasForeignKey<Stock>(s => s.SparePartId);

            // GRN → GRNItems (1–Many)
            modelBuilder.Entity<GRNItem>()
                .HasOne(i => i.GRN)
                .WithMany(g => g.Items)
                .HasForeignKey(i => i.GRNId);
        }

    }
}
