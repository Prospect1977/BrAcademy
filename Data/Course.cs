using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string CourseName { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        [ForeignKey("CourseCategoryID")]
        public CourseCategory CourseCategory { get; set; }
        [Required]
        [DisplayName("Category")]
        public int CourseCategoryID { get; set; }
        [Required]
        
        public string Duration1 { get; set; }
        public string Duration2 { get; set; }
        [DisplayName("Image")]
        public string CourseImageUrl { get; set; }

        [Range(1,5,ErrorMessage="Please enter a valid number from 1 to 5")]
        public float Review { get; set; }

        public int CountReviewers { get; set; }

        public Boolean Active { get; set; }
        public int SortIndex { get; set; }
        public Boolean HomePage { get; set; }
        public Boolean FlgDelete { get; set; }
    }
}
