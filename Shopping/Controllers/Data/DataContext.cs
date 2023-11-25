using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shooping.Data.Entities;
using Shopping.Controllers.Data.Entities;

namespace Shopping.Controllers.Data
{
    public class DataContext : IdentityDbContext<User>
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        {
                
        }

        public DbSet<Country> countries { get; set; }

        // public DbSet<City> Cities { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("Name", "CountryId").IsUnique();
            modelBuilder.Entity<City>().HasIndex("Name", "StateId").IsUnique();
            modelBuilder.Entity<Category>().HasIndex(C => C.Name).IsUnique();

        }
    }
}
