using System.Data;
using Oracle.DataAccess.Client;
using static FYPMSWebsite.Global;

namespace FYPMSWebsite.App_Code
{
    public class DBHelperMethods
    {
        private readonly OracleDBAccess myOracleDBAccess = new OracleDBAccess();
        private readonly FYPMSDB myFYPMSDB = new FYPMSDB();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private string sql;

        public bool CleanUpProjectGroups(System.Web.UI.WebControls.Label labelControl)
        {
            bool result = true;
            sql = "delete from ProjectGroup where groupId not in (select distinct groupid from CSEStudent where groupId is not null)";
            if (!myOracleDBAccess.SetData("CleanUpProjectGroups", sql))
            {
                myHelpers.DisplayMessage(labelControl, sqlError);
                result = false;
            }
            return result;
        }

        public bool CreateFYP(string fypId, string title, string description, string fcategory,
            string type, string otherRequirements, string minStudents, string maxStudents, string isAvailable,
            string supervisor, string cosupervisor)
        {
            //*******************
            // Uses TODO 05, 07 *
            //*******************

            // FIRST, create an Oracle transaction.
            OracleTransaction trans = myOracleDBAccess.BeginTransaction("transaction begin for TODO 05 and 07");
            if (trans == null) { return false; }

            // *** Uses TODO 05 *** SECOND, create the project record.
            if (!myFYPMSDB.CreateFYP(fypId, title, description, fcategory, type,
                otherRequirements, minStudents, maxStudents, isAvailable, trans))
            { return false; }

            // *** Uses TODO 07 *** THIRD, create the Supervises record for the supervisor.
            if (!myFYPMSDB.AddSupervisor(supervisor, fypId, trans)) { return false; }
            {
                // Create the Supervises record for the cosupervisor, if any.
                if (cosupervisor != "")
                {
                    if (!myFYPMSDB.AddSupervisor(cosupervisor, fypId, trans)) { return false; }
                }
            }
            myOracleDBAccess.CommitTransaction("transaction commit for TODO 05 and 07", trans);
            return true;
        }

        public bool CreateRequirementGradesRecord(string facultyUsername, DataTable dtGroupMembers)
        {
            // FIRST, create an Oracle Transaction.
            OracleTransaction trans = myOracleDBAccess.BeginTransaction("transaction begin for TODO 21");
            if (trans == null) { return false; }

            // THEN, create a RequirementGrades record for each student in the group.
            foreach (DataRow row in dtGroupMembers.Rows)
            {
                //***************
                // Uses TODO 21 *
                //***************
                if (!myFYPMSDB.CreateRequirementGrades(facultyUsername, row["USERNAME"].ToString(), "null", "null", "null", "null", trans))
                {
                    return false;
                }
            }
            myOracleDBAccess.CommitTransaction("transaction commit for TODO 21", trans);
            return true;
        }

        public bool UpdateFYP(string fypId, string title, string description, string category,
            string type, string otherRequirements, string minStudents, string maxStudents, string isAvailable,
            string oldCosupervisor, string newCosupervisor)
        {
            //***********************
            // Uses TODO 06, 07, 08 *
            //***********************

            // FIRST, create an Oracle transaction.
            OracleTransaction trans = myOracleDBAccess.BeginTransaction("transaction begin for TODO 06, 07 and 08");
            if (trans == null) { return false; }

            // *** Uses TODO 06 *** SECOND, update the FYP project values.
            if (!myFYPMSDB.UpdateFYP(fypId, title, description, category, type,
                otherRequirements, minStudents, maxStudents, isAvailable, trans))
            { return false; }

            // THIRD, update the cosupervisor, if necessary.
            if (oldCosupervisor != newCosupervisor)
            {
                if (oldCosupervisor != "")
                {
                    // *** Uses TODO 08 *** Delete the old cosupervsior for the project from the Supervises table.
                    if (!myFYPMSDB.RemoveSupervisor(oldCosupervisor, fypId, trans))
                    { return false; }
                }
                if (newCosupervisor != "")
                {
                    // *** Uses TODO 07 *** Insert a new cosupervisor for the project into the Supervises table.
                    if (!myFYPMSDB.AddSupervisor(newCosupervisor, fypId, trans))
                    { return false; }
                }
            }
            myOracleDBAccess.CommitTransaction("transaction commit for TODO 06, 07 and 08", trans);
            return true;
        }

        public string GetNextTableId(string tableName, string idName, System.Web.UI.WebControls.Label labelControl)
        {
            string id = "";
            sql = "select max(" + idName + ") from " + tableName;
            decimal nextId = myOracleDBAccess.GetAggregateValue("DBHelperMethods: GetNextTableId", sql);
            if (nextId != -1)
            {
                id = (nextId + 1).ToString();
            }
            else //An SQL error occurred.
            {
                myHelpers.DisplayMessage(labelControl, "*** Error getting the next " + idName + " for table " + tableName + ". Please contact 3311rep.");
            }
            return id;
        }

        public DataTable GetFYPCategories()
        {
            sql = "select * " +
                "from FYPCategory";
            return myOracleDBAccess.GetData("DBHelperMethods: GetFYPCategories - please contact 3311rep", sql);
        }

        public DataTable GetFYPDetails(string fypId)
        {
            sql = "select * from FYP where fypId=" + fypId;
            return myOracleDBAccess.GetData("DBHelperMethods: GetProjectDetails - please contact 3311rep", sql);
        }

        public DataTable GetFYPDigests()
        {
            sql = "select fypId, title, category, type, minStudents, maxStudents, isAvailable" +
                " from FYP order by title";
            return myOracleDBAccess.GetData("DBHelperMethods: GetProjectDigests - please contact 3311rep", sql);
        }

        public DataTable GetSupervisors(string fypId)
        {
            sql = "select username, name " +
                "from Faculty natural join Supervises " +
                "where fypId=" + fypId;
            return myOracleDBAccess.GetData("DBHelperMethods: GetSupervisors - please contact 3311rep", sql);
        }

        public bool RemoveReader(string groupId)
        {
            sql = "update ProjectGroup set reader=null where groupId=" + groupId;
            return myOracleDBAccess.SetData("DBHelperMethods: RemoveReader - please contact 3311rep", sql);
        }

        public bool RemoveProjectGroupFromFYP(string groupId)
        {
            sql = "update ProjectGroup set assignedFYP=null where groupId=" + groupId;
            return myOracleDBAccess.SetData("DBHelperMethods: RemoveGroupFromProject - please contact 3311rep", sql);
        }

        public FYPRole GetUserRole(string username)
        {
            FYPRole role = FYPRole.None;
            if (username == "coordinator")
            { role = FYPRole.Coordinator; }
            else
            {
                if (myOracleDBAccess.GetAggregateValue("Login", "select count(*) from Faculty where username='" + username + "'") == 1)
                { role = FYPRole.Faculty; }
                else if (myOracleDBAccess.GetAggregateValue("Login", "select count(*) from CSEStudent where username='" + username + "'") == 1)
                { role = FYPRole.Student; }
            }
            return role;
        }
    }
}