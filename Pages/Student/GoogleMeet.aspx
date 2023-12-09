<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="GoogleMeet.aspx.cs" Inherits="Pages_Student_GoogleMeet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/jquery-tagsinput.min.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/chat.css" rel="stylesheet" />
    <script src="../../JS/StudentJS/jquery-tagsinput.min.js"></script>
    <link href="../../CSS/CommonCSS/MultiSelect.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/MultiSelect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                 <asp:TextBox ID="URL"  runat="server" Text="http://" />
                  <iframe id="Iframe1"  src="https://meet.google.com/gzi-aczj-iiz" runat="server" frameborder="0" marginwidth="1"
                   style="position: absolute;top:100px;width:800px;height:400px;border:solid 1px"></iframe>
 <%--      src="<%# TheURL %>"--%>
            </div>
        </div>
    </div>

</asp:Content>

