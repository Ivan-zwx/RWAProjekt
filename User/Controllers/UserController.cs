using DAL.DatabaseAccess;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using User.Utilities;

namespace User.Controllers
{
    public class UserController : Controller
    {
        public UserController() {}

        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Apartments");
            }
            return View((DAL.Models.User)Session["user"]);
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.IsValid = true;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            DAL.Models.User u = DbAccess.AuthUser(email, password);

            if (u == null)
            {
                ViewBag.IsValid = false;
                return View("Login");
            }
            ViewBag.IsAuthorized = true;
            Session["user"] = u;

            this.CreateCookie("user_email", u.Email);
            this.CreateCookie("user_pass", u.PasswordHash);

            return RedirectToAction("Index", "Apartments");
        }

        public ActionResult Logout()
        {
            this.DeleteCookie("user_email");
            this.DeleteCookie("user_pass");
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index", "Apartments");
        }

        public ActionResult Create()
        {
            ViewBag.IsValid = true;
            return View();
        }

        [HttpPost]
        public ActionResult Create(DAL.Models.User u)
        {
            try
            {
                DbAccess.AddUser(u);

                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ViewBag.IsValid = false;
                return View();
            }
        }

        public ActionResult Edit()
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Home");

            ViewBag.IsValid = true;
            return View((DAL.Models.User)Session["user"]);
        }

        [HttpPost]
        public ActionResult Edit(DAL.Models.User u)
        {
            try
            {
                DbAccess.SaveUser(u);
                Session["user"] = u;

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.IsValid = false;
                return View();
            }
        }

        public ActionResult Delete()
        {
            try
            {
                DAL.Models.User u = (DAL.Models.User)Session["user"];
                DbAccess.DeleteUser(u.Id);
                return RedirectToAction("Logout");
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { @error = "Nije moguce izbrisati korisnika" });
            }
        }
    }
}