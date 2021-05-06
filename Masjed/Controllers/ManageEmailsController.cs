using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Masjed.Controllers
{
    public class ManageEmailsController : Controller
    {
        // GET: ManageEmails
        public ActionResult ActivationEmail()
        {
            return PartialView();
        }

        public ActionResult RecoverPassword()
        {
            return PartialView();
        }

        public ActionResult ReActivation()
        {
            return PartialView();
        }

        public ActionResult PasswordChanged()
        {
            return PartialView();
        }
    }
}