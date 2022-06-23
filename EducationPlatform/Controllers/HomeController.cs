using EducationPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationPlatform.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var db = new EducationPlatformEntities();
            var courseList = db.Courses.ToList();
            return View(courseList);
     
        }
    }
}