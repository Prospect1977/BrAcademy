using BrAcademy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Models.Courses
{
    public class CourseDetailsVM
    {
        public Course Course { get; set; }
        public string CourseCategory { get; set; }
        public List<Course> RelatedCourses { get; set; }
        public List<Event> Events { get; set; }
    }
}
