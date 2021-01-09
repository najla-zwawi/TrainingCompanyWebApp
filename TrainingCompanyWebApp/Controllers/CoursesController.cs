using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingCompanyWebApp.Models;
using TrainingCompanyWebApp.Models.Entities;
using TrainingCompanyWebApp.Models.Interfaces;
using TrainingCompanyWebApp.ViewModels;

namespace TrainingCompanyWebApp.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly IUnitOfWork<Courses> _courses;
        private readonly IUnitOfWork<Trainers> _trainers;
      
        public CoursesController(
             IUnitOfWork<Trainers> trainers,
              IUnitOfWork<Courses> courses)
          {
            _courses = courses;
            _trainers = trainers;
           }
        //public async Task<IActionResult> Index1()
        //{
        //    var model = await _courses.Entity.GetAll().Include( a=> a.Trainer)
        //                                      .OrderByDescending(s => s.CreatedDate)
        //                                       .AsNoTracking().ToListAsync();

        //    return View(model);
        //}
        public IActionResult Index(int pageNumber = 1, int pageSize = 4)
        {
            int offset = (pageSize * pageNumber) - pageSize;
            var courses = _courses.Entity.GetAll().Include(a => a.Trainer)
                                       .Skip(offset).Take(pageSize).AsNoTracking();
            if (courses == null)
            {
                return View("NotFound");
            }
            var model = new PagedResult<Courses>
            {
                Data = courses.ToList(),

                TotalItems = _courses.Entity.GetAll().Count(),
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

            var CoursesSearch = _courses.Entity.GetAll().Include(a => a.Trainer)
                                       .Where(e => e.Title.Contains(textSearch)
                                        || (e.Trainer != null && e.Trainer.Name.Contains(textSearch))
                                      );

            var courses = CoursesSearch.Skip(offset).Take(pageSize).AsNoTracking();

            var model = new PagedResult<Courses>
            {
                Data = courses.ToList(),
                TotalItems = CoursesSearch.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            ViewBag.Number = offset;
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var model = await _courses.Entity.GetAll().Include(a => a.Trainer)
                                                      .Where(a => a.Id == id)
                                                      .FirstOrDefaultAsync();
            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(GetCoursesViewModel());
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CoursesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    if (Guid.Empty == (Guid)model.TrainerId )
                    {
                        ViewBag.Message = "Please Select Trainer";
                        return View(GetCoursesViewModel());
                    }
                    if (model.File == null)
                    {
                        ViewBag.Message = "Please select image";
                        return View(GetCoursesViewModel());
                    }
                    var FCode = await _courses.Entity.GetAll().Where(a => a.Code == model.Code).FirstOrDefaultAsync();
                    if (FCode != null)
                    {
                        ViewBag.Message = "The Code of a Course is Used";
                        return View(GetCoursesViewModel());


                    }
                    if (model.File.Length > 0)
                    {
                        if (!CheckImgExtension(model.File))
                        {
                            ViewBag.Message = "The attached file is not an image file";
                            return View(GetCoursesViewModel());
                        }

                        Courses courses = new Courses
                        {
                            CreatedDate = DateTime.Now,
                            Title = model.Title,
                            Details = model.Details,
                            Code = model.Code,
                            CourseFee = model.CourseFee,
                            Seate = model.Seate,
                            HourNum = model.HourNum,
                            RegDate = DateTime.Now,
                            Picture = FileToByte(model.File),
                           

                        };

                        var trainer = await _trainers.Entity.GetByIdAsync(model.TrainerId);
                        if (trainer == null)
                        {
                            return View("NotFound");
                        }
                        courses.Trainer = trainer;


                        _courses.Entity.Insert(courses);
                        await _courses.SaveAsync();
                        //return RedirectToAction(nameof(Details), new { id = photos.Id });
                        return RedirectToAction("Details", new { id = courses.Id });
                    }


                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View(GetCoursesViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var courses = await _courses.Entity.GetByIdAsync(Id);
            if (courses == null)
            {
                return View("NotFound");
            }

            CoursesViewModel CoursesViewModel = new CoursesViewModel
            {
                Id = courses.Id,
                CreatedDate = courses.CreatedDate,
                Code = courses.Code,
                Title = courses.Title,
                Details = courses.Details,
                HourNum = courses.HourNum,
                RegDate = DateTime.Now,
                CourseFee = courses.CourseFee,
                Seate =courses.Seate ,
                DataFile = courses.Picture,
                Trainers = _trainers.Entity.GetAll().ToList(),
            };
            if (courses.Trainer != null)
            {
             
                CoursesViewModel.TrainerId = courses.Trainer.Id;
            }
            return View(CoursesViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CoursesViewModel model, string hidden1)
        {
            string test1 = hidden1;

            if (ModelState.IsValid)
            {
                try
                {
                    if (Guid.Empty == (Guid)model.TrainerId)
                    {
                        ViewBag.Message = "Please Select Trainer";
                        return View(GetCoursesViewModel());
                    }
                   
                    Courses courses = new Courses();
                    courses.Id = model.Id;
                    courses.Code = model.Code;
                    courses.Title = model.Title;
                    courses.HourNum = model.HourNum;
                    courses.Seate = model.Seate;
                    courses.CourseFee = model.CourseFee;
                    courses.Details = model.Details;
                    courses.CreatedDate = model.CreatedDate;
                    courses.ModifiedDate = DateTime.Now;

                    if (model.File != null)
                    {
                        if (!CheckImgExtension(model.File))
                        {
                            ViewBag.Message = "The attached file is not an image file";
                            return View(model);
                        }
                        courses.Picture = FileToByte(model.File);
                    }
                    else
                    {
                        courses.Picture = model.DataFile;
                    }
                    var trainer = await _trainers.Entity.GetByIdAsync(model.TrainerId);
                    if (trainer == null)
                    {
                        return View("NotFound");
                    }
                    courses.Trainer = trainer;
                    _courses.Entity.Update(courses);
                    await _courses.SaveAsync();


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
            var model = await _courses.Entity.GetByIdAsync(id);
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
            var course = await _courses.Entity.GetByIdAsync(id);
            if (course == null)
            {
                return View("NotFound");
            }
            try
            {
                _courses.Entity.Delete(id);
                await _courses.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }
        public async Task<FileContentResult> GetImage(Guid id)
        {
            if (id == Guid.Empty || id == null)
            {
                return null;
            }
            var Img = await _courses.Entity.GetByIdAsync(id);
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
        private CoursesViewModel GetCoursesViewModel()
        {
            CoursesViewModel CoursesViewModel = new CoursesViewModel()
            {
                RegDate = DateTime.Now,
                Trainers = _trainers.Entity.GetAll().AsNoTracking().ToList(),
            };
            return (CoursesViewModel);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<JsonResult> IsCodeInUse(int Code)
        {
            var course = await _courses.Entity.GetAll().FirstOrDefaultAsync(n => n.Code == Code);
            if (course == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"The Course Code:'{Code}' is already in use");
            }
        }
    }
}
