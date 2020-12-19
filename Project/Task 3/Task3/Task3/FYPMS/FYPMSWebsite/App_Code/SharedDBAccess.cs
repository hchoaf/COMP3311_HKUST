using System.Data;
using System.Collections.Generic;

namespace FYPMSWebsite.App_Code
{
    /// <summary>
    /// Project specific methods that access the database.
    /// </summary>

    public class SharedDBAccess
    {
        private readonly FYPMSDB myFYPMSDB = new FYPMSDB();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();

        public SharedDBAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetFaculty(System.Web.UI.WebControls.Label labelControl)
        {
            //***************
            // Uses TODO 03 *
            //***************
            DataTable dtFaculty = myFYPMSDB.GetFaculty();

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "USERNAME", "NAME" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid("TODO 03", dtFaculty, attributeList, labelControl))
            {
                return dtFaculty;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetFYPsGroupHasIndicatedInterestIn(string groupId, System.Web.UI.WebControls.Label labelControl)
        {
            //***************
            // Uses TODO 29 *
            //***************
            DataTable dtProjectsGroupInterestedIn = myFYPMSDB.GetFYPsGroupHasIndicatedInterestIn(groupId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "FYPID", "TITLE", "CATEGORY", "TYPE", "PRIORITY" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid("TODO 29", dtProjectsGroupInterestedIn, attributeList, labelControl))
            {
                return dtProjectsGroupInterestedIn;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetProjectGroupAvailableFYPDigests(string groupId, System.Web.UI.WebControls.Label labelControl)
        {
            //***************
            // Uses TODO 26 *
            //***************
            DataTable dtProjects = myFYPMSDB.GetGroupAvailableFYPDigests(groupId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "FYPID", "TITLE", "CATEGORY", "TYPE", "MINSTUDENTS", "MAXSTUDENTS" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid("TODO 26", dtProjects, attributeList, labelControl))
            {
                return dtProjects;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetProjectGroupMembers(string groupId, System.Web.UI.WebControls.Label labelControl)
        {
            //***************
            // Uses TODO 20 *
            //***************
            DataTable dtGroupMembers = myFYPMSDB.GetProjectGroupMembers(groupId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "USERNAME", "NAME", "GROUPID" };

            if (myHelpers.IsQueryResultValid("TODO 20", dtGroupMembers, attributeList, labelControl))
            {
                return dtGroupMembers;
            }
            else
            {
                return null;
            }
        }

        public string GetStudentGroupId(string username, System.Web.UI.WebControls.Label labelControl)
        {
            string groupId = "SQL_ERROR";

            if (username != "")
            {
                //***************
                // Uses TODO 24 *
                //***************
                DataTable dtGroup = myFYPMSDB.GetStudentGroupId(username);

                // Attributes expected to be returned by the query result.
                var attributeList = new List<string> { "GROUPID" };

                // Display the query result if it is valid.
                if (myHelpers.IsQueryResultValid("TODO 24", dtGroup, attributeList, labelControl))
                {
                    if (dtGroup.Rows.Count != 0) // The student is a member of a group.
                    {
                        groupId = dtGroup.Rows[0]["GROUPID"].ToString();
                    }
                    else // The student is not a member of a group. 
                    {
                        groupId = "";
                    }
                }
            }
            else // There is no username; should not happen!
            {
                myHelpers.DisplayMessage(labelControl, "*** Application error in SharedDBAccess\\GetStudentGroupId: Cannot get the logged in username. Please report to 3311rep.");
            }
            return groupId;
        }

        public DataTable GetSupervisorProjectGroups(string username, System.Web.UI.WebControls.Label labelControl)
        {
            //***************
            // Uses TODO 09 *
            //***************
            DataTable dtGroups = myFYPMSDB.GetSupervisorProjectGroups(username);

            // Attributes expected to be returned by the query result.
            List<string> attributeList = new List<string> { "GROUPID", "GROUPCODE", "ASSIGNEDFYP" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid("TODO 09", dtGroups, attributeList, labelControl))
            {
                return dtGroups;
            }
            else
            {
                return null;
            }
        }

        public string IsGroupAssigned(string groupId, System.Web.UI.WebControls.Label labelControl)
        {
            string isAssigned = "false";

            if (groupId != "")
            {
                //***************
                // Uses TODO 25 *
                //***************
                DataTable dtIsAssigned = myFYPMSDB.GetAssignedFypId(groupId);

                // Attributes expected to be returned by the query result.
                var attributeList = new List<string> { "ASSIGNEDFYP" };

                // Display the query result if it is valid.
                if (myHelpers.IsQueryResultValid("TODO 25", dtIsAssigned, attributeList, labelControl))
                {
                    if (dtIsAssigned.Rows.Count != 0)
                    {
                        if (dtIsAssigned.Rows[0]["ASSIGNEDFYP"].ToString() != "")
                        {
                            isAssigned = "true";
                        }
                    }
                }
                else // An SQL error occurred.
                {
                    isAssigned = "SQL_ERROR";
                }
            }
            return isAssigned;
        }

        /*** METHODS THAT DO NOT USE TODOS DIRECTLY ***/

        public DataTable GetFYPCategories(System.Web.UI.WebControls.Label labelControl)
        {
            DataTable dtCategory = myDBHelpers.GetFYPCategories();

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "CATEGORY" };

            if (myHelpers.IsQueryResultValid("SharedDBAccess: GetFYPCategories", dtCategory, attributeList, labelControl))
            {
                return dtCategory;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetFYPDetails(string fypId, System.Web.UI.WebControls.Label labelControl)
        {
            DataTable dtProject = myDBHelpers.GetFYPDetails(fypId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "FYPID", "TITLE", "DESCRIPTION", "CATEGORY", "TYPE", "OTHERREQUIREMENTS", "MINSTUDENTS", "MAXSTUDENTS", "ISAVAILABLE" };

            // Get the project information; save the result in ViewState and display it if it is not null.
            if (myHelpers.IsQueryResultValid("Get Project Details", dtProject, attributeList, labelControl))
            {
                return dtProject;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetFYPDigests(System.Web.UI.WebControls.Label labelControl)
        {
            DataTable dtProjects = myDBHelpers.GetFYPDigests();

            // Attributes expected to be returned by the query result.
            List<string> attributeList = new List<string> { "FYPID", "TITLE", "CATEGORY", "TYPE", "MINSTUDENTS", "MAXSTUDENTS", "ISAVAILABLE" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid("Get Project Digests", dtProjects, attributeList, labelControl))
            {
                return dtProjects;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetFYPSupervisors(string fypId, System.Web.UI.WebControls.Label labelControl)
        {
            DataTable dtSupervisors = myDBHelpers.GetSupervisors(fypId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "USERNAME", "NAME" };

            if (myHelpers.IsQueryResultValid("GetFYPSupervisors in App_Code\\SharedDBAccess", dtSupervisors, attributeList, labelControl))
            {
                return dtSupervisors;
            }
            else
            {
                return null;
            }
        }
    }
}
