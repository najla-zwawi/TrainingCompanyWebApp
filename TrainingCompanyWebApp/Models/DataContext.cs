using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingCompanyWebApp.Models.Entities;

namespace TrainingCompanyWebApp.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<TrainingCompany> TrainingCompany { get; set; }
        public DbSet<Trainers> Trainers { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<SiteState> SiteState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrainingCompany>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Trainers>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Courses>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Contact>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<SiteState>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }

            modelBuilder.Seed();

            //modelBuilder.Entity<Owner>().HasData(
            //    new Owner()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Ahmed Mohammed",
            //        Description = "Photographer and Designer",
            //    }
            //    );



        }
    }
}
