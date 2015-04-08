using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceReporting
{
    public class UserStatistics
    {
        public int TotalCountofReceipts { get; set; }
        public int TotalCountofReceiptsCurrentMonth { get; set; }
        public int TotalCountofReceiptPreviousMonth { get; set; }

        public string TopCategoryCurrentMonth { get; set; }
        public string TopCategoryAmountSpentCurrentMonth { get; set; }
        public string TotalAmountSpentforCurrentMonth { get; set; }
        public string TotalAmountSpentforPreviousMonth { get; set; }
        public string VenueSpentMostCurrentMonth { get; set; }
        public string VenueSpentMostAmountCurrentMonth { get; set; }
        public string VenueSpentMostUrlLink { get; set; }
        public string PercentageDisplay { get; set; }

        public UserStatistics()
        {
            decimal defaultValue = 0.00m;
        

            TopCategoryCurrentMonth = "No information avaiable";
            TopCategoryAmountSpentCurrentMonth = "No information avaiable";
            TotalAmountSpentforCurrentMonth = defaultValue.ToString("C2");
            TotalAmountSpentforPreviousMonth = defaultValue.ToString("C2");
            VenueSpentMostCurrentMonth = "No information avaiable";
            VenueSpentMostAmountCurrentMonth = "No information avaiable";
            VenueSpentMostUrlLink = "#";
            PercentageDisplay = defaultValue.ToString("P2");
        }

        public UserStatistics(bool hasReceipts, bool hasCurrentMonthReciepts, bool hasPreviousMonthReceipts)
        {
            if (!hasReceipts)
            {
            }


        }

    }
}