using System;
using System.Collections.Generic;
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
    public class CompanyInfoController : BaseController
    {
        private readonly IUnitOfWork<TrainingCompany> _trainingcompany;
        private readonly IUnitOfWork<Contact> _contact;
    
        public CompanyInfoController(IUnitOfWork<TrainingCompany> trainingcompany, IUnitOfWork<Contact> contact)
        {
            _trainingcompany = trainingcompany;         
            _contact = contact;                   
        }
        public async Task<IActionResult> Details()
        {
            var TrainingCompany = await _trainingcompany.Entity.GetAll().AsNoTracking().FirstOrDefaultAsync();

            var Contact = await _contact.Entity.GetAll().AsNoTracking().FirstOrDefaultAsync();

            var CompanyInfoViewModel = new CompanyInfoViewModel
            {
                TrainingCompany = TrainingCompany,
                Contact = Contact
            };
            return View(CompanyInfoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {         
            var TrainingCompany = await _trainingcompany.Entity.GetAll().FirstOrDefaultAsync();
            var Contact = await _contact.Entity.GetAll().FirstOrDefaultAsync();

            CompanyInfoViewModel CompanyInfoViewModel = new CompanyInfoViewModel
            {
                TrainingCompany = TrainingCompany,
                Contact = Contact,
            };
            return View(CompanyInfoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var TrainingCompany = await _trainingcompany.Entity.GetAll().FirstOrDefaultAsync();
                    if (TrainingCompany != null)
                    {
                        TrainingCompany.Name = model.TrainingCompany.Name;
                        TrainingCompany.Specialization = model.TrainingCompany.Specialization;
                        TrainingCompany.Description = model.TrainingCompany.Description;
                        TrainingCompany.CreatedDate = model.TrainingCompany.CreatedDate;
                        TrainingCompany.ModifiedDate = DateTime.Now;
                        if (model.File != null)
                        {
                            TrainingCompany.Picture = FileToByte(model.File);
                        }                       
                        _trainingcompany.Entity.Update(TrainingCompany);
                        await _trainingcompany.SaveAsync();
                    }
                    else
                    {
                        TrainingCompany newtrainingcompany = new TrainingCompany
                        {
                            Name = model.TrainingCompany.Name,
                            Specialization = model.TrainingCompany.Specialization,
                            Description = model.TrainingCompany.Description,
                            CreatedDate = DateTime.Now,
                            Picture = FileToByte(model.File) ?? null
                        };
                        _trainingcompany.Entity.Insert(newtrainingcompany);
                        await _trainingcompany.SaveAsync();
                    }


                    var Contact = await _contact.Entity.GetAll().FirstOrDefaultAsync();
                    if (Contact != null)
                    {
                        Contact.Email = model.Contact.Email;
                        Contact.Phone = model.Contact.Phone;
                        Contact.Facebook = model.Contact.Facebook;
                        Contact.Location = model.Contact.Location;
                        Contact.CreatedDate = model.Contact.CreatedDate;
                        Contact.ModifiedDate = DateTime.Now;
                        _contact.Entity.Update(Contact);
                        await _contact.SaveAsync();
                    }
                    else
                    {
                        //كود زايد للشرح والتوضيح
                        Contact newContact = new Contact
                        {
                            Email = model.Contact.Email,
                            Phone = model.Contact.Phone,
                            Facebook = model.Contact.Facebook,
                           Location = model.Contact.Location,
                           CreatedDate = DateTime.Now
                        };
                        _contact.Entity.Insert(newContact);
                        await _contact.SaveAsync();
                    }


                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Details));
                
            }
            return RedirectToAction("Edit", "CompanyInfo");
        }
  
        public async Task<FileContentResult> GetImage(Guid id)
        {
            if (id == Guid.Empty || id == null)
            {
                return null;
            }

            var Img = await _trainingcompany.Entity.GetByIdAsync(id);
            if (Img != null)
            {
                byte[] image = Img.Picture;
                return File(image, "image/jpg");
            }
            else
            {
                return null;
            }

        }
    }
}
