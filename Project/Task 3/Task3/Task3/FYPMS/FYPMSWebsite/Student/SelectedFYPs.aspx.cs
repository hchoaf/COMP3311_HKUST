using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using FYPMSWebsite.App_Code;

namespace FYPMSWebsite.Student
{
    public partial class SelectedFYPs : System.Web.UI.Page
    {
        //*******************
        // Uses TODO 24, 29 *
        //*******************
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly SharedDBAccess mySharedDBAccess = new SharedDBAccess();
        private readonly string loggedinUsername = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private void GetSelectedProjects(string groupId)
        {
            lblResultMessage.Visible = false;

            // If the group id is empty, the student is not yet a member of any group.
            if (groupId != "")
            {
                // *** Uses TODO 29 in SharedDBAccess ***
                DataTable dtProjects = mySharedDBAccess.GetFYPsGroupHasIndicatedInterestIn(groupId, lblResultMessage);

                if (dtProjects != null)
                {
                    if (dtProjects.Rows.Count != 0)
                    {
                        gvSelectedProjects.DataSource = dtProjects;
                        gvSelectedProjects.DataBind();
                        pnlSelectedProjects.Visible = true;
                        pnlSelectProjects.Visible = true;
                    }
                    else
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "Your group has not indicated an interest in any FYPs.");
                        pnlSelectProjects.Visible = true;
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

        protected void GvSelectedProjects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 6)
            {
                // Offset by 1 due to Details column.
                int fypIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "FYPID", lblResultMessage) + 1;
                int priorityColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PRIORITY", lblResultMessage) + 1;

                if (fypIdColumn != 0 && priorityColumn != 0)
                {
                    e.Row.Cells[fypIdColumn].Visible = false;
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[priorityColumn].HorizontalAlign = HorizontalAlign.Center;
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
                    GetSelectedProjects(groupId);
                }
            }
        }
    }
}