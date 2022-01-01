using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrAcademy.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using BrAcademy.Models.Categories;

namespace BrAcademy.Controllers
{
    public class CourseCategoriesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment _env;
        public CourseCategoriesController(ApplicationDbContext db, IHostingEnvironment env)
        {
            this.db = db;
            _env = env;
        }
        [HttpPost]
        public IActionResult UploadCategoryImage()
        {
            try
            {
                if (Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];
                    var path = Path.Combine(_env.WebRootPath, "images", "Categories");
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
 [HttpPost]
        public IActionResult UploadWideCategoryImage()
        {
            try
            {
                if (Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];
                    var path = Path.Combine(_env.WebRootPath, "images", "Categories","Wide");
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
        // GET: CourseCategories
        public async Task<IActionResult> Index()
        {
            return View(await db.CourseCategories.OrderBy(m=>m.SortIndex).ToListAsync());
        }

        // GET: CourseCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await db.CourseCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseCategory == null)
            {
                return NotFound();
            }

            return View(courseCategory);
        }

        // GET: CourseCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                int LastSortIndex = 0;
                try
                {
                    LastSortIndex = db.CourseCategories.Max(m => m.SortIndex) + 1;
                }
                catch (Exception ex)
                {
                    LastSortIndex = 1;
                }
                courseCategory.SortIndex = LastSortIndex;
                db.Add(courseCategory);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseCategory);
        }

        // GET: CourseCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await db.CourseCategories.FindAsync(id);
            if (courseCategory == null)
            {
                return NotFound();
            }
            return View(courseCategory);
        }
        public async Task<IActionResult> CategoryDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await db.CourseCategories.FindAsync(id);
            if (courseCategory == null)
            {
                return NotFound();
            }
            CategoryDetailsVM model = new CategoryDetailsVM();
            model.CourseCategory = courseCategory;
            model.Courses = db.Courses.Where(m => m.CourseCategoryID == id && m.Active).OrderBy(m => m.SortIndex);
            model.OtherCategories = db.CourseCategories.Where(m => m.Active == true).OrderBy(m => m.SortIndex);
            return View(model);
        }
        // POST: CourseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseCategory courseCategory)
        {
            if (id != courseCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(courseCategory);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseCategoryExists(courseCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseCategory);
        }

        // GET: CourseCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await db.CourseCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseCategory == null)
            {
                return NotFound();
            }

            return View(courseCategory);
        }

        // POST: CourseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseCategory = await db.CourseCategories.FindAsync(id);
            db.CourseCategories.Remove(courseCategory);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseCategoryExists(int id)
        {
            return db.CourseCategories.Any(e => e.Id == id);
        }
        [HttpPost]
        public IActionResult OrderCategoriesList(string CategoryList)
        {
            var list = CategoryList.Split(",");
            int sortIndex = 1;
            foreach (string item in list)
            {
                int CategoryID = int.Parse(item);
                CourseCategory Catg = db.CourseCategories.Find(CategoryID);
                Catg.SortIndex = sortIndex;
                sortIndex++;
            }
            db.SaveChanges();
            return Ok();
        }
    }
}
