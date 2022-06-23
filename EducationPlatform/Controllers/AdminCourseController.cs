using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationPlatform.Models;

namespace EducationPlatform.Controllers
{
    public class AdminCourseController : Controller
    {
        // GET: AdminCourse
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public ActionResult AddCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCourse(Cours obj)
        {
            var db=new EducationPlatformEntities();
            var course = new Cours()
            {
                Name= obj.Name,
                Details = obj.Details,
                Price=obj.Price,
                Duration=obj.Duration,
                Photo=obj.Photo,


            };
           // var course = db.Courses;
           
            db.Courses.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index","Admin");
        }

        
    }
}