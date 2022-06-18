using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationPlatform.Models;

namespace EducationPlatform.Controllers
{
    public class AdminInstitutionController : Controller
    {
        // GET: AdminInstitution
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InstitutionList()
        {
            var db = new EducationPlatformEntities();
            var InstitutionList = db.Institutions.ToList();
            return View(InstitutionList);
        }

        [HttpGet]
        public ActionResult InstitutionUpdate(int id)
        {
            var db = new EducationPlatformEntities();
            var institution = (from i in db.Institutions where i.Id == id select i).FirstOrDefault();
            return View(institution);
        }

        [HttpPost]
        public ActionResult InstitutionUpdate(Institution obj)
        {
            var db=new EducationPlatformEntities();
            var institution=(from i in db.Institutions
                             where i.Id == obj.Id
                             select i).FirstOrDefault();
            //db.Entry(institution).CurrentValues.SetValues(obj);
            institution.Name = obj.Name;
            institution.Address = obj.Address;
            institution.Email = obj.Email;
            institution.Phone = obj.Phone;
            institution.Password = obj.Password;
            institution.WebsiteLink = obj.WebsiteLink;


            db.SaveChanges();
            return RedirectToAction("InstitutionList");
        }

        [HttpGet]
        public ActionResult InstitutionAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InstitutionAdd(Institution obj)
        {
            var db= new EducationPlatformEntities();
            var instituition=new Institution()
            {
                Name = obj.Name,
                Address = obj.Address,
                Email = obj.Email,
                Phone = obj.Phone,
                Password = obj.Password,
                WebsiteLink = obj.WebsiteLink,
                IsValid = "Yes",
                

            };
            db.Institutions.Add(instituition);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin"); //---action name, controller name
        }
    }
}