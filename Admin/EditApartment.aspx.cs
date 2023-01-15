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
    public partial class EditApartment : System.Web.UI.Page
    {
        private static IList<City> _allCities;
        private static IList<Status> _allStatuses;
        private static IList<Owner> _allOwners;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["user"] != "admin")
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                _allCities = DbAccess.GetCities();
                _allStatuses = DbAccess.GetStatus();
                _allOwners = DbAccess.GetOwners();

                if (Session["apartment_id"] != null)
                {
                    int selected_id = (int)Session["apartment_id"];
                    AppendData();
                    FillForm(selected_id);
                }
            }
        }

        private void FillForm(int id)
        {
            Apartment a = DbAccess.GetApartmentById(id);

            txtName.Text = a.Name;
            txtEngName.Text = a.NameEng;
            txtAddress.Text = a.Address;
            txtBeachDistance.Text = a.BeachDistance.ToString();
            txtChildren.Text = a.MaxChildren.ToString();
            txtAdults.Text = a.MaxAdults.ToString();
            txtPrice.Text = a.Price.ToString();
            txtRoomsCount.Text = a.TotalRooms.ToString();
            ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByText(a.City));
            ddlOwner.SelectedIndex = ddlOwner.Items.IndexOf(ddlOwner.Items.FindByText(a.Owner));
            ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(a.Status));
        }

        private void AppendData()
        {
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

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            _allCities.Clear();
            _allOwners.Clear();
            _allStatuses.Clear();
            Response.Redirect("Apartments.aspx");
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            DbAccess.SaveApartment(new Apartment
            {
                Id = (int)Session["apartment_id"],
                Name = txtName.Text,
                NameEng = txtEngName.Text,
                Address = txtAddress.Text,
                MaxAdults = int.Parse(txtAdults.Text),
                MaxChildren = int.Parse(txtChildren.Text),
                City = ddlCity.SelectedItem.Text,
                Owner = ddlOwner.SelectedItem.Text,
                Status = ddlStatus.SelectedItem.Text,
                Price = decimal.Parse(txtPrice.Text),
                TotalRooms = int.Parse(txtRoomsCount.Text)
            });
            Session["apartment_id"] = null;
            Response.Redirect("Apartments.aspx");
        }
    }
}