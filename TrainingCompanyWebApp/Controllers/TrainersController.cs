using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingCompanyWebApp.Models;
using TrainingCompanyWebApp.Models.Entities;
using TrainingCompanyWebApp.Models.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TrainingCompanyWebApp.ViewModels;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;

namespace TrainingCompanyWebApp.Controllers
{
    public class TrainersController : BaseController
    {
        private readonly IUnitOfWork<Trainers> _trainers;
       
        public TrainersController(IUnitOfWork<Trainers> trainers )
        {
            _trainers = trainers;      

        }
        //public async Task<IActionResult> Index1()
        //{
        //    var model = await _trainers.Entity.GetAll().OrderByDescending(s => s.RegDate).AsNoTracking().ToListAsync();
        //    return View(model);
        //}
        public IActionResult Index(int pageNumber = 1, int pageSize = 4)
        {
            int offset = (pageSize * pageNumber) - pageSize;
            var trainers = _trainers.Entity.GetAll()
                                       .Skip(offset).Take(pageSize).AsNoTracking();
            if (trainers == null)
            {
                return View("NotFound");
            }
            var model = new PagedResult<Trainers>
            {
                Data = trainers.ToList(),
                TotalItems = _trainers.Entity.GetAll().Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            ViewBag.Num = offset;
            return View(model);
        }
        public IActionResult Search(string textSearch, int pageNumber = 1, int pageSize = 4)
        {
            if (textSearch != null && TempData["textSearch"] == null)
            {
                TempData["textSearch"] = textSearch;
            }
            else if (textSearch == null && TempData["textSearch"] != null)
            {
                textSearch = (string)TempData["textSearch"];
                TempData["textSearch"] = textSearch;
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.textSearch = textSearch;
            int offset = (pageSize * pageNumber) - pageSize;

            var trainersSearch = _trainers.Entity.GetAll()
                                       .Where(e => e.Name.Contains(textSearch)                                      
                                    );

            var trainers = trainersSearch.Skip(offset).Take(pageSize).AsNoTracking();

            var model = new PagedResult<Trainers>
            {
                Data = trainers.ToList(),
                TotalItems = trainersSearch.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            ViewBag.Number = offset;
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
           
           return View(GetTrainersViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TrainersViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File == null)
                    {
                        ViewBag.Message = "Please select image";
                        return View(GetTrainersViewModel());
                    }
                    var FName = await _trainers.Entity.GetAll().Where(a => a.Name.ToUpper() == model.Name.ToUpper()).FirstOrDefaultAsync();
                    if ( FName != null )
                    {
                        ViewBag.Message = "The Name Of a Trainers is Registerd";
                        return View(GetTrainersViewModel());
                                              
                    }
                    if (model.File.Length > 0)
                    {
                        if (!CheckImgExtension(model.File))
                        {
                            ViewBag.Message = "The attached file is not an image file";
                            return View(GetTrainersViewModel());
                        }

                        Trainers trainer = new Trainers
                        {
                            RegDate = model.RegDate,
                            CreatedDate = DateTime.Now,
                            Name = model.Name,
                            Specialization = model.Specialization,
                            DataFile = FileToByte(model.File),
                        };
                        _trainers.Entity.Insert(trainer);
                        await _trainers.SaveAsync();
                      
                        return RedirectToAction("Details", new { id = trainer.Id });
                    }
                


                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View(GetTrainersViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
           

            var trainers = await _trainers.Entity.GetByIdAsync(Id);
            if (trainers == null)
            {
                return View("NotFound");
            }

            TrainersViewModel TrainersViewModel = new TrainersViewModel
            {
                Id = trainers.Id,
                CreatedDate =trainers.CreatedDate,        
                Specialization = trainers.Specialization,
                Name = trainers.Name,
                Facebook = trainers.Facebook,
                Email=trainers.Email ,
                RegDate=trainers.RegDate,
                DataFile = trainers.DataFile,              
            };
            return View(TrainersViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TrainersViewModel model, string hidden1)
        {
            string test1 = hidden1;
          

            if (ModelState.IsValid)
            {
                try
                {                                  
                        Trainers trainers = new Trainers();
                        trainers.Id = model.Id;
                        trainers.CreatedDate = model.CreatedDate;
                        trainers.Name = model.Name;
                        trainers.Email= model.Email;
                        trainers.Facebook= model.Facebook;
                        trainers.Specialization = model.Specialization;
                        trainers.RegDate = model.RegDate;
                        trainers.ModifiedDate = DateTime.Now;
                    if (model.File != null)
                        {
                            if (!CheckImgExtension(model.File))
                            {
                                ViewBag.Message = "The attached file is not an image file";
                                return View(model);
                            }
                            trainers.DataFile = FileToByte(model.File);
                        }
                        else
                        {
                            trainers.DataFile = model.DataFile; //يأخذ الصورة القديمة التي تم تخزينها في hidden
                        }
                        
                        _trainers.Entity.Update(trainers);
                        await _trainers.SaveAsync();                                        
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
           
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            var model = await _trainers.Entity.GetByIdAsync(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var model = await _trainers.Entity.GetByIdAsync(id);
            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            var trainer = await _trainers.Entity.GetByIdAsync(id);
            if (trainer == null)
            {
                return View("NotFound");
            }
            try
            {
                _trainers.Entity.Delete(id);
                await _trainers.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }
        private TrainersViewModel GetTrainersViewModel()
        {
            TrainersViewModel TrainersViewModel = new TrainersViewModel()
            {
                RegDate = DateTime.Now,
                Trainers = _trainers.Entity.GetAll().AsNoTracking().ToList(),
            };
            return (TrainersViewModel);
        }
        public async Task<FileContentResult> GetImage(Guid id)
        {
            if (id == Guid.Empty || id == null)
            {
                return null;
            }
            var Img = await _trainers.Entity.GetByIdAsync(id);
            if (Img != null)
            {
                byte[] image = Img.DataFile;
                return File(image, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<JsonResult> IsNameInUse(string NAME)
        {
            var trainer = await _trainers.Entity.GetAll().FirstOrDefaultAsync(n => n.Name == NAME);
            if (trainer == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"The Trainer Name:'{NAME}' is already in use");
            }
        }
    }
}
