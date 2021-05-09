using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using PersianDate;

namespace Masjed.Areas.Panel.Controllers
{
    [RouteArea("Panel")]
    [RoutePrefix("needines")]
    public class NeedinesController : Controller
    {
        private Masjed_DBEntities db = new Masjed_DBEntities();

        [Route("show")]
        // GET: Panel/Needines
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NeedinesViewModel needines)
        {
            if(ModelState.IsValid)
            {
                string persiandate = needines.BirthDate.ToString();
                DateTime dt = persiandate.ToEn();
                if(!db.Needines.Any(x => x.Phone == needines.Phone))
                {
                    Needines need = new Needines()
                    {
                        FirstName=needines.FirstName.Trim().ToLower(),
                        LastName=needines.LastName.Trim().ToLower(),
                        Phone=needines.Phone.Trim(),
                        Address=needines.Address.Trim(),
                        Sex=needines.Sex,
                        Job=needines.Job.Trim(),
                        BirthDate=dt,
                        Pic=needines.Pic.Trim(),
                        Income=needines.Income.Trim(),
                        Education=needines.Education,
                        Created_at=DateTime.Now
                    };
                    db.Needines.Add(need);
                    db.SaveChanges();
                    ViewBag.SuccessMessage = "با موفقیت افزوده شد";
                    return View();
                }
                ModelState.AddModelError("Phone", "این شماره تلفن قبلا ایجاد شده است.");
                return View();
            }
            return View();
        }
    }
}