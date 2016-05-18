using System.Collections.Generic;
using System.Data;
using Hearthstone_Deckbuilder.Database.NSDatabaseConnector;
using Hearthstone_Deckbuilder.Database.NSUserDatabase.Controller;
using Hearthstone_Deckbuilder.NSDatatypes;
using Npgsql;

namespace Hearthstone_Deckbuilder.Database.NSDeckDatabase.Controller
{
    public class DeckDatabaseController
    {
        private readonly DatabaseConnectionHandler databaseConnection;

        public DeckDatabaseController()
        {
            databaseConnection = new DatabaseConnectionHandler();
        }

        public bool CreateNewDeck(string deckname, User user)
        {
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("insert into dbdeck(deckname, username) values(:value1, :value2)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters.Add(new NpgsqlParameter("value2", DbType.String));
            command.Parameters[0].Value = deckname;
            command.Parameters[1].Value = user.UserName;
            command.Connection = conn;
            if (databaseConnection.ExecuteChangeQuery(command, conn))
            {
                return true;
            }

            return false;
        }

        public bool AddCardsToDeck(List<Card> cardList, Deck deck)
        {
            string sqlQuery = "insert into dbcardtodeck(deckid, cardid) values ";
            foreach (Card c in cardList)
            {
                sqlQuery += "('" + deck.DeckId + "', '" + c.CardId + "'), ";
            }

            sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 2);
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand(sqlQuery, conn);
            command.Connection = conn;
            if (databaseConnection.ExecuteChangeQuery(command, conn))
            {
                return true;
            }

            return false;
        }

        public bool AddCardToDeck(Card card, Deck deck)
        {
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("insert into dbcardtodeck(deckid, cardid) values(:value1, :value2)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters.Add(new NpgsqlParameter("value2", DbType.String));
            command.Parameters[0].Value = card.CardId;
            command.Parameters[1].Value = deck.DeckId;
            command.Connection = conn;
            if (databaseConnection.ExecuteChangeQuery(command, conn))
            {
                return true;
            }

            return false;
        }

        public Deck GetDeckById(string deckId)
        {
            Deck returnDeck = new Deck();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbdeck where deckid = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = deckId;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                UserDatabaseController udc = new UserDatabaseController();
                returnDeck.DeckId = deckId;
                returnDeck.DeckName = result.Rows[0].ItemArray[1].ToString();
                returnDeck.DeckUser = udc.GetUser(result.Rows[0].ItemArray[2].ToString());
                conn = databaseConnection.ConnectToDatabase();
                conn.CreateCommand();
                command = new NpgsqlCommand("select deckid, cardid, cardtodeckid from dbcardtodeck where deckid = :value1", conn);
                command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
                command.Parameters[0].Value = deckId;
                command.Connection = conn;
                result = databaseConnection.ExecuteSelectQuery(command, conn);
                if (result != null)
                {
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        returnDeck.CardList.Add(new Card(result.Rows[i].ItemArray[1].ToString(), result.Rows[i].ItemArray[2].ToString()));
                    }
                }
            }

            return returnDeck;
        }

        public List<Deck> GetAllDecksByUser(User user)
        {
            List<Deck> deckList = new List<Deck>();
            NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbdeck where username = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = user.UserName;
            command.Connection = conn;
            DataTable result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    deckList.Add(new Deck(result.Rows[i].ItemArray[0].ToString(), result.Rows[i].ItemArray[1].ToString(), user));
                }
            }

            conn = databaseConnection.ConnectToDatabase();
            conn.CreateCommand();
            command = new NpgsqlCommand("select ctd.deckid, ctd.cardid, ctd.cardtodeckid from dbcardtodeck as ctd join dbdeck as d on d.deckid = ctd.deckid join dbuser u on u.username = d.username where u.username = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = user.UserName;
            command.Connection = conn;
            result = databaseConnection.ExecuteSelectQuery(command, conn);
            if (result != null)
            {
                foreach (Deck d in deckList)
                {
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        if (result.Rows[i].ItemArray[0].ToString().Equals(d.DeckId))
                        {
                            d.CardList.Add(new Card(result.Rows[i].ItemArray[1].ToString(), result.Rows[i].ItemArray[2].ToString()));
                        }
                    }
                }
            }

            return deckList;
        }

        public bool UpdateDeckList(Deck updatedDeck)
        {
            if (HasDeckChanged(updatedDeck))
            {
                NpgsqlConnection conn = databaseConnection.ConnectToDatabase();
                conn.CreateCommand();
                NpgsqlCommand command = new NpgsqlCommand("update dbdeck set deckname = :value1 where deckid = :value2", conn);
                command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
                command.Parameters.Add(new NpgsqlParameter("value2", DbType.String));
                command.Parameters[0].Value = updatedDeck.DeckName;
                command.Parameters[1].Value = updatedDeck.DeckId;
                command.Connection = conn;
                if (databaseConnection.ExecuteChangeQuery(command, conn))
                {
                    conn = databaseConnection.ConnectToDatabase();
                    conn.CreateCommand();

                    // Building SQL Query
                    string sqlQuery = "update dbcardtodeck as ctd set cardid = c.cardid from (values ";
                    foreach (Card c in updatedDeck.CardList)
                    {
                        sqlQuery += "('" + c.CardToDeckId + "', '" + c.CardId + "'), ";
                    }

                    sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 2);
                    sqlQuery += ") as c(cardtodeckid, cardid) where c.cardtodeckid = ctd.cardtodeckid";

                    command = new NpgsqlCommand(sqlQuery, conn);
                    command.Connection = conn;
                    if (databaseConnection.ExecuteChangeQuery(command, conn))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool HasDeckChanged(Deck updatedDeck)
        {
            Deck deckOnDB = GetDeckById(updatedDeck.DeckId);
            if (deckOnDB.DeckName.Equals(updatedDeck.DeckName))
            {
                if (updatedDeck.CardList.Count == deckOnDB.CardList.Count)
                {
                    int index = 0;
                    foreach (Card c in deckOnDB.CardList)
                    {
                        if (!c.CardId.Equals(updatedDeck.CardList[index].CardId))
                        {
                            return true;
                        }

                        index++;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }
    }
}
