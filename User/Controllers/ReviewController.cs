using Antlr.Runtime.Tree;
using DAL.DatabaseAccess;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace User.Controllers
{
    public class ReviewController : Controller
    {
        List<Apartment> reservedApartments;

        // GET: Review
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                // load apartments that the user has reserved in the past from the database
                // note: submit button on form should be disabled if user has no reservations or none are selected
                var user = (DAL.Models.User)Session["user"];
                ViewBag.User = user;
                reservedApartments = DbAccess.QueryReservedApartmentsForUser(user.Id).ToList();
                return View(reservedApartments);
            }
            else
            {
                return RedirectToAction("Index", "Apartments");
            }
        }

        [HttpPost]
        public ActionResult SubmitReview(int userId, string apartmentName, string details, int stars)
        {
            try
            {
                if (reservedApartments != null && reservedApartments.Count() != 0)
                {
                    int apartmentId = reservedApartments.Where(x => x.Name == apartmentName).FirstOrDefault().Id;
                    DbAccess.CreateApartmentReviewForUser(userId, apartmentId, details, stars);
                }
            }
            catch (Exception)
            {
                return Json("Slanje recenzije nije uspjelo");
            }
            return Json("Recenzija je poslana");
        }
    }
}