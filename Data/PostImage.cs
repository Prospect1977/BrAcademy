using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class PostImage
    {
        public int Id { get; set; }
        public string IconUrl { get; set; }
        public string ImageUrl { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public bool Flgdelete { get; set; } = false;
    }
}
