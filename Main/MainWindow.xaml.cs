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
        private SearchWindow? searchWnd;
        private ItemWindow? itemWnd;

        public MainWindow()
        {
            InitializeComponent();

            this.Closing += delegate (object? sender, CancelEventArgs ev){
                this.itemWnd?.Close();
                this.searchWnd?.Close();
            };
        }

        private void UpdateItems(object sender, EventArgs ev)
        {
            this.itemWnd?.Close();
            this.itemWnd = new ItemWindow();
            this.itemWnd.ShowDialog();

            // TODO: check this.itemWnd.bHasItemBeenChanged and if its true then update the items combo box by updating its ItemsSource according to clsMainLogic.GetInvoice().Items
        }
        private void SelectInvoice(object sender, EventArgs ev)
        {
            this.searchWnd?.Close();
            this.searchWnd = new SearchWindow();
            this.searchWnd.ShowDialog();

            // TODO: get the invoice number with something like this.searchWnd.GetSelected() and then fill the invoice form with data pertaining to that invoice if it is not null
        }
    }
}
