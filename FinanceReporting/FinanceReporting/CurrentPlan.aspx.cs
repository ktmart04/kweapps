using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FinanceReporting
{
    public partial class CurrentPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserBudgetPlan Bplan;
            UserBudgetPlan CurrentUsage;
            decimal BudgetAllEx = 100.34m;
            decimal BudgetAllex2 = 564.112m;


            if (Session["CheckedInStatus"] != null && Session["CheckedInStatus"].ToString() == "BudgetLoaded")
            {
                if (Session["CheckedInStatus"].ToString() == "BudgetLoaded")
                Bplan = (UserBudgetPlan)Session["UserBudgetPlan"];
            }

            MonthlyDivHeader.Style.Add("border-color", Color.Red.ToString());
            MonthlyDivHeader.Style.Add("border-color", "Red");
            panel1.Style.Add("background-color", "Green" );
            InfoBudgetAll.Text = BudgetAllEx.ToString("C2") + "    |           ";
            InfoBudgetAllCurrent.Text = BudgetAllex2.ToString("C2") + "   |       ";
            InfoBudgetAllSavings.Text = (BudgetAllEx - BudgetAllex2).ToString("C2");

            /*
                        if (Session[""] != null)
                        {
                            UserBudgetPlan pops = (UserBudgetPlan)Session["example"];

                            string answer = pops.budgetCollegeSavings.ToString("c2");
                            string answer2 = Session.SessionID.ToString();
                            string answer3 = ViewState["example"].ToString();
                            //      string answer4 = FinanceReporting.proper 
                
                        } */
        }
    }
}