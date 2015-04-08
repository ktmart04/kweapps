<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPayment.aspx.cs" Inherits="FinanceReporting.NewPayment" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">

        $(document).ready(function () {
            $("#MainContent_showAddPaymentButton").click(function () {
                $("#MainContent_newOption").toggle(300);
            });
        });


        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to delete this Payment type?");
        }


    </script>
    <h1>Payment Types Listed:</h1>
    <br />

    <asp:Table runat="server" ID="currentPayments">
    </asp:Table>
    <div id="Div1" runat="server" class="table-responsive">
        <asp:GridView runat="server" ID="ownedPaymentsGridview" AutoGenerateColumns="false" CssClass="table table-bordered"  OnRowCommand="ownedPaymentsGridview_RowCommand">
            <Columns>
                <asp:BoundField DataField="Description of Payment" HeaderText="Description of Payment" />
                <asp:BoundField DataField="Type of Tender" HeaderText="Type of Tender" />                  
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="deleteItem" Text="Delete Payment" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "PT_ID") %>' OnClientClick="if(!UserDeleteConfirmation()) return false;" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <br />

    <asp:LinkButton runat="server" ID="showAddPaymentButton" Text="New Payment?" OnClientClick="return false;" />

    <div runat="server" id="newOption" style="display:none;">
    <asp:Table runat="server" ID="paymentTable" CssClass="table table-bordered">
         <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server">Description of Payment:</asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="BoxDescriptionOfPymt" runat="server" required></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label1" runat="server">Type of Payment:</asp:Label></asp:TableCell><asp:TableCell>
                    <asp:DropDownList runat="server" ID="BoxValuePTID" DataTextField="TENDER_NAME" DataValueField="TENDER_CODE">
                    </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
      

    </asp:Table>
     

    <br />
    <asp:Button runat="server" ID="submitPaymentButton" OnClick="submitPaymentButton_Click" Text="Submit Payment" />

           </div>
</asp:Content>

