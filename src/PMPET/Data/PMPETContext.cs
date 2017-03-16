using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMPET.Models;
using System.Reflection.Emit;

namespace PMPET.Data
{
    public class PMPETContext:DbContext
    {
        public PMPETContext(DbContextOptions<PMPETContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Viewing> Viewings { get; set; }
        public DbSet<Offer> Offers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<RealEstate>().ToTable("RealEstate");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Viewing>().ToTable("Viewings");
            modelBuilder.Entity<Offer>().ToTable("Offers");
        }
    }
}
