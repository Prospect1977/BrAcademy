using BrAcademy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Models
{
    public class OwlCarouselVM
    {
        public int Id { get; set; }
        public Boolean Active { get; set; }
        public int SortIndex { get; set; }
        public string CustomText { get; set; }
        public string CustomUrl { get; set; }
        public string CustomImageUrl { get; set; }
        public string CategoryImageUrl { get; set; }
        public string CourseImageUrl { get; set; }
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Course> CoursesList { get; set; }
        public IEnumerable<CourseCategory> CategoriesList { get; set; }
    }
}
