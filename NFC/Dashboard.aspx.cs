using NFC.Controller;
using NFC.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFC
{
    public partial class Dashboard : System.Web.UI.Page
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
                LoadInfo();
            }
        }

        private void LoadInfo()
        {
            List<AccessLog> logs = new AccessLogDAO().FindAll();
            CurrentAccess.InnerText = logs.Where(l => l.AccessDate > DateTime.Now.AddHours(-1)).Count().ToString();
            TodayAccess.InnerText = logs.Where(l => l.AccessDate > DateTime.Now.AddDays(-1)).Count().ToString();
            WeekAccess.InnerText = logs.Where(l => l.AccessDate > DateTime.Now.AddDays(-7)).Count().ToString();
            MonthAccess.InnerText = logs.Where(l => l.AccessDate > DateTime.Now.AddMonths(-1)).Count().ToString();
        }
    }
}