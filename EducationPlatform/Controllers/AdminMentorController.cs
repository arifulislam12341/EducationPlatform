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
    }
}