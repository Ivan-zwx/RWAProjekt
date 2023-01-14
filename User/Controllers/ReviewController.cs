using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace User.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Apartments");
            }
        }

        [HttpPost]
        public ActionResult SubmitReview()
        {
            try
            {

            }
            catch (Exception)
            {
                return Json("Slanje recenzije nije uspjelo");
            }
            return Json("Recenzija je poslana");
        }
    }
}