using BrAcademy.Data;
using BrAcademy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrAcademy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult about_us()
        {
            return View();
        }
        public IActionResult Index()
        {
            IndexVM model = new IndexVM();
            model.HomeCourses = db.Courses.Where(m => m.HomePage == true && m.Active == true).OrderBy(m => m.SortIndex).ToList();
            model.CourseCategories = db.CourseCategories.Where(m => m.Active == true).OrderBy(m => m.SortIndex).ToList();
            model.Events = db.Events.Include("Course").Include("Country").Where(m => m.Course.IsComingSoon && m.StartDate >= DateTime.Now && m.Course.Active).OrderBy(m => m.StartDate).Take(1).ToList();
            model.Carousels = db.Carousels.Where(m => m.Active == true).OrderBy(m => m.SortIndex);
            return View(model);
        }
        public string CourseCategoriesMenu()
        {
            IEnumerable<CourseCategory> CourseCategories = db.CourseCategories.Where(m => m.Active == true).OrderBy(m => m.SortIndex);
            return JsonSerializer.Serialize(CourseCategories);
        }
        public string CoursesAndCategories()
        {
            IEnumerable<CourseCategory> CourseCategories = db.CourseCategories.Where(m => m.Active == true);
            IEnumerable<Course> Courses = db.Courses.Where(m => m.Active == true);
            List<SearchItem> SearchItems = new List<SearchItem>();

            foreach (var item in CourseCategories)
            {
                SearchItem s = new SearchItem();
                s.type = "Category";
                s.ID = item.Id;
                s.label = item.CategoryName;
                SearchItems.Add(s);
            }
            foreach (var item in Courses)
            {
                SearchItem s = new SearchItem();
                s.type = "Course";
                s.ID = item.Id;
                s.label = item.CourseName;
                SearchItems.Add(s);
            }
            return JsonSerializer.Serialize(SearchItems);
        }

        public IActionResult Privacy()

        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
