using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GroupProject
{
    public class clsInvoice
    {
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalCost { get; set; }

        public void Clear()
        {
            this.InvoiceID = 0;
            this.InvoiceDate = DateTime.MinValue;
            this.TotalCost = 0.0m;
        }
    }
}
