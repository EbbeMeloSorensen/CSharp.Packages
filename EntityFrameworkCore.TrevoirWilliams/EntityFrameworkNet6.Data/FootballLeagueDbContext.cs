using EntityFrameworkNet6.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNet6.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MELO-HOME\\SQLEXPRESS;User=sa;Password=L1on8Zebra;Initial Catalog=FootballLeague_EFCore");
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
    }
}