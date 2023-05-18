using DiegoWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DiegoWebAPI.Infraestructure.Data.Contexts
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }

        public DbSet<Post> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>();
        }
    }
}
