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
            model.Events = db.Events.Include("Course").Include("Country").Where(m => m.Course.IsComingSoon && m.StartDate >= DateTime.Now && m.Course.Active).OrderBy(m => m.StartDate).Distinct().ToList();
            ViewData["UpComingCourses"] = JsonSerializer.Serialize(db.Courses.Where(m => m.IsComingSoon && m.Active).Select(m => new
            {
                m.CourseName,
                m.Id,
                m.CourseImageUrl,
                m.Review,
                m.CountReviewers,
                m.Duration1,
                m.Duration2,
                Event = (from e in db.Events where e.CourseID == m.Id && e.StartDate >= DateTime.Now select e).Select(m=>new { m.StartDate, m.Country.CountryNameEnglish,m.Country.FlagURL }).OrderBy(m => m.StartDate).FirstOrDefault()
            }));
            
            model.Carousels = db.Carousels.Where(m => m.Active == true).OrderBy(m => m.SortIndex);
            model.OwlCarousels = db.OwlCarousels.Where(m => m.Active == true).OrderBy(m => m.SortIndex);
            List<RecentPost> RecentPosts = new List<RecentPost>();
            RecentPost P0 = new RecentPost { Id = db.Posts.OrderByDescending(m=>m.DataDate).FirstOrDefault().Id, 
                Icon = db.PostImages.Where(m=>m.PostId==db.Posts.OrderByDescending(m=>m.DataDate).FirstOrDefault().Id).FirstOrDefault().ImageUrl };
            RecentPost P1 = new RecentPost { Id = db.Posts.OrderBy(s => s.DataDate).Skip(db.Posts.Count()-2).Take(1).FirstOrDefault().Id, 
                Icon = db.PostImages.Where(m=>m.PostId== db.Posts.OrderBy(s => s.DataDate).Skip(db.Posts.Count() - 2).Take(1).FirstOrDefault().Id).FirstOrDefault().ImageUrl };
 RecentPost P2 = new RecentPost { Id = db.Posts.OrderBy(s => s.DataDate).Skip(db.Posts.Count()-3).Take(1).FirstOrDefault().Id, 
                Icon = db.PostImages.Where(m=>m.PostId== db.Posts.OrderBy(s => s.DataDate).Skip(db.Posts.Count() - 3).Take(1).FirstOrDefault().Id).FirstOrDefault().ImageUrl
 };
 RecentPost P3 = new RecentPost { Id = db.Posts.OrderBy(s => s.DataDate).Skip(db.Posts.Count()-4).Take(1).FirstOrDefault().Id, 
                Icon = db.PostImages.Where(m=>m.PostId== db.Posts.OrderBy(s => s.DataDate).Skip(db.Posts.Count() - 4).Take(1).FirstOrDefault().Id).FirstOrDefault().ImageUrl
 };
 RecentPost P4 = new RecentPost { Id = db.Posts.OrderBy(s => s.DataDate).Skip(db.Posts.Count()-5).Take(1).FirstOrDefault().Id, 
                Icon = db.PostImages.Where(m=>m.PostId== db.Posts.OrderBy(s => s.DataDate).Skip(db.Posts.Count() - 5).Take(1).FirstOrDefault().Id).FirstOrDefault().ImageUrl
 };

            RecentPosts.Add(P0);
            RecentPosts.Add(P1);
            RecentPosts.Add(P2);
            RecentPosts.Add(P3);
            RecentPosts.Add(P4);
            ViewData["RecentPosts"] = RecentPosts.ToList();
            ViewData["Companies"] = db.Companies.OrderBy(m => m.SortIndex).ToList();
            ViewData["TrainingPlan"] = db.TrainingPlans.FirstOrDefault();
            return View(model);
        }
        public class RecentPost
        {
            public int Id { get; set; }
            public string? Icon { get; set; }
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
