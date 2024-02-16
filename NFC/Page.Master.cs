using NFC.Controller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFC
{
    public partial class Page : System.Web.UI.MasterPage
    {
        private Admin admin;
        private LoginController loginSystem = new LoginController();
        protected void Page_Load(object sender, EventArgs e)
        {
            admin = loginSystem.GetCurrentUserAccount();

            if (loginSystem.IsSuperAdminLoggedIn())
            {
                AdminName.InnerText = "Super Admin";
                SubName.InnerText = "Super Admin";
                UserName.InnerText = "Super Admin";
                NavUserName.InnerText = "Super Admin";
            }
            else if (loginSystem.IsDoorManLoggedIn())
            {
                sidebar.Visible = false;
                contentPart.Attributes["class"] += " mx-auto";
                AdminName.InnerText = "DoorMan";
                SubName.InnerText = "DoorMan";
                UserName.InnerText = "DoorMan";
                NavUserName.InnerText = "DoorMan";
            }
            else
            {
                liAdmin.Visible = false;
                AdminName.InnerText = admin.Name;
                SubName.InnerText = "Admin";
                UserName.InnerText = admin.Name;
                NavUserName.InnerText = admin.Name;
            }

            SetMenuHighlight();
        }

        protected void SetMenuHighlight()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            if (path.Equals("/Admin.aspx"))
            {
                liAdmin.Attributes["class"] += " active";
            }
            else if (path.Equals("/User.aspx"))
            {
                liUser.Attributes["class"] += " active";
            }
            else if (path.Equals("/Dashboard.aspx"))
            {
                liDashboard.Attributes["class"] += " active";
            }
            else if (path.Equals("/AccessLog.aspx"))
            {
                liAccessLog.Attributes["class"] += " active";
            }
            else if (path.Equals("/ManagePlace.aspx"))
            {
                liPlace.Attributes["class"] += " active";
            }
        }
    }
}