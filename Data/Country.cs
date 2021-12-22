using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class Country
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string CountryNameEnglish { get; set; }
        public string CountryNameArabic { get; set; }
        public string FlagURL { get; set; }
        public Boolean  Active { get; set; }
        public string PhoneCode { get; set; }
    }

}
