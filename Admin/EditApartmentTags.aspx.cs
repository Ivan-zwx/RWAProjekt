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
	public partial class EditApartmentTags : System.Web.UI.Page
	{
        private static IList<Tag> _allTags;
        private static ISet<Tag> _selectedtags;

        protected void Page_Load(object sender, EventArgs e)
		{
            if ((string)Session["user"] != "admin")
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                _allTags = DbAccess.LoadTags();

                if (Session["apartment_id"] != null)
                {
                    int selected_id = (int)Session["apartment_id"];
                    _selectedtags = new HashSet<Tag>(DbAccess.LoadTagsForApartment(selected_id));
                    _selectedtags.ToList().ForEach(t => _allTags.Remove(t));
                    AppendData();
                    rptTags.DataSource = _selectedtags;
                    rptTags.DataBind();
                    FillForm(selected_id);
                }
            }
        }

        private void AppendData()
        {
            ddlTags.DataSource = _allTags;
            ddlTags.DataValueField = "Id";
            ddlTags.DataTextField = "Name";
            ddlTags.DataBind();
        }

        private void FillForm(int id)
        {
            Apartment a = DbAccess.GetApartmentById(id);

            txtName.Text = a.Name;
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
 
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {

        }
    }
}