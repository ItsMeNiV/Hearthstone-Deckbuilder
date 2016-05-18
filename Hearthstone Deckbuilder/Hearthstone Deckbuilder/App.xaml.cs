using System;
using System.Windows;
using Hearthstone_Deckbuilder.NSGlobalVariables;
using Hearthstone_Deckbuilder.Database.NSCardDatabase.Controller;
using System.Collections.Generic;
using Hearthstone_Deckbuilder.NSDatatypes;

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
                GlobalVariables.ApiKey = e.Args[0];
                GlobalVariables.PostgreConnectionString = e.Args[1];
            }
            else
            {
                Console.WriteLine("Wrong Usage.\nPlease add the API Key and the Postgre Connection String to the debug commandline arguments");
                Shutdown();
            }

            CardDatabaseController cdc = new CardDatabaseController();
            Card card = cdc.GetCardByName("Wisp");
        }
    }
}
