using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using FYPMSWebsite.App_Code;
using static FYPMSWebsite.Global;

namespace FYPMSWebsite.Coordinator
{
    public partial class AssignReaders : System.Web.UI.Page
    {
        //****************************
        // Uses TODO 03, 37, 38, 39 *
        //***************************
        private readonly FYPMSDB myFYPMSDB = new FYPMSDB();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly SharedDBAccess mySharedDBAccess = new SharedDBAccess();

        /***** Private Methods *****/

        private void PopulateAvailableReaders(string fypId)
        {
            //*** Uses TODO 03 in SharedAccess.cs ***
            DataTable dtAvailableReaders = mySharedDBAccess.GetFaculty(lblResultMessage);

            // Display the query result if it is valid.
            if (dtAvailableReaders != null)
            {
                DataTable dtSupervisors = mySharedDBAccess.GetFYPSupervisors(fypId, lblResultMessage);
                if (dtSupervisors != null)
                {
                    if (dtSupervisors.Rows.Count != 0)
                    {
                        foreach (DataRow row in dtSupervisors.Rows)
                        {
                            // Exclude the faculty who are the supervisors of the project.
                            dtAvailableReaders = myHelpers.RemoveSupervisor(dtAvailableReaders, row["USERNAME"].ToString().Trim());
                        }
                    }
                    gvAvailableReaders.DataSource = dtAvailableReaders;
                    gvAvailableReaders.DataBind();
                    gvFYPsWithoutReaders.DataSource = ViewState["dtProjectsWithoutReaders"] as DataTable;
                    gvFYPsWithoutReaders.DataBind();
                    pnlAssignReader.Visible = true;
                }
            }
        }

        private void PopulateFYPsWithoutReaders()
        {
            //***************
            // Uses TODO 37 *
            //***************
            DataTable dtFYPsWithoutReaders = myFYPMSDB.GetFYPsWithoutReaders();

            // Attributes expected to be returned by the query result.
            List<string> attributeList = new List<string> { "GROUPID", "GROUPCODE", "ASSIGNEDFYP", "TITLE", "CATEGORY", "TYPE" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid("TODO 37", dtFYPsWithoutReaders, attributeList, lblResultMessage))
            {
                if (dtFYPsWithoutReaders.Rows.Count != 0)
                {
                    ViewState["dtProjectsWithoutReaders"] = dtFYPsWithoutReaders;
                    gvFYPsWithoutReaders.DataSource = dtFYPsWithoutReaders;
                    gvFYPsWithoutReaders.DataBind();
                    pnlDisplayFYPsWithoutReaders.Visible = true;
                }
                else // No projects without readers.
                {
                    myHelpers.DisplayMessage(lblResultMessage, "There are no projects that require a reader.");
                    pnlDisplayFYPsWithoutReaders.Visible = false;
                }
            }
        }

        /***** Protected Methods *****/

        protected void GvAvailableReaders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 3)
            {
                // Offset by 1 due to Select column.
                int usernameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "USERNAME", lblResultMessage) + 1;

                if (usernameColumn != 0)
                {
                    e.Row.Cells[usernameColumn].Visible = false;
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        TableHeaderCell headerCell = new TableHeaderCell { Text = "FYPS ASSIGNED" };
                        e.Row.Cells.Add(headerCell);
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        TableCell dataCell = new TableCell();

                        //***************
                        // Uses TODO 38 *
                        //***************
                        decimal numberAssigned = myFYPMSDB.NumberOfFYPsAssignedToReader(e.Row.Cells[usernameColumn].Text);

                        if (numberAssigned != -1)
                        {
                            dataCell.Text = numberAssigned.ToString();
                            dataCell.HorizontalAlign = HorizontalAlign.Center;
                        }
                        else
                        {
                            dataCell.Text = "";
                            myHelpers.DisplayMessage(lblResultMessage, sqlError);
                        }
                        e.Row.Cells.Add(dataCell);
                    }
                }
            }
        }

        protected void GvAvailableReaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            string facultyUsername = gvAvailableReaders.SelectedRow.Cells[1].Text; // The username is column 1 in gvAvailableReaders.
            string groupId = gvFYPsWithoutReaders.SelectedRow.Cells[1].Text; // The group id is column 1 in gvFYPsWithoutReaders.

            //***************
            // Uses TODO 39 *
            //***************
            if (myFYPMSDB.AssignReaderToFYP(groupId, facultyUsername))
            {
                pnlAssignReader.Visible = false;
                PopulateFYPsWithoutReaders();
            }
            else // An SQL error occurred.
            {
                PopulateAvailableReaders(ViewState["fypId"].ToString());
                myHelpers.DisplayMessage(lblResultMessage, sqlError);
            }
        }

        protected void GvFYPsWithoutReaders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 7)
            {
                // Offset by 1 due to Select column.
                int groupIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "GROUPID", lblResultMessage) + 1; // Column 1.
                int groupCodeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "GROUPCODE", lblResultMessage) + 1; // Column 2.
                int assignedFYPIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "ASSIGNEDFYP", lblResultMessage) + 1; // Column 3.

                if (groupIdColumn != 0 && groupCodeColumn != 0 && assignedFYPIdColumn != 0)
                {
                    e.Row.Cells[groupIdColumn].Visible = false;
                    e.Row.Cells[assignedFYPIdColumn].Visible = false;
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "GROUPCODE", "GROUP CODE");
                        TableHeaderCell headerCell = new TableHeaderCell { Text = "SUPERVISOR(S)" };
                        e.Row.Cells.Add(headerCell);
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[groupCodeColumn].HorizontalAlign = HorizontalAlign.Center;
                        TableCell dataCell = new TableCell { Text = "" };
                        string fypId = e.Row.Cells[assignedFYPIdColumn].Text;
                        DataTable dtSupervisors = mySharedDBAccess.GetFYPSupervisors(fypId, lblResultMessage);
                        if (dtSupervisors != null)
                        {
                            dataCell.Text = myHelpers.SupervisorsToString(dtSupervisors);
                        }
                        e.Row.Cells.Add(dataCell);
                    }
                }
            }
        }

        protected void GvFYPsWithoutReaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResultMessage.Visible = false;
            txtGroupCode.Text = gvFYPsWithoutReaders.SelectedRow.Cells[2].Text; // Group code is column 3.
            string fypId = gvFYPsWithoutReaders.SelectedRow.Cells[3].Text; // FYP id is column 3.
            txtTitle.Text = gvFYPsWithoutReaders.SelectedRow.Cells[4].Text; // FYP title is column 4.
            ViewState["fypId"] = fypId;
            PopulateAvailableReaders(fypId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateFYPsWithoutReaders();
            }
        }
    }
}