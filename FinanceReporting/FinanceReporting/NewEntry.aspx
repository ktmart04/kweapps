<%@ Page Title="New Receipt Entry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewEntry.aspx.cs" Inherits="FinanceReporting.NewEntry" %>
<%@ Register TagPrefix="AjaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
        
        var pop = $('#MainContent_LinkToShowProducts3');
        var pop2 = $('#MainContent_DivReceiptProductItems');


        $(document).ready(function () {
            $(pop).click(function () {
                $(pop2).toggle();
            });
        });


        var pop3 = $('#LinkToShowProducts3');
        var pop4 = $('#DivReceiptProductItems');


        $(document).ready(function () {
            $(pop3).click(function () {
                $(pop4).toggle();
            });
        });





        $(document).ready(function () {


            $('#MainForm').validate({ // initialize the plugin
                rules: {
                    ctl00$MainContent$ValueVenue: {
                        required: true,

                    },
                    ctl00$MainContent$ValueDOP: {
                        required: true,
                        date: true
                    },
                    ctl00$MainContent$ValueTOP: {
                        required: true,
                        time12h: true
                    },
                    ctl00$MainContent$ValueTaxes: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$ValueTransactionAmount: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$ValueProductCount: {
                        required: true,
                        digits: true
                    },
                    ctl00$MainContent$ValueDescription: {
                        required: true,
                    },

                    ctl00$MainContent$TextBoxTNEWname: {
                        required: true,

                    },
                    ctl00$MainContent$TextBoxTNEWdesc: {
                        required: true,
                    },
                    ctl00$MainContent$TextBoxTNEWquan: {
                        required: true,
                        digits: true
                    },
                    ctl00$MainContent$TextBoxTNEWunit: {
                        required: true,
                        number: true
                    },

                },

                messages: {

                    ctl00$MainContent$ValueVenue: {
                        required: "Please enter the name of the Retail Venue"
                    },

                    ctl00$MainContent$ValueDOP: {
                        required: "Please enter the date of the Receipt",
                        dateITA: "Date must be in mm/dd/yyyy format"

                    },
                    ctl00$MainContent$ValueTOP: {
                        required: "Please enter the time of the receipt purchase",
                        time12h: "Time format must be hh:mm tt"
                    },
                    ctl00$MainContent$ValueTaxes: {
                        required: "Please enter the total of tax charged on the receipt",
                        number: "Please use numbers only. Do not include the '$'"
                    },
                    ctl00$MainContent$ValueTransactionAmount: {
                        required: "Please enter the total amount of the transaction on the receipt",
                        number: "Please use numbers only. Do not include the '$'"
                    },
                    ctl00$MainContent$ValueProductCount: {
                        required: "Please enter the amount of product purchased on this receipt",
                        digits: "Please only enter whole numbers"
                    },
                    ctl00$MainContent$ValueDescription: {
                        required: "Please enter a breif description of the purchase.(For, why it was purchased,etc..)"
                    },

                    ctl00$MainContent$TextBoxTNEWname: {
                        required: "Please enter the name of the Product"
                    },

                    ctl00$MainContent$TextBoxTNEWdesc: {
                        required: "Please enter a minor description of the product"
                    },
                    ctl00$MainContent$TextBoxTNEWquan: {
                        required: "Please enter the quantity amount of this product of the receipt purchase",
                        digits: "Please only enter whole numbers"
                    },
                    ctl00$MainContent$TextBoxTNEWunit: {
                        required: "Please enter the total product price",
                        number: "Please use numbers only. Do not include the '$'"
                    },
                }
            });

        });


    </script>
    <style type="text/css">
        .error {
            color: red;
            font-size: 12px;
            font-style: italic;
        }
    </style>

    <h2><%: Title %></h2>

    <h3>Enter your Receipt Information</h3>
    <div id="form1" class="table-responsive">
        <table id="NewReceiptEntryTable" class="table table-condensed">
            <tr>
                <td>
                    <asp:Label runat="server">Name of Venue:</asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ValueVenue" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Date of Transaction:</asp:Label></td>

                <td>

                    <asp:TextBox ID="ValueDOP" runat="server"></asp:TextBox>
                 <%--   <ajaxToolKit:CalendarExtender ID="ValueDOP_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="ValueDOP">
                    </ajaxToolKit:CalendarExtender> --%> 
                    <asp:CompareValidator ID="datecheck" runat="server" ControlToValidate="ValueDOP" ErrorMessage="Error in date" Text="Error in Date" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>



                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label runat="server">Time of Transaction:</asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="ValueTOP" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Tax Amount of the Transaction:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ValueTaxes" runat="server" MaxLength="8"></asp:TextBox>
                </td>


            </tr>

            <tr>
                <td>
                    <asp:Label runat="server">Total Transaction Amount:</asp:Label></td>

                <td>
                    <asp:TextBox runat="server" ID="ValueTransactionAmount" MaxLength="12"></asp:TextBox></td>

            </tr>

            <tr>
                <td>
                    <asp:Label runat="server">Unique Receipt Code Identifier (URCI)</asp:Label></td>

                <td>
                    <asp:TextBox runat="server" ID="ValueURCI"></asp:TextBox><br />
                    <asp:Label ID="URCIGenerator" runat="server">Don't Have One? Just fill out the rest <br /> and one will be made for this receipt</asp:Label></td>

            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Total Count of Products/Items:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ValueProductCount" runat="server" MaxLength="5"></asp:TextBox>
                </td>


            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server">Receipt Barcode:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ValueBarcode" runat="server" MaxLength="40"></asp:TextBox>
                </td>


            </tr>

            <tr>
                <td>
                    <asp:Label runat="server">Category of Purchase:</asp:Label></td>

                <td>
                    <asp:DropDownList runat="server" ID="ValueSelectedCategory" DataSourceID="SelectCategories" DataTextField="Category_Name" DataValueField="Category_ID">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>

                    <asp:SqlDataSource runat="server" ID="SelectCategories" ConnectionString='<%$ ConnectionStrings:BB_ContactInfoConnectionString %>' SelectCommand="SELECT [Category_ID], [Category_Name] FROM [ZIB_CATEGORY]"></asp:SqlDataSource>
            </tr>

            <tr>
                <td>
                    <asp:Label runat="server" ID="descTextBox">Description of Purchase:</asp:Label></td>

                <td>
                    <asp:TextBox runat="server" ID="ValueDescription" ></asp:TextBox></td>

            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Payment Type ID:</asp:Label></td>

                <td>
                    <asp:DropDownList runat="server" ID="ValuePTID" DataTextField="DESCRIPTION_OF_PAYMENT" DataValueField="PT_ID">
                    </asp:DropDownList>
                    <br />
                    <a runat="server" href="NewPayment.aspx">
                        <asp:Label ID="Label1" CssClass="label label-warning" runat="server">Add New Type of Payment?</asp:Label></a>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="FileUploadLoad" runat="server">Have a picture or file of the receipt? Upload Here:</asp:Label><br />                    
                </td>
                <td>
                    <asp:FileUpload ID="ReceiptImageFileUpload" runat="server" />
                    </td>
            </tr>
        </table>

        <br />



        <asp:Button ID="submitButton" CssClass="btn btn-success btn-lg" Text="Submit" runat="server" OnClick="submit_Click" />
    </div>

    <br />
    <asp:LinkButton runat="server" OnClientClick="return false" ID="LinkToShowProducts3" Text="Enter each Product on Receipt? (Optional)" />


    <div runat="server" id="DivReceiptProductItems" style="display:none">
        <asp:Table runat="server" ID="tableNewEntryNewProduct">
            <asp:TableRow CssClass="warning">
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWname" runat="server" Text="Product Name"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWdesc" runat="server" Text="Description"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWquan" runat="server" MaxLength="7" Width="200" Text="Quantity Amount"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWunit" runat="server" MaxLength="7" Width="200" Text="Unit Price"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWpcode" runat="server" Width="150" Text="Product Code"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button runat="server" ID="productSubmit" Text="Submit Product" OnClick="productSubmit_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />

        <div runat="server" id="ListBoxDiv">
            <p style="font-style: italic;" runat="server">Double Click to Remove for the current Receipt</p>
            <asp:ListBox runat="server" ID="listBoxReceiptProducts" BackColor="#99ccff" Font-Bold="true"></asp:ListBox>

        </div>
    </div>



    <div class="alert alert-warning alert-dismissible" role="alert">
  <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
  <strong>Warning!</strong> Better check yourself, you're not looking too good.
</div>

</asp:Content>
