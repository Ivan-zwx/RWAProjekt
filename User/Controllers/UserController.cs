using DAL.DatabaseAccess;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace User.Controllers
{
    public class UserController : Controller
    {
        public UserController() {}

        // GET: User
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Apartments");
            }
            return View((DAL.Models.User)Session["user"]);
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["user_email"] != null)
            {
                Response.Cookies["user_email"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["user_pass"] != null)
            {
                Response.Cookies["user_pass"].Expires = DateTime.Now.AddDays(-1);
            }
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index", "Apartments");
        }

        // GET: User/Login
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.IsValid = true;
            return View();
        }

        //POST: User/Login
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

            HttpCookie cookie_email = new HttpCookie("user_email");
            cookie_email.Value = u.Email;
            cookie_email.Expires = DateTime.Now.AddDays(1);

            HttpCookie cookie_pass = new HttpCookie("user_pass");
            cookie_pass.Value = u.PasswordHash;
            cookie_pass.Expires = DateTime.Now.AddDays(1);

            Response.SetCookie(cookie_email);
            Response.SetCookie(cookie_pass);

            return RedirectToAction("Index", "Apartments");
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.IsValid = true;
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit
        public ActionResult Edit()
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Home");

            ViewBag.IsValid = true;
            return View((DAL.Models.User)Session["user"]);
        }

        // POST: User/Edit
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

        // POST: User/Delete
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