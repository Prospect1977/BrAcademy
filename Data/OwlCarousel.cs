using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class OwlCarousel
    {
        public int Id { get; set; }
        public Boolean Active { get; set; }
        public int SortIndex { get; set; }
        public string CustomText { get; set; }
        public string CustomUrl { get; set; }
        public string CustomImageUrl { get; set; }
        public int? CourseId { get; set; }
        public int? CategoryId { get; set; }
    }
}
