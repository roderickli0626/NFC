using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace NFC
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context?.Items.Contains("DBContext") == false)
            {
                context.Items["DBContext"] = new MappingDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["MonsterConnectionString"].ConnectionString);
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context?.Items.Contains("DBContext") == true)
            {
                MappingDataContext dbContext = (MappingDataContext)context.Items["DBContext"];
                dbContext.Dispose();
            }
        }
    }
}