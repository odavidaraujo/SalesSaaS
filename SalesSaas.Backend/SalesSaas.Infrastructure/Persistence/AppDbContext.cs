using Microsoft.EntityFrameworkCore;
using SalesSaaS.Domain.Entities;

namespace SalesSaaS.Infrastructure.Persistence {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Relacionamento: Um usuário pode ter várias empresas
            modelBuilder.Entity<User>()
                .HasMany(u => u.Companies)
                .WithOne(c => c.Owner)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
