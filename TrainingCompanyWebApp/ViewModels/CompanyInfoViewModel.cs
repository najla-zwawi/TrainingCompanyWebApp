using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingCompanyWebApp.Models.Entities;

namespace TrainingCompanyWebApp.ViewModels
{
    public class CompanyInfoViewModel
    {
        public TrainingCompany TrainingCompany { get; set; }
        public Contact Contact { get; set; }
        public IFormFile File { get; set; } // To Story New Picture
    }
}
