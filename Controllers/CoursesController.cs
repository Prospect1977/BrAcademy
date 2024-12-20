﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrAcademy.Data;
using System.IO;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using BrAcademy.Models.Courses;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;
using System.ComponentModel;
using System.Collections.Specialized;

namespace BrAcademy.Controllers
{

    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public CoursesController(ApplicationDbContext context, IHostingEnvironment env, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _env = env;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult ReviseVisitors()
        {
            IEnumerable<VisitorCourse> model = _context.VisitorCourses.Include(m => m.Visitor).Include(m => m.Visitor.Country).Include(m => m.Course).OrderByDescending(m => m.InterestedDateTime).ThenBy(m => m.Visitor.Country.CountryNameEnglish);
            return View(model);
        }
        // GET: Courses
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CoursesAdmin(int? id)
        {
            var applicationDbContext = _context.Courses.Where(m => m.CourseCategoryID == id).Include(c => c.CourseCategory).OrderBy(m => m.SortIndex);
            if (id == 0)
            {
                //
                applicationDbContext = _context.Courses.Include(c => c.CourseCategory).OrderBy(m => m.SortIndex);
            }
            else if (id == null)
            {
                id = _context.CourseCategories.OrderBy(m => m.SortIndex).Take(1).Single().Id;
            }
            ViewBag.Categories = new SelectList(_context.CourseCategories.OrderBy(m => m.SortIndex), "Id", "CategoryName", id);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courses/Details/5

        public async Task<IActionResult> Details(int? id)
        {

            var user = await userManager.GetUserAsync(HttpContext.User);

            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.CourseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int CategoryID = 0)
        {
            ViewData["CourseCategoryID"] = new SelectList(_context.CourseCategories, "Id", "CategoryName", CategoryID);
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                int lastsortIndex = 0;
                try
                {
                    lastsortIndex = _context.Courses.Max(m => m.SortIndex);
                }
                catch (Exception)
                {

                }

                course.SortIndex = lastsortIndex + 1;
                course.Active = true;

                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CoursesAdmin), new { id = course.CourseCategoryID });
            }
            //ViewData["CourseCategoryID"] = new SelectList(_context.CourseCategories, "Id", "CategoryName", course.CourseCategoryID);
            //return View(course);
            return Ok();
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CourseCategoryID"] = new SelectList(_context.CourseCategories, "Id", "CategoryName", course.CourseCategoryID);
            ViewData["ImagesOfSameCategory"] = _context.Courses.Where(m=>m.CourseCategoryID == course.CourseCategoryID&&m.CourseImageUrl!=null).OrderBy(m=>m.SortIndex).ToList();
            
            return View(course);
        }
        public List<Course> GetCoursesInCategory(int? id)
        {
            var CoursesList = _context.Courses.Where(m => m.CourseCategoryID == id && m.CourseImageUrl != null).OrderBy(m => m.SortIndex).ToList();
            return CoursesList;
        }
        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult OrderCoursesList(string CourseList)
        {
            var list = CourseList.Split(",");
            int sortIndex = 1;
            foreach (string item in list)
            {
                int CourseID = int.Parse(item);
                Course Course = _context.Courses.Find(CourseID);
                Course.SortIndex = sortIndex;
                sortIndex++;
            }
            _context.SaveChanges();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UploadCourseImage()
        {
            try
            {
                if (Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];
                    var path = Path.Combine(_env.WebRootPath, "images", "Courses");
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
        [HttpPost]
        public IActionResult UploadFullWidthCourseImage()
        {
            try
            {
                if (Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];
                    var path = Path.Combine(_env.WebRootPath, "images", "Courses", "FullWidth");
                    // path=Path.Combine(path,);
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
        [HttpPost]
        public IActionResult UploadWideCourseImage()
        {

            try
            {
                if (Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];
                    var path = Path.Combine(_env.WebRootPath, "images", "Courses", "Wide");
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

        public int SaveUserData(int VisitorId, string CountryCode, string Name, string PhoneNumber, string Email, string WorksAt, string Question, int CourseId)
        {
            Visitor Visitor;
            if (VisitorId > 0)
            {
                Visitor = _context.Visitors.Find(VisitorId);
            }
            else
            {
                if (_context.Visitors.Any(m => m.Email == Email))
                {
                    Visitor = _context.Visitors.FirstOrDefault(m => m.Email == Email);
                }
                else
                {
                    Visitor = new Visitor();
                    Visitor.RegisteredOn = DateTime.Now;
                    _context.Visitors.Add(Visitor);
                }

            }
            int CountryId = _context.Countries.SingleOrDefault(m => m.Alias == CountryCode).Id;
            Visitor.CountryID = CountryId;
            Visitor.Name = Name;
            Visitor.PhoneNumber = PhoneNumber;
            Visitor.WorksAt = WorksAt;
            if (!string.IsNullOrEmpty(Email))
            {
                Visitor.Email = Email;
            }
            else
            {
                Visitor.Email = null;
            }
            _context.SaveChanges();
            VisitorCourse VC = new VisitorCourse();
            VC.CourseID = CourseId;
            VC.VisitorID = Visitor.Id;
            VC.InterestedDateTime = DateTime.Now;
            VC.Question = Question;
            _context.VisitorCourses.Add(VC);
            _context.SaveChanges();
            return Visitor.Id;

        }

        [HttpGet]
        public string GetUserData(int VisitorId)
        {
            Visitor V = _context.Visitors.Find(VisitorId);
            return JsonSerializer.Serialize(V);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course, string ReturnToCoursePage)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (ReturnToCoursePage == "true")
                {
                    return RedirectToAction(nameof(CourseDetails), new { id = id });
                }
                return RedirectToAction(nameof(CoursesAdmin), new { id = course.CourseCategoryID });

                // return Ok();
            }
            ViewData["CourseCategoryID"] = new SelectList(_context.CourseCategories, "Id", "CategoryName", course.CourseCategoryID);

            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.CourseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            var CategoryID = course.CourseCategoryID;
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CoursesAdmin), new { id = CategoryID });
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CourseDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            CourseDetailsVM model = new CourseDetailsVM();
            model.Course = _context.Courses.Find(id);
            int CategoryID = model.Course.CourseCategoryID;
            model.CourseCategory = _context.CourseCategories.Find(CategoryID).CategoryName;
            DateTime today = DateTime.Now;
            model.Events = _context.Events.Where(m => m.CourseID == id && m.StartDate > today && m.Active == true).Include("Country").OrderBy(m => m.StartDate).ToList();
            model.RelatedCourses = _context.Courses.Where(m => m.CourseCategoryID == CategoryID && m.Id != id && m.Active == true).OrderBy(m => m.SortIndex).ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddBulk(int? id)
        {
            CourseCategory MyCategory;
            if (id > 0)
            {
                MyCategory = _context.CourseCategories.Find(id);
            }
            else
            {
                int firstIndex = _context.CourseCategories.Min(m => m.SortIndex);
                MyCategory = _context.CourseCategories.FirstOrDefault(m => m.SortIndex == firstIndex);
            }
            ViewData["CategoriesList"] = new SelectList(_context.CourseCategories.Where(m => m.Active).OrderBy(m => m.SortIndex), "Id", "CategoryName", id);
            return View(MyCategory);
        }
        public string CoursesByCategory(int Id)
        {
            IEnumerable<Course> Courses = _context.Courses.Where(m => m.CourseCategoryID == Id).OrderBy(m => m.SortIndex);
            return JsonSerializer.Serialize(Courses);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public int CoursesFromCSV(int CategoryId, string CoursesList)
        {
            int lastSortIndex = _context.Courses.Max(m => m.SortIndex);
            List<CsvCourse> Records = JsonSerializer.Deserialize<List<CsvCourse>>(CoursesList);
            int count = 0;
            foreach (CsvCourse c in Records)
            {
                Course MyCourse = new Course();
                if (_context.Courses.Any(m => m.Code == c.Code))
                {
                    continue;
                }
                count += 1;
                MyCourse.Code = c.Code;
                MyCourse.CourseName = c.Name;
                MyCourse.Active = false;
                MyCourse.CourseCategoryID = CategoryId;
                MyCourse.Duration1 = "من الأحد إلى الخميس";
                MyCourse.Duration2 = "اسبوع واحد";
                MyCourse.DescriptionDirection = "rtl";
                Random rnd = new Random();
                int rev = rnd.Next(3, 6); // creates a number between 4 and 5
                MyCourse.Review = rev;
                int revCount = rnd.Next(25, 65);
                MyCourse.CountReviewers = revCount;
                lastSortIndex += 1;
                MyCourse.SortIndex = lastSortIndex;
                _context.Courses.Add(MyCourse);
            }
            _context.SaveChanges();
            return count;
        }

        protected class CsvCourse
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public int EventsFromCSV(string EventsList)
        {

            List<CsvEvent> Records = JsonSerializer.Deserialize<List<CsvEvent>>(EventsList);


           // Adding Couses if not exist
            int lastCourseSortIndex = _context.Courses.Max(m => m.SortIndex);
            _context.Events.RemoveRange();

            foreach (CsvEvent e in Records)
            {
                if (e.Code != "#VALUE!")
                {
                    try
                    {
                       
                        if (_context.Courses.Any(m => m.Code == e.Code))
                        {
                            var MyCourse = _context.Courses.FirstOrDefault(m => m.Code == e.Code.Trim());
                            MyCourse.CourseName = e.CourseName.Trim();
                            MyCourse.CourseCategoryID=int.Parse(e.CategoryId);
                        }
                        else
                        {
                            var MyCourse = new Course();
                            MyCourse.Code = e.Code.Trim();
                            MyCourse.CourseName = e.CourseName.Trim();
                            MyCourse.Active = true;
                            MyCourse.CourseCategoryID =int.Parse(e.CategoryId);
                            MyCourse.Duration1 = "من الأحد إلى الخميس";
                            MyCourse.Duration2 = "اسبوع واحد";
                            MyCourse.DescriptionDirection = "rtl";
                            Random rnd = new Random();
                            int rev = rnd.Next(3, 6); // creates a number between 4 and 5
                            MyCourse.Review = rev;
                            int revCount = rnd.Next(25, 65);
                            MyCourse.CountReviewers = revCount;
                            lastCourseSortIndex += 1;
                            MyCourse.SortIndex = lastCourseSortIndex;
                            _context.Courses.Add(MyCourse);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

            }
            _context.SaveChanges();
                int count = 0;
            foreach (CsvEvent e in Records)
            {

                //var csvCode = e.Code.Trim();
                //          csvCode = csvCode.Split("/")[1] + "/" + csvCode.Split("/")[0];
                          if (!_context.Courses.Any(m => m.Code ==e.Code ))
                {
                    continue;
                }
               
                int CourseID = _context.Courses.FirstOrDefault(m => m.Code == e.Code).Id;
               
                if (!string.IsNullOrEmpty(e.Date_1))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_1);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_1).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {


                    }

                }
                if (!string.IsNullOrEmpty(e.Date_2))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_2);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_2).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {

                    }
                }
                if (!string.IsNullOrEmpty(e.Date_3))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_3);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_3).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {


                    }
                }
                if (!string.IsNullOrEmpty(e.Date_4))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_4);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_4).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {


                    }
                }
                if (!string.IsNullOrEmpty(e.Date_5))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_5);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_5).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {


                    }
                }
                if (!string.IsNullOrEmpty(e.Date_6))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_6);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_6).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {


                    }
                }
                if (!string.IsNullOrEmpty(e.Date_7))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_7);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_7).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {


                    }
                }
                if (!string.IsNullOrEmpty(e.Date_8))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_8);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_8).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {


                    }
                }
                if (!string.IsNullOrEmpty(e.Date_9))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_9);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_9).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {


                    }
                }
                if (!string.IsNullOrEmpty(e.Date_10))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_10);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_10).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {


                    }
                }
                if (!string.IsNullOrEmpty(e.Date_11))
                {
                    try
                    {

                        Event MyEvent = new Event();
                        MyEvent.CourseID = CourseID;
                        MyEvent.StartDate = Convert.ToDateTime(e.Date_11);
                        MyEvent.CountryID = _context.Countries.Single(m => m.CountryNameEnglish == e.City_11).Id;
                        _context.Events.Add(MyEvent);
                        count += 1;
                    }
                    catch (Exception)
                    {

                    }
                }


            }
            _context.SaveChanges();
            return count;
        }
        [HttpPost]
        public Boolean SwitchActive(int id)
        {
            Course Course = _context.Courses.Find(id);
            Course.Active = !Course.Active;
            _context.SaveChanges();
            return Course.Active;
        }
        public Boolean SwitchHome(int id)
        {
            Course Course = _context.Courses.Find(id);
            Course.HomePage = !Course.HomePage;
            _context.SaveChanges();
            return Course.HomePage;
        }
        protected class CsvEvent
        {
            public string Code { get; set; }
            public string CourseName { get; set; }
            public string Category { get; set; }
            public string CategoryId { get; set; }
            public string Dummy { get; set; }
            public string Date_1 { get; set; }
            public string Date_2 { get; set; }
            public string Date_3 { get; set; }
            public string Date_4 { get; set; }
            public string Date_5 { get; set; }
            public string Date_6 { get; set; }
            public string Date_7 { get; set; }
            public string Date_8 { get; set; }
            public string Date_9 { get; set; }
            public string Date_10 { get; set; }
            public string Date_11 { get; set; }
            public string Date_12 { get; set; }

            public string City_1 { get; set; }
            public string City_2 { get; set; }
            public string City_3 { get; set; }
            public string City_4 { get; set; }
            public string City_5 { get; set; }
            public string City_6 { get; set; }
            public string City_7 { get; set; }
            public string City_8 { get; set; }
            public string City_9 { get; set; }
            public string City_10 { get; set; }
            public string City_11 { get; set; }
            public string City_12 { get; set; }

        }
    }
}
