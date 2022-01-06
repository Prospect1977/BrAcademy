using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrAcademy.Data;
using System.IO;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using BrAcademy.Models.Courses;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;

namespace BrAcademy.Controllers
{

    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public CoursesController(ApplicationDbContext context, IHostingEnvironment env, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _env = env;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult ReviseVisitors()
        {
            IEnumerable<VisitorCourse> model = _context.VisitorCourses.Include(m => m.Visitor).Include(m => m.Visitor.Country).Include(m => m.Course).OrderByDescending(m => m.InterestedDateTime);
            return View(model);
        }
        // GET: Courses
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CoursesAdmin()
        {
            var applicationDbContext = _context.Courses.Include(c => c.CourseCategory).OrderBy(m => m.SortIndex);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courses/Details/5
       
        public async Task<IActionResult> Details(int? id)
        {

            var user = await userManager.GetUserAsync(HttpContext.User);

            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.CourseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CourseCategoryID"] = new SelectList(_context.CourseCategories, "Id", "CategoryName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                int lastsortIndex = 0;
                try
                {
                    lastsortIndex = _context.Courses.Max(m => m.SortIndex);
                }
                catch (Exception)
                {

                }

                course.SortIndex = lastsortIndex + 1;
                course.Active = true;

                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CoursesAdmin));
            }
            ViewData["CourseCategoryID"] = new SelectList(_context.CourseCategories, "Id", "CategoryName", course.CourseCategoryID);
            //return View(course);
            return Ok();
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CourseCategoryID"] = new SelectList(_context.CourseCategories, "Id", "CategoryName", course.CourseCategoryID);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult OrderCoursesList(string CourseList)
        {
            var list = CourseList.Split(",");
            int sortIndex = 1;
            foreach (string item in list)
            {
                int CourseID = int.Parse(item);
                Course Course = _context.Courses.Find(CourseID);
                Course.SortIndex = sortIndex;
                sortIndex++;
            }
            _context.SaveChanges();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UploadCourseImage()
        {
            try
            {
                if (Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];
                    var path = Path.Combine(_env.WebRootPath, "images", "Courses");
                    return Ok(UploadImage.Save(file, path));
                }
                else
                {
                    return BadRequest("Error!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UploadWideCourseImage()
        {

            try
            {
                if (Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];
                    var path = Path.Combine(_env.WebRootPath, "images", "Courses", "Wide");
                    return Ok(UploadImage.Save(file, path));
                }
                else
                {
                    return BadRequest("Error!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public int SaveUserData(int VisitorId, string CountryCode, string Name, string PhoneNumber, string Email, string WorksAt, string Question, int CourseId)
        {
            Visitor Visitor;
            if (VisitorId > 0)
            {
                Visitor = _context.Visitors.Find(VisitorId);
            }
            else
            {
                if (_context.Visitors.Any(m => m.Email == Email))
                {
                    Visitor = _context.Visitors.FirstOrDefault(m => m.Email == Email);
                }
                else
                {
                    Visitor = new Visitor();
                    Visitor.RegisteredOn = DateTime.Now;
                    _context.Visitors.Add(Visitor);
                }

            }
            int CountryId = _context.Countries.SingleOrDefault(m => m.Alias == CountryCode).Id;
            Visitor.CountryID = CountryId;
            Visitor.Name = Name;
            Visitor.PhoneNumber = PhoneNumber;
            Visitor.WorksAt = WorksAt;
            if (!string.IsNullOrEmpty(Email))
            {
                Visitor.Email = Email;
            }
            else
            {
                Visitor.Email = null;
            }
            _context.SaveChanges();
            VisitorCourse VC = new VisitorCourse();
            VC.CourseID = CourseId;
            VC.VisitorID = Visitor.Id;
            VC.InterestedDateTime = DateTime.Now;
            VC.Question = Question;
            _context.VisitorCourses.Add(VC);
            _context.SaveChanges();
            return Visitor.Id;

        }

        [HttpGet]
        public string GetUserData(int VisitorId)
        {
            Visitor V = _context.Visitors.Find(VisitorId);
            return JsonSerializer.Serialize(V);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course, string ReturnToCoursePage)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //if (course.CourseImageUrl != null)
                    //{
                    //    string pic = Path.GetFileName(File.FileName);
                    //    string path = System.IO.Path.Combine(
                    //    Server.MapPath("~/images/profile"), pic);
                    //    // file is uploaded
                    //    File.SaveAs(path);

                    //    // save the image path path to the database or you can send image 
                    //    // directly to database
                    //    // in-case if you want to store byte[] ie. for DB
                    //    using (MemoryStream ms = new MemoryStream())
                    //    {
                    //        file.InputStream.CopyTo(ms);
                    //        byte[] array = ms.GetBuffer();
                    //    }

                    //}




                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (ReturnToCoursePage == "true")
                {
                    return RedirectToAction(nameof(CourseDetails), new { id = id });
                }
                return RedirectToAction(nameof(CoursesAdmin));

                // return Ok();
            }
            ViewData["CourseCategoryID"] = new SelectList(_context.CourseCategories, "Id", "CategoryName", course.CourseCategoryID);

            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.CourseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CoursesAdmin));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CourseDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            CourseDetailsVM model = new CourseDetailsVM();
            model.Course = _context.Courses.Find(id);
            int CategoryID = model.Course.CourseCategoryID;
            model.CourseCategory = _context.CourseCategories.Find(CategoryID).CategoryName;
            DateTime today = DateTime.Now;
            model.Events = _context.Events.Where(m => m.CourseID == id && m.StartDate > today).Include("Country").OrderBy(m => m.StartDate).ToList();
            model.RelatedCourses = _context.Courses.Where(m => m.CourseCategoryID == CategoryID && m.Id != id && m.Active == true).OrderBy(m => m.SortIndex).ToList();
            return View(model);
        }


        //[HttpPost]
        //public IActionResult CropCourseImage_NotWorking(string ImageName, int width, int height, int x, int y)

        //{
        // string ImageUrl = Request.QueryString("url");
        // string NewFileName;
        //if (ImageUrl.EndsWith("jpeg"))
        //    NewFileName = ImageUrl.Substring(0, ImageUrl.Length - 5) + "_Modified" + Path.GetExtension(Server.MapPath(ImageUrl));
        //else
        //    NewFileName = ImageUrl.Substring(0, ImageUrl.Length - 4) + "_Modified" + Path.GetExtension(Server.MapPath(ImageUrl));






        //var file = Path.Combine(_env.WebRootPath, "images", "Courses", ImageName);

        //using (System.Drawing.Image image = System.Drawing.Image.FromFile(file))
        //{


        //    image.Save("bar.jpg");
        //}


        //using (var inStream)
        //using (var outStream = new MemoryStream())
        //using (var image = Image.Load(inStream, out IImageFormat format))
        //{
        //    image.Mutate(
        //        i => i.Resize(width, height)
        //              .Crop(new Rectangle(x, y, cropWidth, cropHeight)));

        //    image.Save(outStream, format);
        //}




        //Console.WriteLine($"Loading {file}");
        //using (FileStream pngStream = new FileStream(file, FileMode.Open, FileAccess.Read))
        //using (var image = new Bitmap(pngStream))
        //{
        //    var resized = new Bitmap(width, height);
        //    using (var graphics = Graphics.FromImage(resized))
        //    {
        //        graphics.CompositingQuality = CompositingQuality.HighSpeed;
        //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        graphics.CompositingMode = CompositingMode.SourceCopy;
        //        graphics.DrawImage(image, 0, 0, width, height);
        //        resized.Save(file);
        //        Console.WriteLine($"Saving resized-{file} thumbnail");
        //        return Ok();
        //    }
        //}
        //    return Ok();

        //}





        //[HttpPost]
        //public IActionResult CropCourseImage(string filename, IFormFile blob)
        //{
        //    try
        //    {
        //        using (var image = Image.Load(blob.OpenReadStream()))
        //        {
        //            string uniqueFileName = Guid.NewGuid().ToString();
        //           // var path = Path.Combine(_env.WebRootPath, "images", "Courses", uniqueFileName.Substring(0, 5) + "_" + file.FileName);
        //            string systemFileExtenstion = filename.Substring(filename.LastIndexOf('.'));


        //            var newfileName180 = filename+"_" +uniqueFileName.Substring(0, 5);
        //            var filepath160 = Path.Combine(_env.WebRootPath, "images", "Courses", newfileName180);
        //            image.Mutate(x => x.Resize(600, 600));
        //            image.Save(filepath160);

        //            //var newfileName200 = GenerateFileName("Photo_200_200_", systemFileExtenstion);
        //            //var filepath200 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName200}";
        //            //image.Mutate(x => x.Resize(200, 200));
        //            //image.Save(filepath200);

        //            //var newfileName32 = GenerateFileName("Photo_32_32_", systemFileExtenstion);
        //            //var filepath32 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName32}";
        //            //image.Mutate(x => x.Resize(32, 32));
        //            //image.Save(filepath32);

        //        }

        //        return Json(new { Message = "OK" });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { Message = "ERROR" });
        //    }
        //}
        //public string GenerateFileName(string fileTypeName, string fileextenstion)
        //{
        //    if (fileTypeName == null) throw new ArgumentNullException(nameof(fileTypeName));
        //    if (fileextenstion == null) throw new ArgumentNullException(nameof(fileextenstion));
        //    return $"{fileTypeName}_{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{fileextenstion}";
        //}
        //[HttpPost]
        //public IActionResult SaveCroppedImage()
        //{
        //    string base64 = Request.Form["imgCropped"];
        //    byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
        //    var newfileName = Guid.NewGuid().ToString()+".jpg";
        //    string filePath = Path.Combine(_env.WebRootPath, "images", "Courses", newfileName);
        //    using (FileStream stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        stream.Write(bytes, 0, bytes.Length);
        //        stream.Flush();
        //    }
        //    return Ok();
        //}
    }
}
