using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class VisitorCourse
    {
        public int Id { get; set; }
        [ForeignKey("VisitorID")]
        public Visitor Visitor { get; set; }
        public int VisitorID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }
        public int CourseID { get; set; }

        public DateTime InterestedDateTime { get; set; }
        public string Question { get; set; }
    }
}
