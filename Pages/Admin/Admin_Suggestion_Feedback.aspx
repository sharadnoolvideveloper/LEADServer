<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_Suggestion_Feedback.aspx.cs" Inherits="Pages_Admin_Admin_Suggestion_Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/toster.js"></script>
    <script type="text/javascript">
        function success(msg) {
            toastr.options.timeOut = 3500; //3.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.success(msg);
        }
        function warning(msg) {
            toastr.options.timeOut = 3500; //3.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.warning(msg);
        }
        function info(msg) {
            toastr.options.timeOut = 3500; //3.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.info(msg);
        }
        function error(msg) {
            toastr.options.timeOut = 3500; //3.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.error(msg);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-3">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel">
                        <div class="panel-heading" style="background-color: antiquewhite;">
                            <h4>Manager List
                                <span class="pull-right">
                                    <asp:LinkButton ID="btnAll" OnClick="btnAll_Click" runat="server">All</asp:LinkButton>
                                </span>
                            </h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <ul class="list-group" style="height: 620px; overflow: auto;">
                                        <asp:Repeater ID="rptManagerList" OnItemCommand="rptManagerList_ItemCommand" runat="server">
                                            <ItemTemplate>
                                                <li class="list-group-item" style="cursor: pointer;">
                                                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-block text-warning" runat="server">
                                                    <h4>

                                                        <asp:Label ID="lblManagerId" runat="server" Visible="false" Text='<% #Eval("ManagerId") %>'></asp:Label>
                                                        <asp:Label ID="lblManagerName" Font-Size="Smaller" runat="server" Text='<% #Eval("ManagerName") %>'></asp:Label>
                                                        <span class="pull-right" style="display:none;">
                                                            <asp:Label ID="lblCount" runat="server" Text='<% #Eval("ManagerId") %>'></asp:Label>
                                                        </span>
                                                    </h4>
                                                        </asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-9">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel">
                        <div class="panel-heading" style="background-color: antiquewhite;">
                            <div class="row">
                                  <div class="col-md-2"> 
                                  
                                     <asp:DropDownList ID="ddlprogram"  OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"> </asp:DropDownList>
                                    </div>
                                <div class="col-md-3">
                                    <h4>Suggestion/Feedback</h4>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSearch" placeholder="Search" onkeyup="SearchRequest()" CssClass="form-control" AutoComplete="false" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlAcademicYear" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <asp:LinkButton ID="btnExcel" OnClick="btnExcel_Click" CssClass="btn btn-warning btn-floating" data-toggle="tooltip" 
                                        title="Excel Download" runat="server"><span class="fa fa-file-excel-o" ></span></asp:LinkButton>
                                </div>
                            </div>

                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12" style="width: 100%; height: 620px; overflow: auto" id="SearchList">

                                    <asp:Repeater ID="rptSuggestionFeedback"  runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                    <tr>
                                                        <td style="text-align: center"><strong><b>Slno</b><strong></td>
                                                          <td ><strong><b>Date</b><strong>
                                                        </td>
                                                        <td><strong><b>Lead_Id</b><strong>
                                                        </td>
                                                         <td ><strong><b>Student_Name</b><strong>
                                                        </td>
                                                         <td ><strong><b>MobileNo</b><strong>
                                                        </td>
                                                           <td ><strong><b>Request_Head</b><strong>
                                                        </td>
                                                          <td style="display:none;" ><strong><b>Mail_Id</b><strong>
                                                        </td>
                                                        <td><strong><b>Manager_Name</b><strong>
                                                        </td>
                                                      
                                                        <td><strong><b>Suggestion</b><strong>
                                                        </td>
                                                        <td><strong><b>Feedback</b><strong>
                                                        </td>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tbody>
                                                <tr>
                                                    <td style="text-align:center;">
                                                        <span style="display:none;">
                                                            <asp:Label ID="lblSlno" Text='<%# Eval("slno") %>'  runat="server" />
                                                        </span>
                                                     <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                    </td>
                                                      <td style="width: 12%">
                                                        <asp:Label ID="lblDate" Font-Size="Small" runat="server" Text='<%# Eval("Created_Date") %>' />
                                                    </td>
                                                    <td style="width: 6%;">
                                                      <asp:Label ID="lblLead_Id" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                    </td>
                                                    <td style="min-width: 35px; ">
                                                        <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                    </td>
                                                    <td style="min-width: 40px;">
                                                        <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                    </td>
                                                         <td style="min-width: 40px;">
                                                        <asp:Label ID="lblRequestHead" Font-Size="Small" runat="server" Text='<%# Eval("Head_Name") %>' />
                                                    </td>
                                                    <td style="min-width: 50px;display:none;">
                                                        <asp:Label ID="lblMailId" Font-Size="Small" runat="server" Text='<%# Eval("MailId") %>' />
                                                    </td>
                                                    <td style="width: 15%;">
                                                        <span style="display:none;">
                                                               <asp:Label ID="lblManagerId" Font-Size="Small" runat="server" Text='<%# Eval("managerid") %>' />
                                                        </span>
                                                        <asp:Label ID="lblManagerName" Font-Size="Small" runat="server" Text='<%# Eval("ManagerName") %>' />
                                                    </td>
                                                  
                                                    <td style="width: 20%;">
                                                        <asp:Label ID="lblSuggestion" Font-Size="Small" runat="server" Text='<%# Eval("Suggestion") %>' />
                                                    </td>
                                                    <td style="width: 20%;">
                                                           <asp:Label ID="lblFeedback" Font-Size="Small" runat="server" Text='<%# Eval("Feedback") %>' />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

     <script type="text/javascript">
        function SearchRequest() {
            var input, filter, ul, li, a, i;
            input =document.getElementById("<%= txtSearch.ClientID %>");
             filter = input.value.toUpperCase();
             ul = document.getElementById("SearchList");
             li = ul.getElementsByTagName("tbody");
             for (i = 0; i < li.length; i++) {
                 a = li[i];
                 if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
                     li[i].style.display = "";
                 } else {
                     li[i].style.display = "none";

                 }
             }
         }
     </script>
</asp:Content>

