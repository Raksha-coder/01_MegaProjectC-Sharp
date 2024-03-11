using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class userDBContext:DbContext, IuserDBContext
    {
        public userDBContext(DbContextOptions<userDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Country> CountryTable => Set<Country>();

        public DbSet<State> StateTable => Set<State>();

        public DbSet<User> UserTable => Set<User>();



        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Country>()
                .HasMany(u => u.User)
                .WithOne(u => u.Country)
                .HasForeignKey(u => u.CountryId);  //countryid is the foreign key in user

            modelBuilder.Entity<State>()
                .HasMany(s => s.User)
                .WithOne(s => s.State)
                .HasForeignKey(s => s.StateId); //stateis is the foreign key in user


            //set pk
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Country>()
               .HasKey(u => u.CountryId);

            modelBuilder.Entity<State>()
               .HasKey(u => u.StateId);


            //email is unique
            modelBuilder.Entity<User>()
            .HasIndex(e => e.Email)
            .IsUnique();

            //username is unique
            modelBuilder.Entity<User>()
           .HasIndex(e => e.Username)
           .IsUnique();


        }
        }
}
