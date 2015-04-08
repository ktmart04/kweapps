<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrentPlan.aspx.cs" Inherits="FinanceReporting.CurrentPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <br />
    Current Month:
<asp:Label runat="server" />
    
<asp:Label ID="NoBudgetLabel" runat="server">You current do not have a budget set. Click <a href="BudgetPlan.aspx">here</a> to create one</asp:Label>
<asp:Label ID="CurrentBudgetLabel" Text="This is your Current Budget" runat="server" />

    <br />

    <br />


    <div class="container" runat="server" style="width:700px">
      <h2>Your Current Progress:</h2>
      <div class="panel-group" id="accordion">
        <div id="MonthlyDivHeader" runat="server" class="panel panel-default">
          <div id="panel1" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Monthly Budget:<asp:Label runat="server" ID="InfoBudgetAll" />Current Spending:<asp:Label runat="server" ID="InfoBudgetAllCurrent" />Current Savings:<asp:Label runat="server" ID="InfoBudgetAllSavings" /></a>
            </h4>
          </div>
          <div id="collapse1" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
        <div class="panel panel-default" style="border-color:green;">
          <div class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Information 1 <asp:Label runat="server" ID="Label2" Text="Info2"></asp:Label></a>
            </h4>
          </div>
          <div id="collapse2" class="panel-collapse collapse">
            <div class="panel-body">Information for part 2.</div>
          </div>
        </div>
        <div class="panel panel-default">
          <div class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Collapsible Group 3</a>
            </h4>
          </div>
          <div id="collapse3" class="panel-collapse collapse">
            <div class="panel-body">Lorem ipsum dolor sit amet, consectetur adipisicing elit,
            sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,
            quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div1" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div2">Monthly Budget:<asp:Label runat="server" ID="Label1" />Current Spending:<asp:Label runat="server" ID="Label3" />Current Savings:<asp:Label runat="server" ID="Label4" /></a>
            </h4>
          </div>
          <div id="Div2" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div3" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div4">Monthly Budget:<asp:Label runat="server" ID="Label5" />Current Spending:<asp:Label runat="server" ID="Label6" />Current Savings:<asp:Label runat="server" ID="Label7" /></a>
            </h4>
          </div>
          <div id="Div4" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div5" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div6">Monthly Budget:<asp:Label runat="server" ID="Label8" />Current Spending:<asp:Label runat="server" ID="Label9" />Current Savings:<asp:Label runat="server" ID="Label10" /></a>
            </h4>
          </div>
          <div id="Div6" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div7" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div8">Monthly Budget:<asp:Label runat="server" ID="Label11" />Current Spending:<asp:Label runat="server" ID="Label12" />Current Savings:<asp:Label runat="server" ID="Label13" /></a>
            </h4>
          </div>
          <div id="Div8" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div9" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div10">Monthly Budget:<asp:Label runat="server" ID="Label14" />Current Spending:<asp:Label runat="server" ID="Label15" />Current Savings:<asp:Label runat="server" ID="Label16" /></a>
            </h4>
          </div>
          <div id="Div10" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div11" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div12">Monthly Budget:<asp:Label runat="server" ID="Label17" />Current Spending:<asp:Label runat="server" ID="Label18" />Current Savings:<asp:Label runat="server" ID="Label19" /></a>
            </h4>
          </div>
          <div id="Div12" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div13" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div14">Monthly Budget:<asp:Label runat="server" ID="Label20" />Current Spending:<asp:Label runat="server" ID="Label21" />Current Savings:<asp:Label runat="server" ID="Label22" /></a>
            </h4>
          </div>
          <div id="Div14" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div15" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div16">Monthly Budget:<asp:Label runat="server" ID="Label23" />Current Spending:<asp:Label runat="server" ID="Label24" />Current Savings:<asp:Label runat="server" ID="Label25" /></a>
            </h4>
          </div>
          <div id="Div16" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div17" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div18">Monthly Budget:<asp:Label runat="server" ID="Label26" />Current Spending:<asp:Label runat="server" ID="Label27" />Current Savings:<asp:Label runat="server" ID="Label28" /></a>
            </h4>
          </div>
          <div id="Div18" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div19" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div20">Monthly Budget:<asp:Label runat="server" ID="Label29" />Current Spending:<asp:Label runat="server" ID="Label30" />Current Savings:<asp:Label runat="server" ID="Label31" /></a>
            </h4>
          </div>
          <div id="Div20" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div21" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div22">Monthly Budget:<asp:Label runat="server" ID="Label32" />Current Spending:<asp:Label runat="server" ID="Label33" />Current Savings:<asp:Label runat="server" ID="Label34" /></a>
            </h4>
          </div>
          <div id="Div22" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div23" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div24">Monthly Budget:<asp:Label runat="server" ID="Label35" />Current Spending:<asp:Label runat="server" ID="Label36" />Current Savings:<asp:Label runat="server" ID="Label37" /></a>
            </h4>
          </div>
          <div id="Div24" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div25" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div26">Monthly Budget:<asp:Label runat="server" ID="Label38" />Current Spending:<asp:Label runat="server" ID="Label39" />Current Savings:<asp:Label runat="server" ID="Label40" /></a>
            </h4>
          </div>
          <div id="Div26" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div27" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div28">Monthly Budget:<asp:Label runat="server" ID="Label41" />Current Spending:<asp:Label runat="server" ID="Label42" />Current Savings:<asp:Label runat="server" ID="Label43" /></a>
            </h4>
          </div>
          <div id="Div28" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div29" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div30">Monthly Budget:<asp:Label runat="server" ID="Label44" />Current Spending:<asp:Label runat="server" ID="Label45" />Current Savings:<asp:Label runat="server" ID="Label46" /></a>
            </h4>
          </div>
          <div id="Div30" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div31" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div32">Monthly Budget:<asp:Label runat="server" ID="Label47" />Current Spending:<asp:Label runat="server" ID="Label48" />Current Savings:<asp:Label runat="server" ID="Label49" /></a>
            </h4>
          </div>
          <div id="Div32" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>
           <div class="panel panel-default">
          <div id="Div33" runat="server" class="panel-heading">
            <h4 class="panel-title">
              <a data-toggle="collapse" data-parent="#accordion" href="#Div34">Monthly Budget:<asp:Label runat="server" ID="Label50" />Current Spending:<asp:Label runat="server" ID="Label51" />Current Savings:<asp:Label runat="server" ID="Label52" /></a>
            </h4>
          </div>
          <div id="Div34" class="panel-collapse collapse">
            <div class="panel-body">Here you will hold the information for 1</div>
          </div>
        </div>

      </div> 
    </div>



  




</asp:Content>
