using BrAcademy.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Models.Courses
{
    public class EditCourseVM
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string CourseName { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        
        [Required]
        [DisplayName("Category")]
        public int CourseCategoryID { get; set; }
        [Required]
        [Range(1, 500, ErrorMessage = "Please enter a valid range between 1 to 500")]
        public int Duration { get; set; }
        [DisplayName("Image")]
        public string CourseImageUrl { get; set; }

        [Range(1, 5, ErrorMessage = "Please enter a valid number from 1 to 5")]
        public float Review { get; set; }

        public int CountReviewers { get; set; }
        public string Code { get; set; }
    }
}
