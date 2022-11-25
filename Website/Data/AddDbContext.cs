using Microsoft.EntityFrameworkCore;
using Website.Models.Entity;

namespace Website.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(s => s.Catalogue)
                .WithMany(g => g.Products)
                .HasForeignKey(s => s.catalogue_id);
        }

    }
}
