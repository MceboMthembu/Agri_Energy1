using Microsoft.EntityFrameworkCore;
using Agri_Energy1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Agri_Energy1.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farmers> Farmers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<FarmerProducts> FarmerProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Configure AspNetUserRoles primary key as non-clustered
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(ur => new { ur.UserId, ur.RoleId })
                .IsClustered(false);

            modelBuilder.Entity<FarmerProducts>()
                .HasKey(fp => new { fp.FarmerId, fp.ProductId });

            modelBuilder.Entity<FarmerProducts>()
                .HasOne(fp => fp.Farmers)
                .WithMany(f => f.FarmerProducts)
                .HasForeignKey(fp => fp.FarmerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FarmerProducts>()
                .HasOne(fp => fp.Products)
                .WithMany(p => p.FarmerProducts)
                .HasForeignKey(fp => fp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
