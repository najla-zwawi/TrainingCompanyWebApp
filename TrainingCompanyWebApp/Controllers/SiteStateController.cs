using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingCompanyWebApp.Models.Entities;
using TrainingCompanyWebApp.Models.Interfaces;

namespace TrainingCompanyWebApp.Controllers
{
    public class SiteStateController : BaseController
    {
        private readonly IUnitOfWork<SiteState> _sitestate;

        public SiteStateController(IUnitOfWork<SiteState> sitestate)
        {
            _sitestate = sitestate;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var SystemOff = await _sitestate.Entity.GetAll().FirstOrDefaultAsync();
            return View(SystemOff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SiteState model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    _sitestate.Entity.Update(model);
                    await _sitestate.SaveAsync();
                }
                catch
                {
                    throw;
                }
            }
            return View();
        }

     
    }

}
