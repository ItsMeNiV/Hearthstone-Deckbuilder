using System;
using System.Data;
using Hearthstone_Deckbuilder.NSGlobalVariables;
using Npgsql;

namespace Hearthstone_Deckbuilder.Database.NSDatabaseConnector
{
    public class DatabaseConnectionHandler
    {
        public NpgsqlConnection ConnectToDatabase()
        {
            try
            {
                string connectionString = GlobalVariables.PostgreConnectionString;
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public DataTable ExecuteSelectQuery(NpgsqlCommand command, NpgsqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    return null;
                }

                DataTable dataTable = new DataTable();
                DataSet dataSet = new DataSet();
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);
                dataSet.Reset();
                dataAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
                conn.Close();

                return dataTable.Rows.Count <= 0 ? null : dataTable;
            } 
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool ExecuteChangeQuery(NpgsqlCommand command, NpgsqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    return false;
                }

                if (command.ExecuteNonQuery() == 0)
                {
                    conn.Close();
                    return false;
                }

                conn.Close();
                return true;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
