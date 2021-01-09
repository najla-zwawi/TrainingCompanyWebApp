using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingCompanyWebApp.Models.Entities;

namespace TrainingCompanyWebApp.ViewModels
{
    public class CoursesViewModel
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public int Code { get; set; }
        public String Details { get; set; }
        public float CourseFee { get; set; }
        public int HourNum { get; set; }
        public int Seate { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[] DataFile { get; set; }
        //--------------------------------------
        //[FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Please select an Image file.")]
        public IFormFile File { get; set; }
        public List<Trainers> Trainers { get; set; }
        public Guid TrainerId { get; set; }

        //public List<Courses> Courses { get; set; }
    }
}
