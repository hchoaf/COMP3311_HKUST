using System;
using System.Data;
using System.Web.UI.WebControls;
using FYPMSWebsite.App_Code;
using System.Collections.Generic;

namespace FYPMSWebsite.Coordinator
{
    public partial class DisplayReaders : System.Web.UI.Page
    {
        //***************
        // Uses TODO 40 *
        //***************
        private FYPMSDB myFYPMSDB = new FYPMSDB();
        private HelperMethods myHelpers = new HelperMethods();

        //***** Protcted Methods *****

        protected void GvAssignedReaders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 3)
            {
                int groupCodeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "GROUPCODE", lblResultMessage); // Column 1.

                if (groupCodeColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "NAME", "READER");
                        myHelpers.RenameGridViewColumn(e, "GROUPCODE", "GROUP CODE");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[groupCodeColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //***************
            // Uses TODO 40 *
            //***************
            DataTable dtReaders = myFYPMSDB.GetAssignedReaders();

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "NAME", "GROUPCODE", "TITLE" };

            if (myHelpers.IsQueryResultValid("TODO 40", dtReaders, attributeList, lblResultMessage))
            {
                if (dtReaders.Rows.Count != 0)
                {
                    gvAssignedReaders.DataSource = dtReaders;
                    gvAssignedReaders.DataBind();
                    pnlAssignedReaders.Visible = true;
                }
                else // Nothing to display
                {
                    myHelpers.DisplayMessage(lblResultMessage, "There are no projects with assigned readers.");
                    pnlAssignedReaders.Visible = false;
                }
            }
        }
    }
}