using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class VisitorCourseSpeech
    {
        public int Id { get; set; }
        public string SpeechText { get; set; }
        [ForeignKey("VisitorCourseID")]
        public VisitorCourse VisitorCourse { get; set; }
        public int VisitorCourseID { get; set; }
    }
}
