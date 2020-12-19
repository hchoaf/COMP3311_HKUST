using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using FYPMSWebsite.App_Code;
using static FYPMSWebsite.Global;

namespace FYPMSWebsite.Student
{
    public partial class AvailableFYPs : System.Web.UI.Page
    {
        //*******************************
        // Uses TODO 24, 25, 26, 27, 28 *
        //*******************************
        private readonly FYPMSDB myFYPMSDB = new FYPMSDB();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly SharedDBAccess mySharedDBAccess = new SharedDBAccess();
        private readonly string loggedinUsername = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private void GetProjectsAvailableToSelect(string groupId)
        {
            lblResultMessage.Visible = false;

            // If the group id is empty, the student is not yet a member of any group.
            if (groupId != "")
            {
                // *** Uses TODO 26 in SharedDBAccess ***
                DataTable dtProjects = mySharedDBAccess.GetProjectGroupAvailableFYPDigests(groupId, lblResultMessage);

                // Display the query result if it is valid.
                if (dtProjects != null)
                {
                    if (dtProjects.Rows.Count != 0)
                    {
                        gvAvailableForSelection.DataSource = dtProjects;
                        gvAvailableForSelection.DataBind();
                        ViewState["groupId"] = groupId;
                        pnlFYPsAvailableForSelection.Visible = true;
                        pnlBtnSelectFYPs.Visible = true;
                    }
                    else // Nothing to display
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "All projects are closed.");
                    }
                }
            }
            else
            {
                myHelpers.DisplayMessage(lblResultMessage, "You have not yet joined a group.");
                pnlFormProjectGroup.Visible = true;
            }
        }

        /***** Protected Methods *****/

        protected void BtnUpdateFYPInterest_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string groupId = ViewState["groupId"].ToString();
                string fypId;
                string priority;

                // For each project for which a priority has been specified, get the priority and update the InterestedIn table.
                for (int i = 0; i < gvAvailableForSelection.Rows.Count; i++)
                {
                    DropDownList ddlPriority = (DropDownList)gvAvailableForSelection.Rows[i].FindControl("ddlPriority");
                    fypId = gvAvailableForSelection.Rows[i].Cells[2].Text;
                    if (ddlPriority.SelectedIndex != 0)
                    {
                        priority = ddlPriority.SelectedItem.Value;

                        //***************
                        // Uses TODO 28 *
                        //***************
                        if (!myFYPMSDB.IndicateInterestIn(fypId, groupId, priority))
                        {
                            // An SQL error occurred. Exit the insert.
                            myHelpers.DisplayMessage(lblResultMessage, sqlError);
                            return;
                        }
                    }
                }
                // Refresh the web form.
                Response.Redirect("~/Student/SelectedFYPs.aspx");
            }
        }

        protected void GvAvailableForSelection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 8)
            {
                // Offset by 2 due to Details and Priority columns.
                int fypIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "FYPID", lblResultMessage) + 2;
                int minStudentsColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MINSTUDENTS", lblResultMessage) + 2;
                int maxStudentsColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MAXSTUDENTS", lblResultMessage) + 2;

                if (fypIdColumn != 1 && minStudentsColumn != 1 && maxStudentsColumn != 1)
                {
                    e.Row.Cells[fypIdColumn].Visible = false;
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "MINSTUDENTS", "MIN");
                        myHelpers.RenameGridViewColumn(e, "MAXSTUDENTS", "MAX");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[minStudentsColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[maxStudentsColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // *** Uses TODO 24 in SharedDBAccess ***
            string groupId = mySharedDBAccess.GetStudentGroupId(loggedinUsername, lblResultMessage);

            if (groupId != "SQL_ERROR")
            {
                if (!IsPostBack)
                {
                    // If the group id is empty, the student is not yet a member of any group.
                    if (groupId != "")
                    {
                        // *** Uses TODO 25 in SharedDBAccess ***
                        string isAssigned = mySharedDBAccess.IsGroupAssigned(groupId, lblResultMessage);
                        if (isAssigned != "SQL_ERROR")
                        {
                            if (isAssigned == "false")
                            {
                                GetProjectsAvailableToSelect(groupId);
                            }
                            else // Group is already assigned to a project
                            {
                                //***************
                                // Uses TODO 27 *
                                //***************
                                DataTable dtProjectAssigned = myFYPMSDB.GetFYPAssignedToGroup(groupId);

                                // Attributes expected to be returned by the query result.
                                var attributeList = new List<string> { "TITLE" };

                                // Display the query result if it is valid.
                                if (myHelpers.IsQueryResultValid("TODO 27", dtProjectAssigned, attributeList, lblResultMessage))
                                {
                                    if (dtProjectAssigned.Rows.Count != 0)
                                    {
                                        myHelpers.DisplayMessage(lblResultMessage, "Your group is already assigned to FYP - " + dtProjectAssigned.Rows[0]["TITLE"].ToString() + ".");
                                    }
                                    else // Nothing returned; should not happen!
                                    {
                                        myHelpers.DisplayMessage(lblResultMessage, "*** Database error: There is no group with group id " + groupId + ". Please rerun the Task3DB.sql script file.");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "You have not yet joined a group.");
                        pnlFormProjectGroup.Visible = true;
                    }
                }
            }
        }
    }
}