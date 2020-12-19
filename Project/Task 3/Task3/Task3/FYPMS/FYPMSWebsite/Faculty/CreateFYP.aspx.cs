using System;
using System.Data;
using System.Web;
using System.Web.UI;
using FYPMSWebsite.App_Code;
using static FYPMSWebsite.Global;

namespace FYPMSWebsite.Faculty
{
    public partial class CreateFYP : Page
    {
        //***********************
        // USES TODO 03, 05, 07 *
        //***********************
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly SharedDBAccess mySharedDBAccess = new SharedDBAccess();
        private readonly string loggedinUsername = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private bool PopulateCosupervisor()
        {
            bool result = false;
            // *** Uses TODO 03 in App_Code\SharedDBAccess ***
            DataTable dtPossibleCosupervisors = mySharedDBAccess.GetFaculty(lblResultMessage);

            // Populate the cosupervisor dropdown list if the result is valid.
            if (dtPossibleCosupervisors != null)
            {
                // Remove the existing supervisor from the list of potential cosupervisors.
                dtPossibleCosupervisors = myHelpers.RemoveSupervisor(dtPossibleCosupervisors, loggedinUsername);
                ddlCosupervisor.DataSource = dtPossibleCosupervisors;
                ddlCosupervisor.DataValueField = "USERNAME";
                ddlCosupervisor.DataTextField = "NAME";
                ddlCosupervisor.DataBind();
                ddlCosupervisor.Items.Insert(0, "     -- None --");
                ddlCosupervisor.Items.FindByText("     -- None --").Value = "";
                result = true;
            }
            return result;
        }

        private bool PopulateProjectCategory()
        {
            bool result = false;
            DataTable dtCategory = mySharedDBAccess.GetFYPCategories(lblResultMessage);

            // Populate the category dropdown list with the FYP categories if the query result is valid.
            if (dtCategory != null)
            {
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataValueField = "CATEGORY";
                ddlCategory.DataTextField = "CATEGORY";
                ddlCategory.DataBind();
                result = true;
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && hfSQLError.Value == "false")
            {
                lblResultMessage.Visible = false;
                // Collect the FYP information.
                string title = myHelpers.CleanInput(txtTitle.Text);
                string description = myHelpers.CleanInput(txtDescription.Text);
                string cosupervisor = ddlCosupervisor.SelectedValue;
                string category = ddlCategory.SelectedValue;
                string type = rblType.SelectedValue;
                string otherRequirements = myHelpers.CleanInput(txtOtherRequirements.Text);
                string minStudents = rblMinStudents.SelectedValue;
                string maxStudents = rblMaxStudents.SelectedValue;
                string isAvailable = rblIsAvailable.SelectedValue;
                string fypId = myDBHelpers.GetNextTableId("FYP", "fypId", lblResultMessage);

                if (fypId != "")
                {
                    //Create the FYP and Supervises records. *** Uses TODO 05, 07 in App_Code\DBHelperMethods ***
                    if (myDBHelpers.CreateFYP(fypId, title, description, category, type, 
                        otherRequirements, minStudents, maxStudents, isAvailable, loggedinUsername, cosupervisor))
                    {
                        pnlProjectInfo.Visible = false;
                        myHelpers.DisplayMessage(lblResultMessage, "The FYP - " + title + " - has been created.");
                    }
                    else // An SQL error occurred.
                    {
                        myHelpers.DisplayMessage(lblResultMessage, sqlError);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!PopulateCosupervisor())
                {
                    pnlProjectInfo.Visible = false;
                    hfSQLError.Value = "true"; return;
                }
                if (!PopulateProjectCategory())
                {
                    pnlProjectInfo.Visible = false;
                    hfSQLError.Value = "true";
                }
            }
        }

        protected void RblMaxStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblType.SelectedValue == "thesis")
            {
                rblMaxStudents.SelectedValue = "1";
            }
            CvMinMaxStudents.Validate();
        }

        protected void RblMinStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblType.SelectedValue == "thesis")
            {
                rblMinStudents.SelectedValue = "1";
            }
            CvMinMaxStudents.Validate();
        }

        protected void RblType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblType.SelectedValue == "thesis")
            {
                rblMinStudents.SelectedValue = "1";
                rblMaxStudents.SelectedValue = "1";
            }
        }
    }
}