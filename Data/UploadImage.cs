using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
namespace BrAcademy.Data
{

    public static class UploadImage
    {
        public static string Save(IFormFile file, string _path)
        {
            string uniqueFileName = null;
            uniqueFileName = Guid.NewGuid().ToString().Substring(0, 5) + "_" + file.FileName;
            _path = Path.Combine(_path, uniqueFileName);

            using (var fs = new FileStream(_path, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return (uniqueFileName);
        }

        public static string SavePostImage(IFormFile file, string _path)
        {
            string uniqueFileName = null;
            uniqueFileName = Guid.NewGuid().ToString().Substring(0, 5) + "_" + file.FileName;
            string _imagePath = Path.Combine(_path, "Images", uniqueFileName); //the _path is Path.Combine(_env.WebRootPath, "images", "Posts")

            using (var fs = new FileStream(_imagePath, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            string _iconPath = Path.Combine(_path, "Icons", uniqueFileName);
            using (var fs = new FileStream(_iconPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            ResizeImage(_iconPath, 250);
            return (uniqueFileName);
        }

        public static void ResizeImage(string fileNameIn, int Width)
        {
            Bitmap mySourceBitmap = null/* TODO Change to default(_) if this is not a reference type */;
            Bitmap myTargetBitmap = null/* TODO Change to default(_) if this is not a reference type */;
            Graphics myGraphics = null/* TODO Change to default(_) if this is not a reference type */;

            try
            {
                mySourceBitmap = new Bitmap(fileNameIn);

                int newWidth = Width;
                int origWidth = mySourceBitmap.Width;
                int origHeight = mySourceBitmap.Height;
                var newHeight = ((double) origHeight / (double) origWidth) * newWidth;

                myTargetBitmap = new Bitmap(newWidth, (int)newHeight);

                myGraphics = Graphics.FromImage(myTargetBitmap);

                myGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                myGraphics.DrawImage(mySourceBitmap, new Rectangle(0, 0, newWidth, (int)newHeight));
                mySourceBitmap.Dispose();


                myTargetBitmap.Save(fileNameIn, ImageFormat.Jpeg);
                myGraphics.Dispose();
            }
            catch
            {

            }
        }
    }


}
