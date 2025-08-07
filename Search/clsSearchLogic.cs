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
        // GetInvoices(InvoiceNumber, InvoiceDate, InvoiceCost) - return List<clsInvoices>

        public static List<int> GetDistinctInvoiceNumbers()
        {
            int iRetVal = 0;
            List<int> invoiceNumbers = new List<int>();
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetDistinctInvoiceNumbers(), ref iRetVal);
            if (iRetVal > 0)
            {
                invoiceNumbers = ds.Tables[0].AsEnumerable().Select(row => row.Field<int>("InvoiceNum")).ToList();
            }
            return invoiceNumbers;
        }

        public static List<DateTime> GetDistinctDates()
        {
            int iRetVal = 0;
            List<DateTime> invoiceDates = new List<DateTime>();
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetDistinctDates(), ref iRetVal);
            if (iRetVal > 0)
            {
                invoiceDates = ds.Tables[0].AsEnumerable().Select(row => row.Field<DateTime>("InvoiceDate")).ToList();
            }
            return invoiceDates;
        }

        public static List<decimal> GetDistinctCosts()
        {
            int iRetVal = 0;
            List<decimal> invoiceCosts = new List<decimal>();
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetDistinctCosts(), ref iRetVal);
            if (iRetVal > 0)
            {
                invoiceCosts = ds.Tables[0].AsEnumerable().Select(row => row.Field<decimal>("TotalCost")).ToList();
            }
            return invoiceCosts;
        }





        // GetInvoices methods start here

        public static List<clsInvoice> GetInvoices()
        {
            int iRetVal = 0;
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetInvoices(), ref iRetVal);
            
            return GenerateData(ds, iRetVal);
        }

        public static List<clsInvoice> GetInvoices(int invoiceNum)
        {
            int iRetVal = 0;
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetInvoices(invoiceNum), ref iRetVal);

            return GenerateData(ds, iRetVal);
        }

        public static List<clsInvoice> GetInvoices(int invoiceNum, DateTime invoiceDate)
        {
            int iRetVal = 0;
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetInvoices(invoiceNum, invoiceDate), ref iRetVal);

            return GenerateData(ds, iRetVal);
        }

        public static List<clsInvoice> GetInvoices(int invoiceNumber, DateTime invoiceDate, decimal invoiceCost)
        {
            int iRetVal = 0;
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetInvoices(invoiceNumber, invoiceDate, invoiceCost), ref iRetVal);
            
            return GenerateData(ds, iRetVal);
        }

        public static List<clsInvoice> GetInvoices(decimal totalCost)
        {
            int iRetVal = 0;
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetInvoices(totalCost), ref iRetVal);

            return GenerateData(ds, iRetVal);
        }

        public static List<clsInvoice> GetInvoices(int invoiceNum, decimal totalCost)
        {
            int iRetVal = 0;
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetInvoices(invoiceNum, totalCost), ref iRetVal);

            return GenerateData(ds, iRetVal);
        }

        public static List<clsInvoice> GetInvoices(DateTime invoiceDate)
        {
            int iRetVal = 0;
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetInvoices(invoiceDate), ref iRetVal);

            return GenerateData(ds, iRetVal);
        }

        public static List<clsInvoice> GetInvoices(DateTime invoiceDate, decimal totalCost)
        {
            int iRetVal = 0;
            DataSet ds = clsDataAccess.ExecuteSQLStatement(clsSearchSQL.GetInvoices(invoiceDate, totalCost), ref iRetVal);

            return GenerateData(ds, iRetVal);
        }

        private static List<clsInvoice> GenerateData(DataSet ds, int iRetVal)
        {
            List<clsInvoice> invoices = new List<clsInvoice>();
            if (iRetVal > 0)
            {
                invoices = ds.Tables[0].AsEnumerable().Select(row => new clsInvoice
                {
                    InvoiceID = row.Field<int>("InvoiceNum"),
                    InvoiceDate = row.Field<DateTime>("InvoiceDate"),
                    TotalCost = row.Field<decimal>("TotalCost")
                }).ToList();
            }
            return invoices;
        }
    }
}
