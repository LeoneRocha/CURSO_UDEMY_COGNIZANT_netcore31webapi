using CURSO_UDEMY_COGNIZANT_netcore31webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Fireball", Damage = 30 },
                new Skill { Id = 2, Name = "Frenzy", Damage = 20 },
                new Skill { Id = 3, Name = "Dark Hole", Damage = 100 },
                new Skill { Id = 4, Name = "Blizzard", Damage = 50 }
            );

            modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue("Player");
        }
    }
}