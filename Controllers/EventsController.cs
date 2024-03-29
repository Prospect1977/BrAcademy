﻿using BrAcademy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Controllers
{

    //[Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private ApplicationDbContext db;
        public EventsController(ApplicationDbContext db)
        {
            this.db = db;
        }
       
        public ActionResult CourseEvents(int? id)
        {
            ViewData["CourseId"] = id;
            ViewData["CourseName"] = db.Courses.Find(id).CourseName;
            IEnumerable<Event> model = db.Events.Include("Country").Where(m => m.CourseID == id).OrderByDescending(m => m.StartDate);
            ViewData["CountryID"] = new SelectList(db.Countries, "Id", "CountryNameEnglish");
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? CourseId)
        {
            ViewData["CourseId"] = CourseId;
            ViewData["CourseName"] = db.Courses.Find(CourseId).CourseName;
            ViewData["Countries"] = new SelectList(db.Countries, "Id", "CountryNameEnglish");
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
       //[ValidateAntiForgeryToken]
        public ActionResult Create(Event model)
        {
            model.Active = true;
            db.Events.Add(model);
            db.SaveChanges();
            return RedirectToAction(nameof(CourseEvents), new { Id = model.CourseID });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int Id)
        {

            Event Event = db.Events.Find(Id);
            int CourseID = Event.CourseID;
            db.Events.Remove(Event);
            db.SaveChanges();
            //return RedirectToAction(nameof(CourseEvents),new { Id = CourseID });
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {
            Event Ev = db.Events.Find(Id);
            ViewData["CourseName"] = db.Courses.Find(Ev.CourseID).CourseName;
            ViewData["Countries"] = new SelectList(db.Countries, "Id", "CountryNameEnglish", Ev.CountryID);
            return View(Ev);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Event model)
        {
            model.Active = true;
            db.Update(model);
            db.SaveChanges();
            Event ev = db.Events.Find(model.Id);
            return RedirectToAction(nameof(CourseEvents), new { Id = ev.CourseID });
        }
       
    }
}
