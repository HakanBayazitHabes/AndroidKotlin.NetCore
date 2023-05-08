using Microsoft.EntityFrameworkCore;

namespace AndroidKotlin.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p=>p.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(p=>p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(p=>p.Color).IsRequired();
            modelBuilder.Entity<Product>().Property(p=>p.Stock).IsRequired();

            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(c=>c.Name).IsRequired();
            modelBuilder.Entity<Category>().HasMany(c=>c.Products).WithOne(p=>p.Category).HasForeignKey(p=>p.Category_Id).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }

}
