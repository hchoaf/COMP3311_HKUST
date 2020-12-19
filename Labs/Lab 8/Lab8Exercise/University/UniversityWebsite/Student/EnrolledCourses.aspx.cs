using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using UniversityWebsite.App_Code;

namespace UniversityWebsite.Enrollment
{
    public partial class EnrolledCourses : Page
    {
        //***************
        // Uses TODO 06 *
        //***************

        private readonly UniversityDB myUniversityDB = new UniversityDB();
        private readonly HelperMethods myHelperMethods = new HelperMethods();
        private readonly string email = HttpContext.Current.User.Identity.Name;

        /*----- Private Methods -----*/

        private void GetEnrolledCourses(string email)
        {
            //**************
            // Uses TODO 06 *
            //**************
            DataTable dtEnrolledCourses = myUniversityDB.GetEnrolledCourses(email);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "COURSEID", "COURSENAME", "GRADE", "CREDITS", "INSTRUCTOR" };

            // Display the query result if it is valid.
            if (myHelperMethods.IsQueryResultValid("TODO 06", dtEnrolledCourses, attributeList, lblResultMessage))
            {
                if (dtEnrolledCourses.Rows.Count != 0)
                {
                    gvEnrolledCourses.DataSource = dtEnrolledCourses;
                    gvEnrolledCourses.DataBind();
                    pnlEnrolledCourses.Visible = true;
                }
                else // Display a no result message.
                {
                    myHelperMethods.DisplayMessage(lblResultMessage, "You are not enrolled in any courses.");
                }
            }
        }

        /*----- Protected Methods -----*/

        protected void GvEnrolledCourses_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 5)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    myHelperMethods.RenameGridViewColumn(e, "COURSEID", "ID");
                    myHelperMethods.RenameGridViewColumn(e, "COURSENAME", "NAME");
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetEnrolledCourses(email);
            }
        }
    }
}