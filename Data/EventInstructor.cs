using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class EventInstructor
    {
        public int Id { get; set; }

        
        [ForeignKey("EventID")]
        public Event Event { get; set; }

        [Required]
        public int EventID { get; set; }

       [ForeignKey("InstructorID")]
      public Instructor Instructor { get; set; }

       [Required]
       public int InstructorID { get; set; }
    }
}
