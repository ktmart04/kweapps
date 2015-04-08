<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageReceipt.aspx.cs" MasterPageFile="~/Site.Master" Inherits="FinanceReporting.ManageReceipt" %>

<%@ Register Src="~/UserControls/ReceiptInformation.ascx" TagPrefix="Rcpt" TagName="DetailedReceipt" %>

<asp:Content runat="server" ID="ReceiptPlaceHolder" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        #MainContent_overlay
        {
            position: absolute;
            border: 5px solid lightblue;
            padding: 10px;
            background: white;
            width: 700px;
            height: 800px;
            z-index: 200;
        }

        #MainContent_popKwe
        {
            display: block;
            border: 1px solid gray;
            width: 65px;
            text-align: center;
            padding: 6px;
            border-radius: 5px;
            text-decoration: none;
            margin: 0 auto;
        }

        #MainContent_background_overlay
        {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 1;
            background: black;
            opacity: 0.5;
        }

        body
        {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        .modal
        {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
        }

        #divImage
        {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 550px;
            width: 600px;
            padding: 3px;
            border: solid 1px black;
        }


        .error
        {
            color: red;
            font-size: 12px;
            font-style: italic;
        }
    </style>

    <script type="text/javascript">


        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to delete this Receipt along \r\n with any products attached to it?");
        }



        function LoadDiv(url) {
            var img = new Image();
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            var imgLoader = document.getElementById("imgLoader");
            imgLoader.style.display = "block";

            img.onload = function () {

                imgFull.src = img.src;
                imgFull.style.display = "block";
                imgLoader.style.display = "none";

            };

            img.src = url;
            var width = document.body.clientWidth;

            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";

            }

            else {

                bcgDiv.style.height = document.body.scrollHeight + "px";

            }

            imgDiv.style.left = (width - 650) / 2 + "px";
            imgDiv.style.top = "20px";
            bcgDiv.style.width = "100%";
            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }

        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }

        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            //open popup
            $("#MainContent_popKwe").click(function () {
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

//////////////////////////////////////////////////////////////////////////////////////////

            $('#MainForm').validate({ // initialize the plugin
                rules: {
                    ctl00$MainContent$UpdateBoxVenue: {
                        required: true,

                    },
                    ctl00$MainContent$UpdateBoxDate: {
                        required: true,
                        date: true
                    },
                    ctl00$MainContent$UpdateBoxAmount: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$UpdateBoxURCI: {
                        required: true,
                    },
                    ctl00$MainContent$UpdateBoxDescription: {
                        required: true,
                    },
                    ctl00$MainContent$UpdateBoxTax: {
                        required: true,
                        number: true
                    },
                    ctl00$MainContent$UpdateBoxProductCount: {
                        required: true,
                        digits: true
                    },

                },

                messages: {

                    ctl00$MainContent$UpdateBoxVenue: {
                        required: "Please enter the name of the Retail Venue"
                    },

                    ctl00$MainContent$UpdateBoxDate: {
                        required: "Please enter the date of the Receipt",
                        date: "Date must be in mm/dd/yyyy format \r\n or mm/dd/yyyy hh:mm tt"

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


    <div runat="server" id="ReceiptDataTableResponsive" class="table-responsive">
        <asp:Table runat="server" ID="ReceiptTable" CssClass="table table-hover table-bordered">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell ID="TitleVenue"></asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>Date of Receipt:</asp:TableCell>
                <asp:TableCell ID="cellDateOfReceipt"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Total Amount Spent:</asp:TableCell>
                <asp:TableCell ID="cellAmountSpent"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Receipt Identifying Code:</asp:TableCell>
                <asp:TableCell ID="cellURCI"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Category:</asp:TableCell>
                <asp:TableCell ID="cellCategory"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Description:</asp:TableCell>
                <asp:TableCell ID="cellDescription"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Tax of the Transaction:</asp:TableCell>
                <asp:TableCell ID="celltaxOfTheTransacction"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Payment</asp:TableCell>
                <asp:TableCell ID="cellPayment"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Receipt Barcode</asp:TableCell>
                <asp:TableCell ID="cellReceiptBarcode"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Number of Products on Receipt:</asp:TableCell>
                <asp:TableCell ID="cellProductCount"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Last Date Modified:</asp:TableCell>
                <asp:TableCell ID="cellDateModified"></asp:TableCell>
            </asp:TableRow>


        </asp:Table>
    </div>
    <br />

    <asp:GridView runat="server" ID="ReceiptItems">
    </asp:GridView>

    <asp:Label ID="LabelProductItems" runat="server">Product(s) on this Receipt:</asp:Label>
    <div runat="server" id="LineItemsVisibility">
        <div id="TableResponsiveReceiptItems" runat="server" class="table-responsive">
            <asp:Table runat="server" ID="ReceiptItemsTable" CssClass="table table-bordered table-hover">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Product Name</asp:TableHeaderCell><asp:TableHeaderCell>Description</asp:TableHeaderCell><asp:TableHeaderCell>Quantity</asp:TableHeaderCell><asp:TableHeaderCell>Unit Price</asp:TableHeaderCell><asp:TableHeaderCell>Product Code</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </div>


    <asp:Label runat="server" ID="LineItemsProducts" Text="No Products Logged On This Receipt" />

    <br />
    <br />

    <asp:ImageButton runat="server" ID="ReceiptImagePath" Height="150" Width="150" OnClientClick="return LoadDiv(this.src);" />
    <br />

    <asp:TableRow>
        <asp:TableCell>
            <asp:Button runat="server" ID="popKwe" Text="Update" OnClientClick="return false" />
            <asp:Button runat="server" ID="buttonPopUpUpdate" Text="Update" OnClientClick="return false" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:Button runat="server" Text="Delete" CssClass="btn btn-warning btn-lg" ID="btnConfirm" OnClick="OnConfirm" OnClientClick="if(!UserDeleteConfirmation()) return false;" />
        </asp:TableCell>
    </asp:TableRow>

    <div runat="server" id="background_overlay" style="display: none"></div>
    <div runat="server" id="overlay" style="display: none">
        <h2>Update Receipt:
            <asp:Label runat="server" ID="URCIlabelUpdate"></asp:Label>
        </h2>
        <br />
        <asp:Table runat="server" ID="updateTable" CssClass="table table-bordered">

            <asp:TableRow>
                <asp:TableCell>Venue Name:</asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="UpdateBoxVenue"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Date:</asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="UpdateBoxDate"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Total Amount Spent:</asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="UpdateBoxAmount"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>URCI:</asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="UpdateBoxURCI"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Category:</asp:TableCell><asp:TableCell>
                    <asp:DropDownList runat="server" ID="UpdateComboBoxCategory" DataTextField="Category_Name" DataValueField="Category_ID">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Description:</asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="UpdateBoxDescription"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Count of Products:</asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="UpdateBoxProductCount"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Tax Charged:</asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="UpdateBoxTax"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Payment:</asp:TableCell><asp:TableCell>
                    <asp:DropDownList runat="server" ID="UpdateComboBoxPayment" DataTextField="DESCRIPTION_OF_PAYMENT" DataValueField="PT_ID">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Barcode:</asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="UpdateBoxBarcode"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:Label runat="server" ID="labelUpdateImage" Text="Update Image?" />

        <br />
        <div runat="server" id="uploadUpdate">
            <asp:FileUpload ID="ReceiptImageFileUpload" runat="server" />

        </div>
        <br />
        <asp:LinkButton runat="server" ID="editProductsLink" OnClick="editProductsLink_Click">Edit Products to this Receipt?</asp:LinkButton>
        <br />
        <br />

        <asp:Button runat="server" ID="close" OnClientClick="return false" Text="Cancel" />
        <asp:Button runat="server" ID="updateReceipt" Text="Update Receipt" OnClick="updateReceipt_Click" />

        <br />
    </div>

    <br />


    <br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false">

        <Columns>

            <asp:BoundField DataField="Text" />

            <asp:ImageField DataImageUrlField="Value" ControlStyle-Height="100" ControlStyle-Width="100" />

        </Columns>

    </asp:GridView>




    <div id="divBackground" class="modal">
    </div>

    <div id="divImage">

        <table style="height: 100%; width: 100%">

            <tr>

                <td valign="middle" align="center">

                    <img id="imgLoader" alt="" src="images/loader.gif" />

                    <img id="imgFull" alt="Receipt Image" src="null" style="display: none; height: 500px; width: 590px" />

                </td>

            </tr>

            <tr>

                <td align="center" valign="bottom">

                    <input id="btnClose" type="button" value="close" onclick="HideDiv()" />

                </td>

            </tr>

        </table>

    </div>




</asp:Content>
