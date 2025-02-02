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

        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(a => a.CustomerId);

                entity.HasMany(a => a.Rentals)
                    .WithOne(r => r.Customer)
                    .HasForeignKey(r => r.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(r => r.RentalId);

                entity.HasMany(r => r.RentalDetails)
                    .WithOne(d => d.Rental)
                    .HasForeignKey(d => d.RentalId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.MovieId);

                entity.Property(m => m.Price)
                    .HasColumnType("decimal(18,2)");

                entity.HasMany(m => m.RentalDetails)
                    .WithOne(d => d.Movie)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RentalDetail>(entity =>
            {
                entity.HasKey(d => d.RentalDetailsId);

                entity.Property(d => d.Quantity).IsRequired();
            });
        }
    }
}
