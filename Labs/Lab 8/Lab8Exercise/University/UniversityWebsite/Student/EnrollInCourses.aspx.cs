using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using UniversityWebsite.App_Code;

namespace UniversityWebsite.Enrollment
{
    public partial class EnrollInCourses : Page
    {
        //***********************
        // Uses TODO 07, 08, 09 *
        //***********************

        private readonly UniversityDB myUniversityDB = new UniversityDB();
        private readonly HelperMethods myHelperMethods = new HelperMethods();
        private readonly string email = HttpContext.Current.User.Identity.Name;

        /*----- Private Methods -----*/

        private void FindAvailableCourses(string email)
        {
            // Hide previous results.
            lblResultMessage.Visible = false;
            pnlAvailableCourses.Visible = false;

            //***************
            // Uses TODO 08 *
            //***************
            DataTable dtAvailableCourses = myUniversityDB.GetCoursesAvailableToEnrollIn(email);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "COURSEID", "COURSENAME", "CREDITS", "INSTRUCTOR" };

            // Display the query result if it is valid.
            if (myHelperMethods.IsQueryResultValid("TODO 08", dtAvailableCourses, attributeList, lblResultMessage))
            {
                if (dtAvailableCourses.Rows.Count != 0)
                {
                    gvAvailableCourses.DataSource = dtAvailableCourses;
                    gvAvailableCourses.DataBind();
                    pnlAvailableCourses.Visible = true;
                }
                else // Display a no result message.
                {
                    myHelperMethods.DisplayMessage(lblResultMessage, "There are no courses available for you to enroll in.");
                }
            }
        }

        /*----- Protected Methods -----*/

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Hide previous results.
            lblResultMessage.Visible = false;

            if (IsValid)
            {
                //**************
                // Uses TODO 07 *
                //**************
                DataTable dtStudentId = myUniversityDB.GetStudentId(email);

                // Attributes expected to be returned by the query result.
                var attributeList = new List<string> { "STUDENTID" };

                // Display the query result if it is valid.
                if (myHelperMethods.IsQueryResultValid("TODO 07", dtStudentId, attributeList, lblResultMessage))
                {
                    if (dtStudentId.Rows.Count == 1)
                    {
                        string studentId = dtStudentId.Rows[0]["STUDENTID"].ToString();
                        int coursesEnrolled = 0;

                        // Search each row of the GridView to determine if any courses were selected.
                        foreach (GridViewRow row in gvAvailableCourses.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                                if (chkRow != null && chkRow.Checked)
                                {
                                    coursesEnrolled += 1;
                                    // Get the course id of the selected course.
                                    string courseId = myHelperMethods.CleanInput(row.Cells[1].Text);

                                    //***************
                                    // Uses TODO 09 *
                                    //***************
                                    if (!myUniversityDB.EnrollInCourses(studentId, courseId))
                                    {
                                        myHelperMethods.DisplayMessage(lblResultMessage, "*** SQL error in TODO 09: " + Global.sqlError);
                                        return;
                                    }
                                }
                            }

                            // Display a message indicating enrollment result.
                            if (coursesEnrolled != 0)
                            {
                                string successMessage = "You have successfully enrolled in " + coursesEnrolled.ToString();
                                if (coursesEnrolled == 1) { successMessage += " course."; }
                                else { successMessage += " courses."; }
                                myHelperMethods.DisplayMessage(lblResultMessage, successMessage);
                                pnlAvailableCourses.Visible = false;
                            }
                            else
                            {
                                myHelperMethods.DisplayMessage(lblResultMessage, "Please select one or more courses to enroll in.");
                            }
                        }
                    }
                    else // Incorrect query result.
                    {
                        myHelperMethods.DisplayMessage(lblResultMessage, "*** SQL error in TODO 07: Only one student id should be retrieved.");
                    }
                }
            }
        }

        protected void GvAvailableCourses_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FindAvailableCourses(email);
            }
        }
    }
}