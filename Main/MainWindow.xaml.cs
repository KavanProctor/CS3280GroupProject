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
        private List<clsItem> items;

        private decimal cost = 0;
        private List<clsItem> itemsBuffer = new();

        public MainWindow()
        {
            InitializeComponent();

            this.addItemButton.Click += delegate{
                if(this.itemSelection.SelectedIndex < 0) return;

                clsItem item = this.items[this.itemSelection.SelectedIndex];
                if(!this.itemsBuffer.Any(i => i.ItemCode == item.ItemCode))
                {
                    this.itemsBuffer.Add(item);

                    this.SyncUI();
                }
            };

            this.Reset();
        }

        private void Reset()
        {
            this.items = clsMainLogic.GetItems();

            this.itemsBuffer.Clear();

            this.invoiceID.Text = "TBD";
            this.invoiceDate.Text = "";

            this.SyncUI();
        }

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

        private void SyncUI()
        {
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

        private bool WarnUser()
        {
            if(this.invoiceDate.Text != "" || this.itemsBuffer.Count > 0){
                var response = MessageBox.Show("The current invoice will be lost, do you wish to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                return response == MessageBoxResult.Yes;
            }
            return true;
        }

        private void UpdateItems(object sender, EventArgs ev)
        {
            if(!WarnUser()) return;

            ItemWindow itemWnd = new ItemWindow();
            this.Hide();
            itemWnd.ShowDialog();
            this.Show();

            this.Reset();
        }
        private void SelectInvoice(object sender, EventArgs ev)
        {
            if(!WarnUser()) return;

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

                this.SyncUI();
            }
        }

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

        }
    }
}
