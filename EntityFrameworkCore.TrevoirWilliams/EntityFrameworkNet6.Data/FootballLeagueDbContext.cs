﻿using EntityFrameworkNet6.Domain.TPH;
using EntityFrameworkNet6.Domain.TPT;
using EntityFrameworkNet6.Domain.Trevor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkNet6.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // LogTo-instruktionen anfører, at vi gerne vil logge alt, hvad den gør, til konsollen.
            // EnableSensitiveDataLogging-instruktionen anfører, at vi vil have en høj grad af detalje med,
            // hvor man faktisk ser det sql, der genereres!
            // (disse 2 instruktioner bør kun være med under udvikling)

            const string connectionStringMeloHome =
                "Data Source=MELO-HOME\\SQLEXPRESS;User=sa;Password=L1on8Zebra;Initial Catalog=FootballLeague_EFCore;Trust Server Certificate=true";

            const string connectionStringDocker =
                "Data Source=localhost,1400;User=sa;Password=L1on8Zebra;Initial Catalog=FootballLeague_EFCore;Trust Server Certificate=true";

            var connectionString = connectionStringDocker;

            optionsBuilder
                .UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Her bruger vi Fluent API til at definere regler

            // Cascade sletter children, når parent slettes - det er problematisk for en many to many-relation som denne,
            // derfor skifer instruktøren til Restrict, hvilket betyder, at man først kan slette parent, når alle children
            // er slettet

            modelBuilder.Entity<Team>()
                .HasMany(m => m.HomeMatches)
                .WithOne(m => m.HomeTeam)
                .HasForeignKey(m => m.HomeTeamId)
                .IsRequired()
                //.OnDelete(DeleteBehavior.Cascade); 
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Team>()
                .HasMany(m => m.AwayMatches)
                .WithOne(m => m.AwayTeam)
                .HasForeignKey(m => m.AwayTeamId)
                .IsRequired()
                //.OnDelete(DeleteBehavior.Cascade);
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cat>().UseTptMappingStrategy();
            modelBuilder.Entity<Dog>().UseTphMappingStrategy();
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Coach> Coaches { get; set; }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Persian> Persians { get; set; }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<BullDog> BullDogs { get; set; }
    }
}