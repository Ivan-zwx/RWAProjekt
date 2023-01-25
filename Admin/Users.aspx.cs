using DAL.DatabaseAccess;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class Users : System.Web.UI.Page
    {
        private IList<User> _users;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null || (string)Session["user"] != "admin")
            {
                Response.Redirect("Default.aspx");
            }
            _users = DbAccess.LoadUsers();
            rptUsers.DataSource = _users;
            rptUsers.DataBind();
        }
    }
}