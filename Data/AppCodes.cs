using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public static class AppCodes
    {
        //private readonly static ApplicationDbContext db;

        public static string GetCourseWideImage(ApplicationDbContext db, int CourseID)
        {
            return db.Courses.Find(CourseID).CourseWideImageUrl;
        }
        public static string GetCategoryWideImage(ApplicationDbContext db, int CategoryId)
        {
            return db.CourseCategories.Find(CategoryId).CategoryWideImageUrl;
        }
 public static string CourseNameById(ApplicationDbContext db, int CourseID)
        {
            return db.Courses.Find(CourseID).CourseName;
        }
        public static string CategoryNameById(ApplicationDbContext db, int CategoryId)
        {
            return db.CourseCategories.Find(CategoryId).CategoryName;
        }
    }
}
