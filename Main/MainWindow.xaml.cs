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

        private void Reset(){
            this.items = clsMainLogic.GetItems();
            this.itemSelection.Items.Clear();
            this.items.ForEach(i => this.itemSelection.Items.Add(i.ItemCode));

            this.itemsBuffer.Clear();

            this.invoiceID.Text = "TBD";

            this.invoiceItems.Items.Clear();

            this.SyncUI();
        }

        private void SyncUI(){
            this.cost = 0;
            foreach(clsItem item in this.itemsBuffer)
            {
                this.cost += item.Cost;
            }

            // TODO: update this.invoiceItems

            this.invoiceCost.Text = this.cost.ToString();
        }

        private void UpdateItems(object sender, EventArgs ev)
        {
            // NOTE: the requirements say that the items window cannot be opened if an invoice is being created/edited (which thankfully makes everything much simpler)
            // TODO: either prevent the items window from opening (when appropriate) or confirm that the user is okay with losing unsaved info

            ItemWindow itemWnd = new ItemWindow();
            this.Hide();
            itemWnd.ShowDialog();
            this.Show();

            // TODO: check this.itemWnd.bHasItemBeenChanged and if its true then update the items combo box by updating its ItemsSource according to clsMainLogic.GetInvoice().Items
            // NOTE: this todo can probably be ignored? just always call this.Reset ...

            this.Reset();
        }
        private void SelectInvoice(object sender, EventArgs ev)
        {
            SearchWindow searchWnd = new SearchWindow();
            this.Hide();
            searchWnd.ShowDialog();
            this.Show();

            // TODO: get the invoice number with something like this.searchWnd.GetSelected() and then fill the invoice form with data pertaining to that invoice if it is not null
        }
    }
}
