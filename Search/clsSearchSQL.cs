using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CS3280GroupProject.Search
{
    public class clsSearchSQL
    {
        public static string GetInvoices()
        {
            return "SELECT * FROM Invoices";
        }

        public static string GetInvoices(int invoiceNum)
        {
            return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum}";
        }

        public static string GetInvoices(int invoiceNum, DateTime invoiceDate)
        {
            return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND InvoiceDate = #{invoiceDate.ToString("MM/dd/yyyy")}#";
        }

        public static string GetInvoices(int invoiceNum, DateTime invoiceDate, decimal totalCost)
        {
            return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND InvoiceDate = #{invoiceDate.ToString("MM/dd/yyyy")}# AND TotalCost = {totalCost}";
        }

        public static string GetInvoices(decimal totalCost)
        {
            return $"SELECT * FROM Invoices WHERE TotalCost = {totalCost}";
        }

        public static string GetInvoices(int invoiceNum, decimal totalCost)
        {
            return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND TotalCost = {totalCost}";
        }

        public static string GetInvoices(DateTime invoiceDate)
        {
            return $"SELECT * FROM Invoices WHERE InvoiceDate = #{invoiceDate.ToString("MM/dd/yyyy")}#";
        }


        public static string GetDistinctInvoiceNumbers()
        {
            return "SELECT DISTINCT(InvoiceNum) FROM Invoices ORDER BY InvoiceNum";
        }


        public static string GetDistinctDates()
        {
            return "SELECT DISTINCT(InvoiceDate) FROM Invoices ORDER BY InvoiceDate";
        }


        public static string GetDistinctCosts()
        {
            return "SELECT DISTINCT(TotalCost) FROM Invoices ORDER BY TotalCost";
        }
    }
}
