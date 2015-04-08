using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Highcharts.Enums;
using Microsoft.Web.Infrastructure;
using System.Web.Razor;
using System.Web.WebPages.Deployment;
using System.Web.Services;
using System.Web.UI.HtmlControls;


namespace FinanceReporting
{

    public partial class Profile : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("Account/Login.aspx");
            }

            if (Session["CheckedInStatus"] == null)
            {
                Session["CheckedInStatus"] = "CheckIn";
            }
            ReceiptHyperLink.NavigateUrl = "NewEntry";
            ProductHyperLink.NavigateUrl = "NewProductEntry";
            PaymentHyperLink.NavigateUrl = "NewPayment";
            profileName.Text = User.Identity.Name.ToString();

            if (Session["CheckedInStatus"].ToString() == "CheckIn")
            {
             // Session["UserBudgetPlan"] = ReceiptDataAccessClass.LoadBudgetPlan(ReceiptConstants.UserProvCode);
              Session["CheckedInStatus"] = "BudgetLoaded";
                
            }

           // ReceiptDataAccessClass.GetBudgetUsageForMonth(ReceiptConstants.UserProvCode, ReceiptConstants.beginMonth, ReceiptConstants.endMonth);
            UserStatistics Ustats = ReceiptDataAccessClass.GetUserStatistics(ReceiptConstants.UserProvCode, ReceiptConstants.beginMonth, ReceiptConstants.endMonth);


            if (Ustats != null)
            {
                profileCurrentMonth.Text = GetCurrentMonth(DateTime.Now.Month);
                profileCurrentSpent.Text = Ustats.TotalAmountSpentforCurrentMonth;
                profilePreviousSpent.Text = Ustats.TotalAmountSpentforPreviousMonth;
                profileMostSpentPlace.Text = Ustats.VenueSpentMostCurrentMonth;
                profileMostamountNumber.Text = Ustats.VenueSpentMostAmountCurrentMonth;
                profilePercentageChange.Text = Ustats.PercentageDisplay;
                ProfileMostSpentURL.HRef = Ustats.VenueSpentMostUrlLink;
                profileTopCategoryName.Text = Ustats.TopCategoryCurrentMonth;
                profileTopCategoryMonthAmount.Text = Ustats.TopCategoryAmountSpentCurrentMonth;
            }


            if (!IsPostBack)
            {
                GridviewBind();

            }

            if (IsPostBack)
            {
                ReceiptTableGridview.DataSource = Session["cachedProfile"];

            }

            if (ReceiptTableGridview.Rows.Count < 1)
            {
                noInformationLabel.Visible = true;
            }

        }

        protected void GridviewBind()
        {
            Session["cachedProfile"] = ReceiptDataAccessClass.LoadProfileReceipts(ReceiptConstants.UserProvCode, 12);
            ReceiptTableGridview.DataSource = Session["cachedProfile"];
            ReceiptTableGridview.DataBind();

            if (Session["cachedProfile"] != null)
            {
            ReceiptTableGridview.UseAccessibleHeader = true;
            ReceiptTableGridview.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }



        protected void ReceiptTableGridview_Sorting(object sender, GridViewSortEventArgs e)
        {

            DataTable InfoToBeSorted = new DataTable();
            //Retrieve the table from the session object.
            DataSet ds = Session["cachedProfile"] as DataSet;

            if (ds != null)
            {
                InfoToBeSorted = ds.Tables[0];

                //Sort the data.
                InfoToBeSorted.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                ReceiptTableGridview.DataSource = InfoToBeSorted; // Session["cachedProfile"];
                ReceiptTableGridview.DataBind();
                Session["sortedProfile"] = ReceiptTableGridview.DataSource;
            }

        }

        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void ReceiptTableGridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ReceiptTableGridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            if (e.CommandName.Equals("OpenReceipt"))
            {
                //Redirect to item

                Response.Redirect("ManageReceipt.aspx" + "?ManageReceipt="+ e.CommandArgument.ToString());
                    //SecurityEncryptionClass.Encrypt(e.CommandArgument.ToString(), "ManageReceipt"));
            }
        }

        protected void ReceiptTableGridview_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            if (Session["sortedProfile"] != null)
            {
                ReceiptTableGridview.DataSource = Session["sortedProfile"];
            }
            else
            {
                ReceiptTableGridview.DataSource = Session["cachedProfile"];
            }

            ReceiptTableGridview.PageIndex = e.NewPageIndex;
            ReceiptTableGridview.DataBind();
           // Session["cachedProfile"] = ReceiptTableGridview.DataSource;
        }

        private string GetCurrentMonth(int month)
        {
            string TheMonth = "default";

            switch (month)
            {
                case 1: TheMonth = "January";
                    break;
                case 2: TheMonth = "February";
                    break;
                case 3: TheMonth = "March";
                    break;
                case 4: TheMonth = "April";
                    break;
                case 5: TheMonth = "May";
                    break;
                case 6: TheMonth = "June";
                    break;
                case 7: TheMonth = "July";
                    break;
                case 8: TheMonth = "August";
                    break;
                case 9: TheMonth = "September";
                    break;
                case 10: TheMonth = "October";
                    break;
                case 11: TheMonth = "November";
                    break;
                case 12: TheMonth = "December";
                    break;


            }

            return TheMonth;
        }


        [WebMethod]
        public static List<Data> GetData()
        {
            List<Data> dataList = new List<Data>();
            DataTable CategorySpending = ReceiptDataAccessClass.GetCategorySpendingByMonth(ReceiptConstants.UserProvCode, ReceiptConstants.beginMonth, ReceiptConstants.endMonth);

            foreach (DataRow Info in CategorySpending.Rows)
            {
                dataList.Add(new Data(Info["Category_Name"].ToString(), decimal.Parse(Info["Amount_Spent"].ToString())));
            }

      /*    dataList.Add(new Data("Column 1", 100));
            dataList.Add(new Data("Column 2", 200));
            dataList.Add(new Data("Column 3", 300));
            dataList.Add(new Data("Column 4", 400)); */

            return dataList;
        }



    }


    public class Data
    {
        public string ColumnName = "";
        public decimal Value = 0.00m;
        public Data(string columnName, decimal value) { ColumnName = columnName; Value = value; }
    }
}


