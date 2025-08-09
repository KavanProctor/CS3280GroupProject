namespace CS3280GroupProject.Main
{
    /// <summary>
    /// contains dynamic SQL statements to interface with the database
    /// </summary>
    public class clsMainSQL
    {
        /// <summary>
        /// creates an SQL statement to get the highest line number from the LineItems table (only looks at rows with the specified invoice ID)
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <returns>the desired SQL statement</returns>
        public static string GetMaxLineNum(int invoiceID)
        {
            return $"SELECT MAX(LineItemNum) FROM LineItems WHERE InvoiceNum = {invoiceID}";
        }
        
        /// <summary>
        /// creates an SQL statement to calculate the cost of an invoice
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <returns>the desired SQL statement</returns>
        public static string ComputeCost(int invoiceID)
        {
            return $"SELECT SUM(i.Cost) FROM LineItems l INNER JOIN ItemDesc i ON l.ItemCode = i.ItemCode WHERE l.InvoiceNum = {invoiceID}";
        }
        /// <summary>
        /// creates an SQL statement to update the cost of some invoice
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <param name="cost">the new cost</param>
        /// <returns>the desired SQL statement</returns>
        public static string UpdateCost(int invoiceID, decimal cost)
        {
            return $"UPDATE Invoices SET TotalCost = {cost} WHERE InvoiceNum = {invoiceID}";
        }

        /// <summary>
        /// creates an SQL statement to get the invoice ID that was created most recently
        /// </summary>
        /// <returns>the desired SQL statement</returns>
        public static string GetLatestInvoiceID()
        {
            return $"SELECT MAX(InvoiceNum) FROM Invoices";
        }

        /// <summary>
        /// creates an SQL statement to create a new invoice
        /// </summary>
        /// <param name="date">the relevant invoice ID</param>
        /// <param name="cost">the cost of the invoice</param>
        /// <returns>the desired SQL statement</returns>
        public static string CreateInvoice(string date, decimal cost)
        {
            return $"INSERT INTO Invoices (InvoiceDate, TotalCost) VALUES (#{date}#, {cost})";
        }
        /// <summary>
        /// creates an SQL statement to get an invoice specified by an invoice ID
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <returns>the desired SQL statement</returns>
        public static string GetInvoice(int invoiceID)
        {
            return $"SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = {invoiceID}";
        }
        /// <summary>
        /// creates an SQL statement to remove an invoice specified by an invoice ID
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <returns>the desired SQL statement</returns>
        public static string RemoveInvoice(int invoiceID)
        {
            return $"DELETE FROM Invoices WHERE InvoiceNum = {invoiceID}";
        }
        /// <summary>
        /// creates an SQL statement to get all the invoice's items
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <returns>the desired SQL statement</returns>
        public static string RemoveInvoiceItems(int invoiceID)
        {
            return $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceID}";
        }
        /// <summary>
        /// creates an SQL statement to update the date of some invoice
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <param name="date">the new date</param>
        /// <returns>the desired SQL statement</returns>
        public static string SetInvoiceDate(int invoiceID, string date)
        {
            return $"UPDATE Invoices SET InvoiceDate = #{date}# WHERE InvoiceNum = {invoiceID}";
        }

        /// <summary>
        /// creates an SQL statement to add another item to some invoice
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <param name="itemCode">the item to add</param>
        /// <param name="line">the line number</param>
        /// <returns>the desired SQL statement</returns>
        public static string AddItemToInvoice(int invoiceID, string itemCode, int line)
        {
            return $"INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES ({invoiceID}, {line}, '{itemCode}')";
        }
        /// <summary>
        /// creates an SQL statement to get all of the items of some invoice
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <returns>the desired SQL statement</returns>
        public static string GetInvoiceItems(int invoiceID)
        {
            return $"SELECT d.ItemCode, d.ItemDesc, d.Cost FROM ItemDesc d INNER JOIN LineItems l ON d.ItemCode = l.ItemCode WHERE l.InvoiceNum = {invoiceID}";
        }
        /// <summary>
        /// creates an SQL statement to remove an item from some invoice
        /// </summary>
        /// <param name="invoiceID">the relevant invoice ID</param>
        /// <param name="itemCode">the item to remove</param>
        /// <returns>the desired SQL statement</returns>
        public static string RemoveItemFromInvoice(int invoiceID, string itemCode)
        {
            return $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceID} AND ItemCode = '{itemCode}'";
        }

        /// <summary>
        /// creates an SQL statement to get all of the items
        /// </summary>
        /// <returns>the desired SQL statement</returns>
        public static string GetItems()
        {
            return $"SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }
    }
}
