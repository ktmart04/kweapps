using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FinanceReporting
{
    public class Receipt
    {
        public string RDIcode { get; set; }
        public string venueName { get; set; }
        public string timeOfPurchase { get; set; }
        public string datePurchased { get; set; }
        public DateTime dateModified { get; set; }
        public DateTime finalDate { get; set; }
        public DateTime dateReceiptEntered { get; set; }
        public decimal totalAmountOfPurchase { get; set; }
        public string URCI { get; set; }
        public string CategoryType { get; set; }
        public string descriptionOfPurchase { get; set; }
        public string userProviderKey { get; set; }
        public decimal totalTaxOfTransaction { get; set; }
        public int productCount { get; set; }
        public int PTID { get; set; }
        public string PaymentDescription { get; set; }
        public string FRuserID { get; set; }
        public string BarcodeOfReceipt { get; set; }
        public string ReceiptImageURLfile { get; set; }

        public Collection<ReceiptItemProduct> CollectionOfItems { get; set; }
               
    }

    public class PaymentForm
    {
        public int typeID { get; set; }
        public string description { get; set; }
        public string UserProvCode { get; set; }
        public Collection<PaymentForm> CollectionOfPayments { get; set; }

    }
}