using System;
using System.Windows;
using Hearthstone_Deckbuilder.Datatypes;

namespace Hearthstone_Deckbuilder
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (e.Args.Length == 2)
            {
                GlobalVariables.GlobalVariables.API_KEY = e.Args[0].ToString();
                GlobalVariables.GlobalVariables.POSTGRE_CONNECTION_STRING = e.Args[1].ToString();
            }
            else
            {
                Console.WriteLine("Wrong Usage.\nPlease add the API Key and the Postgre Connection String to the debug commandline arguments");
                base.Shutdown();
            }
            Database.UserDatabase.Controller.UserDatabaseController udbc = new Database.UserDatabase.Controller.UserDatabaseController();
            User user = udbc.getUser("testabc");
            if (user.UserName != null)
            {
                if(udbc.checkPasswordForLogin("ayylmao", user))
                {
                    Console.WriteLine("Correct Password!");
                }
                else
                {
                    Console.WriteLine("Wrong Password!");
                }
                if(udbc.updateUserPassword(user, "ayyylmao"))
                {
                    Console.WriteLine("Updated user!");
                }
                else
                {
                    Console.WriteLine("Couldn't update User!");
                }
            }
            user = udbc.getUser("testabc");
            if (user.UserName != null)
            {
                if(udbc.checkPasswordForLogin("ayyylmao", user))
                {
                    Console.WriteLine("Correct Password!");
                }
                else
                {
                    Console.WriteLine("Wrong Password!");
                }
                if (udbc.updateUserPassword(user, "ayylmao"))
                {
                    Console.WriteLine("Updated user!");
                }
                else
                {
                    Console.WriteLine("Couldn't update User!");
                }
            }
        }
    }
}
