using Antlr.Runtime.Tree;
using DAL.DatabaseAccess;
using DAL.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace User.Controllers
{
    public class ReviewController : Controller
    {
        // NIJE RADILO ZBOG PONOVNOG INSTANCIRANJA KONTROLERA IZMEDU DVA ACTIONRESULTA - STATIC VARIABLA JE QUICK FIX RJESENJE - TREBA NACI BOLJE
        //static List<Apartment> reservedApartments;

        // GET: Review
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                // load apartments that the user has reserved in the past from the database
                // note: submit button on form should be disabled if user has no reservations or none are selected
                var reservedApartments = queryReserverApartments();
                return View(reservedApartments);
            }
            else
            {
                return RedirectToAction("Index", "Apartments");
            }
        }

        [HttpPost]
        public ActionResult SubmitReview(int userId, string apartmentName, int stars, string details)
        {
            try
            {
                var reservedApartments = queryReserverApartments();
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
            return Json("Recenzija je uspjesno poslana");
        }

        private List<Apartment> queryReserverApartments()
        {
            var user = (DAL.Models.User)Session["user"];
            ViewBag.User = user;
            return DbAccess.QueryReservedApartmentsForUser(user.Id).ToList();
        }
    }
}