using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["user"] != null)
            {
                if (Request.Cookies["user"].Value == "admin")
                {
                    Session["user"] = "admin";
                    Response.Redirect("Apartments.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelForma.Visible = true;
                PanelIspis.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var username = txtUserName.Text;
                var password = txtPassword.Text;


                if (username != "admin" && password != "admin")
                {
                    PanelIspis.Visible = true;
                    PanelForma.Visible = true;

                    txtUserName.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    Session["user"] = "admin";
                    HttpCookie cookie = new HttpCookie("user");
                    cookie.Value = "admin";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(cookie);
                    Response.Redirect("Apartments.aspx");
                }
            }
        }
    }
}