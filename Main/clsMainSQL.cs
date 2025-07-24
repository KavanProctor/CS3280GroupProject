namespace CS3280GroupProject.Main
{
    public class clsMainSQL
    {
        private static string GetMaxLineNum(int invoiceID)
        {
            return $"SELECT NZ(MAX(LineItemNum), 0) FROM LineItems WHERE InvoiceNum = {invoiceID}";
        }
        
        private static string ComputeCost(int invoiceID)
        {
            return $"SELECT SUM(i.Cost) FROM LineItems l INNER JOIN ItemDesc i ON l.InvoiceNum = i.InvoiceNum WHERE l.InvoiceNum = {invoiceID}";
        }
        private static string UpdateCost(int invoiceID)
        {
            return $"UPDATE Invoices SET TotalCost = ({ComputeCost(invoiceID)}) WHERE InvoiceNum = {invoiceID}";
        }

        public static string GetLatestInvoiceID()
        {
            return $"SELECT MAX(InvoiceNum) FROM Invoices";
        }

        public static string CreateInvoice(string date)
        {
            return $"INSERT INTO Invoices (InvoiceDate, TotalCost) VALUES (#{date}#, 0)";
        }
        public static string GetInvoice(int invoiceID)
        {
            return $"SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = {invoiceID}";
        }
        public static string RemoveInvoice(int invoiceID)
        {
            return $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceID}" + Environment.NewLine + $"DELETE FROM Invoices WHERE InvoiceNum = {invoiceID}";
        }
        public static string SetInvoiceDate(int invoiceID, string date)
        {
            return $"UPDATE Invoices SET InvoiceDate = #{date}# WHERE InvoiceNum = {invoiceID}";
        }

        public static string AddItemToInvoice(int invoiceID, string itemCode)
        {
            return $"INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES ({invoiceID}, ({GetMaxLineNum(invoiceID)}) + 1, '{itemCode}')" + Environment.NewLine + UpdateCost(invoiceID);
        }
        public static string GetInvoiceItems(int invoiceID)
        {
            return $"SELECT d.ItemCode, d.ItemDesc, d.Cost FROM ItemDesc d INNER JOIN LineItems l ON d.ItemCode = l.ItemCode WHERE l.InvoiceNum = {invoiceID}";
        }
        public static string RemoveItemFromInvoice(int invoiceID, string itemCode)
        {
            return $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceID} AND ItemCode = '{itemCode}'" + Environment.NewLine + UpdateCost(invoiceID);
        }

        public static string GetItems()
        {
            return $"SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }
    }
}
