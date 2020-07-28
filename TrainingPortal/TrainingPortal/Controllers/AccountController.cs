using System;
using System.Data.Entity;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using TrainingPortal.Models;
using TrainingPortal.BLL.Interfaces;
using TrainingPortal.BLL.Security;
using TrainingPortal.BLL;
using TrainingPortal.BLL.Repositories;
using TrainingPortal.DAL;
using TrainingPortalDomain;


namespace TrainingPortal.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork _unitOfWork;
        public AccountController()
        {
            this._unitOfWork = new UnitOfWork(new TrainingPortalEntities());
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
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Signup([Bind(Exclude = "IsEmailVerified, ActivationCode")]
            RegisterModel registerModel)
        {
            bool Status = false;
            string message = "";


            //Model Validation
            if (ModelState.IsValid)
            {
                #region Verify Email Exist

                if (_unitOfWork.Student.CheckReg(registerModel.StudentEmail))
                {
                    ModelState.AddModelError("EmailExist", "Email Address already exist.");
                    return View(registerModel);
                }

                #endregion


                #region Activation Code

                registerModel.ActivationCode = Guid.NewGuid();

                #endregion

                #region Password Hash

                registerModel.StudentPassword = Crypto.Hash(registerModel.StudentPassword);
                registerModel.ConfirmPassword = Crypto.Hash(registerModel.ConfirmPassword);

                #endregion

                registerModel.IsEmailVerified = false;

                #region Save to database

                Student learner = new Student();

                var learn = AutoMapper.Mapper.Map(registerModel, learner);

                _unitOfWork.Student.Add(learn);
                _unitOfWork.Complete();

                #endregion

                #region Send Email

                SendVerificationLinkEmail(registerModel.StudentEmail, registerModel.ActivationCode.ToString());

                message = "Registration Successfully done. Account activation link " +
                          " has been sent to your email id: " + registerModel.StudentEmail;
                Status = true;

            }

            #endregion

            else
            {
                message = "Invalid Request.";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;

            return View(registerModel);
        }
        #endregion

        #endregion


     


        #region Account Verification 

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;

            var v = _unitOfWork.Student.VerifyEmail(id);
                if (v != null)
                {
                    v.IsEmailVerified = true;
                   
                
                    return RedirectToAction("UserHome", "Portal", new { id = v.StudentEmail} );
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
                ViewBag.Status = Status;
                return View();
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


            if (ModelState.IsValid)
            {
                var v = _unitOfWork.Student.GetEmail(login.StudentEmail);

                if (v != null)
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
                            return RedirectToAction("UserHome", "Portal", new { id = v.StudentID });
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


     
        #region Send Link to Email for Verification

        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Student/VerifyAccount" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("lukereecephilander21@gmail.com", "Training Portal");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "*******";

            string subject = "Your account is successfully created!;";

            string body = "<br><\br>We are excited to tell you that your Training Portal account is " +
                          "successfully created. Please click the link below to verify your account" +
                          "<br></br><a href = '" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail",
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
            }) ;
                /*smtp.Send(message)*/

        }


        #endregion
    }
}