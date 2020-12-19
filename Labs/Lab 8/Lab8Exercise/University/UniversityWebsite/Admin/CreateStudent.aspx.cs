using System;
using System.Web.UI;
using UniversityWebsite.App_Code;
using static UniversityWebsite.Global;


namespace UniversityWebsite.Admin
{
    public partial class CreateStudentRecord : Page
    {
        //**********************
        // Uses TODO 04(S), 05 *
        //**********************

        private readonly UniversityDB myUniversityDB = new UniversityDB();
        private readonly HelperMethods myHelperMethods = new HelperMethods();

        /*----- Protected Methods -----*/

        protected void CreateStudent_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Hide previous results.
                lblResultMessage.Visible = false;

                // Collect and validate the student information.
                string studentId = myHelperMethods.CleanInput(txtStudentId.Text);
                string email = myHelperMethods.CleanInput(txtEmail.Text);
                string firstName = myHelperMethods.CleanInput(txtFirstName.Text);
                string lastName = myHelperMethods.CleanInput(txtLastName.Text);
                string phoneNo = myHelperMethods.CleanInput(txtPhoneNo.Text);
                string cga = "null";
                string departmentId = ddlDepartments.SelectedValue;
                string admissionYear = myHelperMethods.CleanInput(txtAdmissionYear.Text);

                //***************
                // Uses TODO 05 *
                //***************
                if (myUniversityDB.CreateStudentRecord(studentId, firstName, lastName, email, phoneNo, cga, departmentId, admissionYear))
                {
                    myHelperMethods.DisplayMessage(lblResultMessage, "The record for " + firstName + " " + lastName + " was successfully created.");
                    pnlStudentInfo.Visible = false;
                }
                else // An SQL error occurred.
                {
                    myHelperMethods.DisplayMessage(lblResultMessage, "*** SQL error in TODO 05: " + sqlError + ".");
                }
            }
        }

        protected void CvStudentId_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (IsValid)
            {
                if (!myUniversityDB.IsUnique("Student", "studentId", txtStudentId.Text))
                {
                    args.IsValid = false;
                }
            }
        }

        protected void CvUserEmail_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (txtEmail.Text == "admin" || !myUniversityDB.IsUnique("Student", "email", txtEmail.Text))
            {
                args.IsValid = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the admission year to the current year.
                txtAdmissionYear.Text = DateTime.Now.Year.ToString();

                // Populate the department dropdown list.
                myHelperMethods.PopulateDropdownList(ddlDepartments, lblResultMessage);
            }
        }
    }
}