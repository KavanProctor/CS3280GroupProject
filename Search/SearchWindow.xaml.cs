using CS3280GroupProject.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CS3280GroupProject.Search
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        clsInvoice selectedInvoice;
        clsInvoice filterInvoice;

        public SearchWindow()
        {
            InitializeComponent();

            selectedInvoice = new clsInvoice();
            filterInvoice = new clsInvoice();

            // Populate the InvoiceNumber ComboBox with distinct invoice numbers
            invoiceIDs.ItemsSource = clsSearchLogic.GetDistinctInvoiceNumbers();
            // Populate the InvoiceDate ComboBox with distinct invoice dates
            invoiceDates.ItemsSource = clsSearchLogic.GetDistinctDates();
            // Populate the InvoiceCost ComboBox with distinct invoice costs
            invoiceCosts.ItemsSource = clsSearchLogic.GetDistinctCosts();
            // Populate the invoices DataGrid with all invoices
            invoices.ItemsSource = clsSearchLogic.GetInvoices();

        }

        /// <summary>
        /// Menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void UpdateItems(object sender, EventArgs ev)
        {
            ItemWindow itemWnd = new ItemWindow();
            itemWnd.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// This event handles clearing the filters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFilers_Click(object sender, RoutedEventArgs e)
        {
            invoiceIDs.SelectedIndex = -1; // Clear the selection in the ComboBox
            invoiceDates.SelectedIndex = -1; // Clear the selected date in the ComboBox
            invoiceCosts.SelectedIndex = -1; // Clear the selected cost in the ComboBox

            selectedInvoice.Clear(); // Reset the selected invoice ID
            filterInvoice.Clear(); // Reset the filter invoice

            invoices.ItemsSource = clsSearchLogic.GetInvoices(); // Refresh the DataGrid with all invoices
        }

        /// <summary>
        /// This event is triggered when the selection in the invoiceIDs ComboBox changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invoiceIDs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (invoiceIDs.SelectedIndex == -1)
                return;

            filterInvoice.InvoiceID = (int)invoiceIDs.SelectedItem;

            // If both InvoiceDate and TotalCost are set, filter the invoices
            if (filterInvoice.InvoiceDate != DateTime.MinValue && filterInvoice.TotalCost != 0)
            {
                invoices.ItemsSource = clsSearchLogic.GetInvoices(filterInvoice.InvoiceID, filterInvoice.InvoiceDate, filterInvoice.TotalCost);
                return;
            }
            else
            {
                if(filterInvoice.InvoiceDate != DateTime.MinValue)
                {
                    invoices.ItemsSource = clsSearchLogic.GetInvoices(filterInvoice.InvoiceID, filterInvoice.InvoiceDate);
                    return;
                }
                else if (filterInvoice.TotalCost != 0)
                {
                    invoices.ItemsSource = clsSearchLogic.GetInvoices(filterInvoice.InvoiceID, filterInvoice.TotalCost);
                    return;
                }
                else
                {
                    invoices.ItemsSource = clsSearchLogic.GetInvoices(filterInvoice.InvoiceID);
                    return;
                }
            }
        }

        /// <summary>
        /// This event is triggered when the selection in the invoiceDates ComboBox changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invoiceDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// This event is triggered when the selection in the invoiceCosts ComboBox changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invoiceCosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// This handles the click event for the Select button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            
            try 
            {
                clsInvoice selectedInvoice = invoices.SelectedItem as clsInvoice;
                if (selectedInvoice != null)
                {
                    // Set the selected invoice ID
                    this.selectedInvoice.InvoiceID = selectedInvoice.InvoiceID;
                    this.selectedInvoice.InvoiceDate = selectedInvoice.InvoiceDate;
                    this.selectedInvoice.TotalCost = selectedInvoice.TotalCost;
                    // Close the search window
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select an invoice.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting the invoice: {ex.Message}");
            }
        }
    }
}
