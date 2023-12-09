<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_CollegePrincipal_Details.aspx.cs" Inherits="Pages_Admin_Admin_CollegePrincipal_Details" %>

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
    <style>
        .header {
            padding: 10px 16px;
            background: #555;
            color: #f1f1f1;
        }

        .content {
            padding: 16px;
        }

        .sticky {
            position: fixed;
            top: 0;
            width: 100%;
        }

            .sticky + .content {
                padding-top: 102px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
      <div class="row">
        <div class="col-md-12">
            <div class="panel">
                <div class="panel-head">
                    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
                        <ContentTemplate>
                                    <div class="row">
                                          <div class="col-md-2 col-xs-6">
                            
                                <asp:DropDownList ID="ddlprogram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-xs-6">
                            
                                <asp:DropDownList ID="ddlManagerName" OnSelectedIndexChanged="ddlManagerName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 form-group col-xs-6">
                         
                                <asp:DropDownList ID="ddlTaluka" OnSelectedIndexChanged="ddlTaluka_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-xs-6">
                                <input type="text" placeholder="Search College Name" onkeyup="SearchCollegeDetail()" id="Search" class="form-control" />
                            </div>
                                        <div class="col-md-2 form-group col-xs-2">
                                            <asp:LinkButton ID="btnSaveCollegeDetails" OnClick="btnSaveCollegeDetails_Click" CssClass="btn btn-info"
                                                ValidationGroup="College" runat="server">Save &nbsp;<span class="fa fa-check">
                                 </span> </asp:LinkButton>
                                        </div>
                                        <div class="col-md-1 form-group col-xs-2">
                                            <a href="AdminAnalyticalCharts.aspx" class="btn btn-white btn-floating">
                                                <span class="fa fa-dashboard"></span></a>
                                        </div>
                                        <div class="col-md-1 form-group col-xs-2 hidden">
                                            <asp:LinkButton ID="btnExcel" OnClick="btnExcel_Click" CssClass="btn btn-warning btn-floating" runat="server"> 
                                                <span class="fa fa-file-excel-o"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                 
                
             
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12" style="width: 100%; height: 650px; overflow: auto" id="CollegeDetails">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="Server">

                                <ContentTemplate>
                              
                                    <asp:Repeater ID="rptCollegeDetails" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-hover" id="mytable">
                                                <thead class="header" id="myHeader">
                                                    <tr style="background-color: #13c4a5; color: #fff">
                                                        <td style="display: none">CollegeId
                                                        </td>                                               
                                                         <td><strong><b>Slno</b></strong> </td>
                                                        <td><strong><b>College_Name</b></strong> </td>
                                                        <td><strong><b>Principal_Name</b></strong> </td>
                                                        <td><strong><b>MobileNo</b></strong>
                                                        </td>
                                                        <td><strong><b>Mail_Id</b></strong>
                                                        </td>
                                                        <td><strong><b>Whats_App</b></strong>
                                                        </td>
                                                        <td><strong><b>Facebook_Id</b></strong>
                                                        </td>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tbody class="content">
                                                <tr>
                                        <td>
                                         <%# Container.ItemIndex + 1 %>
                                        </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblCollegeId" runat="server" Text='<%# Eval("CollegeId") %>' />
                                                    </td>
                                                    <td style="min-width: 15%; ">
                                                        <asp:Label ID="lblCollege_Name" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                       
                                                    </td>
                                                    <td style="min-width: 40px;">
                                                        <asp:TextBox ID="txtPrincipal_Name" autocomplete="off" Text='<%# Eval("Principal_Name") %>'  CssClass="form-control" runat="server">

                                                        </asp:TextBox>
                                                    
                                                    </td>
                                                    <td style="min-width: 40px; text-align: center;">
                                                     
                                                        <asp:TextBox ID="txtPrincipal_MobileNo" onkeypress="NumericOnly()" MaxLength="10" Text='<%# Eval("Principal_MobileNo") %>' autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td style="min-width: 50px;">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrincipal_MailId"
                                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                            Display="Dynamic" ErrorMessage="Invalid email address" />
                                           
                                                        <asp:TextBox ID="txtPrincipal_MailId" autocomplete="off" Text='<%# Eval("Principal_MailId") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                       
                                                    </td>
                                                    <td style="min-width: 50px; text-align: center;">
                                                          <asp:TextBox ID="txtPrincipal_WhatsAppNo" autocomplete="off" onkeypress="NumericOnly()" MaxLength="10" Text='<%# Eval("Principal_WhatsAppNo") %>'  CssClass="form-control" runat="server"></asp:TextBox>
                                                  
                                                    </td>
                                                    <td style="width: 15%;">
                                                               <asp:TextBox ID="txtPrincipal_FacebookId" autocomplete="off" Text='<%# Eval("Principal_FacebookId") %>'  CssClass="form-control" runat="server"></asp:TextBox>
                                                      
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                             
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
    </div>

    <script type="text/javascript">
        function SearchCollegeDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("Search");
            filter = input.value.toUpperCase();
            ul = document.getElementById("CollegeDetails");
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
    <script>
        window.onscroll = function () { myFunction() };
        document.getElementById("mytable").onscroll = function () { myFunction() };
        var header = document.getElementById("myHeader");
        var sticky = header.offsetTop;

        function myFunction() {



            if (window.pageYOffset > sticky) {
                header.classList.add("sticky");
            } else {
                header.classList.remove("sticky");
            }
        }
    </script>
</asp:Content>