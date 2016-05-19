using Hearthstone_Deckbuilder.UserInterface.NSLoginWindow.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hearthstone_Deckbuilder.UserInterface.NSLoginWindow
{
    /// <summary>
    /// Interaktionslogik für LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginWindowController controller;

        public LoginWindow()
        {
            controller = new LoginWindowController();
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtRegisterPassword.Password == txtRegisterPasswordConfirmed.Password && !txtRegisterUsername.Text.Equals("") && !txtRegisterPassword.Password.Equals(""))
            {
                if (controller.register(txtRegisterUsername.Text, txtRegisterPassword.Password))
                {
                    txtRegisterPassword.Password = "";
                    txtRegisterPasswordConfirmed.Password = "";
                    txtRegisterUsername.Text = "";
                    btnRegister.IsEnabled = false;
                    MessageBox.Show("Successfully registered!");
                }
            }
            else
            {
                MessageBox.Show("Passwords are not matching");
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!txtLoginUsername.Text.Equals("") && !txtLoginPassword.Password.Equals(""))
            {
                if (controller.login(txtLoginUsername.Text, txtLoginPassword.Password))
                {
                    controller.openMainWindowAndCloseLoginWindow(this);
                }
                else
                {
                    MessageBox.Show("Wrong user password combination");
                }
            }
            else
            {
                MessageBox.Show("Please enter a username and a password");
            }
        }
    }
}