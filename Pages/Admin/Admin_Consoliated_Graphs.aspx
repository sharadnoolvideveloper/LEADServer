<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_Consoliated_Graphs.aspx.cs" Inherits="Pages_Admin_Admin_Consoliated_Graphs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
   
    <script src="../../JS/CommonJS/bootstrap-datepicker_fun.min.js"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
     <script src="../../JS/CommonJS/jsapi.js"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-6">
            <div class="container-fluid">
                <asp:Literal ID="lt1" runat="server"></asp:Literal>
                <div id="chart_div1">
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <asp:Literal ID="lt2" runat="server"></asp:Literal>
                <div id="chart_div2">
                </div>
        </div>
    </div>
    <br />
     <div class="row">
        <div class="col-md-12">
            <div class="container-fluid">
                <asp:Literal ID="lt3" runat="server"></asp:Literal>
                <div id="chart_div3">
                </div>
            </div>
        </div> 
    </div>

</asp:Content>

