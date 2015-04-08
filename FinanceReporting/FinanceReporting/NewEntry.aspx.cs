using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceReporting
{
    public partial class NewEntry : System.Web.UI.Page
    {

        ReceiptDataAccessClass DB = new ReceiptDataAccessClass();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                ValuePTID.DataSource = ReceiptDataAccessClass.LoadPaymentTypes(ReceiptConstants.UserProvCode);
                ValuePTID.DataBind();
            }
            


            if (ValuePTID.Items.Count < 1)
            {
                PaymentForm newPayment = new PaymentForm();
                newPayment.description = "Cash";
                newPayment.typeID = 10;
                newPayment.UserProvCode = ReceiptConstants.UserProvCode;
                ReceiptDataAccessClass.LogNewPayment(newPayment);
                
            }

            ListBoxDiv.Visible = false;


            if (Request["__EVENTARGUMENT"] != null && Request["__EVENTARGUMENT"] == "move")
            {

                int idx = listBoxReceiptProducts.SelectedIndex;
                ListItem item = listBoxReceiptProducts.SelectedItem;
                listBoxReceiptProducts.Items.Remove(item);
                ReceiptConstants.CurrentCollectionOfProducts.RemoveAt(idx);
                listBoxReceiptProducts.SelectedIndex = -1;
            }
            listBoxReceiptProducts.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(listBoxReceiptProducts, "move"));

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            #region Attach Receipt Data
            Receipt newReceipt = new Receipt();
            newReceipt.CollectionOfItems = new System.Collections.ObjectModel.Collection<ReceiptItemProduct>();
            newReceipt.venueName = ValueVenue.Text;
            newReceipt.timeOfPurchase = DateTime.Parse(ValueTOP.Text).ToShortTimeString();
            newReceipt.datePurchased = DateTime.Parse(ValueDOP.Text).ToShortDateString();
            newReceipt.finalDate = DateTime.Parse(newReceipt.datePurchased + " " + newReceipt.timeOfPurchase);
            newReceipt.dateReceiptEntered = DateTime.Now;
            newReceipt.totalAmountOfPurchase = decimal.Parse(ValueTransactionAmount.Text);
            newReceipt.CategoryType = ValueSelectedCategory.Text;
            newReceipt.descriptionOfPurchase = ValueDescription.Text;
            newReceipt.userProviderKey = ReceiptConstants.UserProvCode;
            newReceipt.totalTaxOfTransaction = decimal.Parse(ValueTaxes.Text);
            newReceipt.productCount = int.Parse(ValueProductCount.Text);
            newReceipt.PTID = int.Parse(ValuePTID.Text);
            newReceipt.BarcodeOfReceipt = ValueBarcode.Text;
            newReceipt.FRuserID = User.Identity.Name.ToString();
            newReceipt.ReceiptImageURLfile = ReceiptConstants.DefaultImage;
            if (string.IsNullOrEmpty(ValueURCI.Text))
            {
                newReceipt.URCI = ReceiptDataAccessClass.DetermineUniqueCode(ReceiptConstants.UserProvCode, newReceipt.venueName);
            }
            else
            {
                newReceipt.URCI = ValueURCI.Text;
            }


            int MonthForRDI = newReceipt.finalDate.Month;
            int YearForRDI = newReceipt.finalDate.Year;
            int DayForRDI = newReceipt.finalDate.Day;
            int TimeForRDI = newReceipt.finalDate.Hour;
            int MilForRDI = DateTime.Now.Millisecond;
            int MinforRDI = newReceipt.finalDate.Minute;
            int CurrentHourTimeForRDI = int.Parse(DateTime.UtcNow.Hour.ToString());
            int CurrentMinTimeForRDI = int.Parse(DateTime.UtcNow.Minute.ToString());
            int CurrentSecTimeForRDI = int.Parse(DateTime.UtcNow.Second.ToString());

            foreach (ReceiptItemProduct item in ReceiptConstants.CurrentCollectionOfProducts)
            {
                newReceipt.CollectionOfItems.Add(item);
            }

            newReceipt.RDIcode = YearForRDI.ToString() + MonthForRDI.ToString() + DayForRDI.ToString() + TimeForRDI.ToString() +
                MilForRDI.ToString() + MinforRDI.ToString() + CurrentHourTimeForRDI.ToString() + CurrentMinTimeForRDI.ToString() + CurrentSecTimeForRDI.ToString() + "A";

            if (string.IsNullOrEmpty(newReceipt.totalTaxOfTransaction.ToString()))
            {
                newReceipt.totalTaxOfTransaction = 0.00m;
            }
            #endregion


            //Image Directory Assignment
            if(!Directory.Exists(Server.MapPath(ReceiptConstants.UserFolderRepository)))
           {
               Directory.CreateDirectory(Server.MapPath(ReceiptConstants.UserFolderRepository));
           }

            if (ReceiptImageFileUpload.HasFile)
            {
                string receiptImageFileName = ReceiptImageFileUpload.PostedFile.FileName;
                string receiptImageFileNameExtension = Path.GetExtension(receiptImageFileName);

                receiptImageFileName = newReceipt.RDIcode + receiptImageFileNameExtension;
                newReceipt.ReceiptImageURLfile = ReceiptConstants.UserFolderRepository + "/" + receiptImageFileName;
                string targetFolder = HttpContext.Current.Server.MapPath(ReceiptConstants.UserFolderRepository);
                string targetPath = Path.Combine(targetFolder, receiptImageFileName);
                ReceiptImageFileUpload.SaveAs(targetPath);

                
            }


            DB.LogReceipt(newReceipt);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Receipt has been successfully Submitted!')", true);           
            Reset();
        }





        protected void Reset()
        {
            ValueVenue.Text = string.Empty;
            ValueTOP.Text = string.Empty;
            ValueDOP.Text = string.Empty;
            ValueTransactionAmount.Text = string.Empty;
            ValueURCI.Text = string.Empty;
            ValueDescription.Text = string.Empty;
            ValueTaxes.Text = string.Empty;
            ValueProductCount.Text = string.Empty;
            ValueBarcode.Text = string.Empty;
            ReceiptConstants.CurrentCollectionOfProducts.Clear();
        }


        protected void productSubmit_Click(object sender, EventArgs e)
        {

            ReceiptItemProduct newItem = new ReceiptItemProduct();
            newItem.RPInameOfProduct = TextBoxTNEWname.Text;
            newItem.RPIdescriptionOfItem = TextBoxTNEWdesc.Text;
            newItem.RPIproductCode = TextBoxTNEWpcode.Text;
            newItem.RPIquantityAmount = TextBoxTNEWquan.Text;
            newItem.RPIquantityPrice = decimal.Parse(TextBoxTNEWunit.Text);



            ReceiptConstants.CurrentCollectionOfProducts.Add(newItem);

            ListItem item = new ListItem(newItem.RPInameOfProduct, newItem.RPIdescriptionOfItem);
            listBoxReceiptProducts.Items.Add(item);

            if (listBoxReceiptProducts.Items.Count > 0)
            {
                ListBoxDiv.Visible = true;
            }


            TextBoxTNEWdesc.Text = "Description";
            TextBoxTNEWname.Text = "Product Name";
            TextBoxTNEWpcode.Text = "Product Code";
            TextBoxTNEWquan.Text = "Quantity Amount";
            TextBoxTNEWunit.Text = "Unit Price";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product has been added to this Receipt')", true);
            

        }

        protected void Upload(object sender, EventArgs e)
        {
            //Image Directory Assignment
            if (!Directory.Exists(Server.MapPath(ReceiptConstants.UserFolderRepository)))
            {
                Directory.CreateDirectory(Server.MapPath(ReceiptConstants.UserFolderRepository));
            }

            if (ReceiptImageFileUpload.HasFile)
            {
                string receiptImageFileName = ReceiptImageFileUpload.PostedFile.FileName;
                string receiptImageFileNameExtension = Path.GetExtension(receiptImageFileName);

                receiptImageFileName = "Exd" + receiptImageFileNameExtension;
                string anser = ReceiptConstants.UserFolderRepository + "/" +  receiptImageFileName;
                string targetFolder = HttpContext.Current.Server.MapPath(ReceiptConstants.UserFolderRepository);
                string targetPath = Path.Combine(targetFolder, receiptImageFileName);
                ReceiptImageFileUpload.SaveAs(targetPath);
            }
            


        }     
    

    }

}