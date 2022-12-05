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

        [StringLength(5000)]
        public string Description { get; set; }
        [ForeignKey("CourseCategoryID")]
        public CourseCategory CourseCategory { get; set; }
        [Required]
        [DisplayName("Category")]
        public int CourseCategoryID { get; set; }
        public string Duration1 { get; set; }
        public string Duration2 { get; set; }
        [DisplayName("Image")]
        public string CourseImageUrl { get; set; }
        [DisplayName("Full Width Image")]
        public string FullWidthCourseImageUrl { get; set; }

        [DisplayName("Wide Image")]
        public string CourseWideImageUrl { get; set; }
        [Range(1, 5, ErrorMessage = "Please enter a valid number from 1 to 5")]
        public float Review { get; set; }

        public int CountReviewers { get; set; }

        public Boolean Active { get; set; }
        public int SortIndex { get; set; }
        [MaxLength(25)]
        public string Code { get; set; }
        public Boolean HomePage { get; set; }
        public Boolean FlgDelete { get; set; }
        public Boolean IsComingSoon { get; set; }
        [StringLength(3)]
        public string DescriptionDirection { get; set; }
    }
}
