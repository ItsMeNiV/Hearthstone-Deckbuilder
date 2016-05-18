using Hearthstone_Deckbuilder.UserInterface.NSDeckCreatorWindow.Controller;
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

namespace Hearthstone_Deckbuilder.UserInterface.NSDeckCreatorWindow
{
    /// <summary>
    /// Interaktionslogik für DeckCreatorWindow.xaml
    /// </summary>
    public partial class DeckCreatorWindow : Window
    {

        private DeckCreatorWindowController controller;
        private string SelectedClass;

        public DeckCreatorWindow()
        {
            controller = new DeckCreatorWindowController();
            InitializeComponent();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lvwCardList.Items.Clear();
        }
        
        public void setClass(string selectedClass)
        {
            SelectedClass = selectedClass;
        }
    }
}
