using System;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;

namespace UniversityWebsite.App_Code
{
    //**********************************************************
    //* THE CODE IN THIS CLASS CANNOT BE MODIFIED OR ADDED TO. *
    //*        Report problems to 3311rep@cse.ust.hk.          *
    //**********************************************************

    public class OracleDBAccess
    {
        // Set the connection string to connect to the Oracle database.
        private readonly OracleConnection myOracleDBConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString);

        #region Process an SQL SELECT statement.
        public DataTable GetData(string sql)
        {
            DataTable result = null;
            try
            {
                Global.sqlError = "";
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
                Global.sqlError = ex.Message;
            }
            catch (FormatException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (OracleException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (StackOverflowException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (Exception ex)
            {
                Global.sqlError = ex.Message;
            }
            Global.sqlError += ".";
            return result;
        }
        #endregion Process an SQL SELECT statement.

        #region Process an SQL SELECT statement that returns only a single value.
        public decimal GetAggregateValue(string sql)
        {
            // Return 0 if the table is empty or the column has no values.
            // Return -1 if there is an error in the SELECT statement.
            Global.sqlError = "";
            decimal result = -1;
            try
            {
                if (sql.Trim() == "")
                {
                    throw new ArgumentException("The SQL statement is empty");
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
                if (aggregateValue is decimal)
                {
                    result = (DBNull.Value == aggregateValue ? 0 : Convert.ToDecimal(aggregateValue));
                }
                else
                {
                    Global.sqlError = "The query is not an aggregate query";
                }
            }
            catch (ArgumentException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (FormatException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (OracleException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (StackOverflowException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (Exception ex)
            {
                Global.sqlError = ex.Message;
            }
            Global.sqlError += ".";
            return result;
        }
        #endregion Process an SQL SELECT statement that returns only a single value.

        #region Process SQL INSERT, UPDATE and DELETE statements.

        public bool SetData(string sql, OracleTransaction trans)
        {
            bool result = false;
            Global.sqlError = "";
            try
            {
                if (sql.Trim() == "")
                {
                    throw new ArgumentException("The SQL statement is empty");
                }
                OracleCommand SQLCmd = new OracleCommand(sql, myOracleDBConnection);
                SQLCmd.Transaction = trans;
                SQLCmd.CommandType = CommandType.Text;
                SQLCmd.ExecuteNonQuery();
                result = true;
            }
            catch (ArgumentException ex)
            {
                Global.sqlError = ex.Message;
                myOracleDBConnection.Close();
            }
            catch (FormatException ex)
            {
                Global.sqlError = ex.Message;
                myOracleDBConnection.Close();
            }
            catch (ApplicationException ex)
            {
                Global.sqlError = ex.Message;
                myOracleDBConnection.Close();
            }
            catch (OracleException ex)
            {
                Global.sqlError = ex.Message;
                myOracleDBConnection.Close();
            }
            catch (InvalidOperationException ex)
            {
                Global.sqlError = ex.Message;
                myOracleDBConnection.Close();
            }
            catch (Exception ex)
            {
                Global.sqlError = ex.Message;
                myOracleDBConnection.Close();
            }
            return result;
        }

        public OracleTransaction BeginTransaction()
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
                Global.sqlError = ex.Message;
            }
            catch (Exception ex)
            {
                Global.sqlError = ex.Message;
            }
            Global.sqlError += ".";
            return result;
        }

        public void CommitTransaction(OracleTransaction trans)
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
                Global.sqlError = ex.Message;
            }
            catch (FormatException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (OracleException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (Exception ex)
            {
                Global.sqlError = ex.Message;
            }
            Global.sqlError += ".";
        }

        public void DisposeTransaction(OracleTransaction trans)
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
                Global.sqlError = ex.Message;
            }
            catch (FormatException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (OracleException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                Global.sqlError = ex.Message;
            }
            catch (Exception ex)
            {
                Global.sqlError = ex.Message;
            }
            Global.sqlError += ".";
        }
        #endregion Process SQL INSERT, UPDATE and DELETE statements.
    }
}