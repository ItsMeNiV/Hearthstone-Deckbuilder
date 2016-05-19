using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearthstone_Deckbuilder.Database.NSDatabaseConnector;
using Hearthstone_Deckbuilder.NSDatatypes;
using Npgsql;
using System.Data;
using Hearthstone_Deckbuilder.NSGlobalVariables;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Hearthstone_Deckbuilder.Database.NSCardDatabase.Controller
{
    class CardDatabaseController
    {

        private readonly DatabaseConnectionHandler databaseConnection;

        public CardDatabaseController()
        {
            databaseConnection = new DatabaseConnectionHandler();
        }

        public Card getCardById(string cardId)
        {
            Card returnCard = new Card(cardId);
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard", conn);
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCard = dataTableToCardList(result).ToArray()[0];
            }
            return returnCard;
        }

        public List<Card> GetAllCards()
        {
            List<Card> returnCardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard", conn);
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCardList = dataTableToCardList(result);
            }
            return returnCardList;
        }

        public Card GetCardByName(string name)
        {
            List<Card> cardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard where cardname = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = name;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                cardList = dataTableToCardList(result);
            }
            if (cardList.Count == 1)
            {
                return cardList[0];
            }
            else
            {
                return null;
            }
        }

        public List<Card> GetCardsByManacost(int manacost)
        {
            List<Card> returnCardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard where manacost = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.Int32));
            command.Parameters[0].Value = manacost;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCardList = dataTableToCardList(result);
            }
            return returnCardList;
        }

        public List<Card> GetCardsByAttack(int attack)
        {
            List<Card> returnCardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard where attack = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.Int32));
            command.Parameters[0].Value = attack;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCardList = dataTableToCardList(result);
            }
            return returnCardList;
        }

        public List<Card> GetCardsByHealth(int health)
        {
            List<Card> returnCardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard where health = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.Int32));
            command.Parameters[0].Value = health;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCardList = dataTableToCardList(result);
            }
            return returnCardList;
        }
        public List<Card> GetCardsByType(string type)
        {
            List<Card> returnCardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard where cardtype = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = type;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCardList = dataTableToCardList(result);
            }
            return returnCardList;
        }

        public List<Card> GetCardsByRarity(string rarity)
        {
            List<Card> returnCardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard where rarity = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = rarity;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCardList = dataTableToCardList(result);
            }
            return returnCardList;
        }

        public List<Card> GetCardsByClass(string cardClass)
        {
            List<Card> returnCardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard where cardclass = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = cardClass;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCardList = dataTableToCardList(result);
            }
            return returnCardList;
        }

        public List<Card> GetCardsByDurability(int durability)
        {
            List<Card> returnCardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard where durability = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.Int32));
            command.Parameters[0].Value = durability;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCardList = dataTableToCardList(result);
            }
            return returnCardList;
        }

        public List<Card> SearchCardsByCardname(string searchName)
        {
            List<Card> returnCardList = new List<Card>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbcard where cardname like :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = "%" + searchName + "%";
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                returnCardList = dataTableToCardList(result);
            }
            return returnCardList;
        }

        private List<Card> dataTableToCardList(DataTable dataTable)
        {
            List<Card> returnCardList = new List<Card>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string id = dataTable.Rows[i].ItemArray[0].ToString();
                string name = dataTable.Rows[i].ItemArray[1].ToString();
                int manacost = Convert.ToInt32(dataTable.Rows[i].ItemArray[2]);
                string cardtext = dataTable.Rows[i].ItemArray[3].ToString();
                int attack;
                if (dataTable.Rows[i].ItemArray[4].ToString().Equals(""))
                {
                    attack = 0;
                }
                else
                {
                    attack = Convert.ToInt32(dataTable.Rows[i].ItemArray[4]);
                }
                int health;
                if (dataTable.Rows[i].ItemArray[5].ToString().Equals(""))
                {
                    health = 0;
                }
                else
                {
                    health = Convert.ToInt32(dataTable.Rows[i].ItemArray[5]);
                }
                string type = dataTable.Rows[i].ItemArray[6].ToString();
                string rarity = dataTable.Rows[i].ItemArray[7].ToString();
                string cardclass = dataTable.Rows[i].ItemArray[8].ToString();
                int durability;
                if (dataTable.Rows[i].ItemArray[9].ToString().Equals(""))
                {
                    durability = 0;
                }
                else
                {
                    durability = Convert.ToInt32(dataTable.Rows[i].ItemArray[9]);
                }
                string imgLink = dataTable.Rows[i].ItemArray[10].ToString();

                returnCardList.Add(new Card(id, name, manacost, cardtext, attack, health, type, rarity, cardclass, durability, imgLink));
            }
            return returnCardList;
        }

    }
}
