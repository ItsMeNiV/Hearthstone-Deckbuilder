﻿using System;
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
using Hearthstone_Deckbuilder.UserInterface.NSDeckCreatorWindow;
using Hearthstone_Deckbuilder.UserInterface.ClassSelectorWindow.Controller;

namespace Hearthstone_Deckbuilder.UserInterface.ClassSelectorWindow
{
    /// <summary>
    /// Interaction logic for ClassSelectorWindow.xaml
    /// </summary>
    public partial class ClassSelectorWindow : Window
    {
        private string _classSelected;
        private ClassSelectorWindowController controller;

        public ClassSelectorWindow()
        {
            controller = new ClassSelectorWindowController();
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            controller.openDeckCreatorAndCloseClassSelector(this, cmbClass.Text);
        }

        public string ClassSelected
        {
            get { return _classSelected = cmbClass.Text; }
            set { _classSelected = value; }
        }
    }
}
