using Microsoft.EntityFrameworkCore;
using SalesService.Models;

namespace SalesService.Data
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options) { }

        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesInvoice>()
                .HasMany(i => i.Items)
                .WithOne(i => i.SalesInvoice)
                .HasForeignKey(i => i.SalesInvoiceId);

            modelBuilder.Entity<SalesInvoice>()
                .HasIndex(i => i.InvoiceNumber)
                .IsUnique();
        }
    }
}
