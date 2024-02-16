using NFC.Common;
using NFC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFC
{
    public partial class AccessLog1 : System.Web.UI.Page
    {
        Admin admin;
        LoginController loginSystem = new LoginController();
        protected void Page_Load(object sender, EventArgs e)
        {
            admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && !loginSystem.IsDoorManLoggedIn() && (admin == null))
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadTagType();
            }
        }

        private void LoadTagType()
        {
            ComboType.Items.Clear();
            ComboType.Items.Add(new ListItem("TUTTI", "0"));
            ComboType.Items.Add(new ListItem("TELECOMANDO", ((int)TagType.BUTTON).ToString()));
            ComboType.Items.Add(new ListItem("RFID", ((int)TagType.RFID).ToString()));
            ComboType.Items.Add(new ListItem("TAG", ((int)TagType.TAG).ToString()));
            ComboType.Items.Add(new ListItem("NFC", ((int)TagType.NFC).ToString()));
        }
    }
}