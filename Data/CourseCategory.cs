using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class CourseCategory
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string CategoryImageUrl { get; set; }
        public string CategoryWideImageUrl { get; set; }

        public int SortIndex { get; set; }
        public Boolean Active { get; set; }
    }
}
