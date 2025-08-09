using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GroupProject.Items
{
    class clsItemLogic
    {
        //GetAllItems reutrns List<clsItem>
        //addItem(clsItem)
        //editItem(clsItem oldItem,clsItem newItem)
        //deleteItem(clsItem recordToDelete)
        //isItemOnInvoice(clsItem)
        public List<clsItem> GetAllItems()
        {
            string sql = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
            List<clsItem> items = new List<clsItem>();

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + Directory.GetCurrentDirectory() + "\\Invoice.mdb"))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clsItem item = new clsItem()
                        {
                            ItemCode = reader["ItemCode"].ToString(),
                            ItemDesc = reader["ItemDesc"].ToString(),
                            Cost = Convert.ToDecimal(reader["Cost"])
                        };
                        items.Add(item);
                    }
                }
            }

            return items;
        }
        public void AddItem(clsItem item)
        {
            string sql = $"INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) " +
                         $"VALUES ('{item.ItemCode}', '{item.ItemDesc}', {item.Cost})";

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + Directory.GetCurrentDirectory() + "\\Invoice.mdb"))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void EditItem(clsItem oldItem, clsItem newItem)
        {
            string sql = $"UPDATE ItemDesc SET ItemDesc = '{newItem.ItemDesc}', Cost = {newItem.Cost} WHERE ItemCode = '{oldItem.ItemCode}'";

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + Directory.GetCurrentDirectory() + "\\Invoice.mdb"))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<int> GetInvoicesUsingItem(string itemCode)
        {
            var invoices = new List<int>();
            string sql = $"SELECT DISTINCT InvoiceNum FROM LineItems WHERE ItemCode = '{itemCode}'";

            using (var conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + Directory.GetCurrentDirectory() + "\\Invoice.mdb"))
            {
                conn.Open();
                using (var cmd = new OleDbCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        invoices.Add(Convert.ToInt32(reader["InvoiceNum"]));
                    }
                }
            }
            return invoices;
        }

        public void DeleteItem(string itemCode)
        {
            string sql = $"DELETE FROM ItemDesc WHERE ItemCode = '{itemCode}'";
            using (var conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + Directory.GetCurrentDirectory() + "\\Invoice.mdb"))
            {
                conn.Open();
                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
