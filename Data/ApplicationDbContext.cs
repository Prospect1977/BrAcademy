using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BrAcademy.Models;

namespace BrAcademy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       public DbSet<Country> Countries { get; set; }
       public DbSet<Course> Courses { get; set; }
       public DbSet<Event> Events { get; set; }
       public DbSet<EventInstructor> EventInstructors { get; set; }
       public DbSet<Instructor> Instructors { get; set; }
       public DbSet<InstructorSkill> InstructorSkills { get; set; }
       public DbSet<CourseCategory> CourseCategories { get; set; }
       public DbSet<Visitor> Visitors { get; set; }
       public DbSet<VisitorCourse> VisitorCourses { get; set; }
       public DbSet<VisitorCourseSpeech> VisitorCourseSpeeches { get; set; }
       public DbSet<Carousel> Carousels { get; set; }
       public DbSet<BrAcademy.Models.CarouselVM> CarouselVM { get; set; }

    }
}
