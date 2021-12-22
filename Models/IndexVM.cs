﻿using BrAcademy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Models
{
    public class IndexVM
    {
        public List<CourseCategory> CourseCategories { get; set; }
        public List<Course> HomeCourses { get; set; }
        public List<Event> Events { get; set; }
    }
}