using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra
{

    public class DatabaseContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Lojista> Lojistas { get; set; }

        public DatabaseContext()
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=local_user;Password=local_password;Database=transactions_local_database");
        }

    }
}
