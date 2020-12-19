using FYPMSWebsite.App_Code;
using FYPMSWebsite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Windows.Forms;

namespace FYPMSWebsite
{
    public class Global : HttpApplication
    {
        public enum FYPRole { Coordinator, Faculty, Student, None };
        public static string sqlError;
        public static double maxGroups = 4;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}