using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace User.Utilities
{
    public static class CookieUtils
    {
        private static readonly double cookieExpiration = 1.0;

        public static void SetOrCreateCookie(this Controller controller, string name, string value)
        {
            if (controller.Request.Cookies[name] != null)
            {
                controller.Response.Cookies[name].Value = value;
            }
            else
            {
                controller.CreateCookie(name, value);
            }
        }

        public static void CreateCookie(this Controller controller, string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddDays(cookieExpiration);
            controller.Response.SetCookie(cookie);
        }

        public static void DeleteCookie(this Controller controller, string name)
        {
            if (controller.Request.Cookies[name] != null)
            {
                controller.Response.Cookies[name].Expires = DateTime.Now.AddDays(-cookieExpiration);
            }
        }
    }
}