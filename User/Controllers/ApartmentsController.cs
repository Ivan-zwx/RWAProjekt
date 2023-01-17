using DAL.DatabaseAccess;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User.Utilities;

namespace User.Controllers
{
    public class ApartmentsController : Controller
    {
        public ApartmentsController() {}

        public ActionResult Index()
        {
            List<Apartment> apartments = DbAccess.LoadApartments().ToList();

            if (Request.Cookies["city"] == null || Request.Cookies["adults"] == null || Request.Cookies["children"] == null || Request.Cookies["rooms"] == null || Request.Cookies["sort"] == null)
            {
                apartments.ToList().ForEach(x =>
                {
                    if (x.DeletedAt != "" || x.Status != "Slobodno")
                    {
                        apartments.Remove(x);
                    }
                });
                SetDefaultViewBag();
            }
            else
            {
                try
                {
                    FilterApartments(apartments, Request.Cookies["city"].Value, int.Parse(Request.Cookies["adults"].Value), int.Parse(Request.Cookies["children"].Value), int.Parse(Request.Cookies["rooms"].Value), int.Parse(Request.Cookies["sort"].Value));
                }
                catch
                {
                    apartments.ToList().ForEach(x =>
                    {
                        if (x.DeletedAt != "" || x.Status != "Slobodno")
                        {
                            apartments.Remove(x);
                        }
                    });
                    SetDefaultViewBag();
                }
            }

            ViewBag.Cities = DbAccess.GetCities();

            return View(apartments);
        }

        public ActionResult Details(Nullable<int> id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index", "Apartments");
            }
            try
            {
                Apartment a = DbAccess.GetApartmentById((int)id);
                ViewBag.Tags = DbAccess.LoadTagsForApartment((int)id);
                return View(a);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Apartments");
            }
        }

        public ActionResult Reviews(Nullable<int> id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index", "Apartments");
            }
            try
            {
                IList<Review> ar = DbAccess.QueryReviewsForApartment((int)id);
                ViewBag.ApartmentName = DbAccess.GetApartmentById((int)id).Name;
                return View(ar);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Apartments");
            }
        }

        [HttpPost]
        public ActionResult Filter(string city, int adults, int children, int rooms, int sort)
        {
            List<Apartment> apartments = DbAccess.LoadApartments().ToList();

            FilterApartments(apartments, city, adults, children, rooms, sort);

            this.SetOrCreateCookie("city", city);
            this.SetOrCreateCookie("adults", adults.ToString());
            this.SetOrCreateCookie("children", children.ToString());
            this.SetOrCreateCookie("rooms", rooms.ToString());
            this.SetOrCreateCookie("sort", sort.ToString());

            return PartialView("_ListApartment", apartments);
        }

        public ActionResult Reset()
        {
            this.DeleteCookie("city");
            this.DeleteCookie("adults");
            this.DeleteCookie("children");
            this.DeleteCookie("rooms");
            this.DeleteCookie("sort");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult BookExisting(int userId, int apartmentId, string details)
        {
            try
            {
                DbAccess.AddReservationForExistingUser(userId, apartmentId, details);
            }
            catch
            {
                return Json("Neuspješna rezervacija");
            }
            return Content("Rezervacija je proslijedena administratoru");
        }

        [HttpPost]
        public ActionResult BookNonExisting(int apartmentId, string details, string username, string userEmail, string phone)
        {
            try
            {
                DbAccess.AddReservationForNonExistingUser(apartmentId, details, username, userEmail, phone);
            }
            catch
            {
                return Json("Neuspješna rezervacija");
            }
            return Json("Rezervacija je proslijedena administratoru");
        }

        private void FilterApartments(List<Apartment> apartments, string city, int adults, int children, int rooms, int sort)
        {
            apartments.ToList().ForEach(a =>
            {
                if (a.DeletedAt != "" || a.Status != "Slobodno" || a.City != city || a.MaxAdults < adults || a.MaxChildren < children || a.TotalRooms < rooms)
                {
                    apartments.Remove(a);
                }
            });
            switch (sort)
            {
                case 1:
                    apartments.Sort((a, b) => a.Price.CompareTo(b.Price));
                    break;
                case 2:
                    apartments.Sort((a, b) => -a.Price.CompareTo(b.Price));
                    break;
                default:
                    apartments.Sort((a, b) => a.Id.CompareTo(b.Id));
                    break;
            }
            ViewBag.City = city;
            ViewBag.Adults = adults;
            ViewBag.Children = children;
            ViewBag.Rooms = rooms;
            ViewBag.Sort = sort;
        }

        private void SetDefaultViewBag()
        {
            ViewBag.City = "Zagreb";
            ViewBag.Adults = 1;
            ViewBag.Children = 0;
            ViewBag.Rooms = 1;
            ViewBag.Sort = 0;
        }
    }
}