using BrAcademy.Data;
using BrAcademy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            model.CourseCategories = db.CourseCategories.Where(m => m.Active == true).ToList();
            model.Events = db.Events.Include("Course").Where(m => m.StartDate >= DateTime.Now).OrderBy(m => m.StartDate).ToList();
            return View(model);
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
