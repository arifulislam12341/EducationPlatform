using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationPlatform.Models;
namespace EducationPlatform.Controllers
{
    public class AdminMentorController : Controller
    {
        // GET: AdminMentor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MentorList()
        {
            var db=new EducationPlatformEntities();
            var mentorList=db.Mentors.ToList();
            return View(mentorList);
        }
        [HttpGet]
        public ActionResult MentorAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MentorAdd(Mentor obj)
        {
            var db = new EducationPlatformEntities();
            var mentor = new Mentor()
                                    {
                                        Name = obj.Name,
                                        Address = obj.Address,
                                        Email = obj.Email,
                                        Phone = obj.Phone,
                                        Password = obj.Password,
                                        Gender = obj.Gender,
                                        Institution= obj.Institution,
                                        IsValid = "Yes",


                                    };
            db.Mentors.Add(mentor);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin"); //---action name, controller name
        }

        public ActionResult MentorDelete(int id)
        {
            var db = new EducationPlatformEntities();
            var mentor = (from i in db.Mentors
                           where i.Id == id
                           select i).FirstOrDefault();
            db.Mentors.Remove(mentor);
            db.SaveChanges();
            return RedirectToAction("MentorList");
        }

        //--------update function for Mentor
        [HttpGet]
        public ActionResult MentorUpdate(int id)
        {
            var db = new EducationPlatformEntities();
            var mentor = (from i in db.Mentors where i.Id == id select i).FirstOrDefault();
            return View(mentor);
        }

        [HttpPost]
        public ActionResult MentorUpdate(Mentor obj)
        {
            var db = new EducationPlatformEntities();
            var mentor = (from i in db.Mentors
                               where i.Id == obj.Id
                               select i).FirstOrDefault();
            //db.Entry(institution).CurrentValues.SetValues(obj);
            mentor.Name = obj.Name;
            mentor.Address = obj.Address;
            mentor.Email = obj.Email;
            mentor.Phone = obj.Phone;
            mentor.Password = obj.Password;
            mentor.Gender = obj.Gender;


            db.SaveChanges();
            return RedirectToAction("MentorList");
        }

        //------Activate deactivate function

        public ActionResult MentorActivate(int id)
        {
            var db = new EducationPlatformEntities();
            var mentor = (from i in db.Mentors
                          where i.Id == id
                          select i).FirstOrDefault();

            mentor.IsValid = "Yes";
            db.SaveChanges();
           // ViewBag.Valid=mentor.IsValid.ToString();

            return RedirectToAction("MentorList");
        }
        public ActionResult MentorDeactivate(int id)
        {
            var db = new EducationPlatformEntities();
            var mentor = (from i in db.Mentors
                          where i.Id == id
                          select i).FirstOrDefault();

            mentor.IsValid = "No";
            db.SaveChanges();
            return RedirectToAction("MentorList");
        }

        public ActionResult MentorSearch()
        {

            return View();
        }
        [HttpPost]
        public ActionResult MentorSearchResult()
        {

            var name = Request["searching"];
            var db = new EducationPlatformEntities();

            var searchResult = (from i in db.Mentors
                                where i.Name.Contains(name)

                                select i).ToList();
            // return RedirectToAction()
            return View(searchResult);
        }
    }
}