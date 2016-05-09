using Hearthstone_Deckbuilder.NSDatatypes;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System;
using Hearthstone_Deckbuilder.Database.NSUserDatabase.Controller;
using Hearthstone_Deckbuilder.Database.NSDatabaseConnector;

namespace Hearthstone_Deckbuilder.Database.NSDeckDatabase.Controller
{
    class DeckDatabaseController
    {

        DatabaseConnectionHandler _dc;

        public DeckDatabaseController()
        {
            _dc = new DatabaseConnectionHandler();
        }

        public bool createNewDeck(string deckname, User user)
        {
            NpgsqlConnection conn = _dc.connectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("insert into dbdeck(deckname, username) values(:value1, :value2)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters.Add(new NpgsqlParameter("value2", DbType.String));
            command.Parameters[0].Value = deckname;
            command.Parameters[1].Value = user.userName;
            command.Connection = conn;
            if (_dc.executeChangeQuery(command, conn))
            {
                return true;
            }
            return false;
        }

        public bool addCardsToDeck(List<Card> cardList, Deck deck)
        {
            string sqlQuery = "insert into dbcardtodeck(deckid, cardid) values ";
            foreach (Card c in cardList)
            {
                sqlQuery += "('" + deck.deckId + "', '" + c.cardId + "'), ";
            }
            sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 2);
            NpgsqlConnection conn = _dc.connectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand(sqlQuery, conn);
            command.Connection = conn;
            if (_dc.executeChangeQuery(command, conn))
            {
                return true;
            }
            return false;
        }

        public Deck getDeckById(string deckId)
        {
            Deck returnDeck = new Deck();
            NpgsqlConnection conn = _dc.connectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbdeck where deckid = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = deckId;
            command.Connection = conn;
            DataTable result = _dc.executeSelectQuery(command, conn);
            if (result != null)
            {
                UserDatabaseController udc = new UserDatabaseController();
                returnDeck.deckId = deckId;
                returnDeck.deckName = result.Rows[0].ItemArray[1].ToString();
                returnDeck.deckUser = udc.getUser(result.Rows[0].ItemArray[2].ToString());
                conn = _dc.connectToDatabase();
                conn.CreateCommand();
                command = new NpgsqlCommand("select deckid, cardid, cardtodeckid from dbcardtodeck where deckid = :value1", conn);
                command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
                command.Parameters[0].Value = deckId;
                command.Connection = conn;
                result = _dc.executeSelectQuery(command, conn);
                if (result != null)
                {
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        returnDeck.cardList.Add(new Card(result.Rows[i].ItemArray[1].ToString(), result.Rows[i].ItemArray[2].ToString()));
                    }
                }
            }
            return returnDeck;
        }

        public List<Deck> getAllDecksByUser(User user)
        {
            List<Deck> deckList = new List<Deck>();
            NpgsqlConnection conn = _dc.connectToDatabase();
            conn.CreateCommand();
            NpgsqlCommand command = new NpgsqlCommand("select * from dbdeck where username = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = user.userName;
            command.Connection = conn;
            DataTable result = _dc.executeSelectQuery(command, conn);
            if (result != null)
            {
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    deckList.Add(new Deck(result.Rows[i].ItemArray[0].ToString(), result.Rows[i].ItemArray[1].ToString(), user));
                }
            }
            conn = _dc.connectToDatabase();
            conn.CreateCommand();
            command = new NpgsqlCommand("select ctd.deckid, ctd.cardid, ctd.cardtodeckid from dbcardtodeck as ctd join dbdeck as d on d.deckid = ctd.deckid join dbuser u on u.username = d.username where u.username = :value1", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
            command.Parameters[0].Value = user.userName;
            command.Connection = conn;
            result = _dc.executeSelectQuery(command, conn);
            if (result != null)
            {
                foreach (Deck d in deckList)
                {
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        if (result.Rows[i].ItemArray[0].ToString().Equals(d.deckId))
                        {
                            d.cardList.Add(new Card(result.Rows[i].ItemArray[1].ToString(), result.Rows[i].ItemArray[2].ToString()));
                        }
                    }
                }
            }
            return deckList;
        }

        public bool updateDeckList(Deck updatedDeck)
        {
            if (hasDeckChanged(updatedDeck))
            {
                NpgsqlConnection conn = _dc.connectToDatabase();
                conn.CreateCommand();
                NpgsqlCommand command = new NpgsqlCommand("update dbdeck set deckname = :value1 where deckid = :value2", conn);
                command.Parameters.Add(new NpgsqlParameter("value1", DbType.String));
                command.Parameters.Add(new NpgsqlParameter("value2", DbType.String));
                command.Parameters[0].Value = updatedDeck.deckName;
                command.Parameters[1].Value = updatedDeck.deckId;
                command.Connection = conn;
                if (_dc.executeChangeQuery(command, conn))
                {
                    conn = _dc.connectToDatabase();
                    conn.CreateCommand();
                    //Building SQL Query
                    string sqlQuery = "update dbcardtodeck as ctd set cardid = c.cardid from (values ";
                    foreach (Card c in updatedDeck.cardList)
                    {
                        sqlQuery += "('" + c.cardToDeckId + "', '" + c.cardId + "'), ";
                    }
                    sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 2);
                    sqlQuery += ") as c(cardtodeckid, cardid) where c.cardtodeckid = ctd.cardtodeckid";

                    command = new NpgsqlCommand(sqlQuery, conn);
                    command.Connection = conn;
                    if (_dc.executeChangeQuery(command, conn))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool hasDeckChanged(Deck updatedDeck)
        {
            Deck deckOnDB = getDeckById(updatedDeck.deckId);
            if (deckOnDB.deckName.Equals(updatedDeck.deckName))
            {
                if (updatedDeck.cardList.Count == deckOnDB.cardList.Count)
                {
                    int index = 0;
                    foreach (Card c in deckOnDB.cardList)
                    {
                        if (!c.cardId.Equals(updatedDeck.cardList[index].cardId))
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
