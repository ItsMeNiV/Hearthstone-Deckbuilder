using Hearthstone_Deckbuilder.NSDatatypes;
using Hearthstone_Deckbuilder.UserInterface.NSMainWindow.Controller;
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

namespace Hearthstone_Deckbuilder.UserInterface.NSMainWindow
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainWindowController controller;

        public MainWindow()
        {
            controller = new MainWindowController();
            InitializeComponent();
            InitializeDeckList();
            InitializeCardGallery();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClassSelectorWindow.ClassSelectorWindow classSelectorWindow = new ClassSelectorWindow.ClassSelectorWindow();
            classSelectorWindow.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void InitializeDeckList()
        {
            foreach(Deck deck in controller.getDecksOfLoggedInUser())
            {
                lvwDeckList.Items.Add(deck.DeckName);
            }
        }

        private void InitializeCardGallery()
        {
            foreach(Card card in controller.getCurrentCardList())
            {
                lvwCardList.Items.Add(card.CardName);
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            controller.applyFilters(cmbClass.Text, cmbType.Text, cmbRarity.Text, cmbManaCost.Text, cmbAttack.Text, cmbHealth.Text, cmbDurability.Text);
            lvwCardList.Items.Clear();
            foreach (Card card in controller.getCurrentCardList())
            {
                lvwCardList.Items.Add(card.CardName);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            controller.resetFilters();
            foreach (Card card in controller.getCurrentCardList())
            {
                lvwCardList.Items.Add(card.CardName);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            controller.resetFilters();
            if (!txtSearch.Text.Equals(""))
            {
                controller.searchCards(txtSearch.Text);
                lvwCardList.Items.Clear();
                foreach (Card card in controller.getCurrentCardList())
                {
                    lvwCardList.Items.Add(card.CardName);
                }
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                Card card = controller.getCardByName(item.Content.ToString());
                imgCardImage.Source = new BitmapImage(new Uri(card.ImgLink));
            }
        }
        private void ListViewItemDeck_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                Deck selectedDeck = controller.getDecksOfLoggedInUser().ToArray()[lvwDeckList.SelectedIndex];
                Deck newDeck = new Deck();
                newDeck.DeckId = selectedDeck.DeckId;
                newDeck.DeckName = selectedDeck.DeckName;
                newDeck.DeckUser = selectedDeck.DeckUser;
                newDeck.DeckClass = selectedDeck.DeckClass;
                foreach (Card card in selectedDeck.CardList)
                {
                    newDeck.CardList.Add(controller.getCardById(card.CardId));
                }
                controller.openDeckCreatorWindow(newDeck);
            }
        }

        private void btnDeleteDeck_Click(object sender, RoutedEventArgs e)
        {
            if (lvwDeckList.SelectedIndex != -1)
            {
                controller.deleteSelectedDeck(controller.getDecksOfLoggedInUser().ToArray()[lvwDeckList.SelectedIndex]);
                lvwDeckList.Items.Clear();
                foreach (Deck deck in controller.getDecksOfLoggedInUser())
                {
                    lvwDeckList.Items.Add(deck.DeckName);
                }
            }
        }

        private void btnRefreshDeckList_Click(object sender, RoutedEventArgs e)
        {
            lvwDeckList.Items.Clear();
            foreach (Deck deck in controller.getDecksOfLoggedInUser())
            {
                lvwDeckList.Items.Add(deck.DeckName);
            }
        }

    }
}
