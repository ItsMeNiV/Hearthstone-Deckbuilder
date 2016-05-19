using Hearthstone_Deckbuilder.NSDatatypes;
using Hearthstone_Deckbuilder.NSGlobalVariables;
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
        private Deck openDeck;

        public DeckCreatorWindow(Deck deck)
        {
            openDeck = deck;
            controller = new DeckCreatorWindowController();
            InitializeComponent();
            initializeAllCardList();
            initializeDeckCardList();
            if (openDeck.DeckId == null)
            {
                controller.saveDeckFirstTime(openDeck);
                openDeck = controller.getNewestDeckByUser(GlobalVariables.LoggedInUser);
            }
            SelectedClass = openDeck.DeckClass;
            txtDeckname.Text = openDeck.DeckName;
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
        
        public void setClass(string selectedClass)
        {
            SelectedClass = selectedClass;
        }

        private void btnAddToDeck_Click(object sender, RoutedEventArgs e)
        {
            if (lvwCardList.SelectedIndex != -1)
            {
                Card selectedCard = controller.getCardByName(controller.getCurrentCardList().ToArray()[lvwCardList.SelectedIndex].CardName);
                int counter = 0;
                foreach (Card card in openDeck.CardList)
                {
                    if (card.CardName.Equals(selectedCard.CardName))
                    {
                        counter++;
                    }
                }
                if (counter <= 2)
                {
                    controller.addCardToDeck(selectedCard, openDeck);
                    lvwDeckCardList.Items.Add(selectedCard.CardName);
                }
            }
        }

        private void btnRemoveFromDeck_Click(object sender, RoutedEventArgs e)
        {
            if (lvwDeckCardList.SelectedIndex != -1)
            {
                Card selectedCard = controller.getCardByName(controller.getCurrentCardList().ToArray()[lvwDeckCardList.SelectedIndex].CardName);
                controller.removeCardFromDeck(selectedCard, openDeck);
                lvwDeckCardList.Items.RemoveAt(lvwDeckCardList.SelectedIndex);
            }
        }

        private void btnSaveDeck_Click(object sender, RoutedEventArgs e)
        {
            if(!txtDeckname.Text.Equals(""))
            {
                openDeck.DeckName = txtDeckname.Text;
                openDeck.CardList.Clear();
                foreach (string cardName in lvwDeckCardList.Items)
                {
                    openDeck.CardList.Add(controller.getCardByName(cardName));
                }
                controller.saveDeck(openDeck);
                this.Close();
            }
            else
            {
                MessageBox.Show("You need to enter a deckname");
            }
        }

        private void btnExitWithoutSavingDeck_Click(object sender, RoutedEventArgs e)
        {
            controller.deleteDeck(openDeck);
            this.Close();
        }
        private void initializeAllCardList()
        {
            foreach (Card card in controller.getCurrentCardList())
            {
                lvwCardList.Items.Add(card.CardName);
            }
        }

        private void initializeDeckCardList()
        {
            foreach (Card card in openDeck.CardList)
            {
                lvwDeckCardList.Items.Add(card.CardName);
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

    }
}
