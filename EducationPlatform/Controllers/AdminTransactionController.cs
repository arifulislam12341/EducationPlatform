using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationPlatform.Models;

namespace EducationPlatform.Controllers
{
    public class AdminTransactionController : Controller
    {
        // GET: AdminTransaction
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TransactionHistory()
        {
            var db = new EducationPlatformEntities();
            var transaction = db.Transactions.ToList();
            return View(transaction);
        }
      
    }
}