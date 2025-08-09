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
using CS3280GroupProject.Main;
using CS3280GroupProject.Search;

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
            List<clsItem> items = itemLogic.GetAllItems();
            itemDataGrid.ItemsSource = itemLogic.GetAllItems();
        }
        private clsItemLogic itemLogic = new clsItemLogic();
        private bool bHasItemBeenChanged = false;
        private clsItem? selectedItem = null;
        public bool HasItemBeenChanged => bHasItemBeenChanged;

        private SearchWindow? searchWnd;
        private Main.MainWindow? MainWnd;
        private ItemWindow? itemWnd;

        private void itemAddButton_Click(object sender, RoutedEventArgs e)
        {
            itemCodeTextBox.IsEnabled = true;
            itemDescriptionTextBox.IsEnabled = true;
            itemCostTextBox.IsEnabled = true;

            itemCodeTextBox.Clear();
            itemDescriptionTextBox.Clear();
            itemCostTextBox.Clear();

            itemSaveButton.IsEnabled = true;
        }

        private void itemDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            try
            {
                // Check usage
                var usedOn = itemLogic.GetInvoicesUsingItem(selectedItem.ItemCode);
                if (usedOn.Any())
                {
                    string list = string.Join(", ", usedOn);
                    MessageBox.Show($"Cannot delete item '{selectedItem.ItemCode}' because it is used on invoice(s): {list}");
                    return;
                }

                // Confirm
                if (MessageBox.Show($"Delete item '{selectedItem.ItemCode}'?", "Confirm Delete", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                    return;

                // Delete and refresh
                itemLogic.DeleteItem(selectedItem.ItemCode);
                bHasItemBeenChanged = true;

                itemDataGrid.ItemsSource = itemLogic.GetAllItems(); // refresh grid
                selectedItem = null;

                itemCodeTextBox.Clear();
                itemDescriptionTextBox.Clear();
                itemCostTextBox.Clear();

                itemCodeTextBox.IsEnabled = false;
                itemDescriptionTextBox.IsEnabled = false;
                itemCostTextBox.IsEnabled = false;
                itemSaveButton.IsEnabled = false;

                MessageBox.Show("Item deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message);
            }
        }


        private void itemEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Please select an item to edit.");
                return;
            }

            // Load selected item details into text boxes
            itemCodeTextBox.Text = selectedItem.ItemCode;
            itemDescriptionTextBox.Text = selectedItem.ItemDesc;
            itemCostTextBox.Text = selectedItem.Cost.ToString();

            itemCodeTextBox.IsEnabled = false; // can't edit primary key
            itemDescriptionTextBox.IsEnabled = true;
            itemCostTextBox.IsEnabled = true;

            itemSaveButton.IsEnabled = true;
        }


        private void itemSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(itemCodeTextBox.Text) ||
                string.IsNullOrWhiteSpace(itemDescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(itemCostTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!decimal.TryParse(itemCostTextBox.Text, out decimal cost))
            {
                MessageBox.Show("Cost must be a valid number.");
                return;
            }

            clsItem newItem = new clsItem
            {
                ItemCode = itemCodeTextBox.Text.Trim(),
                ItemDesc = itemDescriptionTextBox.Text.Trim(),
                Cost = cost
            };

            try
            {
                if (selectedItem == null)
                {
                    itemLogic.AddItem(newItem);
                    MessageBox.Show("Item added successfully!");
                }
                else
                {
                    itemLogic.EditItem(selectedItem, newItem);
                    MessageBox.Show("Item updated successfully!");
                }

                itemDataGrid.ItemsSource = itemLogic.GetAllItems(); // refresh grid
                itemCodeTextBox.Clear();
                itemDescriptionTextBox.Clear();
                itemCostTextBox.Clear();

                itemCodeTextBox.IsEnabled = false;
                itemDescriptionTextBox.IsEnabled = false;
                itemCostTextBox.IsEnabled = false;
                itemSaveButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message);
            }
        }


        private void mainMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectInvoice(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWnd = new SearchWindow();
            this.Hide();
            searchWnd.ShowDialog();
            this.Close();
        }
        private void itemDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (itemDataGrid.SelectedItem is clsItem item)
            {
                selectedItem = item;
            }
        }
    }
}
