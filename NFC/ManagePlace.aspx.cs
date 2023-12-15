using NFC.Controller;
using NFC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace NFC
{
    public partial class ManagePlace : System.Web.UI.Page
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
                
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) { return; }
            string title = TxtTitle.Text;
            string ipAddress = TxtIPAddress.Text;
            string note = TxtNote.Text;

            int? placeID = ParseUtil.TryParseInt(HfPlaceID.Value);

            bool success = new BasicController().SaveBasicPlace(placeID, title, ipAddress, note);
            if (success)
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            else
            {
                CustomValidator1.IsValid = false;
                return;
            }

        }
    }
}