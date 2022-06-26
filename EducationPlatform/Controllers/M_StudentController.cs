using EducationPlatform.Auth;
using EducationPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationPlatform.Controllers
{
    [MentorLoginAuth]
    public class M_StudentController : Controller
    {
       
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult StudentAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentAdd(Student st)
        {
            var db = new EducationPlatformEntities();
            var students = new Student()
            {
                Id = st.Id,
                Name = st.Name,
                Address = st.Address,
                Email = st.Email,
                Phone = st.Phone,
                Photo = st.Photo,
                Education = st.Education,
                Institution = st.Institution,
                Password = st.Password,
                IsValid = "Yes",
                Gender = st.Gender,

            };
            db.Students.Add(students);
            db.SaveChanges();
            return RedirectToAction("StudentInformation");
        }
        public ActionResult StudentInformation()
        {
            var db = new EducationPlatformEntities();
            var students = db.Students.ToList();
            return View(students);
        }
        public ActionResult M_StudentDelete(int id)
        {
            var db = new EducationPlatformEntities();
            var students = (from s in db.Students where s.Id == id select s).SingleOrDefault();
            db.Students.Remove(students);
            db.SaveChanges();
            return RedirectToAction("StudentInformation");
        }
    }
}