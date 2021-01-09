using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingCompanyWebApp.Models.Entities;
using TrainingCompanyWebApp.Models.Interfaces;

namespace TrainingCompanyWebApp.ViewComponents
{
    public class FooterInfoViewComponent : ViewComponent
    {
        private readonly IUnitOfWork<TrainingCompany> _trainingcompany;

        public FooterInfoViewComponent(IUnitOfWork<TrainingCompany> trainingcompany)
        {
            _trainingcompany = trainingcompany;
        }

        public IViewComponentResult Invoke()
        {
            var Company = _trainingcompany.Entity.GetAll().FirstOrDefault();
            if (Company == null)
            {
                return View(new TrainingCompany());
            }
            return View(Company);

        }
    }
}
