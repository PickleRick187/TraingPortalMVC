using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrainingPortal.Models;
using TrainingPortal.DAL.Interfaces;
using TrainingPortal.BLL.Security;
using TrainingPortal.DAL;


namespace TrainingPortal.Controllers
{
    public class AccountController : Controller
    {
       
        public AccountController()
        {
        }

        private IStudentRepository _studentRepository;
        private IQueryStudentEmail _queryStudentEmail;
        private TrainingPortalEntities _context;
        public AccountController(IStudentRepository studentRepository, IQueryStudentEmail queryStudentEmail)
        {
            this._studentRepository = studentRepository;
            this._queryStudentEmail = queryStudentEmail;
         
        }


        #region Signup
        //Get Signup Page
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        #region Signup Post Action


        //Post data
        [HttpPost]
        public ActionResult Signup([Bind(Exclude = "IsEmailVerified, ActivationCode")]
            RegisterModel student)
        {

            bool Status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                #region Verify Email Exist

                //var isExist = _queryStudentEmail.IsEmailRegistered(student.StudentEmail);


                var isExist = checkReg(student.StudentEmail);

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

                Student learner = new Student()
                {
                    StudentEmail = student.StudentEmail,
                    StudentPassword = student.StudentPassword,
                    StudentFirstName = student.StudentFirstName,
                    StudentLastName = student.StudentLastName,
                    StudentID = student.StudentID
                };

             TrainingPortalEntities entities = new TrainingPortalEntities();
               
                   entities.Students.Add(learner);
                    entities.SaveChanges();
                
            
               //_studentRepository.InsertStudent(learner);
               //_studentRepository.Save();
           

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

                    return RedirectToAction("UserHome", "Portal");
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
        [AllowAnonymous]
        public ActionResult Signin(StudentLoginModel login, string ReturnUrl = "")
        {
            string message = "";
            TrainingPortalEntities entities = new TrainingPortalEntities();

                //var v = _studentRepository.CheckEmail(login.StudentEmail);

                var v = (from s in entities.Students
                    where s.StudentEmail == login.StudentEmail
                    select s).FirstOrDefault();

                if ( v != null )
                {
                    if (string.Compare(Crypto.Hash(login.StudentPassword), v.StudentPassword) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.StudentEmail, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
                        {
                            Expires = DateTime.Now.AddMinutes(timeout),
                            HttpOnly = true
                        };
                      
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {

                            
                            return RedirectToAction("UserHome", "Portal", new { id = v.StudentID});
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
            return RedirectToAction("Index", "Home");
        }

        #endregion


        public bool checkReg(string EmailID)
        {
            using (TrainingPortalEntities entities = new TrainingPortalEntities())
            {
                var isExist = entities.Students.Where(e => e.StudentEmail == EmailID).FirstOrDefault();
                return isExist != null;
            }
        }


        #region Send Link to Email for Verification

        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Account/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("yadhir007@gmail.com", "Training Portal");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "";

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
            });
            /*   smtp.Send(message)*/

        }

        #endregion
    }
}