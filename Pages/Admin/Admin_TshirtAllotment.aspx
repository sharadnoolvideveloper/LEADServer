<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_TshirtAllotment.aspx.cs" Inherits="Pages_Admin_Admin_TshirtAllotment" %>

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
    <div class="container-fluid">
        <div class="row">
            <div class="panel">
                <div class="panel-heading">
                    <h3>T-Shirt Allotment
                        <span class="pull-right">
                            <asp:LinkButton ID="btnSave" CssClass="btn btn-primary btn-floating" OnClick="btnSave_Click" runat="server"><span class="fa fa-save"></span> </asp:LinkButton></span>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2 form-group">
                            <div class="row  form-group">
                                <div class="col-md-12">
                                    <asp:DropDownList ID="ddlAcademicYear" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:LinkButton ID="btnGenerate" OnClick="btnGenerate_Click" CssClass="btn btn-info btn-block" runat="server">Generate &nbsp; <span class="pull-right"><span class="fa fa-arrow-right"></span>  </span> </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-10" style="height: 650px; overflow: auto" id="RequestForCompletionList">
                            <asp:Repeater ID="rptTshirtAllotment" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-hover" style="width: 100%;">
                                        <thead>
                                            <tr style="background-color: #13c4f5; color: #fff">
                                                <td style="display: none">ManagerId
                                                </td>
                                                <td><strong><b>Slno</b></strong></td>
                                                <td><strong><b>Manager_Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></strong></td>
                                                <td ><strong><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;S</b></strong></td>
                                                <td ><strong><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;M</b></strong></td>
                                                <td><strong><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;L</b></strong></td>
                                                <td><strong><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XL</b></strong></td>
                                                <td><strong><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XXL</b></strong></td>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td style="display: none">
                                                <asp:Label ID="lblManagerId" runat="server" Text='<%# Eval("ManagerId") %>' />
                                            </td>
                                             <td>
                                                 <%#Container.ItemIndex+1 %>
                                            </td>
                                            <td>
                                               
                                                <asp:Label ID="lblManagerName" runat="server" Text='<%# Eval("ManagerName") %>' />
                                            </td>
                                            <td style="min-width: 10%;text-align:center">
                                                <asp:TextBox ID="txtS" MaxLength="4" Width="80px" CssClass="form-control" onkeypress="NumericOnly()" Text='<%# Eval("AllotedS") %>' runat="server"></asp:TextBox>
                                            </td>
                                            <td style="min-width: 10%;text-align:center">
                                                <asp:TextBox ID="txtM" MaxLength="4" Width="80px" CssClass="form-control" onkeypress="NumericOnly()" Text='<%# Eval("AllotedM") %>' runat="server"></asp:TextBox>
                                            </td>
                                            <td style="min-width: 10%;">
                                                <asp:TextBox ID="txtL" CssClass="form-control" Width="80px" MaxLength="4" onkeypress="NumericOnly()" Text='<%# Eval("AllotedL") %>' runat="server"></asp:TextBox>
                                            </td>
                                            <td style="min-width: 10%;">
                                                <asp:TextBox ID="txtXL" CssClass="form-control" Width="80px" MaxLength="4" onkeypress="NumericOnly()" Text='<%# Eval("AllotedXL") %>' runat="server"></asp:TextBox>
                                            </td>
                                            <td style="min-width: 10%;">
                                                <asp:TextBox ID="txtXXL" CssClass="form-control" Width="80px" MaxLength="4" onkeypress="NumericOnly()" Text='<%# Eval("AllotedXXL") %>' runat="server"></asp:TextBox>
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
</asp:Content>

