using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public string Description { get; set; }
    }
}
