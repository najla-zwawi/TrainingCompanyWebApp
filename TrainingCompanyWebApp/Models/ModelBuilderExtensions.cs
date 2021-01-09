using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingCompanyWebApp.Models.Entities;

namespace TrainingCompanyWebApp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainingCompany>().HasData(
              new TrainingCompany()
              {
                  Id = Guid.NewGuid(),
                  Name = "IT Company",
                  Specialization = "Network ",
                  Description = "HUB Tech partners with its clients becoming part of their support team. " +
                  " We work beside you to ensure you have a strategy that allows you to transform your Information infrastructure to keep up with the needs of your organization and your users." +
                  " We have developed proprietary tools and strategies that have enabled us to lower cost and increase the quality of service to our client base, especially to state agencies," +
                  " municipalities and school districts, where cost is a deciding factor in everyday decision making.",
                  CreatedDate = DateTime.Now,
                 

              }
              );
            modelBuilder.Entity<Contact>().HasData(
             new Contact()
             {
                 Id = Guid.NewGuid(),              
                 Phone = "002191234567888",
                 Location = "Tripoli - Libya",
                 CreatedDate = DateTime.Now,
                 Email = "NajlaZwawi@gmail.com",
                 Facebook = "Najla_zwawi"

             }
             );
            modelBuilder.Entity<SiteState>().HasData(
            new SiteState()
            {
                Id = Guid.NewGuid(),
                SystemOff=false,
                SystemOffMessage="The Website is clossing"

            }
            );

        }
    }
}
