namespace CS3280GroupProject.Main
{
    public class clsMainSQL
    {
        public static string GetMaxLineNum(int invoiceID)
        {
            return $"SELECT MAX(LineItemNum) FROM LineItems WHERE InvoiceNum = {invoiceID}";
        }
        
        public static string ComputeCost(int invoiceID)
        {
            return $"SELECT SUM(i.Cost) FROM LineItems l INNER JOIN ItemDesc i ON l.ItemCode = i.ItemCode WHERE l.InvoiceNum = {invoiceID}";
        }
        public static string UpdateCost(int invoiceID, decimal cost)
        {
            return $"UPDATE Invoices SET TotalCost = {cost} WHERE InvoiceNum = {invoiceID}";
        }

        public static string GetLatestInvoiceID()
        {
            return $"SELECT MAX(InvoiceNum) FROM Invoices";
        }

        public static string CreateInvoice(string date, decimal cost)
        {
            return $"INSERT INTO Invoices (InvoiceDate, TotalCost) VALUES (#{date}#, {cost})";
        }
        public static string GetInvoice(int invoiceID)
        {
            return $"SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = {invoiceID}";
        }
        public static string RemoveInvoice(int invoiceID)
        {
            return $"DELETE FROM Invoices WHERE InvoiceNum = {invoiceID}";
        }
        public static string RemoveInvoiceItems(int invoiceID)
        {
            return $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceID}";
        }
        public static string SetInvoiceDate(int invoiceID, string date)
        {
            return $"UPDATE Invoices SET InvoiceDate = #{date}# WHERE InvoiceNum = {invoiceID}";
        }

        public static string AddItemToInvoice(int invoiceID, string itemCode, int line)
        {
            return $"INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES ({invoiceID}, {line}, '{itemCode}')";
        }
        public static string GetInvoiceItems(int invoiceID)
        {
            return $"SELECT d.ItemCode, d.ItemDesc, d.Cost FROM ItemDesc d INNER JOIN LineItems l ON d.ItemCode = l.ItemCode WHERE l.InvoiceNum = {invoiceID}";
        }
        public static string RemoveItemFromInvoice(int invoiceID, string itemCode)
        {
            return $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceID} AND ItemCode = '{itemCode}'";
        }

        public static string GetItems()
        {
            return $"SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }
    }
}
