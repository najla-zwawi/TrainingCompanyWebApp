using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingCompanyWebApp.Models.Entities;

namespace TrainingCompanyWebApp.ViewModels
{
    public class HomeViewModel
    {
        public List<Trainers> Trainers { get; set; }
        public List<Courses> Courses{ get; set; }
        public TrainingCompany TrainingCompany { get; set; }
        public Contact Contact { get; set; }
        public int NumOfTrainers { get; set; }
        public int NumOfCourses { get; set; }
    }
}
