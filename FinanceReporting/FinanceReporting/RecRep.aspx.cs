using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceReporting
{
    public partial class RecRep : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridviewBind();

            }


            if (IsPostBack)
            {
                ReceiptTableGridview.DataSource = Session["cachedRecRep"];

            }

            if (ReceiptTableGridview.Rows.Count < 1)
            {
                noInformationLabel.Visible = true;
            }

        }

        protected void GridviewBind()
        {
            Session["cachedRecRep"] = ReceiptDataAccessClass.LoadProfileReceipts(ReceiptConstants.UserProvCode, 365);
            ReceiptTableGridview.DataSource = Session["cachedRecRep"];
            ReceiptTableGridview.DataBind();

            if (Session["cachedRecRep"] != null)
            {
                ReceiptTableGridview.UseAccessibleHeader = true;
                ReceiptTableGridview.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void ReceiptTableGridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            if (e.CommandName.Equals("OpenReceipt"))
            {
                //Redirect to item

                Response.Redirect("ManageReceipt.aspx" + "?ManageReceipt=" + e.CommandArgument.ToString());
                //SecurityEncryptionClass.Encrypt(e.CommandArgument.ToString(), "ManageReceipt"));
            }
        }

    }
}