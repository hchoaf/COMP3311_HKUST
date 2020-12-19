using System.Data;
using Oracle.DataAccess.Client;


namespace FYPMSWebsite.App_Code
{
    /// <summary>
    /// Student name:  CHO, Hangsun
    /// Student number:  20473370
    /// 
    /// NOTE: This is an individual task. By submitting this file you certify that this
    /// code is the result of YOUR INDIVIDUAL EFFORT and that it has not been developed
    /// in collaoration with or copied from any other person. If this is not the case,
    /// then you must identify by name all the persons with whom you collaborated or
    /// from whom you copied code below.
    /// 
    /// Collaborators: 
    /// </summary>

    public class FYPMSDB
    {
        //******************************** IMPORTANT NOTE **********************************
        // For the web pages to display a query result correctly, the attribute names in   *
        // the query result table must be EXACTLY the same as that in the database tables. *
        // WARNING: DO NOT put single quotes around numeric attribute values as this will  *
        // result in SQL errors when a null value is required for the attribute value.     *
        //         Report problems with the website code to 3311rep@cse.ust.hk.            *
        //**********************************************************************************

        private readonly OracleDBAccess myOracleDBAccess = new OracleDBAccess();
        private DataTable queryResult;
        private decimal aggregateQueryResult;
        private bool updateResult;
        private string sql;

        #region SQL Statements for Faculty Web Pages - TODOS 01 to 23

        public DataTable GetSupervisorFYPDigest(string username)
        {
            //***************************************************************************************
            // TODO 01: Used in Faculty\DisplayFYPs                                                 *
            // Construct the SQL SELECT statement to retrieve the fyp id, title, category, type,    *
            // minimum students and maximum students of the FYPs supervised by a faculty identified *
            // by his/her username. Order the result by title ascending.                            *
            //***************************************************************************************
            sql = "select FYP.fypId, title, category, type, minStudents, maxStudents " + 
                "from FYP, Supervises " + 
                "where FYP.fypId = Supervises.fypId " +
                "and Supervises.username = '" + username + "'" +
                "order by title asc";
            queryResult = myOracleDBAccess.GetData("TODO 01", sql);
            return queryResult;
        }

        public DataTable GetInterestedInFYP(string fypId)
        {
            //***************************************************************************************
            // TODO 02: Used in Faculty\EditFYP                                                     *
            // Construct the SQL SELECT statement to retrieve the fyp id, group id and priority for *
            // all groups interested in an FYP identified by its fyp id.                            *
            //***************************************************************************************
            sql = "select fypId, groupId, priority " +
                "from InterestedIn " +
                "where fypId = '" + fypId + "'";
            queryResult = myOracleDBAccess.GetData("TODO 02", sql);
            return queryResult;
        }

        public DataTable GetFaculty()
        {
            //***************************************************************************************
            // TODO 03: Used in GetFaculty in App_Code\SharedDBAccess                               *
            // Construct the SQL SELECT statement to retrieve the username and name of all faculty. *
            //***************************************************************************************
            sql = "select username, name " +
                "from Faculty";
            queryResult = myOracleDBAccess.GetData("TODO 03", sql);
            return queryResult;
        }

        public DataTable GetCosupervisorInfoForEdit(string fypId, string username)
        {
            //****************************************************************************************
            // TODO 04: Used in Faculty\EditFYP                                                      *
            // Construct the SQL SELECT statement to retrieve the username of the cosupervisor, if   *
            // any, of an FYP, identified by its fyp id, given the username of the other supervisor. *
            //****************************************************************************************
            sql = "select username " +
                "from Supervises " +
                "where fypId = '" + fypId + "'" + 
                "and username != '" + username + "'";
            queryResult = myOracleDBAccess.GetData("TODO 04", sql);
            return queryResult;
        }

        public bool CreateFYP(string fypId, string title, string description, string category,
            string type, string otherRequirements, string minStudents, string maxStudents,
            string isAvailable, OracleTransaction trans)
        {
            //*******************************************************
            // TODO 05: Used in App_Code\DBHelperMethods.CreateFYP  *
            // Construct the SQL INSERT statement to create an FYP. *
            //*******************************************************
            sql = "insert into FYP values (" +
                "'" + fypId + "'" + "," +
                "'" + title + "'" + "," +
                "'" + description + "'" + "," +
                "'" + category + "'" + "," +
                "'" + type + "'" + "," +
                "'" + otherRequirements + "'" + "," +
                "'" + minStudents + "'" + "," +
                "'" + maxStudents + "'" + "," +
                "'" + isAvailable + "'" +
                ")";
            updateResult = myOracleDBAccess.SetData("TODO 05", sql, trans);
            return updateResult;
        }

        public bool UpdateFYP(string fypId, string title, string description, string category,
            string type, string otherRequirements, string minStudents, string maxStudents,
            string isAvailable, OracleTransaction trans)
        {
            //**********************************************************************************************
            // TODO 06: Used in App_Code\DBHelperMethods.UpdateFYP                                         *
            // Construct the SQL UPDATE statement to update all values of an FYP identified by its fyp id. *
            //**********************************************************************************************
            sql = "update FYP " +
                "set title = '" + title + "'" + "," +
                "description = '" + description + "'" + "," +
                "category = '" + category + "'" + "," +
                "type = '" + type + "'" + "," +
                "otherRequirements = '" + otherRequirements + "'" + "," +
                "minStudents = '" + minStudents + "'" + "," +
                "maxStudents = '" + maxStudents + "'" + "," +
                "isAvailable = '" + isAvailable + "'" +
                "where fypId = '" + fypId + "'";
            updateResult = myOracleDBAccess.SetData("TODO 06", sql, trans);
            return updateResult;
        }

        public bool AddSupervisor(string username, string fypId, OracleTransaction trans)
        {
            //*********************************************************************************
            // TODO 07: Used in App_Code\DBHelperMethods.UpdateFYP                            *
            // Construct the SQL INSERT statement to add a supervisor, identified by his/her  *
            // username, to an FYP identified by an fyp id.                                   *
            //*********************************************************************************
            sql = "insert into Supervises " +
                "select username, '" + fypId + "'" +
                "from Faculty " +
                "where username = '" + username + "'";
            updateResult = myOracleDBAccess.SetData("TODO 07", sql);
            return updateResult;
        }

        public bool RemoveSupervisor(string username, string fypId, OracleTransaction trans)
        {
            //***********************************************************************************
            // TODO 08: Used in App_Code\DBHelperMethods.UpdateFYP                              *
            // Construct the SQL DELETE statement to remove a supervisor, identified by his/her *
            // username, from an FYP identified by an fyp id.                                   *
            //***********************************************************************************
            sql = "delete from Supervises " + 
                "where username = '" + username + "'" +
                "and fypId = '" + fypId + "'";
            updateResult = myOracleDBAccess.SetData("TODO 08", sql, trans);
            return updateResult;
        }

        /*
         * According to email, TODO below is meaningless and can be deleted. ************88
         * ********************************************************************************
        public DataTable GetFacultyCode(string username) // NOT USED !!!!
        {
            //******************************************************************************************
            // TODO 09: Used in Faculty\AssignGrades                                                   *
            // Construct the SQL SELECT statement to retrieve the faculty code of a faculty identified *
            // by his/her username.                                                                    *
            //******************************************************************************************
            sql = "";
            queryResult = myOracleDBAccess.GetData("TODO 09", sql);
            return queryResult;
        }
        */

        public DataTable GetSupervisorProjectGroups(string username)
        {
            //***************************************************************************************
            // TODO 09: Used in App_Code\SharedDBAccess                                             *
            // Construct the SQL SELECT statement to retrieve the group id, group code and assigned *
            // fyp id of the project groups supervised by a faculty identified by his/her username. *
            // Order the result by group id ascending.                                              *
            //***************************************************************************************
            sql = "select groupId, groupCode, assignedFYP " +
                "from ProjectGroup, Supervises " +
                "where ProjectGroup.assignedFYP = Supervises.fypId " +
                "and Supervises.username = '" + username + "'" +
                "order by groupId asc";
            queryResult = myOracleDBAccess.GetData("TODO 09", sql);
            return queryResult;
        }

        public DataTable GetFacultyFYPs(string username)
        {
            //*****************************************************************************************
            // TODO 10: Used in Faculty\AssignGroupToFYP                                              *
            // Construct the SQL SELECT statement to retrieve the id and title of the FYPs supervised *
            // by a faculty identified by his/her username. Order the result by title ascending.      *
            //*****************************************************************************************
            sql = "select FYP.fypId, title " +
                "from FYP, Supervises " +
                "where FYP.fypId = Supervises.fypId " +
                "and username = '" + username + "'" +
                "order by title asc";
            queryResult = myOracleDBAccess.GetData("TODO 10", sql);
            return queryResult;
        }

        public DataTable GetGroupsCurrentlyAssigned(string fypId)
        {
            //*****************************************************************************************
            // TODO 11: Used in Faculty\AssignGroupToFYP                                              *
            // Construct the SQL SELECT statement to retrieve the group id and group code, as well as *
            // the name of the students in the group, for those groups that have been assigned to an  *
            // FYP identified by its fyp id. Order the result first by group id ascending and then by *
            // student name ascending.                                                                *
            //*****************************************************************************************
            sql = "select CSEStudent.groupId, groupCode, name " +
                "from ProjectGroup, CSEStudent " +
                "where ProjectGroup.groupId = CSEStudent.groupId " +
                "and assignedFYP = '" + fypId + "'" +
                "order by groupId asc, name asc";
            queryResult = myOracleDBAccess.GetData("TODO 11", sql);
            return queryResult;
        }

        public DataTable GetFYPAvailability(string fypId)
        {
            //***************************************************************************************
            // TODO 12: Used in Faculty\AssignGroupToFYP                                            *
            // Construct the SQL SELECT statement to retrieve the availability of an FYP identified *
            // by its fyp id.                                                                       *
            //***************************************************************************************
            sql = "select isAvailable " +
                "from FYP " +
                "where fypId = '" + fypId + "'";
            queryResult = myOracleDBAccess.GetData("TODO 12", sql);
            return queryResult;
        }

        public DataTable GetGroupsAvailableToAssign(string fypId)
        {
            //*************************************************************************************
            // TODO 13: Used in Faculty\AssignGroupToFYP                                          *
            // Construct the SQL SELECT statement to retrieve the group id and priority, as well  *
            // as the name and username of the students in the group, for those groups that are   *
            // available for assignment and that have indicated an interest in an FYP, identified *
            // by its fyp id, where the FYP is available. Order the result first by group id      *
            // ascending and then by student name ascending.                                      *
            //*************************************************************************************
            sql = "select ProjectGroup.groupId, InterestedIn.priority, CSEStudent.name, CSEStudent.username " + 
                "from ProjectGroup, InterestedIn, CSEStudent, FYP " +
                "where ProjectGroup.groupId = CSEStudent.groupId " +
                "and ProjectGroup.groupId = InterestedIn.groupId " +
                "and InterestedIn.fypId = FYP.fypId " + 
                "and InterestedIn.fypId = '" + fypId + "'" +
                "and FYP.isAvailable = 'Y' " +
                "and ProjectGroup.assignedFYP is null " +
                "order by ProjectGroup.groupId asc, CSEStudent.name asc";
            queryResult = myOracleDBAccess.GetData("TODO 13", sql);
            return queryResult;
        }

        public DataTable GetFYPSupervisorFacultyCodes(string fypId)
        {
            //******************************************************************************************
            // TODO 14: Used in Faculty\AssignGroupToFYP                                               *
            // Construct the SQL SELECT statement to retrieve all the faculty codes of the supervisors *
            // of an FYP identified by its fyp id. Order the result by faculty code ascending.         *
            //******************************************************************************************
            sql = "select facultyCode " +
                "from Faculty, Supervises " +
                "where Faculty.username = Supervises.username " + 
                "and Supervises.fypId = '" + fypId + "'" +
                "order by facultyCode asc";
            queryResult = myOracleDBAccess.GetData("TODO 14", sql);
            return queryResult;
        }

        public decimal GetFacultyCodeSequenceNumber(string groupCodePrefix)
        {
            //*****************************************************************************************
            // TODO 15: Used in Faculty\AssignGroupToFYP                                              *
            // Construct the SQL SELECT statement to retrieve the number of times a given group code  *
            // prefix has been used. A group code prefix is the group code minus its trailing integer *
            // (e.g., for group code "FL1" the prefix is "FL").                                       *
            //*****************************************************************************************
            sql = "with temp(prefix) as "
                + "(select '" + groupCodePrefix + "' as prefix "
                + "from ProjectGroup) "
                + "select count(distinct groupCode) "
                + "from temp left outer join ProjectGroup on prefix = substr(groupCode, 0, length(rtrim(groupCode))-1) "
                + "group by prefix ";

            aggregateQueryResult = myOracleDBAccess.GetAggregateValue("TODO 15", sql);
            return aggregateQueryResult;
        }

        public bool AssignGroupToFYP(string groupId, string groupCode, string fypId)
        {
            //****************************************************************************************
            // TODO 16: Used in Faculty\AssignGroupToFYP                                             *
            // Construct the SQL UPDATE statement to assign a project group, identified by its group *
            // id, to an FYP identified by its fyp id, with a specified group code.                  *
            //****************************************************************************************
            sql = "update ProjectGroup " +
                "set groupCode = '" + groupCode + "'" + "," +
                "assignedFYP = '" + fypId + "'" + 
                "where groupId = '" + groupId + "'";
            updateResult = myOracleDBAccess.SetData("TODO 16", sql);
            return updateResult;
        }
        
        public DataTable GetReaderProjectGroups(string username)
        {
            //*************************************************************************************
            // TODO 17: Used in Faculty\AssignGrades                                              *
            // Construct the SQL SELECT statement to retrieve the group id, group code and fyp id *
            // of the project groups to which a faculty, identified by his/her username, has been *
            // assigned as a reader. Order the result by group id ascending.                      *
            //*************************************************************************************
            sql = "select groupId, groupCode, assignedFYP " +
                "from ProjectGroup " +
                "where reader = '" + username + "'" +
                "order by groupId asc";
            queryResult = myOracleDBAccess.GetData("TODO 17", sql);
            return queryResult;
        }

        public DataTable GetSupervisorRequirementGrades(string groupId, string fypId)
        {
            //************************************************************************************
            // TODO 18: Used in Faculty\AssignGrades                                             *
            // Construct the SQL SELECT statement to retrieve, for all the students in a project *
            // group identified by its group id, the username, name and all their requirement    *
            // grades given by any of the faculty supervising the FYP identified by its fyp id.  *
            //************************************************************************************
            sql = "select CSEStudent.username, CSEStudent.name, proposalReport, progressReport, finalReport, presentation " +
                "from CSEStudent, ProjectGroup, RequirementGrades, Supervises " +
                "where CSEStudent.groupId = ProjectGroup.groupId " +
                "and CSEStudent.username = RequirementGrades.studentUsername " +
                "and ProjectGroup.assignedFYP = Supervises.fypId " +
                "and Supervises.username = RequirementGrades.facultyUsername " +
                "and ProjectGroup.groupId = '" + groupId + "'" +
                "and ProjectGroup.assignedFYP = '" + fypId + "'";
            queryResult = myOracleDBAccess.GetData("TODO 18", sql);
            return queryResult;
        }

        public DataTable GetReaderRequirementGrades(string groupId, string username)
        {
            //************************************************************************************
            // TODO 19: Used in Faculty\AssignGrades                                             *
            // Construct the SQL SELECT statement to retrieve, for all the students in a project *
            // group identified by its group id, the username, name and all their requirement    *
            // grades given by the reader of the FYP iddentified by his/her username.            *
            //************************************************************************************
            sql = "select username, name, proposalReport, progressReport, finalReport, presentation " +
                "from CSEStudent, ProjectGroup, RequirementGrades " +
                "where CSEStudent.groupId = ProjectGroup.groupId " + 
                "and username = studentUsername " +
                "and reader = facultyUsername " +
                "and reader = '" + username + "'" +
                "and ProjectGroup.groupId = '" + groupId + "'";
            queryResult = myOracleDBAccess.GetData("TODO 19", sql);
            return queryResult;
        }

        public DataTable GetProjectGroupMembers(string groupId)
        {
            //****************************************************************************************
            // TODO 20: Used in GetProjectGroupMembers in App_Code\SharedDBAccess                    *
            // Construct the SQL SELECT statement to retrieve the username, name and group id of all *
            // the students in a project group identified by its group id.                           *
            // Order the result by username ascending.                                               *
            //****************************************************************************************
            sql = "select username, name, CSEStudent.groupId " +
                "from ProjectGroup, CSEStudent " +
                "where ProjectGroup.groupId = CSEStudent.groupId " +
                "and ProjectGroup.groupId = '" + groupId + "'" +
                "order by username asc";
            queryResult = myOracleDBAccess.GetData("TODO 20", sql);
            return queryResult;
        }

        public bool CreateRequirementGrades(string facultyUsername, string studentUsername, string proposalReport,
            string progressReport, string finalReport, string presentation, OracleTransaction trans)
        {
            //****************************************************************************************
            // TODO 21: Used in CreateRequirementGradesRecord in App_Code\DBHelperMethods            *
            // Construct the SQL INSERT statement to insert a value for each requirement grade       *
            // for a student, identified by a username, given by a faculty identified by a username. *
            //****************************************************************************************
            sql = "insert into RequirementGrades values ("
                + "'" + facultyUsername + "'" + ","
                + "'" + studentUsername + "'" + ","
                + "" + proposalReport + "" + ","
                + "" + progressReport + "" + ","
                + "" + finalReport + "" + ","
                + "" + presentation + ""
                + ")";

            updateResult = myOracleDBAccess.SetData("TODO 21", sql, trans);
            return updateResult;
        }

        public bool UpdateSupervisorGrades(string fypId, string studentUsername, string proposalReport,
            string progressReport, string finalReport, string presentation)
        {
            //****************************************************************************************
            // TODO 22: Used in Faculty\AssignGrades                                                 *
            // Construct the SQL UPDATE statement to update all the requirement grades given by a    *
            // supervisor who is the supervisor of an FYP identified by its fypid, to a student,     *
            // identified by his/her username.                                                       *
            // NOTE: While a student's grades can be updated by ANY of the supervisors, the username *
            // of only one of the supervisors appears in a record in the RequirementGrades table.    *
            //****************************************************************************************
            sql = "update RequirementGrades " +
                "set proposalReport = '" + proposalReport + "'" + "," +
                "progressReport = '" + progressReport + "'" + "," +
                "finalReport = '" + finalReport + "'" + "," +
                "presentation = '" + presentation + "'" +
                "where studentUsername = '" + studentUsername + "'" +
                "and facultyUsername in (select username from Supervises where fypId = '" + fypId + "')";
            updateResult = myOracleDBAccess.SetData("TODO 22", sql);
            return updateResult;
        }

        public bool UpdateReaderGrades(string facultyUsername, string studentUsername,
            string proposalReport, string progressReport, string finalReport, string presentation)
        {
            //****************************************************************************************
            // TODO 23: Used in Faculty\AssignGrades                                                 *
            // Construct the SQL UPDATE statement to update all the requirement grades given by a    *
            // reader, identified by his/her username, to a student, identified by his/her username. *
            //****************************************************************************************
            sql = "update RequirementGrades " +
                "set proposalReport = '" + proposalReport + "'" + "," +
                "progressReport = '" + progressReport + "'" + "," +
                "finalReport = '" + finalReport + "'" + "," +
                "presentation = '" + presentation + "'" + 
                "where facultyUsername = '" + facultyUsername + "'" +
                "and studentUsername = '" + studentUsername + "'";
            updateResult = myOracleDBAccess.SetData("TODO 23", sql);
            return updateResult;
        }

        #endregion SQL Statements for Faculty Web Pages - TODOS 01 to 23

        #region SQL Statements for Student Web Pages TODOS 24 - 36

        public DataTable GetStudentGroupId(string username)
        {
            //******************************************************************************
            // TODO 24: Used in GetStudentGroupId in App_Code\SharedDBAccess               *
            // Construct the SQL SELECT statement to retrieve the group id for the student *
            // identified by his/her username.                                             *
            //******************************************************************************
            sql = "select groupId "
                + "from CSEStudent "
                + "where username = '" + username + "'";
            queryResult = myOracleDBAccess.GetData("TODO 24", sql);
            return queryResult;
        }

        public DataTable GetAssignedFypId(string groupId)
        {
            //********************************************************************************
            // TODO 25: Used in IsGroupAssigned in App_Code\SharedDBAccess                   *
            // Construct the SQL SELECT statement to retrieve the fyp id of the FYP to which *
            // a project group, identified by its group id, is assigned.                     *
            //********************************************************************************
            sql = "select assignedFYP "
                + "from ProjectGroup "
                + "where groupId = '" + groupId + "'";
            queryResult = myOracleDBAccess.GetData("TODO 25", sql);
            return queryResult;
        }

        public DataTable GetGroupAvailableFYPDigests(string groupId)
        {
            //************************************************************************************
            // TODO 26: Used in GetGroupAvailableFYPDigests in App_Code\SharedDBAccess           *
            // Construct the SQL SELECT statement to retrieve the fyp id, title, category, type, *
            // minimum students and maximum students of the FYPs for which a group, identified   *
            // by its group id, can indicate an interest. Order the result by title ascending.   *
            //************************************************************************************
            sql = "select distinct FYP.fypId, title, category, type, minStudents, maxStudents "
                + "from FYP, ProjectGroup, InterestedIn "
                + "where ProjectGroup.groupId = InterestedIn.groupId "
                + "and ProjectGroup.assignedFYP is null " 
                + "and FYP.fypId not in (select fypId from InterestedIn where groupId = '" + groupId + "') "
                + "and FYP.isAvailable = 'Y' "
                + "and (select count(username) from CSEStudent where groupId = '" + groupId + "' group by groupId) "
                + "between minStudents and maxStudents "
                + "order by title asc";
            queryResult = myOracleDBAccess.GetData("TODO 26", sql);
            return queryResult;
        }

        public DataTable GetFYPAssignedToGroup(string groupId)
        {
            //********************************************************************************
            // TODO 27: Used in Student\AvailableFYPs                                        *
            // Construct the SQL SELECT statement to retrieve the title of the FYP assigned  *
            // to the group identified by its group id. Order the result by title ascending. * 
            //********************************************************************************
            sql = "select title "
                + "from FYP, ProjectGroup "
                + "where FYP.fypId = ProjectGroup.assignedFYP "
                + "and ProjectGroup.groupId = '" + groupId + "'" 
                + "order by title asc";
            queryResult = myOracleDBAccess.GetData("TODO 27", sql);
            return queryResult;
        }

        public bool IndicateInterestIn(string fypId, string groupId, string priority)
        {
            //***************************************************************************************
            // TODO 28: Used in Student\AvailableFYPs                                               *
            // Construct the SQL INSERT statement to indicate interest in an FYP, identified by its *
            // fyp id, by a project group, identified by its group id, with a specified priority.   *
            //***************************************************************************************
            sql = "insert into InterestedIn values ("
                + "'" + fypId + "'" + ","
                + "'" + groupId + "'" + ","
                + "'" + priority + "'" + ")";
            updateResult = myOracleDBAccess.SetData("TODO 28", sql);
            return updateResult;
        }

        public DataTable GetFYPsGroupHasIndicatedInterestIn(string groupId)
        {
            //********************************************************************************************
            // TODO 29: Used in GetFYPsGroupHasIndicatedInterestIn in App_Code\SharedDBAccess            *
            // Construct the SQL SELECT statement to retrieve the fyp id, title, category, type and      *
            // priority of the FYPs for which a project group, identified by its group id, has indicated *
            // an interest. Order the result first by priority ascending and then by title ascending.    *
            //********************************************************************************************
            sql = "select FYP.fypId, title, category, type, priority "
                + "from FYP, InterestedIn "
                + "where FYP.fypId = InterestedIn.fypId "
                + "and groupId = '" + groupId + "'"
                + "order by priority asc, title asc";
            queryResult = myOracleDBAccess.GetData("TODO 29", sql);
            return queryResult;
        }

        public DataTable GetStudentRecord(string username)
        {
            //***************************************************************************************
            // TODO 30: Used in Student\ManageProjectGroup                                          *
            // Construct the SQL SELECT statement to retrieve the record of a student identified by *
            // his/her username.                                                                    *
            //***************************************************************************************
            sql = "select username, name, groupId "
                + "from CSEStudent "
                + "where username = '" + username + "'";
            queryResult = myOracleDBAccess.GetData("TODO 30", sql);
            return queryResult;
        }

        public bool CreateProjectGroup(string groupId)
        {
            //****************************************************************************************
            // TODO 31: Used in Student\ManageProjectGroup                                           *
            // Construct the SQL INSERT statement to add a project group identified by its group id. *
            //****************************************************************************************
            sql = "insert into ProjectGroup values ("
                + "'" + groupId + "'" + ","
                + "null, null, null)";
            updateResult = myOracleDBAccess.SetData("TODO 31", sql);
            return updateResult;
        }



        public bool DeleteProjectGroup(string groupId)
        {
            //*******************************************************************************************
            // TODO 32: Used in Student\ManageProjectGroup                                              *
            // Construct the SQL DELETE statement to delete a project group identified by its group id. *
            //*******************************************************************************************
            sql = "delete from ProjectGroup "
                + "where groupId = '" + groupId + "'";
            updateResult = myOracleDBAccess.SetData("TODO 32", sql);
            return updateResult;
        }

        public bool AddStudentToGroup(string groupId, string username)
        {
            //******************************************************************************************
            // TODO 33: Used in Student\ManageProjectGroup                                             *
            // Construct the SQL UPDATE statement to assign a student, identified by his/her username, *
            // to a group identified by a group id.                                                    *
            //******************************************************************************************
            sql = "update CSEStudent "
                + "set groupId = '" + groupId + "'"
                + "where username = '" + username + "'";
            updateResult = myOracleDBAccess.SetData("TODO 33", sql);
            return updateResult;
        }

        public bool RemoveMemberFromGroup(string username)
        {
            //******************************************************************************************
            // TODO 34: Used in Student\ManageFYPGroup                                                 *
            // Construct the SQL UPDATE statement to remove a student, identified by his/her username, *
            // from a project group.                                                                   *
            //******************************************************************************************
            sql = "update CSEStudent "
                + "set groupId = null "
                + "where username = '" + username + "'";
            updateResult = myOracleDBAccess.SetData("TODO 34", sql);
            return updateResult;
        }

        public DataTable GetAssignedFYPInformation(string username)
        {
            //******************************************************************************************
            // TODO 35: Used in Student\ViewGrades                                                     *
            // Construct the SQL SELECT statement to retrieve the fyp id and title of the FYP to which *
            // a student, identified by his/her username, is assigned.                                 *
            //******************************************************************************************
            sql = "select FYP.fypId, title "
                + "from CSEStudent, ProjectGroup, FYP "
                + "where CSEStudent.groupId = ProjectGroup.groupId "
                + "and ProjectGroup.assignedFYP = FYP.fypId "
                + "and username = '" + username + "'";
            queryResult = myOracleDBAccess.GetData("TODO 35", sql);
            return queryResult;
        }

        public DataTable GetStudentGrades(string username)
        {
            //*******************************************************************************************
            // TODO 36: Used in Student\ViewGrades                                                      *
            // Construct the SQL SELECT statement to retrieve the faculty name, as well as the proposal *
            // report, progress report, final report and presentation grades, given by the supervisor   *
            // and the reader to the student identified by his/her username.                            *
            //*******************************************************************************************
            sql = "select name, proposalReport, progressReport, finalReport, presentation "
                + "from RequirementGrades, Faculty "
                + "where facultyUsername = username "
                + "and studentUsername = '" + username + "'";
            queryResult = myOracleDBAccess.GetData("TODO 36", sql);
            return queryResult;
        }

        #endregion SQL Statements for Student Web Pages TODOS 24 - 36

        #region SQL Statements for Coordinator Web Pages TODOS 37 - 40

        public DataTable GetFYPsWithoutReaders()
        {
            //************************************************************************************
            // TODO 37: Used in Coordinator\AssignReaders                                        *
            // Construct the SQL SELECT statement to retrieve the group id, group code, assigned *
            // fyp id as well as the title, category and type of the FYP for the project groups  *
            // that do not have an assigned reader. Order the result by group code ascending.    *
            //************************************************************************************
            sql = "select groupId, groupCode, assignedFYP, title, category, type "
                + "from ProjectGroup, FYP "
                + "where assignedFYP = fypId "
                + "and reader is null " 
                + "order by groupCode asc";
            queryResult = myOracleDBAccess.GetData("TODO 37", sql);
            return queryResult;
        }

        public decimal NumberOfFYPsAssignedToReader(string username)
        {
            //*********************************************************************************
            // TODO 38: Used in Coordinator\AssignReader                                      *
            // Construct the SQL SELECT statement to retrieve the number of project groups to *
            // which a faculty, identified by his/her username, is assigned as reader.        *
            //*********************************************************************************
            sql = "select count(reader) "
                + "from Faculty left outer join ProjectGroup on username = reader "
                + "where username = '" + username + "'"
                + "group by reader";
            aggregateQueryResult = myOracleDBAccess.GetAggregateValue("TODO 38", sql);
            return aggregateQueryResult;
        }

        public bool AssignReaderToFYP(string groupId, string username)
        {
            //*********************************************************************************
            // TODO 39: Used in Coordinator\AssignReader                                      *
            // Construct the SQL UPDATE statement to assign to a project group, identified by *
            // its group id, a reader, identified by his/her username.                        *
            //*********************************************************************************
            sql = "update ProjectGroup "
                + "set reader = '" + username + "'"
                + "where groupId = '" + groupId + "'";
            updateResult = myOracleDBAccess.SetData("TODO 39", sql);
            return updateResult;
        }

        public DataTable GetAssignedReaders()
        {
            //*********************************************************************************
            // TODO 40: Used in Coordinator\DisplayReaders                                    *
            // Construct the SQL SELECT statement to retrieve the reader name, group code and *
            // FYP title for the project groups with assigned readers. Order the result by    *
            // group code ascending.                                                          *
            //*********************************************************************************
            sql = "select name, groupCode, title "
                + "from ProjectGroup, FYP, Faculty "
                + "where ProjectGroup.assignedFYP = FYP.fypId "
                + "and reader = username "
                + "and reader is not null "
                + "order by groupCode asc";
            queryResult = myOracleDBAccess.GetData("TODO 40", sql);
            return queryResult;
        }

        #endregion SQL Statements for Coordinator Web Pages TODOS 37 - 40
    }
}
