using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using UniversityWebsite.App_Code;
using UniversityWebsite.Models;

namespace UniversityWebsite.Account
{
    public partial class Login : Page
    {
        private HelperMethods myHelperMethods = new HelperMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            // var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            /*if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }*/
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Synchronize users in AspNetUsers and FanClub databases.
                if (myHelperMethods.SynchLoginAndApplicationDatabases(Email.Text, FailureText))
                {
                    // Validate the user password
                    var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                    // This doen't count login failures towards account lockout
                    // To enable password failures to trigger lockout, change to shouldLockout: true
                    var result = signinManager.PasswordSignIn(Email.Text, Password.Text, false, shouldLockout: false);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                            break;
                        case SignInStatus.LockedOut:
                            Response.Redirect("/Account/Lockout");
                            break;
                        case SignInStatus.RequiresVerification:
                            Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                            Request.QueryString["ReturnUrl"],
                                                            RememberMe.Checked),
                                              true);
                            break;
                        case SignInStatus.Failure:
                        default:
                            FailureText.Text = "Invalid email.";
                            ErrorMessage.Visible = true;
                            break;
                    }
                }
                else
                {
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}