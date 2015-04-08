using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceReporting
{
    public partial class NewPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BoxValuePTID.DataSource = ReceiptDataAccessClass.LoadTenderTypes(ReceiptConstants.UserProvCode);
                BoxValuePTID.DataBind();
                GridviewBind();
            }

        }


        protected void GridviewBind()
        {

            DataSet AllPayments = ReceiptDataAccessClass.LoadTenderTypesOwnedbyUser(ReceiptConstants.UserProvCode);
            ownedPaymentsGridview.DataSource = AllPayments;
            ownedPaymentsGridview.DataBind();

        }

        protected void submitPaymentButton_Click(object sender, EventArgs e)
        {
            PaymentForm newPayment = new PaymentForm();
            newPayment.description = BoxDescriptionOfPymt.Text;
            newPayment.UserProvCode = ReceiptConstants.UserProvCode;
            newPayment.typeID = int.Parse(BoxValuePTID.SelectedValue);

            ReceiptDataAccessClass.LogNewPayment(newPayment);
            BoxDescriptionOfPymt.Text = null;
            BoxValuePTID.SelectedIndex = 0;
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",ReceiptConstants.SubmitPaymentMessage.ToString());
            GridviewBind();


        }


        protected void ownedPaymentsGridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {          
           int PTIDOfSelectedPayment = int.Parse(e.CommandArgument.ToString());
           ReceiptDataAccessClass.DeleteUserPayment(ReceiptConstants.UserProvCode, PTIDOfSelectedPayment);
           ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", ReceiptConstants.DeleteShownPaymentMessage.ToString());
           GridviewBind();

        }
    }
}