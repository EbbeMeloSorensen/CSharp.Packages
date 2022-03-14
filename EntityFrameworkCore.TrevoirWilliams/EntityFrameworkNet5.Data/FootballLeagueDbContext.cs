using EntityFrameworkNet5.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNet5.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FootballLeague_EFCore");
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
    }
}
