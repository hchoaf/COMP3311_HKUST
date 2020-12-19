using Oracle.DataAccess.Client;
using System.Data;

namespace UniversityWebsite.App_Code
{
    /// <summary>
    /// Student name: Hangsun CHO
    /// Student id: 20473370
    /// </summary>

    public class UniversityDB
    {
        private readonly OracleDBAccess myOracleDBAccess = new OracleDBAccess();
        private string sql;

        #region SQL statements for admin
        public DataTable GetStudentRecord(string studentId)
        {
            //*********************************************************************
            // TODO 01: Used in Admin/SearchForStudent.aspx.cs                    *
            // Construct the SELECT statement to find the record (i.e., to return *
            // all the attributes) of a student identified by his/her student id. *
            //*********************************************************************
            sql = "select * " +
                "from Student " +
                "where studentId='" + studentId + "'";
            return myOracleDBAccess.GetData(sql);
        }

        public decimal StudentRecordExists(string studentId)
        {
            //******************************************************************
            // TODO 02: Used in Admin/SearchForStudent.aspx.cs                 *
            // Construct the SELECT statement to determine if a record exists  *
            // in the database for a student identified by his/her student id. *
            // The SELECT statement should return:                             *
            //    0 - if the student record does not exist;                    *
            //    1 - if the student record exists.                            *
            //******************************************************************
            sql = "select count(*) " +
                "from Student " +
                "where studentId = '" + studentId + "'";
            return myOracleDBAccess.GetAggregateValue(sql);
        }

        public DataTable GetDepartmentStudentRecords(string departmentId)
        {
            //******************************************************************
            // TODO 03: Used in Admin/SearchStudentRecords.aspx.cs             *
            // Construct the SELECT statement to find the id, last name, first *
            // name, email and cga of the students in a department identified  *
            // by a department id. Order the result by last name ascending.    *
            //******************************************************************
            sql = "select studentId, lastName, firstName, email, cga " + 
                "from Student, Department " + 
                "where Student.departmentId = Department.departmentId " +
                "and Department.departmentId = '" + departmentId +"'";
            return myOracleDBAccess.GetData(sql);
        }

        public DataTable GetDepartments()
        {
            //******************************************************
            // TODO 04: Used in Admin/SearchStudentRecords.aspx.cs *
            // Construct the SELECT statement to retrieve the id   *
            // and name of all departments.                        *
            //******************************************************
            sql = "select departmentId, departmentName " + 
                "from Department";
            return myOracleDBAccess.GetData(sql);
        }

        public bool CreateStudentRecord(string studentId, string firstName, 
            string lastName, string email, string phoneNo, string cga, 
            string departmentId, string admissionYear)
        {
            //*****************************************************************
            // TODO 05: Used in Admin/CreateStudentRecord.aspx.cs             *
            // Construct the INSERT statement to create a new student record. *
            //*****************************************************************
            sql = "insert into Student values (" + 
                "'" + studentId +"'" + "," +
                "'" + firstName + "'" + "," +
                "'" + lastName + "'" + "," +
                "'" + email + "'" + "," +
                "'" + phoneNo + "'" + "," +
                "" + cga + "" + "," +
                "'" + departmentId + "'" + "," +
                "'" + admissionYear + "'" +
                ")";
            return  UpdateData(sql);
        }

        #endregion SQL statements for admin

        #region SQL statements for students

        public DataTable GetEnrolledCourses(string email)
        {
            //***********************************************************************
            // TODO 06: Used in Student/EnrolledCourses.aspx.cs                     *
            // Construct the SELECT statement to find the id, name, grade, credits  *
            // and instructor of the courses in which a student, identified by      *
            // his/her email, is enrolled. Order the result by course id ascending. *
            //***********************************************************************
            sql = "select Course.courseId, courseName, grade, credits, instructor " +
                "from Course, EnrollsIn, Student " +
                "where Course.courseId = EnrollsIn.courseId " +
                "and EnrollsIn.studentId = Student.studentId " +
                "and Student.email = '" + email + "'" + 
                "order by Course.courseId";
            return myOracleDBAccess.GetData(sql);
        }

        public DataTable GetStudentId(string email)
        {
            //***************************************************
            // TODO 07: Used in Student/EnrollInCourses.aspx.cs *
            // Construct the SELECT statement to find the id of *
            // a student identified by his/her email.           *
            //***************************************************
            sql = "select studentId " + 
                "from Student " +
                "where Student.email = '" + email + "'";
            return myOracleDBAccess.GetData(sql);
        }

        public DataTable GetCoursesAvailableToEnrollIn(string email)
        {
            //**********************************************************************
            // TODO 08: Used in Student/EnrollInCourses.aspx.cs                    *
            // Construct the SELECT statement to find the id, name, credits and    *
            // instructor of the courses in which a student, identified by his/her *
            // email, is not enrolled. Order the result by course id ascending.    *
            //**********************************************************************
            sql = "select courseId, courseName, credits, instructor " + 
                "from Course "+
                "where courseId not in (" + 
                "select courseId " + 
                "from EnrollsIn, Student " +
                "where EnrollsIn.studentId = Student.studentId " + 
                "and Student.email = '" + email + "'" +
                ") " +
                "order by courseId asc";
            return myOracleDBAccess.GetData(sql);
        }

        public bool EnrollInCourses(string studentId, string courseId)
        {
            //******************************************************************
            // TODO 09: Used in Student/EnrollInCourses.aspx.cs                *
            // Construct the INSERT statement to enroll a student in a course. *
            //******************************************************************
            sql = "insert into EnrollsIn values (" + 
                "'" + studentId + "'" + ", " +
                "'" + courseId + "'" + ", " + 
                "null" + 
                ")";
            return UpdateData(sql);
        }

        #endregion SQL statements for students

        #region *** DO NOT CHANGE THE METHODS BELOW THIS LINE. THEY ARE NOT TODOS!!! ***!

        public bool IsUnique(string tableName, string attributeName, string attributeValue)
        {
            sql = "select count(*) from " + tableName + " where " + attributeName + "='" + attributeValue + "'";
            if (myOracleDBAccess.GetAggregateValue(sql) == 0) { return true; }
            else { return false; }
        }

        public decimal IsUserInRole(string tableName, string attributeName, string value)
        {
            sql = "select count(*) from " + tableName + " where " + attributeName + "='" + value + "'";
            return myOracleDBAccess.GetAggregateValue(sql);
        }

        private bool UpdateData(string sql)
        {
            OracleTransaction trans = myOracleDBAccess.BeginTransaction();
            if (trans == null) { return false; }  // Error creating the transaction.
            if (myOracleDBAccess.SetData(sql, trans))
            { myOracleDBAccess.CommitTransaction(trans); return true; } // The update succeeded.
            else
            { myOracleDBAccess.DisposeTransaction(trans); return false; } // The update failed.
        }
        #endregion
    }
}