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

        public ActionResult InstitutionDelete(int id)
        {
            var db=new EducationPlatformEntities();
            var student=(from i in db.Institutions
                         where i.Id == id
                         select i).FirstOrDefault();
            db.Institutions.Remove(student);
            db.SaveChanges();
            return RedirectToAction("InstitutionList");
        }

        public ActionResult InstitutionActivate(int id)
        {
            var db = new EducationPlatformEntities();
            var institution = (from i in db.Institutions
                               where i.Id ==id
                               select i).FirstOrDefault();

            institution.IsValid = "Yes";
            db.SaveChanges();
           

            return RedirectToAction("InstitutionList");
        }
        public ActionResult InstitutionDeactivate(int id)
        {
            var db = new EducationPlatformEntities();
            var institution = (from i in db.Institutions
                               where i.Id == id
                               select i).FirstOrDefault();

            institution.IsValid = "No";
            db.SaveChanges();


            return RedirectToAction("InstitutionList");
        }

        public ActionResult SingleInstitutionList(int id)
        {
            var db = new EducationPlatformEntities();
            var institution = (from i in db.Institutions where i.Id == id select i).FirstOrDefault();
            return View(institution);
        }

        public ActionResult InstitutionSearch()
        {

            return View();
        }
        [HttpPost]
        public ActionResult InstitutionSearchResult()
        {

            var search = Request["searching"];
            var db = new EducationPlatformEntities();

            var searchResult = (from i in db.Institutions
                                where i.Name.Contains(search) || i.Email.Contains(search)
                                select i).ToList();
            // return RedirectToAction()
            return View(searchResult);
        }

        public ActionResult InstitutionPasswordChangeRequest()
        {
            var db = new EducationPlatformEntities();

            var RequesList = db.VarsityPasswordChanges.ToList();
            return View(RequesList);
        }
    }
}