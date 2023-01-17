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
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                var reservedApartments = QueryReservedApartments();
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
                var reservedApartments = QueryReservedApartments();
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

        private List<Apartment> QueryReservedApartments()
        {
            var user = (DAL.Models.User)Session["user"];
            ViewBag.User = user;
            return DbAccess.QueryReservedApartmentsForUser(user.Id).ToList();
        }
    }
}