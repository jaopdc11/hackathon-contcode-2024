using Microsoft.EntityFrameworkCore;
using Hackacont2024.Models;
using System.Diagnostics.Eventing.Reader;

namespace Hackacont2024.Data {
    public class ApplicationDbContext : DbContext
    {
        // Represents the collection of 'User' entities in the database.
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Cargo> Cargas { get; set; }
        public DbSet<Caminhao> Caminhoes { get; set; }
        public DbSet<LogModel> Log { get; set; }

        // Constructor to pass the DbContextOptions to the base DbContext class.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Configures the model (entity) relationships and constraints for the database.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensures that the 'Codigo' field is unique across all users in the database.
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Codigo)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
