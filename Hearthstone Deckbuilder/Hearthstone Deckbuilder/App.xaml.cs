using System;
using System.Windows;

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
                GlobalVariables.GlobalVariables.API_KEY = e.Args[0];
                GlobalVariables.GlobalVariables.POSTGRE_CONNECTION_STRING = e.Args[1];
            }
            else
            {
                Console.WriteLine("Wrong Usage.\nPlease add the API Key and the Postgre Connection String to the debug commandline arguments");
            }
        }
    }
}
