using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceReporting
{
    [Serializable]
    public class UserBudgetPlan
    {
        public int PlanId { get; set; }
        public bool BudgetPlanActive { get; set; }
        public decimal budgetHousing { get; set; }
        public decimal IncomeAmount { get; set; }
        public string IncomeType { get; set; }
        public decimal budgetUtilities{ get; set; }
        public decimal budgetPhone{ get; set; }
        public decimal budgetTV{ get; set; }
        public decimal budgetInternet{ get; set; }
        public decimal budgetGroceries{ get; set; }
        public decimal budgetFood{ get; set; }
        public decimal budgetGas{ get; set; }
        public decimal budgetFamilyExpenses{ get; set; }
        public decimal budgetPersonalCare{ get; set; }
        public decimal budgetPets{ get; set; }
        public decimal budgetEntertainment{ get; set; }
        public decimal budgetInsurance{ get; set; }
        public decimal budgetDebtRepayment{ get; set; }
        public decimal budgetPropertyTax{ get; set; }
        public decimal budgetEmergencyFund{ get; set; }
        public decimal budgetRetirementSavings{ get; set; }
        public decimal budgetCollegeSavings{ get; set; }
        public decimal budgetGoal{ get; set; }
        public decimal budgetGifts{ get; set; }
        public decimal budgetOther{ get; set; }
        public DateTime BudgetModified { get; set; }

        public UserBudgetPlan()
        {
          BudgetPlanActive  = false;
         budgetHousing  = 0.00m;
         IncomeAmount = 0.00m;
         budgetUtilities = 0.00m;
         budgetPhone = 0.00m;
         budgetTV = 0.00m;
         budgetInternet = 0.00m;
         budgetGroceries = 0.00m;
         budgetFood = 0.00m;
         budgetGas = 0.00m;
         budgetFamilyExpenses = 0.00m;
         budgetPersonalCare = 0.00m;
         budgetPets = 0.00m;
         budgetEntertainment = 0.00m;
         budgetInsurance = 0.00m;
         budgetDebtRepayment = 0.00m;
         budgetPropertyTax = 0.00m;
         budgetEmergencyFund = 0.00m;
         budgetRetirementSavings = 0.00m;
         budgetCollegeSavings = 0.00m;
         budgetGoal = 0.00m;
         budgetGifts = 0.00m;
         budgetOther = 0.00m;
         BudgetModified = DateTime.Now;
            
        }


        
            
    }
}