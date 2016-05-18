using Hearthstone_Deckbuilder.Database.NSUserDatabase.Controller;
using Hearthstone_Deckbuilder.NSDatatypes;
using Hearthstone_Deckbuilder.NSGlobalVariables;
using Hearthstone_Deckbuilder.UserInterface.NSMainWindow;
using Hearthstone_Deckbuilder.UserInterface.NSMainWindow.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone_Deckbuilder.UserInterface.NSLoginWindow.Controller
{
    class LoginWindowController
    {

        private UserDatabaseController userDatabaseController;

        public LoginWindowController()
        {
            userDatabaseController = new UserDatabaseController();
        }

        public bool login(string enteredUsername, string enteredPassword)
        {
            User user = userDatabaseController.GetUser(enteredUsername);
            if (!user.PasswordHash.Equals(null))
            {
                if (userDatabaseController.CheckPasswordForLogin(enteredPassword, user))
                {
                    //Login successfull!
                    GlobalVariables.LoggedInUser = user;
                    return true;
                }
            }
            return false;
        }

        public void openMainWindowAndCloseLoginWindow(LoginWindow loginWindow)
        {
            MainWindow mainWindow = new MainWindow();
            loginWindow.Close();
            mainWindow.Show();
        }

        public bool register(string enteredUsername, string enteredPassword)
        {
            if (userDatabaseController.GetUser(enteredUsername).PasswordHash.Equals(null))
            {
                if(userDatabaseController.CreateNewUser(enteredUsername, enteredPassword))
                {
                    //User was created
                    return true;
                }
            }
            else
            {
                //User already exists
                return false;
            }
            return false;
        }

    }
}
