using CS3280GroupProject.Items;
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

namespace CS3280GroupProject.Search
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        // SelectedInvoiceID - Holds the invoice Id if the user selected one, and zero if no invoice selected

        private void UpdateItems(object sender, EventArgs ev)
        {
            ItemWindow itemWnd = new ItemWindow();
            itemWnd.Show();
            this.Close();
        }

        private void mainMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
