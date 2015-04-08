<%@ Page Language="C#" Title="Product Entries" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewProductEntry.aspx.cs" Inherits="FinanceReporting.NewProductEntry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #MainContent_overlay {
            position: absolute;
            border: 5px solid lightblue;
            padding: 10px;
            background: white;
            width: 700px;
            height: 480px;
            z-index: 200;
        }


        #MainContent_background_overlay {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 1;
            background: black;
            opacity: 0.5;
        }

        .error {
            color: red;
            font-size: 12px;
            font-style: italic;
        }
    </style>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.13.1/jquery.validate.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.13.1/jquery.validate.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.13.1/additional-methods.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.13.1/additional-methods.min.js"></script>
    <script type="text/javascript">

        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to delete this Product from this Receipt?");
        }

        $(document).ready(function () {
            $("#updateProductButton").click(function () {
                alert('It was clicked');
            });
        });


        $(document).ready(function () {
            //open popup
            $("#MainContent_AddProductLabel").click(function () {
                $("#MainContent_overlay").fadeIn(1000);
                $("#MainContent_example").hide(1000);
                $("#MainContent_background_overlay").fadeIn(500);
                positionPopup();
            });

            //close popup
            $("#MainContent_close, #MainContent_background_overlay").click(function () {
                $("#MainContent_overlay").fadeOut(500);
                $("#MainContent_background_overlay").fadeOut(500);
            });
        });

        //position the popup at the center of the page
        function positionPopup() {
            if (!$("#MainContent_overlay").is(':visible')) {
                return;
            }
            $("#MainContent_overlay").css({
                left: ($(window).width() - $('#MainContent_overlay').width()) / 2,
                top: ($(window).width() - $('#MainContent_overlay').width()) / 7,
                position: 'absolute'
            });
        }

        //maintain the popup at center of the page when browser resized
        $(window).bind('resize', positionPopup);


    </script>


    <h2><%: Title %></h2>

    <h3>Enter each product by filling out the information Below</h3>

    <div id="form1">
        <table>
            <tr>
                <td>
                    <asp:Label runat="server">Enter the date of the receipt the <br /> products were purchased on:</asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ValueSpecifiedDate"></asp:TextBox>
                    <br />
                    <ajaxToolKit:CalendarExtender ID="ValueSpecifiedDate_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="ValueSpecifiedDate">
                    </ajaxToolKit:CalendarExtender>
                </td>
                <td>
                    <asp:Button ID="submit" CssClass="btn btn-primary btn-large" Text="Submit" runat="server" OnClick="submit_Click" />
                </td>
            </tr>
        </table>

        <asp:Label runat="server" ID="labelNoReceipts" Visible="false">There are no receipts for this date</asp:Label>
        <div runat="server" id="ShowRCIchoice">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server">Receipt (identified by the URCI:)</asp:Label>
                    </td>

                    <td>
                        <asp:DropDownList runat="server" ID="ValueURCI"></asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server">Name of the Product:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="ValueProductName"></asp:TextBox>
                    </td>
                    <tr>
                        <td>
                            <asp:Label runat="server">Description:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="ValueDescription"></asp:TextBox>
                        </td>
                        <tr>
                            <td>
                                <asp:Label runat="server">Quantity Amount:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ValueQuantityAmount"></asp:TextBox>
                            </td>
                            <tr>
                                <td>
                                    <asp:Label runat="server">Total Price of Product(s):</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ValueTotalPriceOfProductss"></asp:TextBox>
                                </td>
                            </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="Button1" CssClass="btn btn-primary btn-large" Text="Submit Product" runat="server" OnClick="submitFinal_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <asp:Label runat="server" ID="listofEditProducts" Text="Products on this Receipt:" />
    <br />


    <div id="Div1" runat="server" class="table-responsive">
        <asp:GridView ID="ProductsGridview" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="IPID" OnPageIndexChanging="ProductsGridview_PageIndexChanging" OnRowCancelingEdit="ProductsGridview_RowCancelingEdit" OnRowCommand="ProductsGridview_RowCommand" OnRowDeleting="ProductsGridview_RowDeleting" OnRowEditing="ProductsGridview_RowEditing" OnRowUpdating="ProductsGridview_RowUpdating">

            <Columns>

                <asp:BoundField DataField="Name_Of_Product" HeaderText="Product Name" />

                <asp:BoundField DataField="Description" HeaderText="Description" />

                <asp:BoundField DataField="Quantity" HeaderText="Quantity Amount" DataFormatString="{0}" ItemStyle-Width="10%" />

                <asp:BoundField DataField="TOTAL_PRICE_OF_PRODUCT" HeaderText="Unit Price" DataFormatString="{0:C2}" />

                <asp:BoundField DataField="PRODUCT_CODE" HeaderText="Product Code" />

                <asp:CommandField ShowEditButton="true"  />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="deleteItem" Text="Delete Product" CommandName="DELETEPRODUCT" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "IPID") %>' OnClientClick="if(!UserDeleteConfirmation()) return false;" />
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>

        </asp:GridView>



    </div>

    <br />
    <asp:LinkButton runat="server" ID="AddProductLabel" Text="Add New Product to this Receipt?" OnClientClick="return false" />
    <script type="text/javascript">
        $(document).ready(function () {


            $('#MainForm').validate({ // initialize the plugin
                rules: {
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

    <br />

    <div runat="server" id="background_overlay" style="display: none"></div>
    <div runat="server" id="overlay" style="display: none">
        <h2>Add Product to Receipt:
            <asp:Label runat="server" ID="URCIlabelUpdate"></asp:Label>
        </h2>
        <br />
        <asp:Table runat="server" ID="addProductTable" CssClass="table table-bordered">
            <asp:TableRow>
                <asp:TableCell>Product Name: </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWname" runat="server" /></asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Description: </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWdesc" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Quantity Amount: </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWquan" runat="server" MaxLength="7" Width="200" /></asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Unit Price: </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWunit" runat="server" MaxLength="7" Width="200"></asp:TextBox></asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Product Code: </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="TextBoxTNEWpcode" runat="server" Width="150"></asp:TextBox></asp:TableCell></asp:TableRow>
        </asp:Table>
        <br />

        <asp:Button runat="server" ID="updateReceipt" Text="Add Product" OnClick="updateReceipt_Click" />
        <asp:Button runat="server" ID="close" OnClientClick="return false" Text="Cancel" />



    </div>


</asp:Content>
