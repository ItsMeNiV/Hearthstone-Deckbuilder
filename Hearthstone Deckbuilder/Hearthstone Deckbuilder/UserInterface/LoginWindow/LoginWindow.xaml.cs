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
            if (txtRegisterPassword.Text == txtRegisterPasswordConfirmed.Text && !txtRegisterUsername.Text.Equals("") && !txtRegisterPassword.Text.Equals(""))
            {
                if(controller.register(txtRegisterUsername.Text, txtRegisterPassword.Text))
                {
                    txtRegisterPassword.Text = "";
                    txtRegisterPasswordConfirmed.Text = "";
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
            if(!txtLoginUsername.Text.Equals("") && !txtLoginPassword.Text.Equals(""))
            {
                if(controller.login(txtLoginUsername.Text, txtLoginPassword.Text))
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