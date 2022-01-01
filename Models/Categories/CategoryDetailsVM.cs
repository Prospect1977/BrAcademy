using BrAcademy.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Models.Categories
{
    public class CategoryDetailsVM
    {
        public CourseCategory CourseCategory { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<CourseCategory> OtherCategories { get; set; }
    }
}
