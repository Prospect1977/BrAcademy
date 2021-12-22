using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class InstructorSkill
    {
        public int Id { get; set; }

        [ForeignKey("InstructorID")]
        
        public Instructor Instructor { get; set; }
        [Required]
        public int InstructorID { get; set; }

       
        [ForeignKey("CourseID")]
        public Course Course { get; set; }
        [Required]
        public int CourseID { get; set; }
    }
}
