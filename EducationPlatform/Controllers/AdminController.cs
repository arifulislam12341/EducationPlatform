using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationPlatform.Models;

namespace EducationPlatform.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin obj)
        {
            //TempData["1"] = obj.Email;
            //TempData["2"] = obj.Password;
            var db = new EducationPlatformEntities();
            var admin = (from i in db.Admins
                         where (i.Email==obj.Email && i.Password==obj.Password)
                         select i).FirstOrDefault();
            if (admin != null)
            {
                return RedirectToAction("Index");
                
            }
            else
            {
                TempData["msg"] = "Wrong Email or Password";
                return View();
            }
        }
    }
}