using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using System.Data;

namespace EveHQSharp.SDEConverter
{
    class DatabaseAdapter
    {
        protected static String sqliteFolder = Application.StartupPath;
        private const String SqliteDatabase = "eve.db";

        /// <summary>
        /// Function to return the SQLite DB connection string
        /// </summary>
        /// <returns>A string indicating the connection parameters for SQLite</returns>
        public static String getSqlLiteConnectionString() 
        {
            return "Data Source=\"" + Path.Combine(sqliteFolder, SqliteDatabase) + "\";FailIfMissing=True;Version=3;";
        }

        /// <summary>
        /// Function to check if a connection can be made to the custom database
        /// </summary>
        /// <param name="silentResponse">
        /// Set to "True" if the routine is to show an error message on connection failure
        /// </param>
        /// <returns>A boolean value indicating if the connection was succesful</returns>
        public static Boolean checkCustomDatabaseConnection(Boolean silentResponse = false) 
        {
            SQLiteConnection connection = new SQLiteConnection(getSqlLiteConnectionString());

            try
            {
                connection.Open();
                connection.Close();

                if (!silentResponse)
                {
                    MessageBox.Show(
                        "Connected successfully to SQLite database",
                        "Connection Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                return true;
            }
            catch (Exception exception)
            {
                if (!silentResponse)
                {
                    MessageBox.Show(
                        exception.Message + "\r\nPlease make sure " + SqliteDatabase + " is in the executable folder",
                        "Error Opening SQLite Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }

                return false;
            }
        }

        /// <summary>
        /// Function to return a list of database tables in the custom database
        /// </summary>
        /// <returns>A List(Of String) containing the names of the tables in the custom database</returns>
        public static List<String> getDatabaseTables()
        {
            List<String> tables = new List<String>();

            DataSet schemaTable = getStaticData("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;");

            foreach (DataRow tableRow in schemaTable.Tables[0].Rows)
            {
                tables.Add(tableRow["name"].ToString());
            }

            return tables;
        }

        /// <summary>
        /// Function to return data from a database
        /// </summary>
        /// <param name="sql">A string containing the SQL query to execute</param>
        /// <returns>A Dataset containing the requrested data</returns>
        public static DataSet getStaticData(String sql)
        {
            DataSet eveData = new DataSet();
            SQLiteConnection connection = new SQLiteConnection(getSqlLiteConnectionString());

            try
            {
                connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
                adapter.Fill(eveData, "EveData");
                connection.Close();

                return eveData;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Function to write data to the static database via an SQL string
        /// </summary>
        /// <param name="sql">A string containing the SQL command to execute</param>
        /// <returns>The number of records affected by the command</returns>
        /// <remarks>Returns -2 in the event of an error</remarks>
        public static int setStaticData(String sql)
        {
            int affectedRecords;

            SQLiteConnection connection = new SQLiteConnection(getSqlLiteConnectionString());

            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                affectedRecords = command.ExecuteNonQuery();

                return affectedRecords;
            }
            catch (Exception)
            {
                return -2;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) 
                {
                    connection.Close();
                }
            }
        }
    }
}
