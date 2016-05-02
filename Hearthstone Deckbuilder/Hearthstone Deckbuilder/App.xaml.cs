using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hearthstone_Deckbuilder.GlobalVariables;

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
            }
        }
    }
}
