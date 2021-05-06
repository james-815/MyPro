using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using DataLayer.ViewModels;

namespace Masjed.Controllers
{
    public class AccountController : Controller
    {
        Masjed_DBEntities db = new Masjed_DBEntities();
        // GET: Account
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(u => u.UserName == register.UserName.Trim().ToLower()))
                {
                    if (!db.Users.Any(e => e.Email == register.Email.Trim().ToLower()))
                    {
                        Users user = new Users()
                        {
                            Name = register.Name,
                            UserName = register.UserName,
                            Email = register.Email,
                            Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                            ActiveCode = Guid.NewGuid().ToString(),
                            IsActive = false,
                            RegisterDate = DateTime.Now,
                            RoleID = 2,
                            FailedLogin = 0,
                            Phone = register.Phone
                        };
                        db.Users.Add(user);
                        db.SaveChanges();
                        string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActivationEmail", user);
                        SendEmail.Send(register.Email, "فعالسازی حساب کاربری", body);
                        ViewBag.UserMsg = "باتشکر از ثبت نام شما، ایمیل حاوی لینک فعالسازی برای شما ارسال شد.لطفا جهت فعالسازی حساب کاربری ایمیل وارد شده را چک کنید.";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "ایمیل وارد شده تکراری می باشد";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "نام کاریری وارد شده تکراری می باشد";
                }
            }
            return View(register);
        }


        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                string hashpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var userLogin = db.Users.SingleOrDefault(l => l.UserName == login.UserName && l.Password == hashpassword);
                if (userLogin != null)
                {
                    if (userLogin.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(userLogin.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ViewBag.LoginError="حساب شما فعال نمی باشد";

                    }
                }
                else
                {
                    var CheckUser = db.Users.SingleOrDefault(c => c.UserName == login.UserName);
                    {
                        if (CheckUser == null)
                        {
                            ViewBag.LoginError="کاربر یافت نشد";
                        }
                    }

                    var blockUser = db.Users.SingleOrDefault(b => b.UserName == login.UserName && b.Password != hashpassword);
                    if (blockUser != null)
                    {
                        if (blockUser.FailedLogin == 5)
                        {
                            blockUser.IsActive = false;
                            db.SaveChanges();
                            string ReactiveEmail = PartialToStringClass.RenderPartialView("ManageEmails", "ReActivation", blockUser);
                            SendEmail.Send(blockUser.Email, "مسدودی حساب کاربری", ReactiveEmail);
                            ViewBag.LoginError= "حساب شما مسدود شد لطفا ایمیل ثبت نام شده را چک کنید";
                        }
                        else
                        {
                            blockUser.FailedLogin = blockUser.FailedLogin + 1;
                            db.SaveChanges();
                            ViewBag.LoginError=" کلمه عبور صحیح نمی باشد";
                        }
                    }


                }
            }
            return View(login);
        }


        [Route("Forget")]
        public ActionResult Forget()
        {
            return View();
        }
        
        [Route("Forget")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forget(ForgotPasswordViewModel forget)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(f => f.Email == forget.Email.Trim().ToLower());
                if (user != null)
                {
                        string bodyEmail = PartialToStringClass.RenderPartialView("ManageEmails", "RecoverPassword", user);
                        SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);
                        ViewBag.successforgot = "ایمیل  بازیابی برای شما ارسال شد";  
                }
                else
                {
                    ViewBag.forgetError= "کاربری یافت نشد.";
                }
            }
            return View();
        }



        [Route("LogOut")]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }


        [Route("Recover")]
        public ActionResult Recover(string id)
        {
            return View();
        }

        [Route("Recover")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Recover(string id, RecoveryPasswordViewModel recovery)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.ActiveCode == id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");
                user.ActiveCode = Guid.NewGuid().ToString();
                user.IsActive = true;
                user.FailedLogin = 0;
                db.SaveChanges();
                string bodyEmail = PartialToStringClass.RenderPartialView("ManageEmails", "PasswordChanged", user);
                SendEmail.Send(user.Email, "تغیر موفقیت آمیر کلمه عبور", bodyEmail);

                return View("Login");
            }
            return View();
        }

        public ActionResult Activate(string id)
        {
            var user = db.Users.SingleOrDefault(a => a.ActiveCode == id);
            if (user == null)
            {
                ViewBag.isactive = "حساب شما قبلا فعال شده است";
                return Redirect("/login");
            }
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            db.SaveChanges();
            ViewBag.username = user.Name;
            return View();
        }
    }
}