using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using UniversityWebsite.App_Code;

namespace UniversityWebsite.Student
{
    public partial class SearchStudentRecords : Page
    {
        //**********************
        // Uses TODO 03, 04(S) *
        //**********************

        private readonly UniversityDB myUniversityDB = new UniversityDB();
        private readonly HelperMethods myHelperMethods = new HelperMethods();

        /*----- Protected Methods -----*/

        protected void DdlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide previous results.
            lblResultMessage.Visible = false;
            pnlSearchResult.Visible = false;

            Page.Validate();

            if (IsValid)
            {
                // Get the department id from the dropdown list.
                string departmentId = ddlDepartments.SelectedItem.Value;

                //***************
                // Uses TODO 03 *
                //***************
                DataTable dtStudentRecords = myUniversityDB.GetDepartmentStudentRecords(departmentId);

                // Attributes expected to be returned by the query result.
                var attributeList = new List<string> { "STUDENTID", "LASTNAME", "FIRSTNAME", "EMAIL", "CGA" };

                // Display the query result if it is valid.
                if (myHelperMethods.IsQueryResultValid("TODO 03", dtStudentRecords, attributeList, lblResultMessage))
                {
                    if (dtStudentRecords.Rows.Count != 0)
                    {
                        gvFindStudentRecordsResult.DataSource = dtStudentRecords;
                        gvFindStudentRecordsResult.DataBind();
                        pnlSearchResult.Visible = true;
                    }
                    else // Display a no result message.
                    {
                        myHelperMethods.DisplayMessage(lblResultMessage, "There are no students in the " + ddlDepartments.SelectedItem.Text + " department.");
                    }
                }
            }
        }

        protected void GvFindStudentRecordsResult_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 5)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    myHelperMethods.RenameGridViewColumn(e, "STUDENTID", "ID");
                    myHelperMethods.RenameGridViewColumn(e, "LASTNAME", "LAST NAME");
                    myHelperMethods.RenameGridViewColumn(e, "FIRSTNAME", "FIRST NAME");
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the department dropdown list.
                myHelperMethods.PopulateDropdownList(ddlDepartments, lblResultMessage);
            }
        }
    }
}