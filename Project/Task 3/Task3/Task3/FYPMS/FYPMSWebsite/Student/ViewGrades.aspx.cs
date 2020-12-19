using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using FYPMSWebsite.App_Code;

namespace FYPMSWebsite.Student
{
    public partial class ViewGrades : System.Web.UI.Page
    {
        //*******************
        // Uses TODO 35, 36 *
        //*******************
        private readonly FYPMSDB myFYPMSDB = new FYPMSDB();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly SharedDBAccess mySharedDBAccess = new SharedDBAccess();
        private readonly string loggedinUsername = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private bool IsProjectAssigned(string username)
        {
            bool result = false;

            lblResultMessage.Visible = false;
            //***************
            // Uses TODO 35 *
            //***************
            DataTable dtFYPInfo = myFYPMSDB.GetAssignedFYPInformation(username);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "FYPID", "TITLE" };

            // Return the query result if it is valid.
            if (myHelpers.IsQueryResultValid("TODO 35", dtFYPInfo, attributeList, lblResultMessage))
            {
                if (dtFYPInfo.Rows.Count != 0)
                {
                    DataTable dtSupervisors = mySharedDBAccess.GetFYPSupervisors(dtFYPInfo.Rows[0]["FYPID"].ToString(), lblResultMessage);

                    if (dtSupervisors != null)
                    {
                        if (dtSupervisors.Rows.Count != 0)
                        {
                            txtTitle.Text = dtFYPInfo.Rows[0]["TITLE"].ToString();
                            txtSupervisor.Text = myHelpers.SupervisorsToString(dtSupervisors);
                            result = true;
                        }
                    }
                }
                else // The student is not yet assigned to any FYP.
                {
                    myHelpers.DisplayMessage(lblResultMessage, "You are not yet assigned to a FYP.");
                }
            }
            return result;
        }

        private void PopulateStudentGrades(string username)
        {
            //***************
            // Uses TODO 36 *
            //***************
            DataTable dtGrades = myFYPMSDB.GetStudentGrades(username);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "NAME", "PROPOSALREPORT", "PROGRESSREPORT", "FINALREPORT", "PRESENTATION" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid("TODO 36", dtGrades, attributeList, lblResultMessage))
            {
                if (dtGrades.Rows.Count != 0)
                {
                    gvGrades.DataSource = dtGrades;
                    gvGrades.DataBind();
                    pnlGrades.Visible = true;
                }
                else // Nothing to display.
                {
                    myHelpers.DisplayMessage(lblResultMessage, "None of your FYP requirements have been graded yet.");
                    pnlGrades.Visible = false;
                }
            }
        }

        /***** Protected Methods *****/

        protected void GvGrades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 5)
            {
                int proposalReportColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PROPOSALREPORT", lblResultMessage);
                int progressReportColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PROGRESSREPORT", lblResultMessage);
                int finalReportColumn = myHelpers.GetGridViewColumnIndexByName(sender, "FINALREPORT", lblResultMessage);
                int presentationColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PRESENTATION", lblResultMessage);

                if (proposalReportColumn != -1 && progressReportColumn != -1 && finalReportColumn != -1 && presentationColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "NAME", "FACULTY");
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsProjectAssigned(loggedinUsername))
            {
                PopulateStudentGrades(loggedinUsername);
            }
        }
    }
}