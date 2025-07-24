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

namespace CS3280GroupProject.Items
{
    /// <summary>
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow : Window
    {
        public ItemWindow()
        {
            InitializeComponent();
        }
        private clsItemLogic itemLogic = new clsItemLogic();
        private bool bHasItemBeenChanged = false;
        public bool HasItemBeenChanged => bHasItemBeenChanged;

        private void itemAddButton_Click(object sender, RoutedEventArgs e)
        {
            //Logic for what happens when they want to add a item(probably display text fields and let them save)
            //After they click add allow them to save
        }

        private void itemDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //Logic for what happens when they want to delete item
        }

        private void itemEditButton_Click(object sender, RoutedEventArgs e)
        {
            //open the information on the row they selected and let them edit and save.
            // turn save button on they dont need to verify this
        }

        private void itemSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(itemCodeTextBox.Text) || string.IsNullOrWhiteSpace(itemDescriptionTextBox.Text) || string.IsNullOrWhiteSpace(itemCostTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Save new or updated item
            bHasItemBeenChanged = true;
        }

        //bool hasItemBeenChanged //Set to true when item has been added/edited/deleted
        //bool bHasItemBeenChanged //property
    }
}
