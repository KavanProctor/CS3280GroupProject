using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using CS3280GroupProject.Items;
using CS3280GroupProject.Search;

namespace CS3280GroupProject.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// a list of items that should always be synced to the databse
        /// </summary>
        private List<clsItem> items;

        /// <summary>
        /// the cost of the current invoice
        /// </summary>
        private decimal cost = 0;

        /// <summary>
        /// a list of items that represents the items that are in the current invoice
        /// </summary>
        private List<clsItem> itemsBuffer = new();

        /// <summary>
        /// tracks whether the user is editing or not
        /// </summary>
        private bool editing = false;


        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.addItemButton.Click += delegate{
                if(this.itemSelection.SelectedIndex < 0) return;

                clsItem item = this.items[this.itemSelection.SelectedIndex];
                this.itemsBuffer.Add(item);

                this.SyncUI();
            };

            this.Reset();
        }

        /// <summary>
        /// reset everything (except this.editing)
        /// </summary>
        private void Reset()
        {
            this.items = clsMainLogic.GetItems();

            this.itemsBuffer.Clear();

            this.invoiceID.Text = "TBD";
            this.invoiceDate.Text = "";

            this.SyncUI();
        }

        /// <summary>
        /// click event for the remove button next to each item in the DataGrid
        /// </summary>
        private void RemoveItem(object sender, EventArgs ev)
        {
            Visual parent = VisualTreeHelper.GetParent((Button)sender) as Visual;
            while(!(parent is DataGridRow)){
                parent = VisualTreeHelper.GetParent(parent) as Visual;
            }

            int index = (parent as DataGridRow).GetIndex();
            this.itemsBuffer.RemoveAt(index);

            this.SyncUI();
        }

        /// <summary>
        /// sync the UI to the current state
        /// </summary>
        private void SyncUI()
        {
            this.invoiceDate.IsEnabled =  this.editing;
            this.itemSection.IsEnabled =  this.editing;
            this.editButton.IsEnabled  = !this.editing;
            this.saveButton.IsEnabled  =  this.editing;

            this.cost = 0;
            foreach(clsItem item in this.itemsBuffer)
            {
                this.cost += item.Cost;
            }

            this.invoiceItems.Items.Clear();
            this.itemsBuffer.ForEach(i => this.invoiceItems.Items.Add(i));

            this.itemSelection.Items.Clear();
            this.items.ForEach(i => this.itemSelection.Items.Add(i.ItemCode));

            this.invoiceCost.Text = this.cost.ToString();
        }

        /// <summary>
        /// warn the user about losing data on the current invoice
        /// </summary>
        /// <returns>only true when user is okay with moving on</returns>
        private bool WarnUser()
        {
            if(this.editing){
                var response = MessageBox.Show("The current invoice will be lost, do you wish to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                return response == MessageBoxResult.Yes;
            }
            return true;
        }

        /// <summary>
        /// open a new window so that the user can edit the items in the database
        /// <param name="sender">(unused)</param>
        /// <param name="ev">(unused)</param>
        /// </summary>
        private void UpdateItems(object sender, EventArgs ev)
        {
            if(!this.WarnUser()) return;

            ItemWindow itemWnd = new ItemWindow();
            this.Hide();
            itemWnd.ShowDialog();
            this.Show();

            this.Reset();
        }

        /// <summary>
        /// open a new window so that the user can search for invoices, and maybe pick one for editing
        /// <param name="sender">(unused)</param>
        /// <param name="ev">(unused)</param>
        /// </summary>
        private void SelectInvoice(object sender, EventArgs ev)
        {
            if(!this.WarnUser()) return;

            SearchWindow searchWnd = new SearchWindow();
            this.Hide();
            searchWnd.ShowDialog();
            this.Show();

            this.Reset();

            int id = searchWnd.selectedInvoice.InvoiceID;
            if(id != 0){
                clsInvoice invoice = clsMainLogic.GetInvoice(id);

                this.invoiceID.Text = invoice.InvoiceID.ToString();
                this.invoiceDate.SelectedDate = invoice.InvoiceDate;
                this.itemsBuffer = clsMainLogic.GetInvoiceItems(id);

                this.editing = false;
                this.SyncUI();
            }
        }

        /// <summary>
        /// start creating a brand new invoice
        /// <param name="sender">(unused)</param>
        /// <param name="ev">(unused)</param>
        /// </summary>
        private void StartInvoice(object sender, EventArgs ev)
        {
            if(!this.WarnUser()) return;

            this.editing = true;
            this.Reset();
        }

        /// <summary>
        /// edit the invoice that is currently being displayed
        /// <param name="sender">(unused)</param>
        /// <param name="ev">(unused)</param>
        /// </summary>
        private void EditInvoice(object sender, EventArgs ev)
        {
            if(this.editing) return;

            this.editing = true;
            this.SyncUI();
        }

        /// <summary>
        /// save the current invoice to the database
        /// <param name="sender">(unused)</param>
        /// <param name="ev">(unused)</param>
        /// </summary>
        private void SaveInvoice(object sender, EventArgs ev)
        {
            if(this.invoiceDate.Text == "" || this.itemsBuffer.Count == 0){
                MessageBox.Show("Please create a valid invoice", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int id;
            if(Int32.TryParse(this.invoiceID.Text, out id)){
                clsMainLogic.SaveInvoice(id, this.invoiceDate.Text, this.itemsBuffer);
            }else{
                id = clsMainLogic.CreateInvoice(this.invoiceDate.Text, this.itemsBuffer);
                this.invoiceID.Text = id.ToString();
            }

            this.editing = false;
            this.SyncUI();
        }
    }
}
