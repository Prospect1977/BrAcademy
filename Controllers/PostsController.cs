using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrAcademy.Data;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace BrAcademy.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment _env;
        public PostsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            db = context;
            _env = env;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Posts.Include(p => p.Course).Where(m => m.Flgdelete != true).OrderByDescending(m => m.DataDate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await db.Posts
                .Include(p => p.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            post.PostImages = db.PostImages.Where(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        public async Task<IActionResult> Gallery()
        {
            IEnumerable<Post> Posts = db.Posts.Where(m => m.Flgdelete != true).Include(m => m.Course).OrderByDescending(m => m.DataDate);
            foreach (var post in Posts)
            {
                post.PostImages = db.PostImages.Where(m => m.PostId == post.Id && m.Flgdelete != true);
            }
            // ViewBag.images= JsonSerializer.Serialize( db.PostImages.Where(m=> m.Flgdelete != true));
            return View(Posts);
        }
        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "CourseName");
            ViewData["AllCourses"] = JsonSerializer.Serialize(db.Courses.Where(m => m.FlgDelete != true).Select(m => new { ID = m.Id, label = m.CourseName }));
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,Description,DataDate,EndDate")] Post post, string ImagesUrls)
        {
            if (ModelState.IsValid)
            {
                db.Add(post);
                await db.SaveChangesAsync();

                ImagesUrls = ImagesUrls.Substring(0, ImagesUrls.Length - 1);
                foreach (string ImageUrl in ImagesUrls.Split(","))
                {
                    PostImage img = new PostImage();
                    img.ImageUrl = ImageUrl;
                    img.IconUrl = ImageUrl;
                    img.PostId = post.Id;
                    img.Flgdelete = false;
                    db.PostImages.Add(img);
                }
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CourseName"] = db.Posts.Include("Course").Single(m => m.Id == id).Course.CourseName;
            ViewData["ExistingImages"] = JsonSerializer.Serialize(db.PostImages.Where(m => m.PostId == id && m.Flgdelete != true));
            ViewData["AllCourses"] = JsonSerializer.Serialize(db.Courses.Where(m => m.FlgDelete != true).Select(m => new { ID = m.Id, label = m.CourseName }));

            return View(post);
        }
        public void DeleteImage(int id)
        {
            db.PostImages.Find(id).Flgdelete = true;
            db.SaveChanges();
        }
        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,Description,DataDate,EndDate")] Post post, string ImagesUrls)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(post);
                    try
                    {
                        ImagesUrls = ImagesUrls.Substring(0, ImagesUrls.Length - 1);
                        foreach (string ImageUrl in ImagesUrls.Split(","))
                        {
                            PostImage img = new PostImage();
                            img.ImageUrl = ImageUrl;
                            img.IconUrl = ImageUrl;
                            img.PostId = id;
                            img.Flgdelete = false;
                            db.PostImages.Add(img);
                        }
                    }
                    catch (Exception)
                    {


                    }


                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "CourseName", post.CourseId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await db.Posts
                .Include(p => p.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await db.Posts.FindAsync(id);
            post.Flgdelete = true;
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult UploadPostImage()
        {


            try
            {
                if (Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];
                    var path = Path.Combine(_env.WebRootPath, "images", "Posts");
                    return Ok(UploadImage.SavePostImage(file, path));
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
        private bool PostExists(int id)
        {
            return db.Posts.Any(e => e.Id == id);
        }
       
       

    }
}
