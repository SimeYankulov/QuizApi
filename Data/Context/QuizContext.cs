﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace Data.Context
{

    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext>options):base(options)
        { 
           
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Team_User> TeamUsers { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team_User>()
                .HasKey(tu => new { tu.UserId, tu.TeamId });
            modelBuilder.Entity<Team_User>()
                .HasOne(tu => tu.team)
                .WithMany(t => t.team_Users)
                .HasForeignKey(tu => tu.TeamId);
            modelBuilder.Entity<Team_User>()
                .HasOne(tu => tu.user)
                .WithMany(u => u.team_Users)
                .HasForeignKey(tu => tu.UserId);

            //
        }
    }
}
