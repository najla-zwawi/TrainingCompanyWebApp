using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingCompanyWebApp.Models;
using TrainingCompanyWebApp.Models.Entities;
using TrainingCompanyWebApp.Models.Interfaces;
using TrainingCompanyWebApp.ViewModels;

namespace TrainingCompanyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<TrainingCompany> _trainingcompany;
        private readonly IUnitOfWork<Courses> _courses;
        private readonly IUnitOfWork<Contact> _contact;       
        private readonly IUnitOfWork<Trainers> _trainers;
        private readonly IUnitOfWork<SiteState> _SiteState;

        public HomeController(
              IUnitOfWork<TrainingCompany> trainingcompany,
              IUnitOfWork<Courses> courses,
              IUnitOfWork<Contact> contact,         
              IUnitOfWork<Trainers> trainers,
               IUnitOfWork<SiteState> sitestate)
        {
            _trainingcompany = trainingcompany;
            _courses = courses;
            _contact = contact;         
            _trainers = trainers;
            _SiteState = sitestate;
        }
        public async Task<IActionResult> Index()
        {
            var TrainingCompany = await _trainingcompany.Entity.GetAll().AsNoTracking().FirstOrDefaultAsync();
         
            var Courses = await _courses.Entity.GetAll().Include(a => a.Trainer).AsNoTracking().OrderByDescending(n => n.RegDate).ToListAsync();

            var Trainers = await _trainers.Entity.GetAll().AsNoTracking().OrderBy(n => n.Name).ToListAsync();

            var Contact = await _contact.Entity.GetAll().AsNoTracking().FirstOrDefaultAsync();     

            var HomeViewModel  = new HomeViewModel
            {
                TrainingCompany = TrainingCompany,
                Courses = Courses,
                Trainers = Trainers,
                Contact = Contact,
                NumOfCourses = Courses.Count(),
                NumOfTrainers = Trainers.Count()
            };

            return View(HomeViewModel);
        }
        public async Task<FileContentResult> GetImageCourses(Guid id)
        {
            if (id == Guid.Empty || id == null)
            {
                return null;
            }
            var Img = await _courses.Entity.GetByIdAsync(id);
            if (Img == null)
            {
                return null;
            }
            else
            {
                byte[] image = Img.Picture;
                return File(image, "image/jpg");
            }
        }
        public async Task<FileContentResult> GetImage(Guid id)
        {
            if (id == Guid.Empty || id == null)
            {
                return null;
            }

            var Img = await _trainers.Entity.GetByIdAsync(id);
            if (Img == null)

            {
                return null;
            }
            else
            {
                byte[] image = Img.DataFile;
                return File(image, "image/jpg");
            }

        }
        private byte[] FileToByte(IFormFile file)
        {
            var target = new MemoryStream();
            file.CopyTo(target);
            return (target.ToArray());
        }
       

       
    }
}
