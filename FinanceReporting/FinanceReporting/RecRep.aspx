<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecRep.aspx.cs" Inherits="FinanceReporting.RecRep" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    
    <style type="text/css">

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
</style>


    <script type="text/javascript">
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
     <link id="bs-css" href="css/bootstrap-cerulean.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="ContentM" ContentPlaceHolderID="MainContent" runat="server">
    <h1 style="align-content:center">This is Your RecRep! The repository that handles all of your receipts</h1>
    <br />
    <br />
     <div id="Div1" runat="server" class="table-responsive">
        <asp:Label runat="server" ID="noInformationLabel" Visible="false">You Do Not have any informatinon to display</asp:Label>
        <asp:GridView runat="server" ID="ReceiptTableGridview" CssClass="table table-hover table-striped bootstrap-datatable datatable" OnRowCommand="ReceiptTableGridview_RowCommand" AutoGenerateColumns="false" >
            <%-- OnRowDataBound="ReceiptTableGridview_RowDataBound"  AllowSorting="true"  AllowPaging="true" HeaderStyle-HorizontalAlign="Center" OnSorting="ReceiptTableGridview_Sorting" OnPageIndexChanging="ReceiptTableGridview_PageIndexChanged" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Style="text-align: center;" PagerSettings-Mode="NumericFirstLast" PagerStyle-BorderStyle="None" PagerStyle-ForeColor="#6699FF" --%>
            <Columns>
                <asp:TemplateField HeaderText="Receipt Identifying Code" SortExpression="Receipt Identifying Code">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="URCILinkButton" CommandName="OpenReceipt" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RDI")%>'>
                            <asp:Label ID="NDCLabel" runat="server" Font-Bold="true" Text='<%#DataBinder.Eval(Container.DataItem, "Receipt Identifying Code")%>'></asp:Label>
                        </asp:LinkButton>
                        <%-- <asp:BoundField DataField="Receipt Identifying Code" HeaderText="Receipt Identifying Code" SortExpression="Receipt Identifying Code" /> --%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                <asp:BoundField DataField="Retail Venue Name" HeaderText="Retail Venue Name" SortExpression="Retail Venue Name" />
                <asp:BoundField DataField="Total Amount Spent" HeaderText="Total Amount Spent" SortExpression="Total Amount Spent" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Date of Purchase" HeaderText="Date of Purchase" SortExpression="Date of Purchase" />
               <%-- <asp:BoundField DataField="Date Receipt was Entered" HeaderText="Date Receipt was Entered" SortExpression="Date Receipt was Entered" /> --%>
                <asp:BoundField DataField="Minor Description" HeaderText="Minor Description" SortExpression="Minor Description" />
                <asp:BoundField DataField="Total Items on Receipt" HeaderText="Total Items on Receipt" SortExpression="Total Items on Receipt" />
                <asp:BoundField DataField="Description_of_Payment" HeaderText="Payment Used" SortExpression="Description_of_Payment" />
                <asp:TemplateField HeaderText="Preview Image">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageProfile" runat="server" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Image_Filename") %>'
                            Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                    </ItemTemplate>

                </asp:TemplateField>
            </Columns><%--             <AlternatingRowStyle CssClass="info" />
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" HorizontalAlign="Center" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" /> 

            <PagerStyle BackColor="#003399" Font-Bold="true" Font-Size="Medium" Font-Italic="true" ForeColor="White" /> --%>
        </asp:GridView>
    </div>
    <br />


    
    <div id="divBackground" style=" display: none;
    position: absolute;
    top: 0px;
    left: 0px;
    background-color: black;
    z-index: 100;
    opacity: 0.8;
    filter: alpha(opacity=60);
    -moz-opacity: 0.8;
    min-height: 100%;">

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
