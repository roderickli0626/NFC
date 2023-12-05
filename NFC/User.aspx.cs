using NFC.Common;
using NFC.Controller;
using NFC.DAO;
using NFC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFC
{
    public partial class User1 : System.Web.UI.Page
    {
        Admin admin;
        LoginController loginSystem = new LoginController();
        protected void Page_Load(object sender, EventArgs e)
        {
            admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null))
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadTagType();
                LoadPlace();
            }
        }
        private void LoadPlace()
        {
            List<Place> places = new PlaceDAO().FindAll();
            ControlUtil.DataBind(ComboPlace, places, "Id", "PlaceTitle");
        }
        private void LoadTagType()
        {
            ComboType.Items.Clear();
            ComboType.Items.Add(new ListItem("TUTTI", "0"));
            ComboType.Items.Add(new ListItem("BUTTON", ((int)TagType.BUTTON).ToString()));
            ComboType.Items.Add(new ListItem("RFID", ((int)TagType.RFID).ToString()));
            ComboType.Items.Add(new ListItem("TAG", ((int)TagType.TAG).ToString()));
            ComboType.Items.Add(new ListItem("NFC", ((int)TagType.NFC).ToString()));

            ComboType1.Items.Clear();
            ComboType1.Items.Add(new ListItem("", ""));
            ComboType1.Items.Add(new ListItem("BUTTON", ((int)TagType.BUTTON).ToString()));
            ComboType1.Items.Add(new ListItem("RFID", ((int)TagType.RFID).ToString()));
            ComboType1.Items.Add(new ListItem("TAG", ((int)TagType.TAG).ToString()));
            ComboType1.Items.Add(new ListItem("NFC", ((int)TagType.NFC).ToString()));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            string name = TxtName.Text;
            string surname = TxtSurname.Text;
            string email = TxtEmail.Text;
            string address = TxtAddress.Text;
            string city = TxtCity.Text;
            string phone = TxtPhone.Text;
            string mobile = TxtMobile.Text;
            int? type = ControlUtil.GetSelectedValue(ComboType1);
            string note = TxtNote.Text;

            int? userID = ParseUtil.TryParseInt(HfUserID.Value);

            bool success = new UserController().SaveUser(userID, name, surname, email, address, city, phone, mobile, type, note);
            if (success)
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            else
            {
                ServerValidator.IsValid = false;
                return;
            }
        }
    }
}