using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{

    public static class UploadImage
    {
        public static string Save(IFormFile file, string _path)
        {
            string uniqueFileName = null;
            uniqueFileName = Guid.NewGuid().ToString().Substring(0,5)+"_"+file.FileName;
            _path = Path.Combine(_path,uniqueFileName);

            using (var fs = new FileStream(_path, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return (uniqueFileName);
        }
    }


}
