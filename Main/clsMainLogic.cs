using System.Data;
using System.IO;
using System.Data.OleDb;

using CS3280GroupProject.Items;

namespace CS3280GroupProject.Main
{
    /// <summary>
    /// an interface with the database
    /// </summary>
    public class clsMainLogic
    {
        /// <summary>
        /// run the SQL statement on the database
        /// </summary>
        /// <param name="sql">the SQL statement to execute</param>
        /// <returns>the DataSet that was created by running the SQL statement</returns>
        private static DataSet ExecSQL(string sql)
        {
            DataSet result = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb"))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {
                    conn.Open();

                    adapter.SelectCommand = new OleDbCommand(sql, conn);
                    adapter.SelectCommand.CommandTimeout = 0;

                    adapter.Fill(result);
                }
            }

            return result;
        }

        /// <summary>
        /// create a new invoice
        /// </summary>
        /// <param name="date">the date of the invoice</param>
        /// <param name="items">the items of the invoice</param>
        /// <returns>the invoice ID of the invoice that was just created</returns>
        public static int CreateInvoice(string date, List<clsItem> items)
        {
            ExecSQL(clsMainSQL.CreateInvoice(date, items.Aggregate((decimal)0, (a, b) => a + b.Cost)));

            int id = (int)ExecSQL(clsMainSQL.GetLatestInvoiceID()).Tables[0].Rows[0].ItemArray[0];
            int line = 0;
            foreach(clsItem item in items){
                ExecSQL(clsMainSQL.AddItemToInvoice(id, item.ItemCode, ++line));
            }

            return id;
        }

        /// <summary>
        /// get the max line number of some invoice
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <returns>the highest line number of the specified invoice</returns>
        public static int GetMaxLineNum(int invoiceID)
        {
            return (int)ExecSQL(clsMainSQL.GetMaxLineNum(invoiceID)).Tables[0].Rows[0].ItemArray[0];
        }

        /// <summary>
        /// saves (modifies) an invoice that already exists
        /// </summary>
        /// <param name="invoiceID">the ID of the invoice that will be modified</param>
        /// <param name="date">the new date</param>
        /// <param name="items">the items of the invoice</param>
        public static void SaveInvoice(int invoiceID, string date, List<clsItem> items)
        {
            ExecSQL(clsMainSQL.SetInvoiceDate(invoiceID, date));

            List<clsItem> before = GetInvoiceItems(invoiceID);

            int line = GetMaxLineNum(invoiceID);
            foreach(clsItem item in items){
                // don't add the same item again
                if(!before.Any(i => i.ItemCode == item.ItemCode))
                {
                    ExecSQL(clsMainSQL.AddItemToInvoice(invoiceID, item.ItemCode, ++line));
                }
            }

            foreach(clsItem item in before){
                // only remove if its not in the new list
                if(!items.Any(i => i.ItemCode == item.ItemCode))
                {
                    ExecSQL(clsMainSQL.RemoveItemFromInvoice(invoiceID, item.ItemCode));
                }
            }

            ExecSQL(clsMainSQL.UpdateCost(invoiceID, items.Aggregate((decimal)0, (a, b) => a + b.Cost)));
        }

        /// <summary>
        /// get an invoice from the database
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <returns>the desired invoice</returns>
        public static clsInvoice GetInvoice(int invoiceID)
        {
            var r = ExecSQL(clsMainSQL.GetInvoice(invoiceID)).Tables[0].Rows.OfType<DataRow>().ToList()[0];
            return new clsInvoice()
            {
                InvoiceID=(int)r[0],
                InvoiceDate=(DateTime)r[1],
                TotalCost=(decimal)r[2],
            };
        }

        /// <summary>
        /// transforms a DataSet into a list of clsItems
        /// </summary>
        /// <param name="ds">the DataSet to read from</param>
        /// <returns>the list of items parsed</returns>
        private static List<clsItem> ToItems(DataSet ds)
        {
            return ds.Tables[0].Rows.OfType<DataRow>().Select(r => {
                return new clsItem()
                {
                    ItemCode=(string)r[0],
                    ItemDesc=(string)r[1],
                    Cost=(decimal)r[2],
                };
            }).ToList();
        }

        /// <summary>
        /// gets all of the items of the invoices from the database
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <returns>all of the items of the invoice</returns>
        public static List<clsItem> GetInvoiceItems(int invoiceID)
        {
            return ToItems(ExecSQL(clsMainSQL.GetInvoiceItems(invoiceID)));
        }

        /// <summary>
        /// gets all of the items from the database
        /// </summary>
        /// <returns>a list of all of the items</returns>
        public static List<clsItem> GetItems()
        {
            return ToItems(ExecSQL(clsMainSQL.GetItems()));
        }
    }
}
