using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceReporting
{
    public partial class NewProductEntry : System.Web.UI.Page
    {
        private bool invalidReceiptRCIs = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("Account/Login.aspx");
            }

            ShowRCIchoice.Visible = false;

            if (string.IsNullOrEmpty(Request.QueryString["EditProducts"]))
            {
                Response.Redirect("Profile.aspx");
            }

            if (!Page.IsPostBack)
            {
                GridviewBind();
            }
        }

        protected void GridviewBind()
        {
            Session["cachedProducts"] = ReceiptDataAccessClass.LoadReceiptProducts(ReceiptConstants.UserProvCode, Request.QueryString["EditProducts"].ToString());
            ProductsGridview.DataSource = Session["cachedProducts"];
            ProductsGridview.DataBind();
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            DateTime temp;
            if (DateTime.TryParse(ValueSpecifiedDate.Text, out temp))
            {
                LoadSpecificReceiptRCIs();

                if (invalidReceiptRCIs == false)
                {
                    labelNoReceipts.Visible = false;
                    ShowRCIchoice.Visible = true;
                    
                }

                else
                {
                    labelNoReceipts.Visible = true;
                    ShowRCIchoice.Visible = false;
                }
            }
        }


       
        protected void submitFinal_Click(object sender, EventArgs e)
        {
            ReceiptItemProduct Product = new ReceiptItemProduct();
            //check this logic for date
            Product.finalDate = DateTime.Parse(ValueSpecifiedDate.Text);
            Product.URCI = ValueURCI.Text;
            Product.RPInameOfProduct = ValueProductName.Text;
            Product.RPIdescriptionOfItem = ValueDescription.Text;
            Product.RPIquantityAmount = ValueQuantityAmount.Text;
            Product.RPIquantityPrice = decimal.Parse(ValueTotalPriceOfProductss.Text);

            ReceiptDataAccessClass.LogProduct(Product, ReceiptConstants.UserProvCode);

            Reset();

        }

        private void LoadSpecificReceiptRCIs()
        {
            SqlCommand cmd = new SqlCommand(ReceiptConstants.SelectUserRCIS, new SqlConnection(ReceiptConstants.connectionString));
            cmd.Parameters.AddWithValue("@SelectedDate", DateTime.Parse(ValueSpecifiedDate.Text));
            cmd.Parameters.AddWithValue("@SelectedUser", User.Identity.Name.ToString());
            cmd.Connection.Open();

            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ValueURCI.DataSource = ddlValues;

            ValueURCI.DataValueField = ReceiptConstants.rciCode;
            ValueURCI.DataTextField = ReceiptConstants.rciCode;

            if (ValueURCI.Items.Count < 1)
            {
                invalidReceiptRCIs = true;
            }
            ValueURCI.DataBind();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void Reset()
        {
            ValueSpecifiedDate.Text = string.Empty;
            ValueURCI.SelectedIndex = 0;
            ValueProductName.Text = string.Empty;
            ValueDescription.Text = string.Empty;
            ValueQuantityAmount.Text = string.Empty;
            ValueTotalPriceOfProductss.Text = string.Empty;
        }

        protected void ProductsGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //e.NewPageIndex;
        }

        protected void ProductsGridview_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ProductsGridview.EditIndex = -1;
            GridviewBind();
        }

        protected void ProductsGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection iou = new SqlConnection(ReceiptConstants.connectionString);
            GridViewRow row = (GridViewRow)ProductsGridview.Rows[e.RowIndex];

            Label lbldeleteid = (Label)row.FindControl("lblIPID");

         //   conn.Open();

            SqlCommand cmd = new SqlCommand("delete FROM itemized_products where ipid='" + Convert.ToInt32(ProductsGridview.DataKeys[e.RowIndex].Value.ToString()) + "'", iou);
            
            //cmd.ExecuteNonQuery();

            //conn.Close();

            //gvbind();

        }

        protected void ProductsGridview_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ProductsGridview.EditIndex = e.NewEditIndex;
            GridviewBind();

        }

        protected void ProductsGridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userid = Convert.ToInt32(ProductsGridview.DataKeys[e.RowIndex].Value.ToString());

            GridViewRow row = (GridViewRow)ProductsGridview.Rows[e.RowIndex];

            Label lblID = (Label)row.FindControl("lblID");

            //TextBox txtname=(TextBox)gr.cell[].control[];
            
            TextBox ProductNameBox = new TextBox();
             ProductNameBox.Text = ((TextBox)row.Cells[0].Controls[0]).Text.TrimEnd();
            ProductNameBox.Text = ProductNameBox.Text.Trim();


            TextBox DescripBox = (TextBox)row.Cells[1].Controls[0];
            DescripBox.Text = DescripBox.Text.Trim();

            TextBox QuantityBox = (TextBox)row.Cells[2].Controls[0];
            QuantityBox.Text = QuantityBox.Text.Trim();


            TextBox UnitPriceBox = (TextBox)row.Cells[3].Controls[0];
            UnitPriceBox.Text = UnitPriceBox.Text.Trim();



            TextBox ProductCodeBox = (TextBox)row.Cells[4].Controls[0];
            ProductCodeBox.Text = ProductCodeBox.Text.Trim();


            //TextBox textadd = (TextBox)row.FindControl("txtadd");

            //TextBox textc = (TextBox)row.FindControl("txtc");

            ProductsGridview.EditIndex = -1;



           // gvbind();

            //GridView1.DataBind();


        }

        protected void updateReceipt_Click(object sender, EventArgs e)
        {
            Receipt ReceiptInfo = ReceiptDataAccessClass.GetReceiptDetails(Request.QueryString["EditProducts"].ToString(), ReceiptConstants.UserProvCode);
            ReceiptItemProduct newItem = new ReceiptItemProduct();
            newItem.RPInameOfProduct = TextBoxTNEWname.Text;
            newItem.RPIdescriptionOfItem = TextBoxTNEWdesc.Text;
            newItem.RPIproductCode = TextBoxTNEWpcode.Text;
            newItem.RPIquantityAmount = TextBoxTNEWquan.Text;
            newItem.RPIquantityPrice = decimal.Parse(TextBoxTNEWunit.Text);
            newItem.URCI = ReceiptInfo.URCI;
            newItem.finalDate = ReceiptInfo.finalDate;
            newItem.RDIcode = ReceiptInfo.RDIcode;
            ReceiptDataAccessClass.LogProduct(newItem, ReceiptConstants.UserProvCode);
            GridviewBind();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",ReceiptConstants.SubmitProductMessage.ToString()); 
        }


        protected void ProductsGridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "Edit")
            {
                

            }


            if (e.CommandName.ToString() == "DELETEPRODUCT")
            {
                ReceiptDataAccessClass.DeleteSpecificProductFromReceipt(ReceiptConstants.UserProvCode, int.Parse(e.CommandArgument.ToString()), Request.QueryString["EditProducts"].ToString());
                GridviewBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", ReceiptConstants.DeleteShownProductMessage.ToString());
            }
        }

    }
}