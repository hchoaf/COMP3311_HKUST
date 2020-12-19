using System;
using System.Data;
using System.Web.UI.WebControls;
using FYPMSWebsite.App_Code;

namespace FYPMSWebsite
{
    public partial class DisplayAllFYPs : System.Web.UI.Page
    {
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly SharedDBAccess mySharedDBAccess = new SharedDBAccess();

        /***** Protected Methods *****/

        protected void GvProjects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 8)
            {
                // Offset by 1 due to Details column.
                int titleColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TITLE", lblResultMessage) + 1;
                int categoryColumn = myHelpers.GetGridViewColumnIndexByName(sender, "CATEGORY", lblResultMessage) + 1;
                int typeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TYPE", lblResultMessage) + 1;
                int minStudentsColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MINSTUDENTS", lblResultMessage) + 1;
                int maxStudentsColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MAXSTUDENTS", lblResultMessage) + 1;
                int isAvailableColumn = myHelpers.GetGridViewColumnIndexByName(sender, "ISAVAILABLE", lblResultMessage) + 1;

                if (titleColumn != -1 && categoryColumn != -1 && typeColumn != -1 && minStudentsColumn != -1 && maxStudentsColumn != -1 && isAvailableColumn != -1)
                {
                    e.Row.Cells[1].Visible = false;
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "MINSTUDENTS", "MIN");
                        myHelpers.RenameGridViewColumn(e, "MAXSTUDENTS", "MAX");
                        myHelpers.RenameGridViewColumn(e, "ISAVAILABLE", "AVAILABLE?");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[minStudentsColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[maxStudentsColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[isAvailableColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        protected void gvProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProjects.PageIndex = e.NewPageIndex;
            gvProjects.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblResultMessage.Visible = false;

            DataTable dtProjects = mySharedDBAccess.GetFYPDigests(lblResultMessage);

            // Display the query result if it is valid.
            if (dtProjects != null)
            {
                if (dtProjects.Rows.Count != 0)
                {
                    gvProjects.DataSource = dtProjects;
                    gvProjects.DataBind();
                    pnlProjectInfo.Visible = true;
                }
                else // Nothing to display
                {
                    myHelpers.DisplayMessage(lblResultMessage, "There are no available projects.");
                }
            }
        }
    }
}