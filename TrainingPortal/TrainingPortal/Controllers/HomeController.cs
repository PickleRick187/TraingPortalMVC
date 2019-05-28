using System.Web.Mvc;
using TrainingPortal.BLL;
using  TrainingPortal.DAL;
using TrainingPortal.BLL.Repositories;
using TrainingPortal.Models;

using TrainingPortal.BLL.Interfaces;
using System.Net.Configuration;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Data;

namespace TrainingPortal.Controllers
{
    
    public class HomeController : Controller
    {
        private UnitOfWork _unitOfWork;

        public HomeController()
        {
            this._unitOfWork = new UnitOfWork(new TrainingPortalEntities());
        }

        
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public PartialViewResult _CourseDetails(int id)
        {
            var model = _unitOfWork.Courses.GetByID(id);
            return PartialView(model);
        }


        public FileContentResult GetImage(int id)
        {
            var image = _unitOfWork.Courses.GetByID(id);
            if (image != null)
            {
                return File(image.PhotoFile, image.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


        public PartialViewResult Courses()
        {

            return PartialView();
        }


        [HttpGet]
        public ActionResult Programming()
        {
            return View();
        }

        //public ActionResult Payment(int id)
        //{
        //    var query = _unitOfWork.Courses.GetByID(id);
            
        //    CourseView courseView = new CourseView();

        //    var course = AutoMapper.Mapper.Map(query, courseView);
        //    return View(course);
        //}

        public ActionResult CsharpCourse()
        {

            return View();
        }

        public ActionResult Purchase()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Purchase(PaymentTypeView paymentTypeView)
        {

            PaymentType payment = new PaymentType();

            var autoPayment = AutoMapper.Mapper.Map(paymentTypeView, payment);
             _unitOfWork.Payment.PurchaseCourse(autoPayment);
           return RedirectToAction("ThankYouForPurchasing");
           
        }

        public ActionResult ThankYouForPurchasing()
        {
            return View();
        }



        public void ExecuteEnrollProcdure()
        {
            SqlConnection mySqlConnection = new SqlConnection("server=localhost;database=TrainingPortal;Integrated Security=True;");

            SqlCommand mySqlCmd = mySqlConnection.CreateCommand();

            mySqlCmd.CommandText = "EXECUTE EnrollStudent @dateAndTime, @courseID, @studentID, @paymentTypeID, @trainingID";

            mySqlCmd.Parameters.Add("@dateAndTime", SqlDbType.DateTime).Value = "2019-04-12";
            mySqlCmd.Parameters.Add("@courseID", SqlDbType.VarChar, 30).Value = 1;
            mySqlCmd.Parameters.Add("@studentID", SqlDbType.VarChar, 30).Value = 1;
            mySqlCmd.Parameters.Add("@paymentTypeID", SqlDbType.VarChar, 30).Value = 1;
            mySqlCmd.Parameters.Add("@trainingID", SqlDbType.VarChar, 30).Value = 1;

            mySqlConnection.Open();
            mySqlCmd.ExecuteNonQuery();
            mySqlConnection.Close();


        }


        public void SendMail()
        {
            SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (MailMessage mm = new MailMessage())
            {
                mm.From = new MailAddress(secObj.From); //--- Email address of the sender
                mm.To.Add("yadhir007@gmail.com"); //---- Email address of the recipient.
                mm.Subject = "This is sent from yadz app"; //---- Subject of email.
                mm.Body = "Hello coool"; //---- Content of email.

                SmtpClient smtp = new SmtpClient();
                smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                //---Your Email and password
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587; //---- SMTP Server port number. This varies from host to host. 
                smtp.Send(mm);
            }
        }

    }


    



}