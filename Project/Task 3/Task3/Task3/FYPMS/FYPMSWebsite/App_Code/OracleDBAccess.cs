using System;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;

namespace FYPMSWebsite.App_Code
{
    //**********************************************************
    //* THE CODE IN THIS CLASS CANNOT BE MODIFIED OR ADDED TO. *
    //*        Report problems to 3311rep@cse.ust.hk.          *
    //**********************************************************

    public class OracleDBAccess
    {
        // Set the connection string to connect to the Oracle database.
        private OracleConnection myOracleDBConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["FYPMSConnectionString"].ConnectionString);

        private bool IsAggregate(string sql)
        {
            if (sql.IndexOf(" avg(", StringComparison.OrdinalIgnoreCase) >= 0) { return true; }
            if (sql.IndexOf(" count(", StringComparison.OrdinalIgnoreCase) >= 0) { return true; }
            if (sql.IndexOf(" min(", StringComparison.OrdinalIgnoreCase) >= 0) { return true; }
            if (sql.IndexOf(" max(", StringComparison.OrdinalIgnoreCase) >= 0) { return true; }
            if (sql.IndexOf(" sum(", StringComparison.OrdinalIgnoreCase) >= 0) { return true; }
            if (sql.IndexOf(" stdev(", StringComparison.OrdinalIgnoreCase) >= 0) { return true; }
            return false;
        }

        private void SetSQLError(string source, string error)
        {
            Global.sqlError = "*** SQL error in " + source + ": " + error + ".";
        }

        #region Process an SQL SELECT statement.

        public DataTable GetData(string source, string sql)
        {
            DataTable result = null;
            try
            {
                if (sql.Trim() == "")
                {
                    throw new ArgumentException("The SQL statement is empty");
                }

                DataTable dt = new DataTable();
                if (myOracleDBConnection.State != ConnectionState.Open)
                {
                    myOracleDBConnection.Open();
                    OracleDataAdapter da = new OracleDataAdapter(sql, myOracleDBConnection);
                    da.Fill(dt);
                    myOracleDBConnection.Close();
                }
                else
                {
                    OracleDataAdapter da = new OracleDataAdapter(sql, myOracleDBConnection);
                    da.Fill(dt);
                }
                result = dt;
            }
            catch (ArgumentException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (FormatException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (OracleException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (StackOverflowException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (Exception ex)
            {
                SetSQLError(source, ex.Message);
            }
            return result;
        }
        
        #endregion Process an SQL SELECT statement.

        #region Process an SQL SELECT statement that returns only a single value.

        public decimal GetAggregateValue(string source, string sql)
        {
            // Return 0 if the table is empty or the column has no values.
            // Return -1 if there is an error in the SELECT statement.

            decimal result = -1;
            try
            {
                if (sql.Trim() == "")
                {
                    throw new ArgumentException("The SQL statement is empty");
                }
                if (!IsAggregate(sql))
                {
                    throw new ArgumentException("The query is not an aggregate query");
                }
                object aggregateValue;
                if (myOracleDBConnection.State != ConnectionState.Open)
                {
                    myOracleDBConnection.Open();
                    OracleCommand SQLCmd = new OracleCommand(sql, myOracleDBConnection);
                    SQLCmd.CommandType = CommandType.Text;
                    aggregateValue = SQLCmd.ExecuteScalar();
                    myOracleDBConnection.Close();
                }
                else
                {
                    OracleCommand SQLCmd = new OracleCommand(sql, myOracleDBConnection);
                    SQLCmd.CommandType = CommandType.Text;
                    aggregateValue = SQLCmd.ExecuteScalar();
                }
                if (aggregateValue == DBNull.Value) { result = 0; }
                else if (aggregateValue is decimal)
                {
                    result = Convert.ToDecimal(aggregateValue);
                }
                else
                {
                    throw new ArgumentException("The query is not an aggregate query");
                }
            }
            catch (ArgumentException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (FormatException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (OracleException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (StackOverflowException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (Exception ex)
            {
                SetSQLError(source, ex.Message);
            }
            return result;
        }
        
        #endregion Process an SQL SELECT statement that returns only a single value.

        #region Process SQL INSERT, UPDATE and DELETE statements.

        public bool SetData(string TODO, string sql)
        {
            // Single update transaction update.
            OracleTransaction trans = BeginTransaction(TODO);
            if (trans == null) { return false; }
            if (UpdateData(TODO, sql, trans))
            { CommitTransaction(TODO, trans); return true; } // The update succeeded.
            else
            { DisposeTransaction(TODO, trans); return false; } // The update failed.
        }

        public bool SetData(string TODO, string sql, OracleTransaction trans)
        {
            // Multiple update transaction method.
            if (UpdateData(TODO, sql, trans))
            { return true; } // The update succeeded.
            else
            { DisposeTransaction(TODO, trans); return false; } // The update failed
        }

        private bool UpdateData(string source, string sql, OracleTransaction trans)
        {
            bool result = false;
            try
            {
                if (sql.Trim() == "")
                {
                    throw new ArgumentException("The SQL statement is empty");
                }
                if (myOracleDBConnection.State != ConnectionState.Open)
                {
                    myOracleDBConnection.Open();
                }
                OracleCommand SQLCmd = new OracleCommand(sql, myOracleDBConnection);
                SQLCmd.Transaction = trans;
                SQLCmd.CommandType = CommandType.Text;
                SQLCmd.ExecuteNonQuery();
                result = true;
            }
            catch (ArgumentException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (FormatException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (ApplicationException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (OracleException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (Exception ex)
            {
                SetSQLError(source, ex.Message);
            }
            return result;
        }

        public OracleTransaction BeginTransaction(string source)
        {
            OracleTransaction result = null;
            try
            {
                if (myOracleDBConnection.State != ConnectionState.Open)
                {
                    myOracleDBConnection.Open();
                    result = myOracleDBConnection.BeginTransaction();
                }
                else
                {
                    result = myOracleDBConnection.BeginTransaction();
                }
            }
            catch (InvalidOperationException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (Exception ex)
            {
                SetSQLError(source, ex.Message);
            }
            return result;
        }

        public void CommitTransaction(string source, OracleTransaction trans)
        {
            try
            {
                if (myOracleDBConnection.State == ConnectionState.Open)
                {
                    trans.Commit();
                    myOracleDBConnection.Close();
                }
            }
            catch (ApplicationException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (FormatException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (OracleException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (Exception ex)
            {
                SetSQLError(source, ex.Message);
            }
        }

        private void DisposeTransaction(string source, OracleTransaction trans)
        {
            try
            {
                if (myOracleDBConnection.State == ConnectionState.Open)
                {
                    trans.Dispose();
                    myOracleDBConnection.Close();
                }
            }
            catch (ApplicationException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (FormatException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (OracleException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                SetSQLError(source, ex.Message);
            }
            catch (Exception ex)
            {
                SetSQLError(source, ex.Message);
            }
        }
        
        #endregion Process SQL INSERT, UPDATE and DELETE statements.

    }
}