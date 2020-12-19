using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using UniversityWebsite.Models;
using static UniversityWebsite.Global;

namespace UniversityWebsite.App_Code
{
    public class HelperMethods : Page
    {
        private readonly UniversityDB myUniversityDB = new UniversityDB();

        public string CleanInput(string input)
        {
            // Replace single quote by two quotes and remove leading and trailing spaces.
            return input.Replace("'", "''").Trim();
        }

        public void DisplayMessage(Label labelControl, string message)
        {
            if (message.Substring(0, 3) == "***" || message.Substring(0, 6) == "Please") // Error message.
            {
                labelControl.ForeColor = System.Drawing.Color.Red;
            }
            else // Information message.
            {
                labelControl.ForeColor = System.Drawing.Color.Blue; // "#FF0000FF"
            }
            labelControl.Text = message;
            labelControl.Visible = true;
        }

        public int GetColumnIndexByName(GridViewRowEventArgs e, string columnName)
        {
            for (int i = 0; i < e.Row.Controls.Count; i++)
                if (e.Row.Cells[i].Text.ToLower().Trim() == columnName.ToLower().Trim())
                {
                    return i;
                }
            return -1;
        }

        public bool IsQueryResultValid(string TODO, DataTable datatableToCheck, List<string> columnsNames, Label labelControl)
        {
            bool isQueryResultValid = true;
            if (datatableToCheck != null)
            {
                if (datatableToCheck.Columns != null && datatableToCheck.Columns.Count == columnsNames.Count)
                {
                    foreach (string columnName in columnsNames)
                    {
                        if ((!datatableToCheck.Columns.Contains(columnName)) && columnName != "ANYNAME")
                        {
                            DisplayMessage(labelControl, "*** The SELECT statement of " + TODO + " does not retrieve the attribute " + columnName + ".");
                            isQueryResultValid = false;
                            break;
                        }
                    }
                }
                else
                {
                    DisplayMessage(labelControl, "*** The SELECT statement of " + TODO + " retrieves " + datatableToCheck.Columns.Count + " attributes while the required number is " + columnsNames.Count + ".");
                    isQueryResultValid = false;
                }
            }
            else // An SQL error occurred.
            {
                DisplayMessage(labelControl, "*** SQL error in " + TODO + ": " + sqlError);
                isQueryResultValid = false;
            }
            return isQueryResultValid;
        }

        public void PopulateDropdownList(DropDownList ddlDepartments, Label controlLabel)
        {
            //***************
            // Uses TODO 04 *
            //***************
            DataTable dtDepartments = myUniversityDB.GetDepartments();

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "DEPARTMENTID", "DEPARTMENTNAME" };

            // Display the query result if it is valid.
            if (IsQueryResultValid("TODO 04", dtDepartments, attributeList, controlLabel))
            {
                if (dtDepartments.Rows.Count != 0)
                {
                    ddlDepartments.DataSource = dtDepartments;
                    ddlDepartments.DataValueField = "DEPARTMENTID";
                    ddlDepartments.DataTextField = "DEPARTMENTNAME";
                    ddlDepartments.DataBind();
                    ddlDepartments.Items.Insert(0, "-- Select --");
                    ddlDepartments.Items.FindByText("-- Select --").Value = "none selected";
                    ddlDepartments.SelectedIndex = 0;
                }
                else
                {
                    DisplayMessage(controlLabel, "*** Error in TODO 04 or the database: No departments were retrieved.");
                }
            }
        }

        public void RenameGridViewColumn(GridViewRowEventArgs e, string fromName, string toName)
        {
            int col = GetColumnIndexByName(e, fromName);
            // If the column is not found, ignore renaming.
            if (col != -1)
            {
                e.Row.Cells[col].Text = toName;
            }
        }

        public bool SynchLoginAndApplicationDatabases(string email, Literal literalControl)
        {
            bool synchResult = true;
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // Get the role of the user.
            UniversityRole role = GetUserRole(email);
            ApplicationUser user = manager.FindByName(email);

            switch (role)
            {
                case UniversityRole.None:
                    // The user is not an admin or a student. If the user is in AspNetUsers, then delete him/her from AspNetUsers.
                    if (user != null)
                    {
                        manager.Delete(user);
                    }
                    break;
                case UniversityRole.Student:
                    // If the user is in UniversityDB, but not in AspNetUsers, add the user to AspNetUsers in his/her specified role.
                    if (user == null)
                    {
                        user = new ApplicationUser() { UserName = email };
                        IdentityResult result = manager.Create(user, "University1#");
                        if (result.Succeeded)
                        {
                            IdentityResult roleResult = manager.AddToRole(user.Id, role.ToString());
                            if (!roleResult.Succeeded)
                            {
                                manager.Delete(user);
                                literalControl.Text = "Cannot create role " + role.ToString() + " for user with email '" + email + "'. Please contact 3311rep.";
                                synchResult = false;
                            }
                        }
                        else
                        {
                            literalControl.Text = ((string[])result.Errors)[0] + " Please contact 3311rep.";
                            synchResult = false;
                        }
                    }
                    break;
                default:
                    // No action need for Admin role.
                    break;
            }
            return synchResult;
        }

        public UniversityRole GetUserRole(string email)
        {
            // If the user is neither an admin nor a student, return the None role.
            UniversityRole resultRole = UniversityRole.None;

            // If the username is admin, return the Admin role.
            if (email == "admin")
            { resultRole = UniversityRole.Admin; }

            // Else if the user is a student, return the Student role.
            else if (myUniversityDB.IsUserInRole(UniversityRole.Student.ToString(), "email", email) == 1)
            { resultRole = UniversityRole.Student; }

            return resultRole;
        }
    }
}