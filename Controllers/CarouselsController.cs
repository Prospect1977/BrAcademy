using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrAcademy.Data;
using BrAcademy.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace BrAcademy.Controllers
{
    public class CarouselsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment _env;
        public CarouselsController(ApplicationDbContext db, IHostingEnvironment env)
        {
            this.db = db;
            _env = env;
        }

        // GET: Carousels
        public async Task<IActionResult> Index()
        {
            IEnumerable<Carousel> model = db.Carousels.OrderBy(m => m.SortIndex);
            return View(model);
        }

        // GET: Carousels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselVM = await db.CarouselVM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carouselVM == null)
            {
                return NotFound();
            }

            return View(carouselVM);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            //IEnumerable<Course> CoursesList= db.Courses.Where(m => m.Active == true).OrderBy(m => m.SortIndex);

            ViewData["CoursesList"] = new SelectList(db.Courses.Where(m => m.Active == true).OrderBy(m => m.SortIndex), "Id", "CourseName");
            ViewData["CategoriesList"] = new SelectList(db.CourseCategories.Where(m => m.Active == true).OrderBy(m => m.SortIndex), "Id", "CategoryName");

            return View();
        }

        // POST: Carousels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Carousel carousel)
        {
            //if (ModelState.IsValid)
            //{
            int LastSortIndex = 0;
            try
            {
                LastSortIndex = db.Carousels.Max(m => m.SortIndex);
            }
            catch (Exception)
            {


            }
            carousel.SortIndex = LastSortIndex + 1;
            db.Carousels.Add(carousel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(carousel);
        }

        // GET: Carousels/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["CoursesList"] = new SelectList(db.Courses.Where(m => m.Active == true).OrderBy(m => m.SortIndex), "Id", "CourseName");
            ViewData["CategoriesList"] = new SelectList(db.CourseCategories.Where(m => m.Active == true).OrderBy(m => m.SortIndex), "Id", "CategoryName");

            Carousel model = db.Carousels.Find(id);
            int courseid = 0;
            if (model.CourseId != null)
            {
                courseid = (int)model.CourseId;
                ViewData["CourseImage"] = AppCodes.GetCourseWideImage(db, courseid);
            }
            else
            {
                ViewData["CourseImage"] = "";
            }

            int categoryid = 0;
            if (model.CategoryId != null)
            {
                categoryid = (int)model.CategoryId;
                ViewData["CategoryImage"] = AppCodes.GetCategoryWideImage(db, categoryid);
            }
            else
            {
                ViewData["CategoryImage"] = "";
            }

            return View(model);

        }

        // POST: Carousels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Carousel carousel)
        {
            if (id != carousel.Id)
            {
                return NotFound();
            }


            db.Update(carousel);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Carousels/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carousel = await db.Carousels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carousel == null)
            {
                return NotFound();
            }

            return View(carousel);
        }

        // POST: Carousels/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carousel = await db.Carousels.FindAsync(id);
            db.Carousels.Remove(carousel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarouselExists(int id)
        {
            return db.Carousels.Any(e => e.Id == id);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UploadCarouselImage()
        {
            try
            {
                if (Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];
                    var path = Path.Combine(_env.WebRootPath, "images", "Carousels");
                    return Ok(UploadImage.Save(file, path));
                }
                else
                {
                    return BadRequest("Error!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        public void OrderCarouselsList(string CarouselsList)
        {
            var list = CarouselsList.Split(",");
            int sortIndex = 1;
            foreach (string item in list)
            {
                int CarouselId = int.Parse(item);
                Carousel carousel = db.Carousels.Find(CarouselId);
                carousel.SortIndex = sortIndex;
                sortIndex++;
            }
            db.SaveChanges();

        }
       
        [HttpGet]
        public string CourseImage(int id)
        {
            return AppCodes.GetCourseWideImage(db, id);
        }
        [HttpGet]
        public string CategoryImage(int id)
        {
            return AppCodes.GetCategoryWideImage(db, id);
        }

    }
}
