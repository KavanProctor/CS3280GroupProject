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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateItems(object sender, EventArgs ev)
        {
            ItemWindow itemWnd = new ItemWindow();
            this.Hide();
            itemWnd.ShowDialog();
            this.Show();

            // TODO: check this.itemWnd.bHasItemBeenChanged and if its true then update the items combo box by updating its ItemsSource according to clsMainLogic.GetInvoice().Items
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
