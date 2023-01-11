using DAL.DatabaseAccess;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Admin
{
    public partial class AddApartment : System.Web.UI.Page
    {
        private static IList<City> _allCities;
        private static IList<Status> _allStatuses;
        private static IList<Owner> _allOwners;
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
                _selectedtags = new HashSet<Tag>();
                _allCities = DbAccess.GetCities();
                _allStatuses = DbAccess.GetStatus();
                _allOwners = DbAccess.GetOwners();
                AppendData();
            }

        }

        private void AppendData()
        {
            ddlTags.DataSource = _allTags;
            ddlTags.DataValueField = "Id";
            ddlTags.DataTextField = "Name";
            ddlTags.DataBind();

            ddlCity.DataSource = _allCities;
            ddlCity.DataValueField = "Id";
            ddlCity.DataTextField = "Name";
            ddlCity.DataBind();

            ddlStatus.DataSource = _allStatuses;
            ddlStatus.DataValueField = "Id";
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataBind();

            ddlOwner.DataSource = _allOwners;
            ddlOwner.DataValueField = "Id";
            ddlOwner.DataTextField = "Name";
            ddlOwner.DataBind();
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            DbAccess.AddApartment(new Apartment
            {
                Name = txtName.Text,
                NameEng = txtEngName.Text,
                Address = txtAddress.Text,
                MaxAdults = int.Parse(txtAdults.Text),
                MaxChildren = int.Parse(txtChildren.Text),
                City = ddlCity.SelectedItem.Text,
                Owner = ddlOwner.SelectedItem.Text,
                Status = ddlStatus.SelectedItem.Text,
                Price = decimal.Parse(txtPrice.Text),
                TotalRooms = int.Parse(txtRoomsCount.Text),
                BeachDistance = int.Parse(txtBeachDistance.Text)
            });
            _selectedtags.ToList().ForEach(t => DbAccess.AddTaggedApartment(txtName.Text, t.Name));
            _selectedtags.Clear();
            Response.Redirect("Apartments.aspx");
        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            _selectedtags.Clear();
            _allCities.Clear();
            _allOwners.Clear();
            _allStatuses.Clear();
            _selectedtags.Clear();
            Response.Redirect("Apartments.aspx");
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            Tag t = _allTags.FirstOrDefault(x => x.Name == ddlTags.SelectedItem.Text);

            if (_selectedtags.Add(t))
            {
                _allTags.Remove(t);
            }

            if (_allTags.Count == 0)
            {
                ddlTags.Visible = false;
                btnAddTag.Visible = false;
                lblTags.Visible = false;
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
            LinkButton button = sender as LinkButton;
            string name = button.CommandArgument;
            Tag t = _selectedtags.FirstOrDefault(x => x?.Name == name);
            _allTags.Add(t);
            _selectedtags.Remove(t);

            if (_allTags.Count > 0)
            {
                ddlTags.Visible = true;
                btnAddTag.Visible = true;
                lblTags.Visible = true;
            }

            ddlTags.DataSource = _allTags;
            ddlTags.DataValueField = "Id";
            ddlTags.DataTextField = "Name";
            ddlTags.DataBind();

            rptTags.DataSource = _selectedtags;
            rptTags.DataBind();
        }
    }
}