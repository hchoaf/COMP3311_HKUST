using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FYPMSWebsite
{
    public partial class SiteMaster : MasterPage
    {
        private enum Role { Coordinator, Faculty, Student, None }
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Role role = Role.None;

            // Show homepage menu items.
            liShowFYPs.Visible = true;
            // Hide other menu items.
            // Hide Faculty menu items.
            liCreateFYP.Visible = false;
            liDisplayEditFYP.Visible = false;
            liAssignGroup.Visible = false;
            liAssignGrades.Visible = false;
            // Hide FYP Coordinator menu items.
            liAssignReader.Visible = false;
            liDisplayReaders.Visible = false;
            // Hide Student menu items.
            liAvailableFYPs.Visible = false;
            liSelectedFYPs.Visible = false;
            liManageGroup.Visible = false;
            liViewGrades.Visible = false;

            string userId = HttpContext.Current.User.Identity.GetUserId();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (userId != null)
            {
                if (manager.IsInRole(userId, "Coordinator")) { role = Role.Coordinator; }
                if (manager.IsInRole(userId, "Faculty")) { role = Role.Faculty; }
                if (manager.IsInRole(userId, "Student")) { role = Role.Student; }
            }

            switch (role)
            {
                case Role.Coordinator:
                    // Hide homepage menu items.
                    liShowFYPs.Visible = false;
                    // Show Coordinator menu items.
                    liAssignReader.Visible = true;
                    liDisplayReaders.Visible = true;
                    break;
                case Role.Faculty:
                    // Hide homepage menu items.
                    liShowFYPs.Visible = false;
                    // Show Faculty menu items.
                    liCreateFYP.Visible = true;
                    liDisplayEditFYP.Visible = true;
                    liAssignGroup.Visible = true;
                    liAssignGrades.Visible = true;
                    Session["canEditFYP"] = true;
                    break;
                case Role.Student:
                    // Hide homepage menu items.
                    liShowFYPs.Visible = false;
                    // Show Student menu items.
                    liAvailableFYPs.Visible = true;
                    liSelectedFYPs.Visible = true;
                    liManageGroup.Visible = true;
                    liViewGrades.Visible = true;
                    break;
                case Role.None:
                    break;
            }
        }
    
        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}