using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class Visitor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WorksAt { get; set; }
        public DateTime RegisteredOn { get; set; }

        public int UserId { get; set; }
        public int MyProperty { get; set; }
        [ForeignKey("CountryID")]
        public Country Country { get; set; }
        public int CountryID { get; set; }
    }
}
