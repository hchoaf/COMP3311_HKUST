using System;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using UniversityWebsite.App_Code;

namespace UniversityWebsite.Student
{
    public partial class SearchForStudent : Page
    {
        //*******************
        // Uses TODO 01, 02 *
        //*******************

        private readonly UniversityDB myUniversityDB = new UniversityDB();
        private readonly HelperMethods myHelperMethods = new HelperMethods();

        /*----- Private Methods -----*/
        private bool StudentIdIsValid(string studentId)
        {
            bool result = true;

            //***************
            // Uses TODO 02 *
            //***************
            decimal queryResult = myUniversityDB.StudentRecordExists(studentId);

            if (queryResult == 0) // There is no such student id.
            {
                result = false;
                myHelperMethods.DisplayMessage(lblResultMessage, "There is no student with this student id.");
            }
            else if (queryResult == -1) // An SQL error occurred.
            {
                result = false;
                myHelperMethods.DisplayMessage(lblResultMessage, "*** SQL error in TODO 02: " + Global.sqlError);
            }
            return result;
        }

        /*----- Protected Methods -----*/

        protected void BtnFindStudent_Click(object sender, EventArgs e)
        {
            // Hide previous results.
            lblResultMessage.Visible = false;
            pnlStudentRecord.Visible = false;

            string studentId = myHelperMethods.CleanInput(txtStudentId.Text);

            if (IsValid && StudentIdIsValid(studentId))
            {
                //***************
                // Uses TODO 01 *
                //***************
                DataTable dtStudentRecord = myUniversityDB.GetStudentRecord(studentId);

                // Attributes expected to be returned by the query result.
                var attributeList = new List<string> { "STUDENTID", "FIRSTNAME", "LASTNAME", "EMAIL",
                    "PHONENO", "CGA", "DEPARTMENTID", "ADMISSIONYEAR" };

                // Display the query result if it is valid.
                if (myHelperMethods.IsQueryResultValid("TODO 01", dtStudentRecord, attributeList, lblResultMessage))
                {
                    // Display a no result message if nothing was retrieved from the database.
                    if (dtStudentRecord.Rows.Count != 0)
                    {
                        gvStudentRecord.DataSource = dtStudentRecord;
                        gvStudentRecord.DataBind();
                        pnlStudentRecord.Visible = true;
                    }
                    else // Display a no result message.
                    {
                        myHelperMethods.DisplayMessage(lblResultMessage, "No record for the student was found.");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}