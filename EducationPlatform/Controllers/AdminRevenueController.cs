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
    public class AdminRevenueController : Controller
    {
        // GET: AdminRevenue
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Revenue()
        {
            var db=new EducationPlatformEntities();
            var sellingAmount=(from i in db.Transactions
                                    where ( i.Id>0)
                                    select i.CreditedAmount).Sum();
           
            var myEarning = 0.6 * sellingAmount;
            var varsityEarning = 0.4 * sellingAmount;
            
            ViewBag.sellingAmount = sellingAmount;
            ViewBag.myEarning = myEarning;
            ViewBag.varsityEarning = varsityEarning;


            
            var InstitutionList = db.Institutions.ToList();
            return View(InstitutionList);



        }

        public ActionResult InstitutionRevenue(int id)
        {
            var db = new EducationPlatformEntities();
            var sellingAmount = (from i in db.Transactions
                                 where (i.InstitutionId ==id)
                                 select i.CreditedAmount).Sum();

            var totalSellingCourse = (from i in db.Transactions
                                 where (i.InstitutionId == id)
                                 select i.Id).Count();

            var varsityName = (from i in db.Institutions
                               where i.Id == id
                               select i.Name).FirstOrDefault();
            
            var myEarning = 0.6 * sellingAmount;
            var varsityEarning = 0.4 * sellingAmount;

            ViewBag.varsityName = varsityName;
            ViewBag.totalSellingCourse = totalSellingCourse;
            ViewBag.sellingAmount = sellingAmount;
            ViewBag.myEarning = myEarning;
            ViewBag.varsityEarning = varsityEarning;
            
            return View();
        }
    }
}