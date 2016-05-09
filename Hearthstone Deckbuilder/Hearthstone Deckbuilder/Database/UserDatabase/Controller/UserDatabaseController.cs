using System;
using System.Data;
using System.Security.Cryptography;
using Hearthstone_Deckbuilder.NSDatatypes;
using Npgsql;
using Hearthstone_Deckbuilder.Database.NSCryptoHandler;
using Hearthstone_Deckbuilder.Database.NSDatabaseConnector;

namespace Hearthstone_Deckbuilder.Database.NSUserDatabase.Controller
{
    public class UserDatabaseController
    {
        DatabaseConnectionHandler _dc;
        public UserDatabaseController()
        {
            _dc = new DatabaseConnectionHandler();
        }

        public bool createNewUser(string username, string plainTextPassword)
        {
            byte[] passwordSalt = CryptoHandler.generatePasswordSalt();
            string passwordHash = CryptoHandler.hashPassword(plainTextPassword, passwordSalt);
            NpgsqlConnection conn = _dc.connectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("insert into DBUser values(:value1, :value2, :value3)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters.Add(new NpgsqlParameter("value2", DbType.String));
            command.Parameters.Add(new NpgsqlParameter("value3", DbType.String));
            command.Parameters[0].Value = username;
            command.Parameters[1].Value = passwordHash;
            command.Parameters[2].Value = Convert.ToBase64String(passwordSalt);
            command.Connection = conn;
            if (_dc.executeChangeQuery(command, conn))
            {
                return true;
            }
            return false;
        }

        public User getUser(string username)
        {
            NpgsqlConnection conn = _dc.connectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from DBUser where username = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = username;
            command.Connection = conn;
            DataTable result = _dc.executeSelectQuery(command, conn);
            User user = new User();
            if (result != null)
            {
                user.userName = result.Rows[0].ItemArray[0].ToString();
                user.passwordHash = result.Rows[0].ItemArray[1].ToString();
                user.passwordSalt = result.Rows[0].ItemArray[2].ToString();
            }
            else
            {
                Console.WriteLine("Couldn't get user!");
            }
            return user;
        }

        public bool updateUserPassword(User user, string newPassword)
        {
            NpgsqlConnection conn = _dc.connectToDatabase();
            conn.CreateCommand();
            byte[] passwordSalt = CryptoHandler.generatePasswordSalt();
            string passwordHash = CryptoHandler.hashPassword(newPassword, passwordSalt);
            NpgsqlCommand command = new NpgsqlCommand("update DBUser set passwordhash = :value1, passwordsalt = :value2 where username = :value3");
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters.Add(new NpgsqlParameter("value2", DbType.String));
            command.Parameters.Add(new NpgsqlParameter("value3", DbType.String));
            command.Parameters[0].Value = passwordHash;
            command.Parameters[1].Value = Convert.ToBase64String(passwordSalt);
            command.Parameters[2].Value = user.userName;
            command.Connection = conn;
            if (_dc.executeChangeQuery(command, conn))
            {
                return true;
            }
            return false;
        }

        public bool deleteUser(User user)
        {
            NpgsqlConnection conn = _dc.connectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("delete from DBUser where username = :value1");
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = user.userName;
            command.Connection = conn;
            if(_dc.executeChangeQuery(command, conn))
            {
                return true;
            }
            return false;
        }

        public bool checkPasswordForLogin(string passwordEntered, User user)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(user.passwordHash);
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(passwordEntered, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

    }
}
