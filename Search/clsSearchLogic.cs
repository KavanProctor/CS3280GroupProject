using CS3280GroupProject.Items;
using CS3280GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GroupProject.Search
{
    public class clsSearchLogic
    {
        // GetDistinctInvoiceNumber
        // GetDistinctDates
        // GetDistinctCosts

        // GetInvoices(InvoiceNumber, InvoiceDate, InvoiceCost) - return List<clsInvoices>

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

        public static List<clsItem> GetItems()
        {
            return ExecSQL(clsMainSQL.GetItems()).Tables[0].Rows.OfType<DataRow>().Select(r => {
                return new clsItem()
                {
                    ItemCode = (string)r[0],
                    ItemDesc = (string)r[1],
                    Cost = (decimal)r[2],
                };
            }).ToList();
        }
    }
}
