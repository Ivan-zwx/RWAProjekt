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
            return View();
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