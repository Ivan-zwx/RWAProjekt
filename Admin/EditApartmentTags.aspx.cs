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
            // NEED TO IMPLEMENT FUNCTIONALITY (NEW PROCEDURE AND DAL METHOD PROBABLY)

            Tag t = _allTags.FirstOrDefault(x => x.Name == ddlTags.SelectedItem.Text);
            if (_selectedtags.Add(t))
            {
                _allTags.Remove(t);
            }

            ddlTags.DataSource = _allTags;
            ddlTags.DataValueField = "Id";
            ddlTags.DataTextField = "Name";
            ddlTags.DataBind();

            rptTags.DataSource = _selectedtags;
            rptTags.DataBind();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            // NEED TO IMPLEMENT FUNCTIONALITY (NEW PROCEDURE AND DAL METHOD PROBABLY)

            Button button = sender as Button;
            string name = button.CommandArgument;
            Tag t = _selectedtags.FirstOrDefault(x => x.Name == name);
            _allTags.Add(t);
            _selectedtags.Remove(t);

            ddlTags.DataSource = _allTags;
            ddlTags.DataValueField = "Id";
            ddlTags.DataTextField = "Name";
            ddlTags.DataBind();

            rptTags.DataSource = _selectedtags;
            rptTags.DataBind();
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            int selected_apartment_id = (int)Session["apartment_id"];

            // na bazu - pobrisi sve tagove apartmana
            // na bazu - dodaj sve odabrane tagove na apartman

            DbAccess.RemoveAllTaggedApartmentsById(selected_apartment_id);
            foreach (var selectedTag in _selectedtags)
            {
                DbAccess.AddTaggedApartmentById(selected_apartment_id, selectedTag.Name);
            }

            _allTags.Clear();
            _selectedtags.Clear();
            Session["apartment_id"] = null;
            Response.Redirect("Apartments.aspx");
        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            _allTags.Clear();
            _selectedtags.Clear();
            Session["apartment_id"] = null;
            Response.Redirect("Apartments.aspx");
        }
    }
}