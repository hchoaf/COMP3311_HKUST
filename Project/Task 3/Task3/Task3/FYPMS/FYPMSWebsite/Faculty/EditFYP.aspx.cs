using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using FYPMSWebsite.App_Code;
using static FYPMSWebsite.Global;

namespace FYPMSWebsite.Faculty
{
    public partial class EditFYP : System.Web.UI.Page
    {
        //***********************************
        // Uses TODO 02, 03, 04, 06, 07, 08 *
        //***********************************
        private readonly FYPMSDB myFYPMSDB = new FYPMSDB();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly SharedDBAccess mySharedDBAccess = new SharedDBAccess();
        private readonly string loggedinUsername = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private bool IsInterestIndicated(string fypId)
        {
            bool result = false;
            //***************
            // Uses TODO 02 *
            //***************
            DataTable dtInterestedIn = myFYPMSDB.GetInterestedInFYP(fypId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "FYPID", "GROUPID", "PRIORITY" };

            // Determine whether the result is valid.
            if (myHelpers.IsQueryResultValid("TODO 02", dtInterestedIn, attributeList, lblResultMessage))
            {
                // If a tuple exists for the FYP, return true.
                if (dtInterestedIn.Rows.Count != 0) { result = true; }
            }
            else // An SQL errpr occurred.
            {
                pnlFYPInfo.Visible = false;
                hfSQLError.Value = "true";
            }
            return result;
        }

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

        private bool PopulateFYPCategoryDropdownList()
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

        private void PopulateFYPInformation(string fypId)
        {
            // Cannot edit an FYP if any group has indicated an interest in it.
            if (!IsInterestIndicated(fypId))
            {
                if (PopulateCosupervisor() && PopulateFYPCategoryDropdownList())
                {
                    DataTable dtFYPDetails = mySharedDBAccess.GetFYPDetails(fypId, lblResultMessage);

                    // Get the FYP information; save the result in ViewState and display it if the query is valid.
                    if (dtFYPDetails != null)
                    {
                        if (dtFYPDetails.Rows.Count != 0)
                        {
                            ViewState["fypId"] = dtFYPDetails.Rows[0]["FYPID"].ToString().Trim();
                            ViewState["oldTitle"] = txtTitle.Text = dtFYPDetails.Rows[0]["TITLE"].ToString().Trim();
                            ViewState["oldDescription"] = txtDescription.Text = dtFYPDetails.Rows[0]["DESCRIPTION"].ToString().Trim();
                            ViewState["oldCategory"] = ddlCategory.SelectedValue = dtFYPDetails.Rows[0]["CATEGORY"].ToString().Trim();
                            ViewState["oldType"] = rblType.SelectedValue = dtFYPDetails.Rows[0]["TYPE"].ToString().Trim();
                            ViewState["oldOtherRequirements"] = txtRequirements.Text = dtFYPDetails.Rows[0]["OTHERREQUIREMENTS"].ToString().Trim();
                            ViewState["oldMinStudents"] = rblMinStudents.SelectedValue = dtFYPDetails.Rows[0]["MINSTUDENTS"].ToString().Trim();
                            ViewState["oldMaxStudents"] = rblMaxStudents.SelectedValue = dtFYPDetails.Rows[0]["MAXSTUDENTS"].ToString().Trim();
                            ViewState["oldIsAvailable"] = rblIsAvailable.SelectedValue = dtFYPDetails.Rows[0]["ISAVAILABLE"].ToString().Trim();

                            //***************
                            // Uses TODO 04 *
                            //***************
                            DataTable dtCosupervisor = myFYPMSDB.GetCosupervisorInfoForEdit(fypId, loggedinUsername);

                            // Attributes expected to be returned by the query result.
                            List<string> attributeList = new List<string> { "USERNAME" };

                            // Get the cosupervisor information; save the result in ViewState and display it if it is valid.
                            if (myHelpers.IsQueryResultValid("TODO 04", dtCosupervisor, attributeList, lblResultMessage))
                            {
                                if (dtCosupervisor.Rows.Count != 0)
                                {
                                    ViewState["oldCosupervisor"] = ddlCosupervisor.SelectedValue = dtCosupervisor.Rows[0]["USERNAME"].ToString();
                                }
                                else
                                {
                                    ViewState["oldCosupervisor"] = "";
                                }
                            }
                            else // An SQL error occurred.
                            {
                                // Clear the cosupervisor dropdown list.
                                ddlCosupervisor.Items.Clear();
                            }
                        }
                        else // Nothing to display. Should not happen.
                        {
                            myHelpers.DisplayMessage(lblResultMessage, "*** Database error: The selected FYP does not exist. Please rerun the Task3DB.sql script file.");
                        }
                    }
                }
            }
            else
            {
                if (hfSQLError.Value == "false") // No SQL error occurred.
                {
                    Session["canEditFYP"] = false;
                    Response.Redirect("~/Faculty/DisplayFYPs.aspx");
                }
            }
        }

        private bool ProjectInformationIsChanged(DataTable dtOldNewProjectValues)
        {
            bool result = false;
            foreach (DataRow row in dtOldNewProjectValues.Rows)
            {
                if (!row["oldValue"].ToString().Equals(row["newValue"].ToString()))
                {
                    result = true;
                }
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Faculty/DisplayProjects");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValid && hfSQLError.Value == "false")
            {
                lblResultMessage.Visible = false;
                string fypId = ViewState["fypId"].ToString();
                string oldCosupervisor = ViewState["oldCosupervisor"].ToString();
                string resultMessage = "You have not changed any information.";

                // Collect the updated FYP values.
                string newTitle = myHelpers.CleanInput(txtTitle.Text);
                string newDescription = myHelpers.CleanInput(txtDescription.Text);
                string newCategory = ddlCategory.SelectedValue;
                string newType = rblType.SelectedValue;
                string newOtherRequirements = myHelpers.CleanInput(txtRequirements.Text);
                string newMinStudents = rblMinStudents.SelectedValue;
                string newMaxStudents = rblMaxStudents.SelectedValue;
                string newIsAvailable = rblIsAvailable.SelectedValue;
                string newCosupervisor = ddlCosupervisor.SelectedValue;

                DataTable dtOldNewProjectValues = new DataTable();
                dtOldNewProjectValues.Columns.Add("oldValue", typeof(string));
                dtOldNewProjectValues.Columns.Add("newValue", typeof(string));
                // Collect the old and new FYP values into a DataTable.
                dtOldNewProjectValues.Rows.Add(new object[] { ViewState["oldTitle"].ToString(), newTitle });
                dtOldNewProjectValues.Rows.Add(new object[] { ViewState["oldDescription"].ToString(), newDescription });
                dtOldNewProjectValues.Rows.Add(new object[] { ViewState["oldCategory"].ToString(), newCategory });
                dtOldNewProjectValues.Rows.Add(new object[] { ViewState["oldType"].ToString(), newType });
                dtOldNewProjectValues.Rows.Add(new object[] { ViewState["oldOtherRequirements"].ToString(), newOtherRequirements });
                dtOldNewProjectValues.Rows.Add(new object[] { ViewState["oldMinStudents"].ToString(), newMinStudents });
                dtOldNewProjectValues.Rows.Add(new object[] { ViewState["oldMaxStudents"].ToString(), newMaxStudents });
                dtOldNewProjectValues.Rows.Add(new object[] { ViewState["oldIsAvailable"].ToString(), newIsAvailable });

                // Update the changed FYP information.
                if (ProjectInformationIsChanged(dtOldNewProjectValues) || oldCosupervisor != newCosupervisor)
                {
                    // *** Uses TODO 06, 07, 08 in App_Code\DBHelperMethods ***
                    if (myDBHelpers.UpdateFYP(fypId, newTitle, newDescription, newCategory, newType, newOtherRequirements,
                        newMinStudents, newMaxStudents, newIsAvailable, oldCosupervisor, newCosupervisor))
                    {
                        pnlFYPInfo.Visible = false; ;
                        resultMessage = "The information for - " + newTitle + " - has been updated.";
                    }
                    else // An SQL error occurred.
                    {
                        resultMessage = sqlError;
                    }
                }
                myHelpers.DisplayMessage(lblResultMessage, resultMessage);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["canEditFYP"] = true;
                PopulateFYPInformation(Request["fypId"]);
            }
        }

        protected void RblMaxStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblType.SelectedValue == "thesis")
            {
                rblMaxStudents.SelectedValue = "1";
            }
            cvMinMaxStudents.Validate();
        }

        protected void RblMinStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblType.SelectedValue == "thesis")
            {
                rblMinStudents.SelectedValue = "1";
            }
            cvMinMaxStudents.Validate();
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