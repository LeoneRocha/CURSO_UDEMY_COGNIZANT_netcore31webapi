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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}