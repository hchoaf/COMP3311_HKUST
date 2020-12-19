using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using static FYPMSWebsite.Global;

namespace FYPMSWebsite.App_Code
{
    /// <summary>
    /// Helpers for the website project.
    /// </summary>

    public class HelperMethods
    {
        public HelperMethods()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string CleanInput(string input)
        {
            // Replace single quote by two quotes and remove leading and trailing spaces.
            return input.Replace("'", "''").Trim();
        }

        public void DisplayMessage(Label labelControl, string message)
        {
            if (message != "" && message != null)
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
            }
            else // Error in displaying message.
            {
                labelControl.Text = "*** Internal system error getting error message. Please contact 3311rep.";
            }
            labelControl.Visible = true;
        }

        public int GetGridViewColumnIndexByName(object sender, string attributeName, Label labelControl)
        {
            DataTable dt = ((DataTable)((GridView)sender).DataSource);
            if (dt != null)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.ToUpper().Trim() == attributeName.ToUpper().Trim())
                    {
                        return i;
                    }
                }
                DisplayMessage(labelControl, "*** SQL error: The attribute " + attributeName + " is missing in the query result.");
            }
            return -1;
        }

        public bool IsQueryResultValid(string TODO, DataTable datatableToCheck, List<string> columnNames, Label labelControl)
        {
            bool isQueryResultValid = true;
            if (datatableToCheck != null)
            {
                if (datatableToCheck.Columns != null && datatableToCheck.Columns.Count == columnNames.Count)
                {
                    // Check if the query result contains the required attributes.
                    foreach (string columnName in columnNames)
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
                    DisplayMessage(labelControl, "*** The SELECT statement of " + TODO + " retrieves " + datatableToCheck.Columns.Count + " attributes while the required number is " + columnNames.Count + ".");
                    isQueryResultValid = false;
                }
            }
            else // An SQL error occurred.
            {
                DisplayMessage(labelControl, sqlError);
                isQueryResultValid = false;
            }
            return isQueryResultValid;
        }

        public bool IsValidAndInRange(string number, decimal min, decimal max)
        {
            decimal n;
            if (decimal.TryParse(number, out n))
            {
                if (min <= n && n <= max)
                { return true; }
            }
            return false;
        }

        public DataTable RemoveSupervisor(DataTable dtFaculty, string username)
        {
            // Remove the existing supervisor from the list of potential cosupervisors.
            foreach (DataRow rowFaculty in dtFaculty.Rows)
            {
                if (rowFaculty["USERNAME"].ToString().Trim().Equals(username))
                {
                    dtFaculty.Rows.Remove(rowFaculty);
                    return dtFaculty;
                }
            }
            return dtFaculty;
        }

        public void RenameGridViewColumn(GridViewRowEventArgs e, string fromName, string toName)
        {
            for (int i = 0; i < e.Row.Controls.Count; i++)
            {
                if (e.Row.Cells[i].Text.ToUpper().Trim() == fromName.ToUpper().Trim())
                {
                    e.Row.Cells[i].Text = toName;
                }
            }
        }

        public string SupervisorsToString(DataTable dtSupervisors)
        {
            string result = dtSupervisors.Rows[0]["NAME"].ToString().Trim();
            if (dtSupervisors.Rows.Count == 2)
            {
                result = result + ", " + dtSupervisors.Rows[1]["NAME"].ToString().Trim();
            }
            return result;
        }
    }
}
