using System;
using System.Data;
using FYPMSWebsite.App_Code;

namespace FYPMSWebsite
{
    public partial class DisplayFYPDetails : System.Web.UI.Page
    {
        private HelperMethods myHelpers = new HelperMethods();
        private SharedDBAccess mySharedDBAccess = new SharedDBAccess();

        /***** Protected Methods *****/

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request["returnUrl"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string fypId = Request["fypId"];

            if (fypId != "")
            {
                DataTable dtProject = mySharedDBAccess.GetFYPDetails(fypId, lblResultMessage);

                // Get the project information and display it if it is valid.
                if (dtProject != null)
                {
                    if (dtProject.Rows.Count != 0)
                    {
                        txtTitle.Text = dtProject.Rows[0]["TITLE"].ToString().Trim();
                        txtDescription.Text = dtProject.Rows[0]["DESCRIPTION"].ToString().Trim();
                        txtCategory.Text = dtProject.Rows[0]["CATEGORY"].ToString().Trim();
                        txtType.Text = dtProject.Rows[0]["TYPE"].ToString().Trim();
                        txtRequirements.Text = dtProject.Rows[0]["OTHERREQUIREMENTS"].ToString().Trim();
                        txtMinStudents.Text = dtProject.Rows[0]["MINSTUDENTS"].ToString().Trim();
                        txtMaxStudents.Text = dtProject.Rows[0]["MAXSTUDENTS"].ToString().Trim();

                        DataTable dtSupervisors = mySharedDBAccess.GetFYPSupervisors(fypId, lblResultMessage);

                        if (dtSupervisors != null)
                        {
                            if (dtSupervisors.Rows.Count != 0)
                            {
                                txtSupervisor.Text = myHelpers.SupervisorsToString(dtSupervisors);
                            }
                            else // Nothing to display.
                            {
                                myHelpers.DisplayMessage(lblResultMessage, "There are no supervisors for this project.");
                            }
                        }
                    }
                    else // Nothing to display.
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "There are no projects.");
                    }
                }
            }
        }
    }
}