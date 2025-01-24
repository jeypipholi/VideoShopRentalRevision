using Microsoft.EntityFrameworkCore;
using VideoShopRentalRevision.Models;

namespace VideoShopRentalRevision.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(a => a.RentalId);
                entity.Property(c => c.TotalCost)
                    .HasColumnType("decimal(18,2)");

            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(a => a.CustomerId);

                entity.HasMany(a => a.Rentals)
                    .WithOne(c => c.Customer)
                    .HasForeignKey(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(a => a.MovieId);
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");

                entity.HasMany(p => p.Rentals)
                    .WithOne(m => m.Movie)
                    .HasForeignKey(f => f.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
