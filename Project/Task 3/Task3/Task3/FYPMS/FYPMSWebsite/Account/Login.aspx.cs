using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using FYPMSWebsite.Models;
using System.Windows.Forms;
using System.Linq;
using FYPMSWebsite.App_Code;
using static FYPMSWebsite.Global;

namespace FYPMSWebsite.Account
{
    public partial class Login : Page
    {
        private DBHelperMethods myDBHelpers = new DBHelperMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            // RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            // ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            // OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            // if (!String.IsNullOrEmpty(returnUrl))
            // {
            //     RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            // }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Synchronize AspNetUsers and FYPMS databases.
                SynchLogin(Username.Text);

                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(Username.Text, Password.Text, false, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = "The username does not exist.";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }

        public void SynchLogin(string username)
        {
            IdentityResult roleResult = new IdentityResult();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // Set the role of the user.
            FYPRole role = myDBHelpers.GetUserRole(username);

            switch (role)
            {
                case FYPRole.Coordinator:
                    break;
                case FYPRole.Faculty:
                    ApplicationUser facultyUser = manager.FindByName(username);
                    // If the username is not in AspNetUsers, then add the username in the Faculty role.
                    if (facultyUser == null)
                    {
                        facultyUser = new ApplicationUser() { UserName = username };
                        IdentityResult result = manager.Create(facultyUser, "FYProject1#");
                        if (result.Succeeded)
                        {
                            roleResult = manager.AddToRole(facultyUser.Id, "Faculty");
                            if (!roleResult.Succeeded)
                            {
                                MessageBox.Show(roleResult.Errors.FirstOrDefault());
                            }
                        }
                        else
                        {
                            MessageBox.Show(result.Errors.FirstOrDefault());
                        }
                    }
                    break;
                case FYPRole.Student:
                    ApplicationUser studentUser = manager.FindByName(username);
                    // If the username is not in AspNetUsers, then add the username in the Faculty role.
                    if (studentUser == null)
                    {
                        studentUser = new ApplicationUser() { UserName = username };
                        IdentityResult result = manager.Create(studentUser, "FYProject1#");
                        if (result.Succeeded)
                        {
                            roleResult = manager.AddToRole(studentUser.Id, "Student");
                            if (!roleResult.Succeeded)
                            {
                                MessageBox.Show(roleResult.Errors.FirstOrDefault());
                            }
                        }
                        else
                        {
                            MessageBox.Show(result.Errors.FirstOrDefault());
                        }
                    }
                    break;
                case FYPRole.None:
                    ApplicationUser noUser = manager.FindByName(username);
                    // If the username is in AspNetUsers, then delete it.
                    if (noUser != null)
                    {
                        manager.Delete(noUser);
                    }
                    break;
            }
        }
    }
}