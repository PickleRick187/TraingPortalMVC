using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrainingPortal.Models;
using TrainingPortal.Models.Extended;


namespace TrainingPortal.Controllers
{
    public class AccountController : Controller
    {
        private IStudentRepository studentRepository;

        public AccountController()
        {
            this.studentRepository = new StudentRepository(new TrainingPortalEntities());
        }

        public AccountController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        #region Signup

        //Get
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        #region Signup Post Action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Exclude = "IsEmailVerified, ActivationCode")]
            Student student)
        {
            bool Status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                #region Verify Email Exist

                var isExist = studentRepository.IsEmailExist(student.StudentEmail);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email Address already exist.");
                    return View(student);
                }

                #endregion

                #region Activation Code

                student.ActivationCode = Guid.NewGuid();

                #endregion

                #region Password Hash

                student.StudentPassword = Crypto.Hash(student.StudentPassword);
                student.ConfirmPassword = Crypto.Hash(student.ConfirmPassword);

                #endregion

                student.IsEmailVerified = false;

                #region Save to database

                studentRepository.InsertStudent(student);
                studentRepository.Save();

                #endregion

                #region Send Email

                SendVerificationLinkEmail(student.StudentEmail, student.ActivationCode.ToString());

                message = "Registration Successfully done. Account activation link " +
                          " has been sent to your email id: " + student.StudentEmail;
                Status = true;


            }

            #endregion

            else
            {
                message = "Invalid Request.";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;

            return View(student);
        }
        #endregion




        #endregion


        #region Account Verification 

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (TrainingPortalEntities entities = new TrainingPortalEntities())
            {

                entities.Configuration.ValidateOnSaveEnabled = false;
                var v = entities.Students.Where(s => s.ActivationCode == new Guid(id)).FirstOrDefault();

                if (v != null)
                {
                    v.IsEmailVerified = true;
                    entities.SaveChanges();
                }
                else
                {
                    ViewBag.Message = "Invalid Request";

                }

                ViewBag.Status = Status;
                return View();
            }


        }

        #endregion


        #region Signin

        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }


        #region Signin Post Action

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Signin(StudentLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (TrainingPortalEntities entities = new TrainingPortalEntities())
            {
                var v = entities.Students.Where(s => s.StudentEmail == login.StudentEmail).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.StudentPassword), v.StudentPassword) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.StudentEmail, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {

                            
                            return RedirectToAction("UserHome", "Portal", new {  id = v.StudentID});
                        }
                    }
                    else
                    {
                        message = "Invalid Creditentials provided.";
                    }
                }
                else
                {
                    message = "Invalid creditential provided.";
                }
            }

            ViewBag.Message = message;
            return View();
        }

        #endregion


        #endregion


        #region Signout

        [Authorize]
        [HttpPost]
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Signin", "Account");
        }

        #endregion



        #region Send Link to Email for Verification

        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Account/VerifyAccount" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("trainingportaleoh@gmail.com", "Training Portal");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "TrainingPortal1";

            string subject = "Your account is successfully created!;";

            string body = "<br><\br>We are excited to tell you that your Training Portal account is " +
                          "successfully created. Please click the link below to verify your account" +
                          "<br></br><a href = '" + link + "'>" + link + "</a>";
                
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);

        }

        #endregion
    }
}