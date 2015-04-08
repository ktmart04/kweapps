using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Text;
using System.IO;


namespace FinanceReporting
{
    internal class ReceiptDataAccessClass
    {
        public static UserStatistics GetUserStatistics(string userCode, DateTime beginMonth, DateTime endMonth)
        {
            UserStatistics Uinfo = new UserStatistics();

            try
            {

                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {
                    DataSet UserStats = new DataSet();
                    
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetUserStatistics", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserCode", userCode);
                    cmd.Parameters.AddWithValue("@BeginMonth", beginMonth.ToShortDateString());
                    cmd.Parameters.AddWithValue("@EndMonth", endMonth.ToShortDateString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(UserStats);
                    con.Close();
                    cmd.Dispose();
                    con.Dispose();
                    da.Dispose();
                    
                    //Get User total receipt count
                    DataRow UserStatsDataRow = UserStats.Tables[0].Rows[0];
                    Uinfo.TotalCountofReceipts = int.Parse(UserStatsDataRow["TOTAL RECEIPTS"].ToString());
                    //Get User total receipt count Current month
                    UserStatsDataRow = UserStats.Tables[1].Rows[0];
                    Uinfo.TotalCountofReceiptsCurrentMonth = int.Parse(UserStatsDataRow["TOTAL RECEIPTS CURRENT MONTH"].ToString());
                    //Get User total receipt count Previous month
                    UserStatsDataRow = UserStats.Tables[2].Rows[0];
                    Uinfo.TotalCountofReceiptPreviousMonth = int.Parse(UserStatsDataRow["TOTAL RECEIPTS PREVIOUS MONTH"].ToString());

                    if (Uinfo.TotalCountofReceipts == 0)
                    {
                        return Uinfo;
                    }
                    
                    
                    if (Uinfo.TotalCountofReceiptPreviousMonth > 0)
                    {
                    //Get User previous month spending 
                    UserStatsDataRow = UserStats.Tables[4].Rows[0];
                    Uinfo.TotalAmountSpentforPreviousMonth = decimal.Parse(UserStatsDataRow["Amount Spent"].ToString()).ToString("C2");                    
                    }

                    if (Uinfo.TotalCountofReceiptsCurrentMonth > 0)
                    {
                        //Get User current month spending
                        UserStatsDataRow = UserStats.Tables[3].Rows[0];
                        Uinfo.TotalAmountSpentforCurrentMonth = decimal.Parse(UserStatsDataRow["Amount Spent"].ToString()).ToString("C2");
                        //Get the venue where the money was spent the most for the current month
                        UserStatsDataRow = UserStats.Tables[5].Rows[0];
                        Uinfo.VenueSpentMostCurrentMonth = UserStatsDataRow["Venue"].ToString();
                        //get the amount that was spent above
                        Uinfo.VenueSpentMostAmountCurrentMonth = decimal.Parse(UserStatsDataRow["Amount Spent"].ToString()).ToString("C2");
                        //Get the url to the highest amount receipt of the month 
                        Uinfo.VenueSpentMostUrlLink = "ManageReceipt.aspx?ManageReceipt=" + UserStatsDataRow["RDI"].ToString();
                        //Get User current month spending 
                        UserStatsDataRow = UserStats.Tables[6].Rows[0];
                        //get the category that was spent the most for the current month
                        Uinfo.TopCategoryCurrentMonth = UserStatsDataRow["CATEGORY"].ToString();
                        //get the amount spent in the category for the current month
                        Uinfo.TopCategoryAmountSpentCurrentMonth = decimal.Parse(UserStatsDataRow["TOTAL SPENT"].ToString()).ToString("C2");

                        decimal currentSpending = decimal.Parse(Uinfo.TotalAmountSpentforCurrentMonth.Trim('$'));
                        decimal previousSpent = decimal.Parse(Uinfo.TotalAmountSpentforPreviousMonth.Trim('$'));

                        if (previousSpent != 0.00m || previousSpent != 0)
                        {
                            decimal percentageChange = (currentSpending - previousSpent) / (previousSpent);


                            if (percentageChange < 0)
                            {
                                Uinfo.PercentageDisplay = "You have spent " + percentageChange.ToString("P2").Trim('-') + " " + "less this month.";
                            }

                            else
                            {
                                Uinfo.PercentageDisplay = "You have spent " + percentageChange.ToString("P2") + " " + "more this month.";
                            }
                        }
                    }

                    UserStatistics u = new UserStatistics();


                    return Uinfo;

                }
            }

            catch (SqlException ex)
            {
                string errorMessage = ex.Message.ToString();
                return null;
            }

        }




        public static Receipt GetReceiptDetails(string RDICode, string UserCode)
        {
            Receipt selectedReceipt = new Receipt();
            DataTable receiptDatasetInformation = new DataTable();
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmdGetReceipt = new SqlCommand("sp_GetReceiptDetails", con);
                cmdGetReceipt.CommandType = CommandType.StoredProcedure;
                cmdGetReceipt.Parameters.AddWithValue("@RDICode", RDICode);
                cmdGetReceipt.Parameters.AddWithValue("@UserCode", UserCode);
                SqlDataAdapter da = new SqlDataAdapter(cmdGetReceipt);
                da.Fill(receiptDatasetInformation);
                con.Close();
                cmdGetReceipt.Dispose();
                con.Dispose();
                da.Dispose();
            }

            DataRow ReceiptInfoRow = receiptDatasetInformation.Rows[0];
            selectedReceipt.RDIcode = ReceiptInfoRow["rdiCode"].ToString();
            selectedReceipt.URCI = ReceiptInfoRow["rciCode"].ToString();
            selectedReceipt.CategoryType = ReceiptInfoRow["Category_Name"].ToString();
            selectedReceipt.venueName = ReceiptInfoRow["Venue_Name"].ToString();
            selectedReceipt.totalAmountOfPurchase = decimal.Parse(ReceiptInfoRow["Amount_Spent"].ToString());
            selectedReceipt.totalTaxOfTransaction = decimal.Parse(ReceiptInfoRow["TaxAmountOfTransaction"].ToString());
            selectedReceipt.finalDate = DateTime.Parse(ReceiptInfoRow["DateTimeofPurchase"].ToString());
            selectedReceipt.PaymentDescription = ReceiptInfoRow["Description_of_Payment"].ToString();
            selectedReceipt.descriptionOfPurchase = ReceiptInfoRow["descriptioninfo"].ToString();
            selectedReceipt.ReceiptImageURLfile = ReceiptInfoRow["Image_Filename"].ToString();
            
            
            return selectedReceipt;
            
        }


        internal static Receipt GetReceiptAndProductDetails(string RDIcode)
        {
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {

                Receipt ManagedReceipt = new Receipt();
                ManagedReceipt.CollectionOfItems = new System.Collections.ObjectModel.Collection<ReceiptItemProduct>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetProductItemsByReceipt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserCode", ReceiptConstants.UserProvCode);
                cmd.Parameters.AddWithValue("@rdiCode", RDIcode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ReceiptInformation = new DataTable();
                da.Fill(ReceiptInformation);

                //Example to retrieve receipt details then retrieve contents by foreach row if exists
                if (ReceiptInformation.Rows.Count > 0)
                {

                    DataRow TopReceiptRow = ReceiptInformation.Rows[0];
                    ManagedReceipt.RDIcode = TopReceiptRow["rdiCode"].ToString();
                    ManagedReceipt.URCI = TopReceiptRow["Receipt Identifying Code"].ToString();
                    ManagedReceipt.CategoryType = TopReceiptRow["Category"].ToString();
                    ManagedReceipt.venueName = TopReceiptRow["Retail Venue Name"].ToString();
                    ManagedReceipt.totalAmountOfPurchase = decimal.Parse(TopReceiptRow["Total Amount Spent"].ToString());
                    ManagedReceipt.totalTaxOfTransaction = decimal.Parse(TopReceiptRow["Taxes Charged"].ToString());
                    ManagedReceipt.finalDate = DateTime.Parse(TopReceiptRow["Date of Purchase"].ToString());
                    ManagedReceipt.PaymentDescription = TopReceiptRow["Description_of_Payment"].ToString();
                    ManagedReceipt.descriptionOfPurchase = TopReceiptRow["Minor Description"].ToString();
                    ManagedReceipt.productCount = int.Parse(TopReceiptRow["Count_of_Total_Items"].ToString());
                    ManagedReceipt.dateReceiptEntered = DateTime.Parse(TopReceiptRow["DateOfReceiptEntry"].ToString());
                    ManagedReceipt.BarcodeOfReceipt = TopReceiptRow["ReceiptBarcode"].ToString();
                    ManagedReceipt.ReceiptImageURLfile = TopReceiptRow["Image_Filename"].ToString();
                    if (string.IsNullOrEmpty(TopReceiptRow["DateReceiptModified"].ToString()))
                    {
                        ManagedReceipt.dateModified = DateTime.Parse(TopReceiptRow["DateOfReceiptEntry"].ToString());                       
                    }
                    else
                    {
                        ManagedReceipt.dateModified = DateTime.Parse(TopReceiptRow["DateReceiptModified"].ToString());
                    }
                    foreach (DataRow ReceiptRow in ReceiptInformation.Rows)
                    {
                        if (!string.IsNullOrEmpty(ReceiptRow["Product Name"].ToString()) && !string.IsNullOrEmpty(ReceiptRow["Description"].ToString()))
                        {
                            ReceiptItemProduct ReceiptItem = new ReceiptItemProduct();
                            ReceiptItem.RPInameOfProduct = ReceiptRow["Product Name"].ToString();
                            ReceiptItem.RPIdescriptionOfItem = ReceiptRow["Description"].ToString();
                            ReceiptItem.RPIproductCode = ReceiptRow["Product Code/SKU"].ToString();
                            ReceiptItem.RPIquantityAmount = ReceiptRow["Quantity of Product"].ToString();
                            ReceiptItem.RPIquantityPrice = decimal.Parse(ReceiptRow["Unit Price"].ToString());


                            ManagedReceipt.CollectionOfItems.Add(ReceiptItem);

                        }

                    }
                }


                #region Misc
                //second method to retrieve itmes
                /*
                SqlCommand secondary = new SqlCommand("sp_GetProductItemsByReceipt", con);
                secondary.CommandType = CommandType.StoredProcedure;
                secondary.Parameters.AddWithValue("@UserCode", ReceiptConstants.UserProvCode);
                secondary.Parameters.AddWithValue("@rdiCode", ManagedReceipt.RDIcode);
                secondary.Parameters.AddWithValue("@rciCode", ManagedReceipt.URCI);
                SqlDataAdapter ProductsAdapter = new SqlDataAdapter(secondary);
                DataTable CollectionOfProducts = new DataTable();
                ProductsAdapter.Fill(CollectionOfProducts);*/

                //Example to show all receipts

                /*SqlCommand cmd2 = new SqlCommand("select * from itemized_products", con); //Select * from [BB_ContactInfo].[dbo].[ReceiptPages]
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable ProductInformation = new DataTable();
                da2.Fill(ProductInformation);
                ReceiptItems.DataSource = ProductInformation;
                ReceiptItems.DataBind(); */

                //SqlCommand Inhouse = new SqlCommand("ALTER TABLE [ReceiptPages] ALTER COLUMN USER_PROV_CODE nvarchar(40) null", con);
                //Inhouse.ExecuteScalar();

                //   SqlDataReader dr = cmd.ExecuteReader();
                //ReceiptTableGridview.DataSource = dr;
                #endregion

                return ManagedReceipt;
            }
        }

        public static DataSet LoadProfileReceipts(string userCode, int ReceiptDisplayType)
        {
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                string StoredProcedure;

                switch (ReceiptDisplayType)
                {
                    case 12:
                        StoredProcedure = "sp_GetProfileReceiptsByUserForMonth";
                        break;
                    case 365:
                        StoredProcedure = "sp_GetProfileReceiptsByUser";
                        break;
                    default:
                        StoredProcedure = "sp_GetProfileReceiptsByUser";
                        break;
                }

                SqlCommand cmd = new SqlCommand(StoredProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserCode", userCode);
                cmd.Parameters.AddWithValue("@BeginMonth", ReceiptConstants.beginMonth);
                cmd.Parameters.AddWithValue("@EndMonth", ReceiptConstants.endMonth);

                //   SqlDataReader dr = cmd.ExecuteReader();
                //ReceiptTableGridview.DataSource = dr;
                //  OnSorting="ReceiptTableGridviewSorting"
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet profileViewDatasetInformation = new DataSet();
                da.Fill(profileViewDatasetInformation);
                con.Close();

                cmd.Dispose();
                con.Dispose();
                da.Dispose();


                if (profileViewDatasetInformation.Tables[0].Rows.Count > 0)
                    return profileViewDatasetInformation;

                else
                    profileViewDatasetInformation.Dispose();
                    return null;

            }


        }


        internal static UserBudgetPlan LoadBudgetPlan(string UserCode)
        {
            UserBudgetPlan UserCurrentBudget = new UserBudgetPlan();
            DataTable BudgetPlanInformation = new DataTable();
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmdGetBudget = new SqlCommand("sp_LoadUserBudgetPlan", con);
                cmdGetBudget.CommandType = CommandType.StoredProcedure;
                cmdGetBudget.Parameters.AddWithValue("@UserCode", UserCode);
                SqlDataAdapter da = new SqlDataAdapter(cmdGetBudget);
                da.Fill(BudgetPlanInformation);
                con.Close();
                cmdGetBudget.Dispose();
                con.Dispose();
                da.Dispose();


                if (BudgetPlanInformation.Rows.Count == 0)
                {
                    return null;
                }
            }

            DataRow BudgetRow = BudgetPlanInformation.Rows[0];
            UserCurrentBudget.PlanId = int.Parse(BudgetRow["BudgetPlanId"].ToString());        
            UserCurrentBudget.BudgetPlanActive = true;
            UserCurrentBudget.IncomeAmount = decimal.Parse(BudgetRow["IncomeAmount"].ToString());
            UserCurrentBudget.budgetHousing = decimal.Parse(BudgetRow["BudgetHousing"].ToString());
            UserCurrentBudget.budgetUtilities = decimal.Parse(BudgetRow["BudgetUtilities"].ToString());
            UserCurrentBudget.budgetPhone = decimal.Parse(BudgetRow["BudgetPhone"].ToString());
            UserCurrentBudget.budgetTV = decimal.Parse(BudgetRow["BudgetTV"].ToString());
            UserCurrentBudget.budgetInternet = decimal.Parse(BudgetRow["BudgetInternet"].ToString());
            UserCurrentBudget.budgetGroceries = decimal.Parse(BudgetRow["BudgetGroceries"].ToString());
            UserCurrentBudget.budgetFood = decimal.Parse(BudgetRow["BudgetFood"].ToString());
            UserCurrentBudget.budgetGas = decimal.Parse(BudgetRow["BudgetGas"].ToString());
            UserCurrentBudget.budgetFamilyExpenses = decimal.Parse(BudgetRow["BudgetFamilyExpenses"].ToString());
            UserCurrentBudget.budgetPersonalCare = decimal.Parse(BudgetRow["BudgetPersonalCare"].ToString());
            UserCurrentBudget.budgetPets = decimal.Parse(BudgetRow["BudgetPets"].ToString());
            UserCurrentBudget.budgetEntertainment = decimal.Parse(BudgetRow["BudgetEntertainment"].ToString());
            UserCurrentBudget.budgetInsurance = decimal.Parse(BudgetRow["BudgetInsurance"].ToString());
            UserCurrentBudget.budgetDebtRepayment = decimal.Parse(BudgetRow["BudgetDebtRepayment"].ToString());
            UserCurrentBudget.budgetPropertyTax = decimal.Parse(BudgetRow["BudgetPropertyTax"].ToString());
            UserCurrentBudget.budgetEmergencyFund = decimal.Parse(BudgetRow["BudgetEmergencyFund"].ToString());
            UserCurrentBudget.budgetRetirementSavings = decimal.Parse(BudgetRow["BudgetRetirement"].ToString());
            UserCurrentBudget.budgetCollegeSavings = decimal.Parse(BudgetRow["BudgetCollegeSavings"].ToString());
            UserCurrentBudget.budgetGoal = decimal.Parse(BudgetRow["BudgetGoals"].ToString());
            UserCurrentBudget.budgetGifts = decimal.Parse(BudgetRow["BudgetGifts"].ToString());
            UserCurrentBudget.budgetOther = decimal.Parse(BudgetRow["BudgetOther"].ToString());


            BudgetPlanInformation.Dispose();
           
          

            

            return UserCurrentBudget;
        }

        public static string GetCurrentMonthSpending(string userCode, DateTime beginMonth, DateTime endMonth)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_profileCurrenttMonthSpentAmount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserCode", userCode);
                    cmd.Parameters.AddWithValue("@BeginMonth", beginMonth.ToShortDateString());
                    cmd.Parameters.AddWithValue("@EndMonth", endMonth.ToShortDateString());

                    string resultanswer = cmd.ExecuteScalar().ToString();
                    decimal AmountSpent = 0.00m;
                    cmd.Parameters.Clear();

                    con.Close();

                    if (resultanswer != "")
                    {
                        AmountSpent = decimal.Parse(resultanswer);
                    }

                    cmd.Dispose();
                    con.Dispose();
                    return AmountSpent.ToString("C");

                }
            }

            catch (SqlException ex)
            {
                string errorMessage = ex.Message.ToString();
                return errorMessage;
            }

        }

        public static string GetPreviousMonthSpending(string userCode, DateTime beginMonth, DateTime endMonth)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_profileCurrenttMonthSpentAmount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserCode", userCode);
                    cmd.Parameters.AddWithValue("@BeginMonth", beginMonth.AddMonths(-1).ToShortDateString());
                    cmd.Parameters.AddWithValue("@EndMonth", endMonth.AddMonths(-1).ToShortDateString());

                    string resultanswer = cmd.ExecuteScalar().ToString();
                    decimal AmountSpent = 0.00m;

                    con.Close();

                    if (resultanswer != "")
                    {
                        AmountSpent = decimal.Parse(resultanswer);
                    }

                    return AmountSpent.ToString("C");

                }
            }

            catch (SqlException ex)
            {
                string errorMessage = ex.Message.ToString();
                return errorMessage;
            }


        }


        public static DataRow GetLargestReceiptOfMonth(string userCode, DateTime beginMonth, DateTime endMonth)
        {

            try
            {

                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetMaxReceiptForMonth", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserCode", userCode);
                    cmd.Parameters.AddWithValue("@BeginMonth", beginMonth.ToShortDateString());
                    cmd.Parameters.AddWithValue("@EndMonth", endMonth.ToShortDateString());


                    SqlDataAdapter da = new SqlDataAdapter(cmd);


                    DataTable ReceiptInformation = new DataTable();
                    da.Fill(ReceiptInformation);
                    con.Close();

                    DataRow ReceiptInfoRow = ReceiptInformation.Rows[0];

                    if (ReceiptInfoRow == null)
                    {
                        DataTable dt = new DataTable();
                        ReceiptInfoRow = dt.Rows[0];
                    }
                    //  selectedReceipt.RDIcode = ReceiptInfoRow["Venue"].ToString();
                    // selectedReceipt.RDIcode = ReceiptInfoRow["Amount Spent"].ToString();

                    cmd.Dispose();
                    con.Dispose();

                    return ReceiptInformation.Rows[0];



                }
            }

            catch (SqlException ex)
            {

                string error = ex.Message.ToString();
                return null;
            }

            catch (IndexOutOfRangeException ex)
            {
                string error = ex.Message.ToString();
                return null;
            }



        }


        public static UserStatistics GetCategorySpentInfo(string userCode, DateTime beginMonth, DateTime endMonth)
        {
            try
            {
                UserStatistics UserStats = new UserStatistics();

                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetMaxCategoryForMonth", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserCode", userCode);
                    cmd.Parameters.AddWithValue("@BeginMonth", beginMonth.ToShortDateString());
                    cmd.Parameters.AddWithValue("@EndMonth", endMonth.ToShortDateString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);


                    DataTable UserInformation = new DataTable();
                    da.Fill(UserInformation);
                    con.Close();

                    DataRow UserInfoRow = UserInformation.Rows[0];
                    UserStats.TopCategoryCurrentMonth = UserInfoRow["CATEGORY"].ToString();
                    UserStats.TopCategoryAmountSpentCurrentMonth = decimal.Parse(UserInfoRow["TOTAL SPENT"].ToString()).ToString("C2");




                    return UserStats;

                }
            }

            catch (SqlException ex)
            {
                string errorMessage = ex.Message.ToString();
                return null;
            }


        }




        public static DataSet LoadPaymentTypes(string userCode)
        {

            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_LoadPaymentsByUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserCode", userCode);


                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet userPaymentsDatasetInformation = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(userPaymentsDatasetInformation);
                con.Close();

                return userPaymentsDatasetInformation;
            }
        
        }


        public static DataSet LoadCategoryTypes()
        {

            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmdGetCategories = new SqlCommand("select * from zib_category", con);
                // cmdGetCategories.CommandType = CommandType.StoredProcedure;
                // cmdGetCategories.Parameters.AddWithValue("@UserCode", userCode);


                SqlDataAdapter da = new SqlDataAdapter(cmdGetCategories);

                DataSet categoryInformation = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(categoryInformation);
                con.Close();

                return categoryInformation;

            }
        }

        public static DataSet LoadTenderTypes(string userCode)
        {

            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tender_types", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //
               // cmd.Parameters.AddWithValue("@UserCode", userCode);


                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet userPaymentTendersDatasetInformation = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(userPaymentTendersDatasetInformation);
                con.Close();

                return userPaymentTendersDatasetInformation;
            }




        }

        public static DataSet LoadTenderTypesOwnedbyUser(string userCode)
        {
           using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_LoadPaymentsOwnedByUser", con);   //show PTID in stored procedure
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@UserCode", userCode);


                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet userPaymentTendersDatasetInformation = new DataSet();
                da.Fill(userPaymentTendersDatasetInformation);
                con.Close();

                con.Dispose();
                cmd.Dispose();
                return userPaymentTendersDatasetInformation;

            }
        }


        public static void DeleteReceiptAndReceiptsProducts(string RdiCode, string UserCode)
        {
            if (!string.IsNullOrEmpty(GetReceiptDetails(RdiCode, UserCode).ReceiptImageURLfile))
            {
                FileInfo ReceiptImage = new FileInfo(HttpContext.Current.Server.MapPath(GetReceiptDetails(RdiCode, UserCode).ReceiptImageURLfile));

                if (ReceiptImage.Directory.Parent.Name == "ReceiptContents")
                {
                    ReceiptImage.Delete();
                }

            }

            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmdDeleteReceipt = new SqlCommand("Delete From receiptpages where rdicode = @RDICODE AND User_Prov_Code = @UserCode", con);
                cmdDeleteReceipt.Parameters.AddWithValue("@RDICODE", RdiCode);
                cmdDeleteReceipt.Parameters.AddWithValue("@UserCode", UserCode);
                cmdDeleteReceipt.ExecuteNonQuery();
                cmdDeleteReceipt.Parameters.Clear();
                con.Close();
            }
            

        }

        public static void DeleteUserPayment(string usercode, int PTID)
        {
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmdDeletePaymeent = new SqlCommand("DELETE FROM payment_types where UserCode =  @UserCode AND PT_ID = @PTID", con);
                cmdDeletePaymeent.Parameters.AddWithValue("@UserCode", usercode);
                cmdDeletePaymeent.Parameters.AddWithValue("@PTID", PTID);
                cmdDeletePaymeent.ExecuteNonQuery();
                cmdDeletePaymeent.Parameters.Clear();
                con.Close();
            }


        }

        public static void DeleteSpecificProductFromReceipt(string usercode, int ProductID, string RDICode)
        {
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmdDeleteProduct = new SqlCommand("DELETE from Itemized_products where User_Prov_code = @UserCode and IPID = @IPID and RDI_CODE = @RDI", con);
                cmdDeleteProduct.Parameters.AddWithValue("@UserCode", usercode);
                cmdDeleteProduct.Parameters.AddWithValue("@IPID", ProductID);
                cmdDeleteProduct.Parameters.AddWithValue("@RDI", RDICode);
                cmdDeleteProduct.ExecuteNonQuery();
                cmdDeleteProduct.Parameters.Clear();
                con.Close();
            }


        }


        public static DataSet GetUserPayments(string userCode)
        {
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                SqlCommand cmdGetPayments = new SqlCommand("select * from tender_types", con);
                // cmd.CommandType = CommandType.StoredProcedure;
                cmdGetPayments.Parameters.AddWithValue("@UserCode", userCode);

                SqlDataAdapter da = new SqlDataAdapter(cmdGetPayments);

                DataSet userPaymentInformation = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(userPaymentInformation);
                con.Close();

                return userPaymentInformation;
            }
        }

        public static void LogNewPayment(PaymentForm NewPayment)
        {
            try
            {
                //Create SQL connection
                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {
                    SqlCommand cmdInsertNewPayment = new SqlCommand("sp_InsertNewPayment", con);
                    cmdInsertNewPayment.CommandType = CommandType.StoredProcedure;

                    cmdInsertNewPayment.Parameters.AddWithValue("@UserCode", NewPayment.UserProvCode);
                    cmdInsertNewPayment.Parameters.AddWithValue("@TenderTypeID", NewPayment.typeID);
                    cmdInsertNewPayment.Parameters.AddWithValue("@PaymentDescription",  NewPayment.description);

                    con.Open();
                    cmdInsertNewPayment.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = "Error in entering the Receipt Code code RDECS30. ";
                errorMessage += ex.Message;
                throw new Exception(errorMessage);
            }
        }


        internal void LogReceipt(Receipt Receipt)
        {

            try
            {

                //Create SQL connection
                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {

                    //Create SQL Command 
                    SqlCommand cmdInsertReceipt = new SqlCommand(ReceiptConstants.InsertReceiptStatementReceipt, con);
                    SqlCommand cmdInsertProduct = new SqlCommand(ReceiptConstants.InsertProductOnReceipt, con);
                    cmdInsertReceipt.CommandType = CommandType.StoredProcedure;
                    cmdInsertProduct.CommandType = CommandType.StoredProcedure;

                    //Parameters for Receipt/Product Page
                    cmdInsertReceipt.Parameters.AddWithValue("@RDI", Receipt.RDIcode);
                    cmdInsertReceipt.Parameters.AddWithValue("@URCI", Receipt.URCI);
                    cmdInsertReceipt.Parameters.AddWithValue("@USERCODE", Receipt.userProviderKey);
                    cmdInsertReceipt.Parameters.AddWithValue("@CATEGORYCODE", Receipt.CategoryType);
                    cmdInsertReceipt.Parameters.AddWithValue("@VENUENAME", Receipt.venueName);
                    cmdInsertReceipt.Parameters.AddWithValue("@AMOUNTSPENT", Receipt.totalAmountOfPurchase);
                    cmdInsertReceipt.Parameters.AddWithValue("@TAXAMOUNT", Receipt.totalTaxOfTransaction);
                    cmdInsertReceipt.Parameters.AddWithValue("@DATEOFPURCHASE", Receipt.finalDate);
                    cmdInsertReceipt.Parameters.AddWithValue("@DATEOFENTRY", Receipt.dateReceiptEntered);
                    cmdInsertReceipt.Parameters.AddWithValue("@DESCRIPTIONINFO", Receipt.descriptionOfPurchase);
                    cmdInsertReceipt.Parameters.AddWithValue("@PRODUCTCOUNT", Receipt.productCount);
                    cmdInsertReceipt.Parameters.AddWithValue("@PTID", Receipt.PTID);
                    cmdInsertReceipt.Parameters.AddWithValue("@RECEIPTBARCODE", Receipt.BarcodeOfReceipt);
                    cmdInsertReceipt.Parameters.AddWithValue("@IMAGEURL", Receipt.ReceiptImageURLfile);

                    //Parameters for Item(s) on the Receipt Page

                    if (Receipt.CollectionOfItems.Count > 0)
                    {
                        con.Open();
                        foreach (ReceiptItemProduct Product in Receipt.CollectionOfItems)
                        {
                            cmdInsertProduct.Parameters.AddWithValue("@RDICode", Receipt.RDIcode);
                            cmdInsertProduct.Parameters.AddWithValue("@RCICode", Receipt.URCI);
                            cmdInsertProduct.Parameters.AddWithValue("@DATEOFRECEIPT", Receipt.finalDate);
                            cmdInsertProduct.Parameters.AddWithValue("@UserProvCode", Receipt.userProviderKey);
                            cmdInsertProduct.Parameters.AddWithValue("@ProductName", Product.RPInameOfProduct);
                            cmdInsertProduct.Parameters.AddWithValue("@ProductDescription", Product.RPIdescriptionOfItem);
                            cmdInsertProduct.Parameters.AddWithValue("@QuantityAmount", int.Parse(Product.RPIquantityAmount)); 
                            cmdInsertProduct.Parameters.AddWithValue("@UnitPrice", Product.RPIquantityPrice);
                            cmdInsertProduct.Parameters.AddWithValue("@ProductCode", Product.RPIproductCode);                         
                            cmdInsertProduct.ExecuteNonQuery();
                            cmdInsertProduct.Parameters.Clear();
                        }
                        con.Close();

                    }



                    con.Open();
                    cmdInsertReceipt.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (SqlException ex)
            {
                string errorMessage = "Error in entering the Receipt Code code RDECS60. ";
                errorMessage += ex.Message;
                throw new Exception(errorMessage);

            }
            finally
            {

            }




        }

        internal static void LogProduct(ReceiptItemProduct NewProduct, string UserCode)
        {
            
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                SqlCommand cmdInsertNewProduct = new SqlCommand("sp_InsertNewProduct", con);
                cmdInsertNewProduct.CommandType = CommandType.StoredProcedure;
                cmdInsertNewProduct.Parameters.AddWithValue("@RCI", NewProduct.URCI);
                cmdInsertNewProduct.Parameters.AddWithValue("@DATEOFRECEIPT", NewProduct.finalDate);
                cmdInsertNewProduct.Parameters.AddWithValue("@NAMEOFPRODUCT", NewProduct.RPInameOfProduct);
                cmdInsertNewProduct.Parameters.AddWithValue("@DESCRIPTION", NewProduct.RPIdescriptionOfItem);
                cmdInsertNewProduct.Parameters.AddWithValue("@QUANTITY", NewProduct.RPIquantityAmount);
                cmdInsertNewProduct.Parameters.AddWithValue("@TOTALPRICEOFPRODUCT", NewProduct.RPIquantityPrice);
                cmdInsertNewProduct.Parameters.AddWithValue("@PRODUCTCODE", NewProduct.RPIproductCode);
                cmdInsertNewProduct.Parameters.AddWithValue("@RDICODE", NewProduct.RDIcode);
                cmdInsertNewProduct.Parameters.AddWithValue("@USERPROVCODE", UserCode);


          

                try
                {
                    con.Open();
                    cmdInsertNewProduct.ExecuteNonQuery();
                    con.Close();

                }
                catch (SqlException ex)
                {
                    string errorMessage = "Error in entering insertion. Error code RDAC. ";
                    errorMessage += ex.Message;
                    throw new Exception(errorMessage);

                }
                finally
                {
                    con.Close();
                }

            }

        }

        internal static void UpdateImage(string Usercode, string RDICode, string UpdatedImageURL)
        {
            try
            {
                //Create SQL connection
                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {
                    con.Open();
                    //Create SQL Command 
                    SqlCommand cmdUpdateImage = new SqlCommand("UPDATE dbo.ReceiptPages SET [Image_Filename] = @ImageFile, [DateReceiptModified] = GETDATE() WHERE [USER_PROV_CODE] = @UserCode AND [rdiCode] = @RDIcode", con);
                    cmdUpdateImage.CommandType = CommandType.Text;


                    cmdUpdateImage.Parameters.AddWithValue("@RDIcode", RDICode);
                    cmdUpdateImage.Parameters.AddWithValue("@ImageFile", UpdatedImageURL);
                    cmdUpdateImage.Parameters.AddWithValue("@UserCode", Usercode);


                    cmdUpdateImage.ExecuteNonQuery();
                    cmdUpdateImage.Parameters.Clear();
                    con.Close();


                }
            }

            catch (Exception Ex)
            {
                string error = "Error RDACUR. " + Ex.InnerException.ToString() + Ex.Message.ToString();


            }


        }


        internal static void UpdateReceipt(string UserCode, string RDICode, Receipt Receipt)
        {
            try
            {
                //Create SQL connection
                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {
                    con.Open();
                    //Create SQL Command 
                    SqlCommand cmdUpdateReceipt = new SqlCommand("sp_UpdateReceipt", con);
                    cmdUpdateReceipt.CommandType = CommandType.StoredProcedure;


                    cmdUpdateReceipt.Parameters.AddWithValue("@RDIcode", RDICode);
                    cmdUpdateReceipt.Parameters.AddWithValue("@URCI", Receipt.URCI);
                    cmdUpdateReceipt.Parameters.AddWithValue("@UserCode", UserCode);
                    cmdUpdateReceipt.Parameters.AddWithValue("@CategoryCode", Receipt.CategoryType);
                    cmdUpdateReceipt.Parameters.AddWithValue("@VenueName", Receipt.venueName);
                    cmdUpdateReceipt.Parameters.AddWithValue("@AmountSpent", Receipt.totalAmountOfPurchase);
                    cmdUpdateReceipt.Parameters.AddWithValue("@TaxAmount", Receipt.totalTaxOfTransaction);
                    cmdUpdateReceipt.Parameters.AddWithValue("@DatePurchaseReceipt", Receipt.finalDate);
                    cmdUpdateReceipt.Parameters.AddWithValue("@Description", Receipt.descriptionOfPurchase);
                    cmdUpdateReceipt.Parameters.AddWithValue("@CountOfItems", Receipt.productCount);
                    cmdUpdateReceipt.Parameters.AddWithValue("@PaymentCode", Receipt.PaymentDescription);
                    cmdUpdateReceipt.Parameters.AddWithValue("@Barcode", Receipt.BarcodeOfReceipt);               
                  //  cmdUpdateReceipt.Parameters.AddWithValue("@ImageFile", Receipt.ReceiptImageURLfile);


                    cmdUpdateReceipt.ExecuteNonQuery();
                    cmdUpdateReceipt.Parameters.Clear();
                    con.Close();


                }
            }

            catch (Exception Ex)
            {
                string error = "Error RDACUR. " + Ex.InnerException.ToString() + Ex.Message.ToString();


            }





        }

        internal static void CreateNewBudgetPlan(UserBudgetPlan Budget)
        {
            try
            {
                //Create SQL connection
                using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
                {
                    con.Open();
                    //Create SQL Command 
                    SqlCommand cmdBudget = new SqlCommand("sp_InsertNewBudgetPlan", con);
                    cmdBudget.CommandType = CommandType.StoredProcedure;

                    cmdBudget.Parameters.AddWithValue("@UserCode", ReceiptConstants.UserProvCode);
                    cmdBudget.Parameters.AddWithValue("@DateModified", Budget.BudgetModified);
                    cmdBudget.Parameters.AddWithValue("@IncomeType", "M");
                     cmdBudget.Parameters.AddWithValue("@BudgetPlanActive", Budget.BudgetPlanActive );
                     cmdBudget.Parameters.AddWithValue("@IncomeAmount", Budget.IncomeAmount );
                     //cmdBudget.Parameters.AddWithValue("@IncomeType", Budget.IncomeType);
                     cmdBudget.Parameters.AddWithValue("@BudgetHousing", Budget.budgetHousing );
                     cmdBudget.Parameters.AddWithValue("@BudgetUtilities", Budget.budgetUtilities );
                     cmdBudget.Parameters.AddWithValue("@BudgetPhone", Budget.budgetPhone );
                     cmdBudget.Parameters.AddWithValue("@BudgetTV", Budget.budgetTV );
                     cmdBudget.Parameters.AddWithValue("@BudgetInternet", Budget.budgetInternet );
                     cmdBudget.Parameters.AddWithValue("@BudgetGroceries", Budget.budgetGroceries );
                     cmdBudget.Parameters.AddWithValue("@BudgetFood", Budget.budgetFood );
                     cmdBudget.Parameters.AddWithValue("@BudgetGas", Budget.budgetGas );
                     cmdBudget.Parameters.AddWithValue("@BudgetFamilyExpenses", Budget.budgetFamilyExpenses );
                     cmdBudget.Parameters.AddWithValue("@BudgetPersonalCare", Budget.budgetPersonalCare );
                     cmdBudget.Parameters.AddWithValue("@BudgetPets", Budget.budgetPets );
                     cmdBudget.Parameters.AddWithValue("@BudgetEntertainment", Budget.budgetEntertainment );
                     cmdBudget.Parameters.AddWithValue("@BudgetInsurance", Budget.budgetInsurance );
                     cmdBudget.Parameters.AddWithValue("@BudgetDebtRepayment", Budget.budgetDebtRepayment);
                     cmdBudget.Parameters.AddWithValue("@BudgetPropertyTax", Budget.budgetDebtRepayment);
                     cmdBudget.Parameters.AddWithValue("@BudgetEmergencyFund", Budget.budgetEmergencyFund );
                     cmdBudget.Parameters.AddWithValue("@BudgetRetirementSavings", Budget.budgetRetirementSavings);
                     cmdBudget.Parameters.AddWithValue("@BudgetCollegeSavings", Budget.budgetCollegeSavings );
                     cmdBudget.Parameters.AddWithValue("@BudgetGoal", Budget.budgetGoal);
                     cmdBudget.Parameters.AddWithValue("@BudgetGifts", Budget.budgetGifts );
                    cmdBudget.Parameters.AddWithValue("@BudgetOther", Budget.budgetOther );



                    cmdBudget.ExecuteNonQuery();
                    cmdBudget.Parameters.Clear();
                    con.Close();
                    con.Dispose();
                    cmdBudget.Dispose();


                }
            }

            catch (Exception Ex)
            {
                string error = "Error RDACUR. " + Ex.InnerException.ToString() + Ex.Message.ToString();


            }


        }


        internal static string DetermineUniqueCode(string userpcode, string VenueName)
        {
            char[] alphaCharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789YUOIHEAZXWVTSRQPNMLKJGFDCB".ToCharArray();            
            int dateSwitchExpression = DateTime.Now.Minute;
            string uniqueIdentifierSet1 = alphaCharSet[dateSwitchExpression].ToString();
            if (dateSwitchExpression > 36)
            {
                uniqueIdentifierSet1 = new StringBuilder(uniqueIdentifierSet1 + uniqueIdentifierSet1).ToString();
            }
            string uniqueCode = "DEF";
            string identifierHash = "3REP";
            string y = uniqueIdentifierSet1.ToString();

            if (dateSwitchExpression >= 1 && dateSwitchExpression <= 3)
            {
                switch (dateSwitchExpression)
                {
                    case 1:
                        StringBuilder firstCase = new StringBuilder(dateSwitchExpression.ToString() +userpcode.Substring(0,2) + VenueName.Substring(0,VenueName.Length -(VenueName.Length-2)) + y);
                        identifierHash = firstCase.ToString();
                        break;
                    case 2:
                        StringBuilder secondCase = new StringBuilder(dateSwitchExpression.ToString() + userpcode.Substring(3, 5) + VenueName.Substring(0, VenueName.Length - (VenueName.Length - 3)) + y);
                        identifierHash = secondCase.ToString();
                        break;
                    case 3:
                        StringBuilder thirdCase = new StringBuilder(dateSwitchExpression.ToString() + userpcode.Substring(5, 7) + VenueName.Substring(0, VenueName.Length - (VenueName.Length - 1)) + y);
                        identifierHash = thirdCase.ToString();
                        break;
                }


                uniqueCode = "PA";
                identifierHash = uniqueCode + identifierHash;
            }
            else
            {
                if (dateSwitchExpression >= 4 && dateSwitchExpression <= 6)
                {
                    switch (dateSwitchExpression)
                    {
                        case 1:
                            StringBuilder firstCase = new StringBuilder(dateSwitchExpression.ToString() + userpcode.Substring(30, 33) + VenueName.Substring(0, VenueName.Length - (VenueName.Length - 2)) + y);
                            identifierHash = firstCase.ToString();
                            break;
                        case 2:
                            StringBuilder secondCase = new StringBuilder(dateSwitchExpression.ToString() + userpcode.Substring(32, 34) + VenueName.Substring(0, VenueName.Length - (VenueName.Length - 3)) + y);
                            identifierHash = secondCase.ToString();
                            break;
                        case 3:
                            StringBuilder thirdCase = new StringBuilder(dateSwitchExpression.ToString() + userpcode.Substring(31, 35) + VenueName.Substring(0, VenueName.Length - (VenueName.Length - 1)) + y);
                            identifierHash = thirdCase.ToString();
                            break;
                    }

                }
                else
                {
                    if (dateSwitchExpression >= 7 && dateSwitchExpression <= 9)
                    {
                        switch (dateSwitchExpression)
                        {
                            case 1:
                                StringBuilder firstCase = new StringBuilder(dateSwitchExpression.ToString() + userpcode.Substring(28, 31) + VenueName.Substring(0, VenueName.Length - (VenueName.Length - 2)) + y);
                                identifierHash = firstCase.ToString();
                                break;
                            case 2:
                                StringBuilder secondCase = new StringBuilder(dateSwitchExpression.ToString() + userpcode.Substring(29, 33) + VenueName.Substring(0, VenueName.Length - (VenueName.Length - 3)) + y);
                                identifierHash = secondCase.ToString();
                                break;
                            case 3:
                                StringBuilder thirdCase = new StringBuilder(dateSwitchExpression.ToString() + userpcode.Substring(27, 34) + VenueName.Substring(0, VenueName.Length - (VenueName.Length - 1))+ y);
                                identifierHash = thirdCase.ToString();
                                break;
                        }

                    }
                    else
                    {
                        identifierHash = VenueName + identifierHash + dateSwitchExpression.ToString();
                    }
                }
            }

            return identifierHash;
        }


        public static DataSet LoadReceiptProducts(string userCode, string RDIcode)
        {
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmdSelectProducts = new SqlCommand("select * from itemized_products where user_prov_code = @UserCode and Rdi_Code = @RDI", con);
             //   cmd.CommandType = CommandType.StoredProcedure;
                cmdSelectProducts.Parameters.AddWithValue("@UserCode", userCode);
                cmdSelectProducts.Parameters.AddWithValue("@RDI", RDIcode);
                //   SqlDataReader dr = cmd.ExecuteReader();
                //ReceiptTableGridview.DataSource = dr;
                //  OnSorting="ReceiptTableGridviewSorting"
                SqlDataAdapter da = new SqlDataAdapter(cmdSelectProducts);

                DataSet ProductsViewDatasetInformation = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ProductsViewDatasetInformation);
                con.Close();

                return ProductsViewDatasetInformation;

            }


        }

        /// <summary>
        /// Method to Retrieve the amount of spending by category between the dates supplied
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="beginMonth"></param>
        /// <param name="endMonth"></param>
        /// <returns></returns>
        public static DataTable GetCategorySpendingByMonth(string userCode, DateTime beginMonth, DateTime endMonth)
        {
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetCategorySpendingByMonth", con); 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserCode", userCode);
                cmd.Parameters.AddWithValue("@BeginMonth", beginMonth.ToShortDateString());
                cmd.Parameters.AddWithValue("@EndMonth", endMonth.ToShortDateString());



                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable userCategoryDatasetInformation = new DataTable();
                da.Fill(userCategoryDatasetInformation);
                con.Close();

                con.Dispose();
                cmd.Dispose();
                return userCategoryDatasetInformation;

            }
        }

        public static UserBudgetPlan GetBudgetUsageForMonth(string userCode, DateTime beginMonth, DateTime endMonth)
        {
            UserBudgetPlan BudgetUser = new UserBudgetPlan();
            DataTable BudgetUsageInformation = new DataTable();
            using (SqlConnection con = new SqlConnection(ReceiptConstants.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetBudgetUsageForMonth", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserCode", userCode);
                cmd.Parameters.AddWithValue("@BeginMonth", beginMonth.ToShortDateString());
                cmd.Parameters.AddWithValue("@EndMonth", endMonth.ToShortDateString());

                SqlDataAdapter BudgetDA = new SqlDataAdapter(cmd);


                BudgetDA.Fill(BudgetUsageInformation);
                con.Close();

                con.Dispose();
                cmd.Dispose();
                BudgetDA.Dispose();
            
             if (BudgetUsageInformation.Rows.Count == 0)
                {
                    BudgetUsageInformation.Dispose();
                    return null;
                }
            }

            DataRow BudgetUseRow = BudgetUsageInformation.Rows[0];

            foreach (DataRow dr in BudgetUsageInformation.Rows)
            {
               // dr.ItemArray.GetValue(0

            }
            DataColumn BudgetUseColumn = BudgetUsageInformation.Columns["CategoryName"];
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow["AmountSpent"].ToString());
            BudgetUser.budgetFood = decimal.Parse(BudgetUseRow["Fast Food"].ToString()) + decimal.Parse(BudgetUseRow["Diner Service"].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());
            BudgetUser.budgetGroceries = decimal.Parse(BudgetUseRow[""].ToString());




            //U.PlanId = int.Parse(BudgetRow["BudgetPlanId"].ToString());   

            return BudgetUser;
        }


    }
}
    