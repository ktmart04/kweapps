<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BudgetPlan.aspx.cs" Inherits="FinanceReporting.BudgetPlan" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .error
        {
            color: red;
            font-size: 12px;
            font-style: italic;
        }
    </style>



    <script type="text/javascript">

        $(document).ready(function () {


            $('#MainForm').validate({ // initialize the plugin
                rules: {
                    ctl00$MainContent$BoxWagesIncome: {
                        required: true,
                        number: true

                    },
                    ctl00$MainContent$BoxOtherIncome: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseHousing: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseUtilities: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpensePhone: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseTV: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseInternet: {
                        required: true,
                        number: true
                    },

                    ctl00$MainContent$BoxExpenseGroceries: {
                        required: true,
                        number: true

                    },
                    ctl00$MainContent$BoxExpenseFood: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseGas: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseFamily: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseHygiene: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpensePets: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseEntertainment: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseInsurance: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseDebtRepayment: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpensePropertyTax: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseEmergencyFund: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseRetirement: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseCollegeSavings: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseGoals: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseGifts: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$BoxExpenseOther: {
                        required: true,
                        number: true
                    },

                },

            });

        });


    </script>
    <script>
        function costController($scope) {
           // $scope.quantity = 1;
            // $scope.price = 9.99;
            $scope.getTotal = function () {
                var total = 0;
                //for (var i = 0; i < $scope.cart.products.length; i++) {
                  //  var product = $scope.cart.products[i];
                    total += price + price2;
                }
                return total;
            }
     



           
</script>



    <div ng-app="" ng-controller="costController">

    <asp:Table runat="server" HorizontalAlign="Center" BorderWidth="0" CssClass="budcalc">
        <asp:TableRow>

            <asp:TableHeaderCell ColumnSpan="2">
          <h3 style="text-align:center">Monthly Budget Calculator</h3><br />
                <h5>Here, set the budget in which you want to set on a monthly basis. (Do not include the $ symbol)</h5>
               
            </asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell ColumnSpan="2"><h3 style="text-align:center">Income</h3></asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Salary/Wages:</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxWagesIncome" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Other:</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxOtherIncome" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <%--<asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Frequency of the Listed Income:</asp:TableCell><asp:TableCell><asp:DropDownList runat="server" ID="IncomeFrequency">
                <asp:ListItem Value="W">Weekly</asp:ListItem>
                <asp:ListItem Value="B">Bi-Weekly</asp:ListItem>
                <asp:ListItem Value="M" Selected="True">Monthly</asp:ListItem>
                <asp:ListItem Value="A">Anually</asp:ListItem>
                </asp:DropDownList></asp:TableCell>
        </asp:TableRow>--%>
        <asp:TableRow>
            <asp:TableCell><br /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell ColumnSpan="2" HorizontalAlign="Center"><h3 style="text-align:center">Expenses</h3></asp:TableHeaderCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Housing (mortgage/rent):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseHousing" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Utilities (gas/electric/water/garbage):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseUtilities" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Phone (landline and cell phones):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpensePhone" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">TV (cable or satellite/movie subscriptions):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseTV" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Internet:</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseInternet" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Groceries:</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseGroceries" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Food (dining, fast food, etc.):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseFood" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Gas:</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseGas" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Family Expenses (day care/tuition/activities/child support/alimony):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseFamily" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Personal Care (hair cuts/toiletries/clothing):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseHygiene" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Pets (food/vet/grooming):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpensePets" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Entertainment (books and mags/movies/hobbies):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseEntertainment" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Insurance (car/home/life/disability/health/dental):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseInsurance" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Debt Repayment (credit cards/car loans/home equity/student loans):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseDebtRepayment" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Property Tax (if not included in mortgage):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpensePropertyTax" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Emergency Fund:</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseEmergencyFund" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Retirement Savings:</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseRetirement" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">College Savings:</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseCollegeSavings" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Savings Towards a Goal (buy a house or car/go on vacation):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseGoals" MaxLength="10"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Gifts/Charities Donations (including tithes):</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseGifts" MaxLength="10"  ng-model="price2"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Other:</asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="BoxExpenseOther" MaxLength="10"  ng-model="price"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> </asp:TableCell>

            <asp:TableCell>
                <asp:Button runat="server" ID="buttonSubmitBudget" Text="Set Your Budget" CssClass="btn btn-lg btn-success" OnClick="buttonSubmitBudget_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right"><b>Total:</b><p>Total = {{ getTotal() }}</p></asp:TableCell>

            <asp:TableCell HorizontalAlign="Left">
                <asp:TextBox runat="server" ID="res"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
        </div>













</asp:Content>
