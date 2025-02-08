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
                    .WithOne(r => r.Customer);
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(r => r.RentalId);

                entity.HasMany(r => r.RentalDetails)
                    .WithOne(d => d.Rental)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.MovieId);

                entity.Property(m => m.Price)
                    .HasColumnType("decimal(18,2)");

                entity.HasMany(m => m.RentalDetails)
                    .WithOne(d => d.Movie)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RentalDetail>(entity =>
            {
                entity.HasKey(d => d.RentalDetailsId);

                entity.HasOne(d => d.Rental)
                      .WithMany(r => r.RentalDetails)
                      .HasForeignKey(d => d.RentalId);

                entity.HasOne(m => m.Movie)
                      .WithMany(r => r.RentalDetails)
                      .HasForeignKey(i => i.MovieId);
            });
        }
    }
}
