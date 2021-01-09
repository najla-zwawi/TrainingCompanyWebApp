using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingCompanyWebApp.Models.Entities;

namespace TrainingCompanyWebApp.ViewModels
{
    public class TrainersViewModel
    {
        public Guid Id { get; set; }
        
        public String Name { get; set; }
        public String Specialization { get; set; }
        public DateTime RegDate { get; set; }
        public string Facebook { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        //public DateTime ModifiedDate { get; set; }
        public byte[] DataFile { get; set; }
        //--------------------------------------
        //[FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Please select an Image file.")]
        public IFormFile File { get; set; }
        public List<Trainers> Trainers { get; set; }
    }
}
