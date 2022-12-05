using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    
   
    public class Post
    {
//        ApplicationDbContext db;
//        public Post(ApplicationDbContext context)
//        {
//db=context;
//        }
        public int Id { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
       
        public DateTime? DataDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool Flgdelete { get; set; } = false;
        [ForeignKey("Id")]
        [NotMapped]
        public IEnumerable<PostImage> PostImages { get; set; }
        
    }
}
