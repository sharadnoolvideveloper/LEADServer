<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="ManagerCollege_PrincipalDetails.aspx.cs" Inherits="Pages_Manager_ManagerCollege_PrincipalDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
      <script src="../../JS/CommonJS/toster.js"></script>
    <script type="text/javascript">
        function success(msg) {
            toastr.options.timeOut = 1500; //1.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.success(msg);
        }
        function warning(msg) {
            toastr.options.timeOut = 1000; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.warning(msg);
        }
        function info(msg) {
            toastr.options.timeOut = 1000; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.info(msg);
        }
        function error(msg) {
            toastr.options.timeOut = 2000; //2.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.error(msg);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <br />
    <div class="row">
        <div class="col-md-11">
            <div class="panel">
                <div class="panel-head">
                    <div class="container">
                        <br />
                        <div class="row">
                               <div class="col-md-2 col-xs-2">
                         
                                <asp:DropDownList ID="ddlTaluka" OnSelectedIndexChanged="ddlTaluka_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-md-6 col-xs-4">
                                <input type="text" placeholder="Search College Name" onkeyup="SearchCollegeDetail()" id="Search" class="form-control" />
                            </div>
                            <div class="col-md-2 col-xs-3">
                                <asp:LinkButton ID="btnSaveCollegeDetails" OnClick="btnSaveCollegeDetails_Click" CssClass="btn btn-info" 
                                    ValidationGroup="College" runat="server">Save &nbsp;<span class="fa fa-check">
                                 </span> </asp:LinkButton>
                            </div>
                            <div class="col-md-2 col-xs-3">
                                <a href="DashBoard.aspx?vwType=DashBoard" class="btn btn-white"> <span class="fa fa-dashboard">
                                 </span> </a>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12" style="width: 100%; height: 650px; overflow: auto" id="CollegeDetails">
                            <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">

                                <ContentTemplate>
                                    <asp:Repeater ID="rptCollegeDetails" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-hover">
                                                <thead>
                                                    <tr style="background-color: #13c4a5; color: #fff">
                                                        <td style="display: none">CollegeId
                                                        </td>                                               
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
                                            <tbody>
                                                <tr>
                                        
                                                    <td style="display: none">
                                                        <asp:Label ID="lblCollegeId" runat="server" Text='<%# Eval("CollegeId") %>' />
                                                    </td>
                                                    <td style="min-width: 15%; ">
                                                        <asp:Label ID="lblCollege_Name" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                       
                                                    </td>
                                                    <td style="min-width: 40px;">
                                                        <asp:TextBox ID="txtPrincipal_Name" Text='<%# Eval("Principal_Name") %>'  CssClass="form-control" runat="server">

                                                        </asp:TextBox>
                                                    
                                                    </td>
                                                    <td style="min-width: 40px; text-align: center;">
                                                     
                                                        <asp:TextBox ID="txtPrincipal_MobileNo" onkeypress="NumericOnly()" MaxLength="10" Text='<%# Eval("Principal_MobileNo") %>'  CssClass="form-control" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td style="min-width: 50px;">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrincipal_MailId"
                                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                            Display="Dynamic" ErrorMessage="Invalid email address" />
                                           
                                                        <asp:TextBox ID="txtPrincipal_MailId" Text='<%# Eval("Principal_MailId") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                       
                                                    </td>
                                                    <td style="min-width: 50px; text-align: center;">
                                                          <asp:TextBox ID="txtPrincipal_WhatsAppNo" onkeypress="NumericOnly()" MaxLength="10" Text='<%# Eval("Principal_WhatsAppNo") %>'  CssClass="form-control" runat="server"></asp:TextBox>
                                                  
                                                    </td>
                                                    <td style="width: 15%;">
                                                               <asp:TextBox ID="txtPrincipal_FacebookId" Text='<%# Eval("Principal_FacebookId") %>'  CssClass="form-control" runat="server"></asp:TextBox>
                                                      
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
        <div class="col-md-1">
            <div class="bg_ss z-depth-2 hidden-xs" style="position: fixed; top: 0; right: 0; height: 100%; width: 50px;">
                <h3 class="fa-rotate-90 text-muted text-nowrap" style="letter-spacing: 10px; margin-top: 50px; color: white;">&nbsp;&nbsp;&nbsp;
                 <span>Principal</span>    <span>Details</span>
                </h3>
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
</asp:Content>

