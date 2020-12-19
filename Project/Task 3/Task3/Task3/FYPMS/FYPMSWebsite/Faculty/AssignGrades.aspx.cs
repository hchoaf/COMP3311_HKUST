using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using FYPMSWebsite.App_Code;
using static FYPMSWebsite.Global;

namespace FYPMSWebsite.Faculty
{
    public partial class AssignGrades : Page
    {
        //*******************************************
        // Uses TODO 09, 17, 18, 19, 20, 21, 22, 23 *
        //*******************************************
        private readonly FYPMSDB myFYPMSDB = new FYPMSDB();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly SharedDBAccess mySharedDBAccess = new SharedDBAccess();
        private readonly string loggedinUsername = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private DataTable GetRequirementGrades(string groupId)
        {
            string TODO;
            DataTable dtRequirementGrades;
            if (rblGraderRole.SelectedValue == "supervisor")
            {
                string fypId = (ViewState["dtGroups"] as DataTable).Rows[Convert.ToInt16(ddlGroups.SelectedIndex) - 1]["ASSIGNEDFYP"].ToString();
                TODO = "TODO 18";
                //***************
                // Uses TODO 18 *
                //***************
                dtRequirementGrades = myFYPMSDB.GetSupervisorRequirementGrades(groupId, fypId);
            }
            else // Grader role is reader.
            {
                TODO = "TODO 19";
                //***************
                // Uses TODO 19 *
                //***************
                dtRequirementGrades = myFYPMSDB.GetReaderRequirementGrades(groupId, loggedinUsername);
            }

            // Attributes expected to be returned by the query result.
            List<string> attributeList = new List<string> { "USERNAME", "NAME", "PROPOSALREPORT", "PROGRESSREPORT", "FINALREPORT", "PRESENTATION" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid(TODO, dtRequirementGrades, attributeList, lblResultMessage))
            {
                return dtRequirementGrades;
            }
            else
            {
                return null;
            }
        }

        private bool GradeIsValid(string gradeName, string grade)
        {
            if (lblGradeErrorMessage.Visible == false)
            {
                if (myHelpers.IsValidAndInRange(grade, 0, 100) || grade == "")
                {
                    return true;
                }
                else
                {
                    myHelpers.DisplayMessage(lblGradeErrorMessage, "Please enter a valid " + gradeName + " grade.");
                    lblGradeErrorMessage.Visible = true;
                }
            }
            return false;
        }

        private void PopulateRequirementGrades(string groupId)
        {
            DataTable dtRequirementGrades = GetRequirementGrades(groupId);

            if (dtRequirementGrades != null)
            {
                if (dtRequirementGrades.Rows.Count != 0)
                {
                    gvStudents.DataSource = dtRequirementGrades;
                    gvStudents.DataBind();
                    pnlGroupMembers.Visible = true;
                }
                else // No Requirement records found; create requirement records for each student in the group.
                {
                    // Uses TODO 20 in App_code\SharedDBAccess ***
                    DataTable dtGroupMembers = mySharedDBAccess.GetProjectGroupMembers(groupId, lblResultMessage);

                    if (dtGroupMembers != null)
                    {
                        if (dtGroupMembers.Rows.Count != 0)
                        {
                            // *** Uses TODO 21 in App_Code\DBHelperMethods ***
                            if (myDBHelpers.CreateRequirementGradesRecord(loggedinUsername, dtGroupMembers))
                            {
                                PopulateRequirementGrades(groupId);
                            }
                            else // An SQL error occurred.
                            {
                                myHelpers.DisplayMessage(lblResultMessage, sqlError);
                                pnlGroupMembers.Visible = false;
                            }
                        }
                        else
                        {
                            myHelpers.DisplayMessage(lblResultMessage, "*** Database error: There are no members in the selected project group. Please rerun the Task3DB.sql script file.");
                            pnlGroupMembers.Visible = false;
                        }
                    }
                }
            }
        }

        private void PopulateProjectGroupsDropDownList()
        {
            lblResultMessage.Visible = false;
            pnlGroupMembers.Visible = false;
            DataTable dtGroups;

            if (rblGraderRole.SelectedValue == "supervisor")
            {
                // *** Uses TODO 09 in GetFacultyGroups in App_Code/SharedDBAccess *** ***
                dtGroups = mySharedDBAccess.GetSupervisorProjectGroups(loggedinUsername, lblResultMessage);
            }
            else // Reader role.
            {
                //***************
                // Uses TODO 17 *
                //***************
                dtGroups = myFYPMSDB.GetReaderProjectGroups(loggedinUsername);

                // Attributes expected to be returned by the query result.
                List<string> attributeList = new List<string> { "GROUPID", "GROUPCODE", "ASSIGNEDFYP" };

                // Set dtGroups to null if the query result is not valid.
                if (!myHelpers.IsQueryResultValid("TODO 17", dtGroups, attributeList, lblResultMessage))
                { dtGroups = null; }
            }

            if (dtGroups != null)
            {
                if (dtGroups.Rows.Count != 0)
                {
                    ddlGroups.DataSource = dtGroups;
                    ddlGroups.DataValueField = "GROUPID";
                    ddlGroups.DataTextField = "GROUPCODE";
                    ddlGroups.DataBind();
                    ddlGroups.Items.Insert(0, "-- Select Group --");
                    ViewState["dtGroups"] = dtGroups;
                    pnlSelectGroup.Visible = true;
                }
                else // Nothing to display.
                {
                    if (rblGraderRole.SelectedValue == "supervisor")
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "There are no project groups for you to grade as supervisor.");
                    }
                    else
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "There are no project groups for you to grade as reader.");
                    }
                    pnlSelectGroup.Visible = false;
                }
            }
        }

        private void UpdateGrades(string fypId, string studentUsername, string proposalReport, string progressReport, string finalReport, string presentation)
        {
            if (rblGraderRole.SelectedValue == "supervisor")
            {
                //***************
                // Uses TODO 22 *
                //***************
                if (myFYPMSDB.UpdateSupervisorGrades(fypId, studentUsername, proposalReport, progressReport, finalReport, presentation))
                {
                    gvStudents.EditIndex = -1;
                    PopulateRequirementGrades(ddlGroups.SelectedValue);
                }
                else
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
            }
            else // Grader role is reader.
            {
                //***************
                // Uses TODO 23 *
                //***************
                if (myFYPMSDB.UpdateReaderGrades(loggedinUsername, studentUsername, proposalReport, progressReport, finalReport, presentation))
                {
                    gvStudents.EditIndex = -1;
                    PopulateRequirementGrades(ddlGroups.SelectedValue);
                }
                else
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
            }
        }

        /***** Protected Methods *****/

        protected void DdlGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroups.SelectedIndex != 0)
            {
                lblResultMessage.Visible = false;
                PopulateRequirementGrades(ddlGroups.SelectedValue);
            }
            else
            {
                pnlGroupMembers.Visible = false;
            }
        }

        protected void GvStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStudents.EditIndex = -1;
            lblGradeErrorMessage.Visible = false;
            pnlGroupMembers.Visible = false;
            PopulateRequirementGrades(ddlGroups.SelectedValue);
        }

        protected void GvStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 7)
            {
                int proposalReportColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PROPOSALREPORT", lblResultMessage) + 1;
                int progressReportColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PROGRESSREPORT", lblResultMessage) + 1;
                int finalReportColumn = myHelpers.GetGridViewColumnIndexByName(sender, "FINALREPORT", lblResultMessage) + 1;
                int presentationColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PRESENTATION", lblResultMessage) + 1;

                if (proposalReportColumn != -1 && progressReportColumn != -1 && finalReportColumn != -1 && presentationColumn != -1)
                {
                    e.Row.Cells[1].Visible = false;
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "PROPOSALREPORT", "PROPOSAL");
                        myHelpers.RenameGridViewColumn(e, "PROGRESSREPORT", "PROGRESS");
                        myHelpers.RenameGridViewColumn(e, "FINALREPORT", "FINAL");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[proposalReportColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[progressReportColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[finalReportColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[presentationColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        protected void GvStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStudents.EditIndex = e.NewEditIndex;
            PopulateRequirementGrades(ddlGroups.SelectedValue);
        }

        protected void GvStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Page.IsValid)
            {
                lblGradeErrorMessage.Visible = false;
                string fypId = (ViewState["dtGroups"] as DataTable).Rows[Convert.ToInt16(ddlGroups.SelectedIndex) - 1]["ASSIGNEDFYP"].ToString();

                GridViewRow row = gvStudents.Rows[e.RowIndex];
                string studentUsername = (row.Cells[1].Controls[0] as TextBox).Text;
                string proposalReport = (row.Cells[3].Controls[0] as TextBox).Text;
                string progressReport = (row.Cells[4].Controls[0] as TextBox).Text;
                string finalReport = (row.Cells[5].Controls[0] as TextBox).Text;
                string presentation = (row.Cells[6].Controls[0] as TextBox).Text;

                // Check if all grades are valid.
                if (GradeIsValid("proposal", proposalReport) && GradeIsValid("progress", progressReport)
                    && GradeIsValid("final", finalReport) && GradeIsValid("presentation", presentation))
                {
                    if (proposalReport == "") { proposalReport = "null"; }
                    if (progressReport == "") { progressReport = "null"; }
                    if (finalReport == "") { finalReport = "null"; }
                    if (presentation == "") { presentation = "null"; }

                    UpdateGrades(fypId, studentUsername, proposalReport, progressReport, finalReport, presentation);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Initially the grader role is supervisor.
                rblGraderRole.SelectedValue = "supervisor";
                PopulateProjectGroupsDropDownList();
            }
        }

        protected void RblGraderRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProjectGroupsDropDownList();
        }
    }
}