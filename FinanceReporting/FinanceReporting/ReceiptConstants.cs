using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FinanceReporting
{
    public class ReceiptConstants
    {
        // string appname = System.AppDomain.CurrentDomain.FriendlyName.ToString();



        public static string UserProvCode
        {
            get
            {
                string UPcode;
                try
                {
                     UPcode = Membership.GetUser().ProviderUserKey.ToString();
                }

                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
               return  UPcode;
            }

            set
            {
                UserProvCode = value;
            }
        }

        public static Collection<ReceiptItemProduct> CurrentCollectionOfProducts = new Collection<ReceiptItemProduct>();
        public static DateTime beginMonth = DateTime.Now.AddDays(-(DateTime.Now.Day - 1));
        public static DateTime endMonth = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)).AddMonths(1);
        public static string connectionString = ConfigurationManager.ConnectionStrings["BB_ContactInfoConnectionString"].ConnectionString;
        public const string rciCode = "rciCode";
        public const string DefaultImage = "~/Images/ImageDefault.jpg";       
        public static string UserFolderRepository = "~/Images/ReceiptContents/" + UserProvCode;
        public const string SelectUserRCIS = "SELECT rciCode FROM ReceiptPages WHERE convert(date,DateTimeOfPurchase,101) = @SelectedDate AND usercode =  @SelectedUser";
        public const string InsertProductOnReceipt = "sp_InsertItemProducts";
        public const string InsertReceiptStatementReceipt = "sp_InsertNewReceipt";
        public const string InsertStatementReceiptProdudct = "INSERT INTO [BB_ContactInfo].[kwekum1].[Itemized_Products] ([RCI],[DATE_OF_RECEIPT],[NAME_OF_PRODUCT]" +
                ",[DESCRIPTION],[QUANTITY],[TOTAL_PRICE_OF_PRODUCT]) values (@RCI,@DATEOFRECEIPT,@NAMEOFPRODUCT,@DESCRIPTION,@QUANTITY,@TOTALPRICEOFPRODUCT)";

        public static string ReceiptSuccessMessage = "Receipt Placed Successfully!";
        public static string UpdateSuccessMessage = "Receipt has been successfully updated!";
        public static string NewPaymentMessage = "New Payment has successfully been added!";
        public static string DeletedPaymentMessage = "Payment Type has been deleted.";
        public static string DeletedProductMessage = "Proudct has been successfully deleted.";
        public static string ProductSucccessMessage = "New Product has Successfully been added.";
        public static string SubmitReceiptMessage
        {
            #region ReceiptSuccess
            get
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(ReceiptSuccessMessage);
                sb.Append("')};");
                sb.Append("</script>");
                ReceiptSuccessMessage = sb.ToString();

                return ReceiptSuccessMessage;
            }
            set
            {

                ReceiptSuccessMessage = value;
            }


            #endregion
        }
        public static string SubmitPaymentMessage
        {
            #region ReceiptSuccess
            get
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(NewPaymentMessage);
                sb.Append("')};");
                sb.Append("</script>");
                NewPaymentMessage = sb.ToString();

                return NewPaymentMessage;
            }
            set
            {

                NewPaymentMessage = value;
            }


            #endregion
        }
        public static string SubmitProductMessage
        {
            #region ReceiptSuccess
            get
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(ProductSucccessMessage);
                sb.Append("')};");
                sb.Append("</script>");
                ProductSucccessMessage = sb.ToString();

                return ProductSucccessMessage;
            }
            set
            {

                ProductSucccessMessage = value;
            }


            #endregion
        }
        public static string DeleteShownPaymentMessage
        {
            #region ReceiptSuccess
            get
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(DeletedPaymentMessage);
                sb.Append("')};");
                sb.Append("</script>");
                DeletedPaymentMessage = sb.ToString();

                return DeletedPaymentMessage;
            }
            set
            {

                DeletedPaymentMessage = value;
            }


            #endregion
        }
        public static string DeleteShownProductMessage
        {
            #region ReceiptSuccess
            get
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(DeletedProductMessage);
                sb.Append("')};");
                sb.Append("</script>");
                DeletedProductMessage = sb.ToString();

                return DeletedProductMessage;
            }
            set
            {

                DeletedProductMessage = value;
            }


            #endregion
        }
        public static string UpdateReceiptMessage
        {
            #region ReceiptSuccess
            get
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(UpdateSuccessMessage);
                sb.Append("')};");
                sb.Append("</script>");
                UpdateSuccessMessage = sb.ToString();

                return UpdateSuccessMessage;
            }
            set
            {

                UpdateSuccessMessage = value;
            }


            #endregion
        }
       
       
    }
}