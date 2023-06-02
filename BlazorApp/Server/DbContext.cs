using BlazorApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseSqlite(Configuration.GetConnectionString("Database"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .HasIndex(b => b.Phone)
                .IsUnique();
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
