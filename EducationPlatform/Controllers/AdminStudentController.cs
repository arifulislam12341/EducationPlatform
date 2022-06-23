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

        public ActionResult StudentDelete(int id)
        {
            var db = new EducationPlatformEntities();
            var student = (from i in db.Students
                          where i.Id == id
                          select i).FirstOrDefault();
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("StudentList");
        }

        //--------update function for Student
        [HttpGet]
        public ActionResult StudentUpdate(int id)
        {
            var db = new EducationPlatformEntities();
            var student = (from i in db.Students where i.Id == id select i).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public ActionResult StudentUpdate(Student obj)
        {
            var db = new EducationPlatformEntities();
            var student = (from i in db.Students
                          where i.Id == obj.Id
                          select i).FirstOrDefault();
            //db.Entry(institution).CurrentValues.SetValues(obj);

            student.Name = obj.Name;
            student.Address = obj.Address;
            student.Email = obj.Email;
            student.Phone = obj.Phone;
            student.Password = obj.Password;
            student.Gender = obj.Gender;
            student.Institution = obj.Institution;
            student.Education = obj.Education;

            db.SaveChanges();
            return RedirectToAction("StudentList");
        }

        public ActionResult StudentActivate(int id)
        {
            var db = new EducationPlatformEntities();
            var student = (from i in db.Students
                               where i.Id == id
                               select i).FirstOrDefault();

            student.IsValid = "Yes";
            db.SaveChanges();


            return RedirectToAction("StudentList");
        }
        public ActionResult StudentDeactivate(int id)
        {
            var db = new EducationPlatformEntities();
            var student = (from i in db.Students
                           where i.Id == id
                           select i).FirstOrDefault();

            student.IsValid = "No";
            db.SaveChanges();


            return RedirectToAction("StudentList");
        }
    }
}