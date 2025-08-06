using System.Data;
using System.IO;
using System.Data.OleDb;

using CS3280GroupProject.Items;

namespace CS3280GroupProject.Main
{
    // struct Invoice;
    
    public class clsMainLogic
    {
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

        // public void CreateInvoice(string date, List<string> items);
        // public void InvoiceSetItems(int invoiceID, string item);
        // public void InvoiceSetDate(int invoiceID, string date);
        // public Invoice GetInvoice(int invoiceID);
        // public Invoice GetLatestInvoice();

        public static List<clsItem> GetItems(){
            return ExecSQL(clsMainSQL.GetItems()).Tables[0].Rows.OfType<DataRow>().Select(r => {
                return new clsItem()
                {
                    ItemCode=(string)r[0],
                    ItemDesc=(string)r[1],
                    Cost=(decimal)r[2],
                };
            }).ToList();
        }
    }
}
