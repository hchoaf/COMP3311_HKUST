using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using FYPMSWebsite.App_Code;

namespace FYPMSWebsite.Faculty
{
    public partial class DisplayFYPs : System.Web.UI.Page
    {
        //***************
        // Uses TODO 01 *
        //***************
        private FYPMSDB myFYPMSDB = new FYPMSDB();
        private HelperMethods myHelpers = new HelperMethods();
        readonly string loggedinUsername = HttpContext.Current.User.Identity.Name;

        /***** Protected Methods *****/

        protected void GvProjects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 7)
            {
                // Offset by 1 due to Edit column is column 0.
                int fypIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "FYPID", lblResultMessage) + 1;
                int minStudentsColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MINSTUDENTS", lblResultMessage) + 1;
                int maxStudentsColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MAXSTUDENTS", lblResultMessage) + 1;
                if (fypIdColumn != 0 && minStudentsColumn != 0 && maxStudentsColumn != 0)
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
            lblResultMessage.Visible = false;
            pnlFYPInfo.Visible = false;

            //***************
            // Uses TODO 01 *
            //***************
            DataTable dtFYPs = myFYPMSDB.GetSupervisorFYPDigest(loggedinUsername);

            // Attributes expected to be returned by the query result.
            List<string> attributeList = new List<string> { "FYPID", "TITLE", "CATEGORY", "TYPE", "MINSTUDENTS", "MAXSTUDENTS" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid("TODO 01", dtFYPs, attributeList, lblResultMessage))
            {
                if (dtFYPs.Rows.Count != 0)
                {
                    gvFYPs.DataSource = dtFYPs;
                    gvFYPs.DataBind();
                    pnlFYPInfo.Visible = true;
                }
                else // Nothing to display
                {
                    myHelpers.DisplayMessage(lblResultMessage, "You have not posted any FYPs.");
                }
            }

            if ((bool)Session["canEditFYP"] == false)
            {
                myHelpers.DisplayMessage(lblResultMessage, "The selected FYP cannot be edited as one or more project groups have indicated an interest in this FYP.");
            }
        }
    }
}