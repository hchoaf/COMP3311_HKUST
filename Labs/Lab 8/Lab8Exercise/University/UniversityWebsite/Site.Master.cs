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
using static UniversityWebsite.Global;

namespace UniversityWebsite
{
    public partial class SiteMaster : MasterPage
    {
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
            UniversityRole role = UniversityRole.None;

            // Hide all menu items.
            // Hide admin menu items.
            liAdminDropDown.Visible = false;
            liCreateStudentRecord.Visible = false;
            liSearchStudentRecord.Visible = false;
            liDisplayDepartmentStudentRecords.Visible = false;
            // Hide student menu items.
            liEnrollInCourses.Visible = false;
            liDisplayEnrolledCourses.Visible = false;

            string userId = HttpContext.Current.User.Identity.GetUserId();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (userId != null)
            {
                if (manager.IsInRole(userId, "Admin")) { role = UniversityRole.Admin; }
                if (manager.IsInRole(userId, "Student")) { role = UniversityRole.Student; }
            }
            {
                switch (role)
                {
                    case UniversityRole.Admin:
                        // Show admin menu items.
                        liAdminDropDown.Visible = true;
                        liCreateStudentRecord.Visible = true;
                        liSearchStudentRecord.Visible = true;
                        liDisplayDepartmentStudentRecords.Visible = true;
                        break;
                    case UniversityRole.Student:
                        // Show student menu items.
                        liEnrollInCourses.Visible = true;
                        liDisplayEnrolledCourses.Visible = true;
                        break;
                    case UniversityRole.None:
                        break;
                }
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}