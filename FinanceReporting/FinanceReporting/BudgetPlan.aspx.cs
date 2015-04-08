using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceReporting
{
    public partial class BudgetPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserBudgetPlan bs = new UserBudgetPlan();
            bs.budgetCollegeSavings = 100;
            Session["example"] = bs;


            if (Session["UserBudgetPlan"] != null)
            {
                //populate fields
            }
            //check to see if a budget plan exists, if so automatically populate the textboxes with the
            //values that the user has submitted, then allow the update                                                                            

        }

        protected void buttonSubmitBudget_Click(object sender, EventArgs e)
        {
            //create some kind of function to determine if budget already exists
            //using some kind of session variable, then either update or add on that

            UserBudgetPlan UserBudgetPlan = new UserBudgetPlan();
            UserBudgetPlan.budgetEntertainment = decimal.Parse(BoxExpenseDebtRepayment.Text);
            UserBudgetPlan.BudgetPlanActive = true;
            UserBudgetPlan.IncomeAmount = decimal.Parse(BoxWagesIncome.Text);
            UserBudgetPlan.budgetHousing = decimal.Parse(BoxExpenseHousing.Text);
            UserBudgetPlan.budgetUtilities = decimal.Parse(BoxExpenseUtilities.Text);
            UserBudgetPlan.budgetPhone = decimal.Parse(BoxExpensePhone.Text);
            UserBudgetPlan.budgetTV = decimal.Parse(BoxExpenseTV.Text);
            UserBudgetPlan.budgetInternet = decimal.Parse(BoxExpenseInternet.Text);
            UserBudgetPlan.budgetGroceries = decimal.Parse(BoxExpenseGroceries.Text);
            UserBudgetPlan.budgetFood = decimal.Parse(BoxExpenseFood.Text);
            UserBudgetPlan.budgetGas = decimal.Parse(BoxExpenseGas.Text);
            UserBudgetPlan.budgetFamilyExpenses = decimal.Parse(BoxExpenseFamily.Text);
            UserBudgetPlan.budgetPersonalCare = decimal.Parse(BoxExpenseHygiene.Text);
            UserBudgetPlan.budgetPets = decimal.Parse(BoxExpensePets.Text);
            UserBudgetPlan.budgetEntertainment = decimal.Parse(BoxExpenseEntertainment.Text);
            UserBudgetPlan.budgetInsurance = decimal.Parse(BoxExpenseInsurance.Text);
            UserBudgetPlan.budgetDebtRepayment = decimal.Parse(BoxExpenseDebtRepayment.Text);
            UserBudgetPlan.budgetPropertyTax = decimal.Parse(BoxExpensePropertyTax.Text);
            UserBudgetPlan.budgetEmergencyFund = decimal.Parse(BoxExpenseEmergencyFund.Text);
            UserBudgetPlan.budgetRetirementSavings = decimal.Parse(BoxExpenseRetirement.Text);
            UserBudgetPlan.budgetCollegeSavings = decimal.Parse(BoxExpenseCollegeSavings.Text);
            UserBudgetPlan.budgetGoal = decimal.Parse(BoxExpenseGoals.Text);
            UserBudgetPlan.budgetGifts = decimal.Parse(BoxExpenseGifts.Text);
            UserBudgetPlan.budgetOther = decimal.Parse(BoxExpenseOther.Text);
            ReceiptDataAccessClass.CreateNewBudgetPlan(UserBudgetPlan);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your Budget Plan has been successfully updated!')", true);           

        }


        protected void ClearBoxes()
        {
            BoxExpenseDebtRepayment.Text = string.Empty;
            BoxWagesIncome.Text = string.Empty;
            BoxExpenseHousing.Text = string.Empty;
            BoxExpenseUtilities.Text = string.Empty;
            BoxExpensePhone.Text = string.Empty;
            BoxExpenseTV.Text = string.Empty;
            BoxExpenseInternet.Text = string.Empty;
            BoxExpenseGroceries.Text = string.Empty;
            BoxExpenseFood.Text = string.Empty;
            BoxExpenseGas.Text = string.Empty;
            BoxExpenseFamily.Text = string.Empty;
            BoxExpenseHygiene.Text = string.Empty;
            BoxExpensePets.Text = string.Empty;
            BoxExpenseEntertainment.Text = string.Empty;
            BoxExpenseInsurance.Text = string.Empty;
            BoxExpenseDebtRepayment.Text = string.Empty;
            BoxExpensePropertyTax.Text = string.Empty;
            BoxExpenseEmergencyFund.Text = string.Empty;
            BoxExpenseRetirement.Text = string.Empty;
            BoxExpenseCollegeSavings.Text = string.Empty;
            BoxExpenseGoals.Text = string.Empty;
            BoxExpenseGifts.Text = string.Empty;
            BoxExpenseOther.Text = string.Empty;

        }
    }
}