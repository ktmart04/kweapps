using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceReporting
{
    public class ReceiptItemProduct : Receipt
    {
        //public DateTime dateOfReceipt { get; set; }
        //public string URCI { get; set; }
        public string RPInameOfProduct { get; set; }
        public string RPIdescriptionOfItem { get; set; }
        public string RPIquantityAmount { get; set; }
        public string RPIproductCode { get; set; }
        public decimal RPIquantityPrice { get; set; }

    }
}