using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationPlatform.Auth;
using EducationPlatform.Models;

namespace EducationPlatform.Controllers
{
    [AdminLogged]
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

        public ActionResult SingleStudentList(int id)
        {
            var db = new EducationPlatformEntities();
            var student = (from i in db.Students where i.Id == id select i).FirstOrDefault();
            return View(student);
        }

        

        public ActionResult ValidateStudentCourse(int id)
        {
            var transactionId = id;
            var db = new EducationPlatformEntities();
            var studentId= (from i in db.Transactions
                             where i.Id == transactionId
                             select i.StudentId).FirstOrDefault() ;
            var courseId= (from i in db.Transactions
                           where i.Id == transactionId
                           select i.CourseId).FirstOrDefault();

            var existStudent = (from i in db.ValidStudents
                                where (i.StudentId == studentId && i.CourseId==courseId)
                                select i).FirstOrDefault();

            //return View(existStudent);
            if (existStudent == null)
            {
                var validStudent = new ValidStudent()
                {
                   StudentId = studentId,
                    CourseId = courseId,

                };
                db.ValidStudents.Add(validStudent);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            TempData["msg"] = "Student Already Validate";
            return RedirectToAction("TransactionHistory", "AdminTransaction");
            
            

        }

        public ActionResult StudentSearch()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult StudentSearchResult()
        {
            
            var search = Request["searching"];
            var db= new EducationPlatformEntities();
            
            var searchResult = (from i in db.Students
                                where i.Name.Contains(search) || i.Email.Contains(search)
            
                                select i).ToList();
           // return RedirectToAction()
            return View(searchResult);
        }

        public ActionResult StudentPasswordChangeRequest()
        {
            var db=new EducationPlatformEntities();

            var RequesList = db.PasswordChanges.ToList();
            return View(RequesList);
        }

        public ActionResult StudentCertificate()
        {
            var db = new EducationPlatformEntities();

            var certificateRequest = db.Certificates.ToList();
            return View(certificateRequest);
        }

        public ActionResult ApproveCertificate(int id)
        {
            var db = new EducationPlatformEntities();

            var certificateRequest = (from i in db.Certificates
                                      where i.Id == id
                                      select i).FirstOrDefault();

            certificateRequest.Status = "Approved";
            db.SaveChanges();

            return RedirectToAction("StudentCertificate");

        }
    }
}