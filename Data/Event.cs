using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class Event
    {
        public int Id { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }
        [Required]
        public int CourseID { get; set; }

        
        public String Description { get; set; }
        [Required]
        [Display(Name ="Start Date")]
        public DateTime StartDate { get; set; }
        public Boolean DisplayShortenedDate { get; set; }
         public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [ForeignKey("CountryID")]
        public Country Country { get; set; }
        [Required]
        public int CountryID { get; set; }
       

    }
}
