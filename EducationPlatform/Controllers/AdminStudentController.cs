using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationPlatform.Models;

namespace EducationPlatform.Controllers
{
    public class AdminStudentController : Controller
    {
        // GET: AdminStudent
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentList()
        {
            var db = new EducationPlatformEntities();
            var studentList = db.Students.ToList();
            return View(studentList);
        }

        [HttpGet]
        public ActionResult StudentAdd()
        {
            return View();
        }

        public ActionResult StudentAdd(Student obj)
        {
            var db = new EducationPlatformEntities();
            var student = new Student()
            {
                Name = obj.Name,
                Address = obj.Address,
                Email = obj.Email,
                Phone = obj.Phone,
                Password = obj.Password,
                Gender = obj.Gender,
                Institution = obj.Institution,
                IsValid = "Yes",
                Education=obj.Education,


            };
            db.Students.Add(student);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin"); //---action name, controller name
        }
    }
}