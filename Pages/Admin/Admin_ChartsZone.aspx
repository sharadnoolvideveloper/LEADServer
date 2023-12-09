<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_ChartsZone.aspx.cs" Inherits="Pages_Admin_Admin_ChartsZone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div>
        <asp:Literal ID="lt" runat="server"></asp:Literal>
    </div>
   <div id="visualization" style="margin: 1em"> </div>
    <div id="chart_div" style="width: 650px; height: 350px;"></div> 
</asp:Content>

