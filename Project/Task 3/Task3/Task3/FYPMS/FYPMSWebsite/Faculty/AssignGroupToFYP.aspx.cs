using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using FYPMSWebsite.App_Code;
using static FYPMSWebsite.Global;

namespace FYPMSWebsite.Faculty
{
    public partial class AssignGroupToFYP : System.Web.UI.Page
    {
        //*******************************************
        // Uses TODO 09, 10, 11, 12, 13, 14, 15, 16 *
        //*******************************************
        private readonly FYPMSDB myFYPMSDB = new FYPMSDB();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly SharedDBAccess mySharedDBAccess = new SharedDBAccess();
        private readonly string loggedinUsername = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private string CreateGroupCode(string fypId)
        {
            string groupCode = "";

            //***************
            // Uses TODO 14 *
            //***************
            DataTable dtFacultyCodes = myFYPMSDB.GetFYPSupervisorFacultyCodes(fypId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "FACULTYCODE" };

            if (myHelpers.IsQueryResultValid("TODO 14", dtFacultyCodes, attributeList, lblResultMessage))
            {
                if (dtFacultyCodes.Rows.Count != 0)
                {
                    foreach (DataRow row in dtFacultyCodes.Rows)
                    {
                        groupCode += row["FACULTYCODE"].ToString().Trim();
                    }
                    // Determine the sequence number for the group code.
                    //***************
                    // Uses TODO 15 *
                    //***************
                    decimal sequenceCode = myFYPMSDB.GetFacultyCodeSequenceNumber(groupCode);

                    if (sequenceCode != -1)
                    {
                        groupCode += (sequenceCode + 1).ToString();
                    }
                    else // An SQL error occurred.
                    {
                        myHelpers.DisplayMessage(lblResultMessage, sqlError);
                        groupCode = "";
                    }
                }
                else // Nothing to display
                {
                    myHelpers.DisplayMessage(lblResultMessage, "*** Database error: There are no faculty that supervise the FYP with id " + fypId + ". Please rerun the Task3DB.sql script file.");
                    lblResultMessage.Visible = true;
                }
            }
            return groupCode;
        }

        private DataTable CreateGroupsAvailableForAssignmentGroupList(DataTable dtGroups)
        {
            // Create a new DataTable containing only one row for each group.
            DataTable dtGroupList = new DataTable();
            dtGroupList.Columns.Add("GROUPID", typeof(string));
            dtGroupList.Columns.Add("PRIORITY", typeof(string));
            dtGroupList.Columns.Add("MEMBERS", typeof(string));
            string previousGroupId = "";
            string groupId = "";
            string priority = "";
            string members = "";
            foreach (DataRow row in dtGroups.Rows)
            {
                groupId = row["GROUPID"].ToString();
                if (previousGroupId != groupId && previousGroupId != "")
                {
                    members = members.Remove(members.Length - 2);
                    dtGroupList.Rows.Add(new object[] { previousGroupId, priority, members });
                    members = "";
                }
                priority = row["PRIORITY"].ToString();
                members = members + row["NAME"].ToString() + ", ";
                previousGroupId = groupId;
            }
            members = members.Remove(members.Length - 2);
            dtGroupList.Rows.Add(new object[] { groupId, priority, members });
            return dtGroupList;
        }

        private DataTable CreateGroupsCurrentlyAssignedGroupList(DataTable dtGroups)
        {
            // Create a new DataTable containing only one row for each group.
            DataTable dtGroupList = new DataTable();
            dtGroupList.Columns.Add("GROUPID", typeof(string));
            dtGroupList.Columns.Add("CODE", typeof(string));
            dtGroupList.Columns.Add("MEMBERS", typeof(string));
            string previousGroupId = "";
            string groupId = "";
            string groupCode = "";
            string members = "";
            foreach (DataRow row in dtGroups.Rows)
            {
                groupId = row["GROUPID"].ToString();
                if (previousGroupId != groupId && previousGroupId != "")
                {
                    members = members.Remove(members.Length - 2);
                    dtGroupList.Rows.Add(new object[] { previousGroupId, groupCode, members });
                    members = "";
                }
                groupCode = row["GROUPCODE"].ToString();
                members = members + row["NAME"].ToString() + ", ";
                previousGroupId = groupId;
            }
            members = members.Remove(members.Length - 2);
            dtGroupList.Rows.Add(new object[] { groupId, groupCode, members });
            return dtGroupList;
        }

        public double GetNumberOfGroupsSupervised(string username, Label labelControl)
        {
            double result = 0;

            //*** Uses TODO 09 in SharedDBAccess ***
            DataTable dtGroups = mySharedDBAccess.GetSupervisorProjectGroups(username, labelControl);
            if (dtGroups != null)
            {
                if (dtGroups.Rows.Count != 0)
                {
                    foreach (DataRow row in dtGroups.Rows)
                    {
                        if (row["GROUPCODE"].ToString().Trim().Length == 3)
                        {
                            result += 1;
                        }
                        else
                        {
                            result += 0.5;
                        }
                    }
                }
            }
            return result;
        }

        private bool IsMaxGroups(double numGroups)
        {
            bool result = true;
            if (numGroups < maxGroups)
            {
                result = false;
            }
            else // Already supervising the maximum number of groups.
            {
                myHelpers.DisplayMessage(lblNumberAssigned, "You are supervising or cosupervising the maximum number of groups.");
                pnlGroupsAvailableForAssignment.Visible = false;
            }
            return result;
        }

        private void PopulateGroupsAvailableForAssignment(string fypId)
        {
            lblAvailableResultMessage.Visible = false;
            gvAvailableForAssignment.Visible = false;
            pnlGroupsAvailableForAssignment.Visible = false;
            pnlBtnAssignGroups.Visible = false;

            // Check if maximum allowed project groups has been reached.
            if (!IsMaxGroups(Convert.ToDouble(ViewState["numGroups"])))
            {
                // Check if the FYP is open.
                if (FYPIsAvailable(fypId))
                {
                    //***************
                    // Uses TODO 13 *
                    //***************
                    DataTable dtAvailableForAssignment = myFYPMSDB.GetGroupsAvailableToAssign(fypId);

                    // Attributes expected to be returned by the query result.
                    var attributeList = new List<string> { "GROUPID", "PRIORITY", "NAME", "USERNAME" };

                    // Display the query result if it is valid.
                    if (myHelpers.IsQueryResultValid("TODO 13", dtAvailableForAssignment, attributeList, lblResultMessage))
                    {
                        if (dtAvailableForAssignment.Rows.Count != 0)
                        {
                            ViewState["dtAvailableForAssignment"] = dtAvailableForAssignment;
                            dtAvailableForAssignment = CreateGroupsAvailableForAssignmentGroupList(dtAvailableForAssignment);
                            gvAvailableForAssignment.DataSource = dtAvailableForAssignment;
                            gvAvailableForAssignment.DataBind();
                            gvAvailableForAssignment.Visible = true;
                            pnlBtnAssignGroups.Visible = true;
                        }
                        else
                        {
                            myHelpers.DisplayMessage(lblAvailableResultMessage, "No groups are available to asssign to this FYP.");
                            lblAvailableResultMessage.Visible = true;
                        }
                        pnlGroupsAvailableForAssignment.Visible = true;
                    }
                }
                else // The FYP is closed.
                {
                    if (hfDBError.Value == "false")
                    {
                        myHelpers.DisplayMessage(lblAvailableResultMessage, "The FYP is closed.");
                        lblAvailableResultMessage.Visible = true;
                        pnlGroupsAvailableForAssignment.Visible = true;
                    }
                }
            }
        }

        private bool PopulateGroupsCurrentlyAssigned(string fypId)
        {
            bool result = false;
            lblAssignedResultMessage.Visible = false;
            gvCurrentlyAssigned.Visible = false;
            pnlGroupsCurrentlyAssigned.Visible = false;

            //***************
            // Uses TODO 11 *
            //***************
            DataTable dtCurrentlyAssigned = myFYPMSDB.GetGroupsCurrentlyAssigned(fypId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "GROUPID", "GROUPCODE", "NAME" };

            // Display the query result if it is not null.
            if (myHelpers.IsQueryResultValid("TODO 11", dtCurrentlyAssigned, attributeList, lblResultMessage))
            {
                if (dtCurrentlyAssigned.Rows.Count != 0)
                {
                    dtCurrentlyAssigned = CreateGroupsCurrentlyAssignedGroupList(dtCurrentlyAssigned);
                    gvCurrentlyAssigned.DataSource = dtCurrentlyAssigned;
                    gvCurrentlyAssigned.DataBind();
                    gvCurrentlyAssigned.Visible = true;
                }
                else // Nothing to display
                {
                    myHelpers.DisplayMessage(lblAssignedResultMessage, "No groups are assigned to this FYP.");
                    lblAssignedResultMessage.Visible = true;
                }
                result = true;
                pnlGroupsCurrentlyAssigned.Visible = true;
            }
            return result;
        }

        private void PopulateFYPDropDownList()
        {
            if (ShowNumberOfGroupsAssigned())
            {
                //***************
                // Uses TODO 10 *
                //***************
                DataTable dtFYPs = myFYPMSDB.GetFacultyFYPs(loggedinUsername);

                // Attributes expected to be returned by the query result.
                var attributeList = new List<string> { "FYPID", "TITLE" };

                // Display the query result if it is validl.
                if (myHelpers.IsQueryResultValid("TODO 11", dtFYPs, attributeList, lblResultMessage))
                {
                    if (dtFYPs.Rows.Count != 0)
                    {
                        ddlFYPs.DataSource = dtFYPs;
                        ddlFYPs.DataValueField = "fypId";
                        ddlFYPs.DataTextField = "title";
                        ddlFYPs.DataBind();
                        ddlFYPs.Items.Insert(0, "-- Select Project --");
                    }
                    else // Nothing to display.
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "You have not posted any FYPs.");
                        pnlSelectFYP.Visible = false;
                    }
                }
            }
        }

        private bool FYPIsAvailable(string fypId)
        {
            bool FYPIsAvailable = false;
            //***************
            // Uses TODO 12 *
            //***************
            DataTable dtFYPAvailability = myFYPMSDB.GetFYPAvailability(fypId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "ISAVAILABLE" };

            if (myHelpers.IsQueryResultValid("TODO 12", dtFYPAvailability, attributeList, lblResultMessage))
            {
                if (dtFYPAvailability.Rows.Count != 0)
                {
                    hfDBError.Value = "false";
                    if (dtFYPAvailability.Rows[0]["ISAVAILABLE"].ToString() == "Y")
                    {
                        FYPIsAvailable = true;
                    }
                }
                else // Nothing to display
                {
                    hfDBError.Value = "true";
                    myHelpers.DisplayMessage(lblResultMessage, "*** Database error: There is no FYP with fyp id " + fypId + ". Please rerun the Task3DB.sql script file.");
                    pnlGroupsAvailableForAssignment.Visible = false;
                    pnlGroupsCurrentlyAssigned.Visible = false;
                    pnlBtnAssignGroups.Visible = false;
                }
            }
            return FYPIsAvailable;
        }

        private bool ShowNumberOfGroupsAssigned()
        {
            bool result = false;
            double numGroups = GetNumberOfGroupsSupervised(loggedinUsername, lblResultMessage);

            if (numGroups != -1)
            {
                ViewState["numGroups"] = numGroups;
                if (!IsMaxGroups(numGroups))
                {
                    if (numGroups != 1)
                    {
                        myHelpers.DisplayMessage(lblNumberAssigned, "You are currently supervising " + numGroups + " groups.");
                    }
                    else
                    {
                        myHelpers.DisplayMessage(lblNumberAssigned, "You are currently supervising " + numGroups + " group.");
                    }
                }
                result = true;
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void BtnAssignGroups_Click(object sender, EventArgs e)
        {
            DataTable dtAvailableForAssignment = ViewState["dtAvailableForAssignment"] as DataTable;
            string fypId = ddlFYPs.SelectedValue;
            string groupId = "";

            // Determine if any groups were selected for assignment to FYPs.
            foreach (GridViewRow row in gvAvailableForAssignment.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkSelected") as CheckBox);
                    if (chkRow != null && chkRow.Checked)
                    {
                        // Get the group id.
                        groupId = row.Cells[1].Text;
                        // Construct the group code.
                        string groupCode = CreateGroupCode(fypId);
                        if (groupCode == "") { return; } // An SQL error occurred.

                        // Get the students in the group.
                        DataTable dtGroupMembers = dtAvailableForAssignment.Select("GROUPID=" + groupId).CopyToDataTable();

                        //***************
                        // Uses TODO 16 *
                        //***************
                        if (!myFYPMSDB.AssignGroupToFYP(groupId, groupCode, fypId))
                        {
                            myHelpers.DisplayMessage(lblResultMessage, sqlError);
                        }
                    }
                }
            }

            // If the groupCode is not empty, a group was selected.
            if (groupId != "")
            {
                // Show the result message and refresh the web form.
                if (ShowNumberOfGroupsAssigned())
                {
                    PopulateGroupsCurrentlyAssigned(fypId);
                    PopulateGroupsAvailableForAssignment(fypId);
                }
            }
            else // No group was selected.
            {
                myHelpers.DisplayMessage(lblResultMessage, "No group was selected for assignment.");
            }
        }

        protected void DdlFYPs_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResultMessage.Visible = false;
            if (ddlFYPs.SelectedIndex != 0)
            {
                if (PopulateGroupsCurrentlyAssigned(ddlFYPs.SelectedValue))
                {
                    PopulateGroupsAvailableForAssignment(ddlFYPs.SelectedValue);
                }
            }
            else
            {
                pnlGroupsCurrentlyAssigned.Visible = false;
                pnlGroupsAvailableForAssignment.Visible = false;
            }
        }

        protected void GvAvailableForAssignment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 4)
            {
                int selectColumn = 0;
                // Offset by 1 due to Select column.
                int groupIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "GROUPID", lblResultMessage) + 1;
                int priorityColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PRIORITY", lblResultMessage) + 1;

                if (groupIdColumn != 0 && priorityColumn != 0)
                {
                    e.Row.Cells[groupIdColumn].Visible = false;
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[selectColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[priorityColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        protected void GvCurrentlyAssigned_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 3)
            {
                int groupIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "GROUPID", lblResultMessage);
                int groupCodeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "CODE", lblResultMessage);

                if (groupIdColumn != -1 && groupCodeColumn != -1)
                {
                    e.Row.Cells[groupIdColumn].Visible = false;
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[groupCodeColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateFYPDropDownList();
            }
        }
    }
}