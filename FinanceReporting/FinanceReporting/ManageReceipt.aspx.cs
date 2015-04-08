using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceReporting
{
    public partial class ManageReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["ManageReceipt"] == null)
            {
                Response.Redirect("Profile.aspx");
            }

            if (!Page.IsPostBack)
            {
                ReceiptDisplayBind();
             //   Session["ReceiptDisplayBind"] =  ReceiptDisplayBind();
            }

        }

        protected void ReceiptDisplayBind()
        {
            UpdateComboBoxPayment.DataSource = ReceiptDataAccessClass.LoadPaymentTypes(ReceiptConstants.UserProvCode);
            UpdateComboBoxCategory.DataSource = ReceiptDataAccessClass.LoadCategoryTypes();
            UpdateComboBoxPayment.DataBind();
            UpdateComboBoxCategory.DataBind();




            Receipt Display = ReceiptDataAccessClass.GetReceiptAndProductDetails(Request.QueryString["ManageReceipt"].ToString());
            TitleVenue.Text = Display.venueName;
            cellAmountSpent.Text = Display.totalAmountOfPurchase.ToString("C2");
            cellDateOfReceipt.Text = Display.finalDate.ToString();
            cellURCI.Text = Display.URCI;
            URCIlabelUpdate.Text = Display.URCI;
            cellDescription.Text = Display.descriptionOfPurchase;
            cellCategory.Text = Display.CategoryType.ToString();
            cellReceiptBarcode.Text = Display.BarcodeOfReceipt;
            cellPayment.Text = Display.PaymentDescription;
            cellProductCount.Text = Display.productCount.ToString();
            celltaxOfTheTransacction.Text = Display.totalTaxOfTransaction.ToString("C2");
            cellDateModified.Text = Display.dateModified.ToString();
            ReceiptImagePath.ImageUrl = Display.ReceiptImageURLfile;
           
            ReceiptImagePath.AlternateText = "Receipt Image of: " + Display.URCI;



            //for the updating the receipt item fields

            ListItem selectedPayment = new ListItem();
            selectedPayment.Value = UpdateComboBoxPayment.Items.FindByText(Display.PaymentDescription).Value;
            selectedPayment.Text = Display.PaymentDescription;
            ListItem selectedCategory = new ListItem();
            selectedCategory.Value = UpdateComboBoxCategory.Items.FindByText(Display.CategoryType).Value;
            selectedCategory.Text = Display.CategoryType;

            UpdateBoxAmount.Text = Display.totalAmountOfPurchase.ToString("C2").Trim('$');
            UpdateBoxBarcode.Text = Display.BarcodeOfReceipt;
            UpdateBoxDate.Text = Display.finalDate.ToString();
            UpdateBoxProductCount.Text = Display.productCount.ToString();
            UpdateBoxDescription.Text = Display.descriptionOfPurchase;
            UpdateComboBoxCategory.SelectedIndex = UpdateComboBoxCategory.Items.IndexOf(selectedCategory);
            UpdateComboBoxPayment.SelectedIndex = UpdateComboBoxPayment.Items.IndexOf(selectedPayment);
            UpdateBoxTax.Text = Display.totalTaxOfTransaction.ToString("C2").Trim('$');
            UpdateBoxURCI.Text = Display.URCI;
            UpdateBoxVenue.Text = Display.venueName;
            // ReceiptImage.ImageUrl = @"C:\Users\KTM4362\Pictures\120.jpg"; //make sre to change---------------


            if (Display.CollectionOfItems.Count > 0)
            {
                LineItemsProducts.Visible = false;
                // dropdownProducts.SelectedIndex = 0;
                // dropdownProducts.DataSource = Display.CollectionOfItems;
                //dropdownProducts.DataBind();
                //dropdownProducts.Text = Display.CollectionOfItems[dropdownProducts.SelectedItem].RPInameOfProduct;
                //dropdownProducts.Text = Display.CollectionOfItems[dropdownProducts.SelectedIndex].RPInameOfProduct;

                foreach (ReceiptItemProduct RPI in Display.CollectionOfItems)
                {

                    TableCell productName = new TableCell();
                    TableCell productDescription = new TableCell();
                    TableCell productBarcode = new TableCell();
                    TableCell productQuantity = new TableCell();
                    TableCell productUnitPrice = new TableCell();

                    productName.Text = RPI.RPInameOfProduct;
                    productDescription.Text = RPI.RPIdescriptionOfItem;
                    productBarcode.Text = RPI.RPIproductCode;
                    productQuantity.Text = RPI.RPIquantityAmount.ToString();
                    productUnitPrice.Text = RPI.RPIquantityPrice.ToString("C2");


                    TableRow ProductDisplayRow = new TableRow();
                    ProductDisplayRow.Cells.Add(productName);
                    ProductDisplayRow.Cells.Add(productDescription);
                    ProductDisplayRow.Cells.Add(productQuantity);
                    ProductDisplayRow.Cells.Add(productUnitPrice);
                    ProductDisplayRow.Cells.Add(productBarcode);
                    ReceiptItemsTable.Rows.Add(ProductDisplayRow);


                }
            }

            else
            {
                LineItemsVisibility.Visible = false;
                LineItemsProducts.Visible = true;
            }

        }




        protected void updateReceipt_Click(object sender, EventArgs e)
        {
            Receipt UpdatedReceipt = new Receipt();
            UpdatedReceipt.RDIcode = Request.QueryString["ManageReceipt"];
            UpdatedReceipt.BarcodeOfReceipt = UpdateBoxBarcode.Text;
            UpdatedReceipt.totalAmountOfPurchase = decimal.Parse(UpdateBoxAmount.Text);
            UpdatedReceipt.venueName = UpdateBoxVenue.Text;
            UpdatedReceipt.finalDate = DateTime.Parse(UpdateBoxDate.Text);
            UpdatedReceipt.URCI = UpdateBoxURCI.Text;
            UpdatedReceipt.totalTaxOfTransaction = decimal.Parse(UpdateBoxTax.Text);
            UpdatedReceipt.CategoryType = UpdateComboBoxCategory.SelectedValue;
            UpdatedReceipt.PaymentDescription = UpdateComboBoxPayment.SelectedValue;
            UpdatedReceipt.descriptionOfPurchase = UpdateBoxDescription.Text;
            UpdatedReceipt.productCount = int.Parse(UpdateBoxProductCount.Text);
            UpdatedReceipt.ReceiptImageURLfile = ReceiptDataAccessClass.GetReceiptDetails(UpdatedReceipt.RDIcode, ReceiptConstants.UserProvCode).ReceiptImageURLfile;
          

            if (ReceiptImageFileUpload.HasFile)
            {
                //clear here, pull current receipt path and delete to prevent over kill stock of images ----------
                if (!string.IsNullOrEmpty(UpdatedReceipt.ReceiptImageURLfile))
                {
                    FileInfo ReceiptImage = new FileInfo(HttpContext.Current.Server.MapPath(UpdatedReceipt.ReceiptImageURLfile));

                    if (ReceiptImage.Directory.Parent.Name == "ReceiptContents")
                    {
                        ReceiptImage.Delete();
                    }

                }

                string receiptImageFileName = ReceiptImageFileUpload.PostedFile.FileName;
                string receiptImageFileNameExtension = Path.GetExtension(receiptImageFileName);

                receiptImageFileName = UpdatedReceipt.RDIcode + receiptImageFileNameExtension;
                UpdatedReceipt.ReceiptImageURLfile = ReceiptConstants.UserFolderRepository + "/" + receiptImageFileName;
                string targetFolder = HttpContext.Current.Server.MapPath(ReceiptConstants.UserFolderRepository);
                string targetPath = Path.Combine(targetFolder, receiptImageFileName);
                ReceiptImageFileUpload.SaveAs(targetPath);
                ReceiptDataAccessClass.UpdateImage(ReceiptConstants.UserProvCode, Request.QueryString["ManageReceipt"], UpdatedReceipt.ReceiptImageURLfile);
              
            }

            ReceiptDataAccessClass.UpdateReceipt(ReceiptConstants.UserProvCode, Request.QueryString["ManageReceipt"].ToString(), UpdatedReceipt);
            ReceiptDisplayBind();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Receipt has been successfully updated!')", true); //Correct way to show receipt messages.....

        }



        public void OnConfirm(object sender, EventArgs e)
        {

            //Create delete receipt and the functionality to delete any associated products
            ReceiptDataAccessClass.DeleteReceiptAndReceiptsProducts(Request.QueryString["ManageReceipt"].ToString(), ReceiptConstants.UserProvCode);

            //make sure to add a message that receipt has been deleted
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Receipt has been successfully deleted!')", true);
            //then redirect to the profile page
            Response.Redirect("Profile.aspx");

        }

        protected void editProductsLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewProductEntry.aspx" + "?EditProducts=" + Request.QueryString["ManageReceipt"].ToString());
        }
    }
}