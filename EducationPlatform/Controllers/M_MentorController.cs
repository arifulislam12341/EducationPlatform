using EducationPlatform.Auth;
using EducationPlatform.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EducationPlatform.Controllers
{
    public class M_MentorController : Controller
    {
        // GET: M_Mentor
        [MentorLoginAuth]
        public ActionResult Index()
        {
            return View();
        }
        [MentorLoginAuth]
        public ActionResult M_MentorInformation()
        {
            var db = new EducationPlatformEntities();
            if (Session["MentorEmail"] != null)
            {
                var email = Session["MentorEmail"].ToString();

                // var mentor = db.Mentors.Where(d => d.Email == email).FirstOrDefault();
                var mentor = (from p in db.Mentors where p.Email == email select p).FirstOrDefault();
                return View(mentor);
            }
            //  else
            //    {
            //        return RedirectToAction("Index");
            //   }

            return View();
        }
        public ActionResult M_MentorInformationDownload(int id)
        {

            var db = new EducationPlatformEntities();
            var mentorName = (from p in db.Mentors where p.Id == id select p.Name).FirstOrDefault();
            var mentorAddress = (from p in db.Mentors where p.Id == id select p.Address).FirstOrDefault();
            var mentorEmail = (from p in db.Mentors where p.Id == id select p.Email).FirstOrDefault();
            var mentorPhonenumber = (from p in db.Mentors where p.Id == id select p.Phone).FirstOrDefault();
            var mentorPhoto = (from p in db.Mentors where p.Id == id select p.Photo).FirstOrDefault();
            var mentorGender = (from p in db.Mentors where p.Id == id select p.Gender).FirstOrDefault();
            var mentorPassword = (from p in db.Mentors where p.Id == id select p.Password).FirstOrDefault();
            var mentorInstitution = (from p in db.Mentors where p.Id == id select p.Institution).FirstOrDefault();
            var mentorIsValid = (from p in db.Mentors where p.Id == id select p.IsValid).FirstOrDefault();
            var dateime = DateTime.Now.ToString("dddd, dd MMMM yyyy").ToString();


            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable.
            System.Data.DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("particular:");
            dataTable.Columns.Add("Details");
            // dataTable.Columns.Add("Name");
            //Add rows to the DataTable.
            dataTable.Rows.Add(new object[] { "Print Time", dateime });
            dataTable.Rows.Add(new object[] { " MentorId", id });
            dataTable.Rows.Add(new object[] { "MentorName", mentorName });
            dataTable.Rows.Add(new object[] { "MentorAddress",mentorAddress });
            dataTable.Rows.Add(new object[] { "MentorEmail", mentorEmail });
            dataTable.Rows.Add(new object[] { "MentorPhonenumber",mentorPhonenumber });
            dataTable.Rows.Add(new object[] { "MentorPhoto",mentorPhoto });
            dataTable.Rows.Add(new object[] { "MentorGender",mentorGender });
            dataTable.Rows.Add(new object[] { "MentorInstitution", mentorInstitution}); 
                 dataTable.Rows.Add(new object[] { "MentorIsValid",mentorIsValid });
            dataTable.Rows.Add(new object[] { "MentorPassword",mentorPassword });



            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 10));
            // Open the document in browser after saving it

            doc.Save(mentorName + dateime + ".pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            //close the document
            doc.Close(true);
            return View();


        }







        [MentorLoginAuth]
        public ActionResult M_MentorConfirmDelete(int id)
        {
            Session["MentorId"] = id;
            return View();
        }
        [MentorLoginAuth]
        public ActionResult M_MentorDelete()
        {
            var mentorid = Session["MentorId"].ToString();
            var id = int.Parse(mentorid);
            var db = new EducationPlatformEntities();
            var mentor = (from p in db.Mentors where p.Id == id select p).SingleOrDefault();
            db.Mentors.Remove(mentor);
            db.SaveChanges();
            return RedirectToAction("M_MentorLogIN");
        }
        [MentorLoginAuth]
        [HttpGet]
        public ActionResult M_MentorUpdate(int id)
        {
            var db = new EducationPlatformEntities();
            var mentor = (from p in db.Mentors where p.Id == id select p).SingleOrDefault();
            return View(mentor);
        }
        [MentorLoginAuth]
        [HttpPost]
        public ActionResult M_MentorUpdate(Mentor obj)
        {

            var db = new EducationPlatformEntities();
            var mentor = (from p in db.Mentors where p.Id == obj.Id select p).SingleOrDefault();
            mentor.Id = obj.Id;
            mentor.Name = obj.Name;
            mentor.Address = obj.Address;
            mentor.Email = obj.Email;
            mentor.Phone = obj.Phone;
            mentor.Password = obj.Password;
            mentor.Gender = obj.Gender;

            //  db.Entry(mentor).CurrentValues.SetValues(obj);
            db.SaveChanges();
            return RedirectToAction("M_MentorInformation");

        }
        [HttpGet]
        public ActionResult M_MentorLogIN()
        {
            return View();
        }
        [HttpPost]

        public ActionResult M_MentorLogIN(Mentor m)
        {
            var db = new EducationPlatformEntities();
            var mentor = db.Mentors.Where(d => d.Email == m.Email && d.Password == m.Password && d.IsValid == "Yes").FirstOrDefault();

            //    var mentor =(from p in db.Mentors where p.Email==m.Email && p.Password==m.Password && p.IsValid=="Yes" select p).FirstOrDefault();
            if (mentor != null)
            {
               
                Session["MentorEmail"] = m.Email.ToString();
                FormsAuthentication.SetAuthCookie(m.Email, true);
                //FormsAuthentication.SetAuthCookie(a.Fname, true);
                return RedirectToAction("M_MentorInformation");
            }
            else
            {
                ViewBag.log = "Username or password is incorrect!";
            }

            return View();
        }
        [MentorLoginAuth]
        public ActionResult M_MentorCourseDetails()
        {

            var db = new EducationPlatformEntities();
            var email = Session["MentorEmail"].ToString();
            var mentorId = (from i in db.Mentors where i.Email == email select i.Id).FirstOrDefault();
            var course = (from c in db.Courses where c.MentorId == mentorId select c).ToList();
            return View(course);
        }

        [MentorLoginAuth]
        public ActionResult M_MentorStudentId(int id)
        {
            Session["COURSEID"] = id;//this session id used to review student M_MentorStudentReview
            var db = new EducationPlatformEntities();
            var studentId = (from p in db.ValidStudents where p.CourseId == id select p).ToList();




            return View(studentId);
        }
        [MentorLoginAuth]
        public ActionResult M_MentorStudentList(int id)
        {

            var db = new EducationPlatformEntities();
            var studentId = (from p in db.Students where p.Id == id select p).ToList();




            return View(studentId);
        }
        [MentorLoginAuth]
        [HttpGet]
        public ActionResult M_MentorCounsiling(int id)
        {
            Session["COURSEID"] = id;
            return View();
        }
        [MentorLoginAuth]
        [HttpPost]
        public ActionResult M_MentorCounsiling(Counseling obj)
        {
            var courseid = Session["COURSEID"].ToString();


            var db = new EducationPlatformEntities();
            var email = Session["MentorEmail"].ToString();
            var mentorId = (from i in db.Mentors where i.Email == email select i.Id).FirstOrDefault();
            var counsiling = new Counseling()
            {
                MentorId = mentorId,
                CourseId = int.Parse(courseid),
                MeetLink = obj.MeetLink,
                Details = obj.Details,
                Date = obj.Date,
            };
            db.Counselings.Add(counsiling);
            db.SaveChanges();
            // db.Counselings.Add(obj);
            return RedirectToAction("M_MentorCourseDetails");

        }

        [MentorLoginAuth]
        [HttpGet]
        public ActionResult M_MentorNotice(int id)
        {
            Session["COURSEID"] = id;
            return View();

        }
        [MentorLoginAuth]
        [HttpPost]
        public ActionResult M_MentorNotice(Notice obj)
        {
            var courseid = Session["COURSEID"].ToString();
            var db = new EducationPlatformEntities();
            var email = Session["MentorEmail"].ToString();
            var mentorId = (from i in db.Mentors where i.Email == email select i.Id).FirstOrDefault();
            var mentorname = (from i in db.Mentors where i.Id == mentorId select i.Name).FirstOrDefault();
            var notice = new Notice()
            {
                CourseId = int.Parse(courseid),
                AnnouncedBy = mentorname,
                AnnouncerId = mentorId,
                Details = obj.Details,
                Date = obj.Date,
            };
            db.Notices.Add(notice);
            db.SaveChanges();



           




            return RedirectToAction("M_MentorCourseDetails");

        }
        [MentorLoginAuth]
        public ActionResult M_MentorRatings(int id)
        {

            var db = new EducationPlatformEntities();
            var email = Session["MentorEmail"].ToString();
            var mentorId = (from i in db.Mentors where i.Email == email select i.Id).FirstOrDefault();


            var viewrate = (from i in db.Ratings where i.CourseId == id && i.MentorId == mentorId select i).ToList();
            return View(viewrate);
        }
        [MentorLoginAuth]
        [HttpGet]
        public ActionResult M_MentorAssignment(int id)
        {
            Session["COURSEID"] = id;

            return View();
        }
        [MentorLoginAuth]
        [HttpPost]
        public ActionResult M_MentorAssignment(Assignment obj)
        {
            var courseid = Session["COURSEID"].ToString();
            // TempData["id"]=courseid;
            var db = new EducationPlatformEntities();
            var email = Session["MentorEmail"].ToString();
            var mentorId = (from i in db.Mentors where i.Email == email select i.Id).FirstOrDefault();
            //  TempData["id"]=mentorId;
            var assignment = new Assignment()
            {
                MentorId = mentorId,
                CourseId = int.Parse(courseid),
                Question = obj.Question,
                Date = obj.Date,
            };
            // TempData["x"] = obj.Question;
            // TempData["y"] = obj.Date;
            db.Assignments.Add(assignment);
            db.SaveChanges();
            return RedirectToAction("M_MentorCourseDetails");

        }
        [MentorLoginAuth]
        [HttpGet]
        public ActionResult M_MentorStudentReview(int id)//this student id came from M_MentorStudentId
        {
            Session["StudentId"] = id;
            return View();
        }
        [MentorLoginAuth]
        [HttpPost]
        public ActionResult M_MentorStudentReview(Reviewstudent obj)
        {
            var studentid = Session["StudentId"].ToString();
            var courseid = Session["COURSEID"].ToString();
            var db = new EducationPlatformEntities();
            var email = Session["MentorEmail"].ToString();
            var mentorId = (from i in db.Mentors where i.Email == email select i.Id).FirstOrDefault();

            var review = new Reviewstudent()
            {
                MentorId = mentorId,
                CourseId = int.Parse(courseid),
                StudentId = int.Parse(studentid),
                FeedBack = obj.FeedBack,
                Date = obj.Date,
            };
            db.Reviewstudents.Add(review);
            db.SaveChanges();
            return RedirectToAction("M_MentorCourseDetails");
        }
        [MentorLoginAuth]
        [HttpGet]
        public ActionResult M_MentorStudentCertificate(int id)
        {
            Session["studentId"] = id;
            return View();
        }
        [MentorLoginAuth]
        [HttpPost]
        public ActionResult M_MentorStudentCertificate(Certificate obj)
        {
            var studentId = Session["studentId"].ToString();
            
            var coursid = Session["COURSEID"].ToString();
            var db = new EducationPlatformEntities();
            var email = Session["MentorEmail"].ToString();
            var recomanderId = (from i in db.Mentors where i.Email == email select i.Id).FirstOrDefault();
            var recommanderName = (from i in db.Mentors where i.Email == email select i.Name).FirstOrDefault();

            var certifacte = new Certificate()
            {

                CourseId = int.Parse(coursid),
                RecommderId = recomanderId,
                RecomendBy = recommanderName,
                Status = "Pending",
                Comments = obj.Comments,
                Date = obj.Date,
                ApplierId = int.Parse(studentId),
            };
            db.Certificates.Add(certifacte);
            db.SaveChanges();

            MailMessage mail = new MailMessage();
            // var studentId = (from p in db.Results where p.Id == id select p.StudentId).FirstOrDefault();
            var studentid = int.Parse(studentId);
            var studentEmail = (from p in db.Students where p.Id == studentid select p.Email).FirstOrDefault();
            var studentName = (from p in db.Students where p.Id == studentid select p.Name).FirstOrDefault();
            mail.To.Add(studentEmail);
            mail.From = new MailAddress("19-40116-1@student.aiub.edu");
            mail.Subject = "Your  certificate is recommanded by "+recommanderName;
            string Body = "Hello " + studentName + "<br/>Your certificate is recommanded by "+recommanderName +" wait for approval of ABC Education  <br/>" +
                "Check the Website";

            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-mail.outlook.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("19-40116-1@student.aiub.edu", "aleX@monaf 32");
            smtp.EnableSsl = true;
            smtp.Send(mail);





            //TempData["x"] = studentId;
            //TempData["y"] = coursid;
            return RedirectToAction("M_MentorCourseDetails");
        }
        [MentorLoginAuth]
        [HttpGet]
        public ActionResult M_MentorStudentResult()
        {
           
            
            return View();
        }
        [MentorLoginAuth]
        [HttpPost]
        public ActionResult M_MentorStudentResult(Result obj)
        {

             var studentId= Session["studentId"].ToString();
            //   var courseId = Session["courseId"].ToString();
            //   var id = int.Parse(courseId);
            var db = new EducationPlatformEntities();
            //  var coursename = (from p in db.Courses where p.Id == id select p.Name).FirstOrDefault();
            var email = Session["MentorEmail"].ToString();
            var mentorId = (from i in db.Mentors where i.Email == email select i.Id).FirstOrDefault();

            var courseId = (from i in db.Assignments where i.MentorId == mentorId select i.CourseId).FirstOrDefault();
            var institutionid = (from i in db.Transactions where i.CourseId == courseId select i.InstitutionId).FirstOrDefault();
            var courseName = (from i in db.Courses where i.Id == courseId select i.Name).FirstOrDefault();

            //var studentId = (from i in db.Certificates where i.CourseId == courseId select i.ApplierId).FirstOrDefault();

          //  var studentId = Session["studentId"].ToString();


            var assignmentId = (from i in db.Assignments where i.MentorId == mentorId select i.Id).FirstOrDefault();
          //  var studentId = (from i in db.AnswerScripts where i.AssignmentId == assignmentId select i.StudentId).FirstOrDefault();
            var result = new Result()
            {
                CourseId = courseId,
                CourseName = courseName,
                StudentId = int.Parse(studentId),
                MentorId = mentorId,
                AssignmentId = assignmentId,
                InstitutionId = institutionid,
                Mark = obj.Mark,
                Date = obj.Date,
                Comment = obj.Comment,
            };
            db.Results.Add(result);
            db.SaveChanges();
            return RedirectToAction("M_MentorCourseDetails");

        }
        [MentorLoginAuth]
        public ActionResult M_MentorAssignmentList(int id)
        {
            var db = new EducationPlatformEntities();
            var assignment = (from p in db.Assignments where p.CourseId == id select p).ToList();
            return View(assignment);

        }
        [MentorLoginAuth]
        public ActionResult M_MentorViewAnswer(int id)
        {
            Session["studentId"] = id;
            var db = new EducationPlatformEntities();
            var answer = (from p in db.AnswerScripts where p.StudentId == id select p).FirstOrDefault();
            return View(answer);
        }
        [MentorLoginAuth]
        public ActionResult M_MentorTrackStudent(int id)
        {
            var db = new EducationPlatformEntities();
            var answer = (from p in db.AnswerScripts where p.AssignmentId == id select p).ToList();
            return View(answer);
        }


        [MentorLoginAuth]
        public ActionResult M_MentorLogOut()
        {
            Session.Clear();
            return RedirectToAction("M_MentorLogIN");
        }

        [MentorLoginAuth]
        public ActionResult M_MentorAllStudentResults(int id)
        {
            // Session["CourseId"] = id;
            // var courseid = Session["CourseId"].ToString();
            // var Id=int.Parse(courseid);
            var db = new EducationPlatformEntities();

            var allstudentresults = (from p in db.Results where p.CourseId == id select p).ToList();

            return View(allstudentresults);
        }
        [MentorLoginAuth]
        public ActionResult M_MentorReturnResult(int id)
        {

            
            var db = new EducationPlatformEntities();
            var results = (from p in db.Results where p.Id == id select p).FirstOrDefault();


            results.BackResult = "Yes";

            db.SaveChanges();
            MailMessage mail = new MailMessage();
            var studentId = (from p in db.Results where p.Id == id select p.StudentId).FirstOrDefault();
           
           var studentEmail=(from p in db.Students where p.Id==studentId select p.Email).FirstOrDefault();
            var studentName = (from p in db.Students where p.Id == studentId select p.Name).FirstOrDefault();
            mail.To.Add(studentEmail);
            mail.From = new MailAddress("19-40116-1@student.aiub.edu");
            mail.Subject = "Your Result has been published";
            string Body = "Hello"+ studentName +"<br/>Your result is published <br/>" +
                "Check the Website";

            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-mail.outlook.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("19-40116-1@student.aiub.edu", "aleX@monaf 32"); 
            smtp.EnableSsl = true;
            smtp.Send(mail);




            return RedirectToAction("M_MentorCourseDetails");
        }
        [MentorLoginAuth]
        public ActionResult M_MentorNotReturnResult(int id)
        {
            var db = new EducationPlatformEntities();
            var results = (from p in db.Results where p.Id == id select p).FirstOrDefault();

            results.BackResult = null;

            db.SaveChanges();
            return RedirectToAction("M_MentorCourseDetails");
        }

        [MentorLoginAuth]
        public ActionResult M_MentorSeeNotice(int id)
        {
            var db = new EducationPlatformEntities();

            var institutionname = (from p in db.Mentors where p.Id == id select p.Institution).FirstOrDefault();
            var notice = (from p in db.Notices where p.AnnouncedBy == institutionname select p).ToList();

            return View(notice);
        }
        [MentorLoginAuth]
        [HttpGet]
        public ActionResult M_MentorUploadModule(int id)
        {
            Session["courseid"] = id;
            return View();
        }
        [MentorLoginAuth]
        [HttpPost]
        public ActionResult M_MentorUploadModule(CourseDetail obj)
        {
            var courseid = Session["courseid"].ToString();

            var db = new EducationPlatformEntities();
            var email = Session["MentorEmail"].ToString();
            var mentorId = (from i in db.Mentors where i.Email == email select i.Id).FirstOrDefault();
            var module = new CourseDetail()
            {
                CourseId = int.Parse(courseid),
                MentorId = mentorId,
                Material = obj.Material,
                Module = obj.Module,
                Date = obj.Date,
                Description = obj.Description,
            };
            db.CourseDetails.Add(module);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        [MentorLoginAuth]
        public ActionResult M_MentorSeeUploadModule(int id)
        {


            var db = new EducationPlatformEntities();
            var seemodule = (from i in db.CourseDetails where i.CourseId == id select i).ToList();

            return View(seemodule);

        }



    }
}