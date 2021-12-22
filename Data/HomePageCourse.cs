using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class HomePageCourse
    {
        public int Id { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        [Required]
        public int CourseID { get; set; }

        public int SortIndex { get; set; }

    }
}
