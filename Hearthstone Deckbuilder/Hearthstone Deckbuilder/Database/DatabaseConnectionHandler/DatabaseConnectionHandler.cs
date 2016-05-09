using System;
using Npgsql;
using System.Data;
using Hearthstone_Deckbuilder.NSGlobalVariables;

namespace Hearthstone_Deckbuilder.Database.NSDatabaseConnector
{
    public class DatabaseConnectionHandler
    {

        public NpgsqlConnection connectToDatabase()
        {
            try
            {
                string connectionString = GlobalVariables.POSTGRE_CONNECTION_STRING;
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public DataTable executeSelectQuery(NpgsqlCommand command, NpgsqlConnection conn)
        {
            try
            {
                if(conn == null)
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

                if(dataTable.Rows.Count <= 0)
                {
                    return null;
                }
                return dataTable;
            } catch(NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool executeChangeQuery(NpgsqlCommand command, NpgsqlConnection conn)
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
