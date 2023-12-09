<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="SanboxWiseManagerList.aspx.cs" Inherits="Pages_Admin_SanboxWiseManagerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/CommonJS/toster.js"></script>
    <script src="../../JS/CommonJS/bootstrap-datepicker_fun.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/components.css" rel="stylesheet" />

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="container-fluid" style="background-color: white;">
        <div class="row">
        <div class="col-md-2">
                                <label for="ddlProgramType">Select Program</label>
                                <asp:DropDownList ID="ddlprogramId"  OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                  
                                </asp:DropDownList>
                            </div>
            </div>
        <br />
        <div class="panel-group" id="accordion">
            <div style="max-height:650px;overflow:auto;">
                <asp:LinkButton ID="btnExcel" OnClick="btnExcel_Click" Visible="false" CssClass="btn btn-warning btn-floating" runat="server">
                                                     <span class="fa fa-file-excel-o"></span>
                </asp:LinkButton>
            <asp:Repeater ID="rptSandboxList" OnItemDataBound="rptSandboxList_ItemDataBound" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">                               
                                <a data-toggle="collapse" data-parent="#accordion" href='#<%# Eval("Sandbox") %>'>
                                    <table class="table table-hover">
                                        <thead>
                                            <tr style="background-color:#2255a4;color:whitesmoke;">
                                                <td>Sand box</td>                                              
                                                <td >All Projects</td>
                                                <td >Proposed</td>
                                                <td >Approved</td>
                                                <td >Request For Modification</td>
                                                <td >Request For Completion</td>
                                                <td >Completed</td>
                                                <td >Rejected</td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                 <td style="width:20%">
                                                      <asp:Label ID="SandboxName" runat="server" Text='<%# Eval("Sandbox") %>'></asp:Label>

                                                 </td>
                                                <td >
                                                    <asp:Label ID="lblAllProjectTop" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                   <asp:Label ID="lblProposedTop" runat="server" Text="0"></asp:Label> 
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblApprovedTop" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblRMTop" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                    <asp:Label ID="lblRCTop" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblCompletedTop" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblRejectedTop" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>


                                </a>

                            </h4>
                        </div>
                        <div id='<%# Eval("Sandbox") %>' class="panel-collapse collapse">
                              <div style="max-height:650px;overflow:auto;">
                            <asp:Repeater ID="rptManagerList" OnItemDataBound="rptManagerList_ItemDataBound" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-hover">
                                         <thead>
                                            <tr style="background-color:aliceblue">
                                                <td>Manager Name</td>                                              
                                                <td >All Projects</td>
                                                <td >Proposed</td>
                                                <td >Approved</td>
                                                <td >Request For Modification</td>
                                                <td >Request For Completion</td>
                                                <td >Completed</td>
                                                <td >Rejected</td>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                             <td style="display:none;">
                                                <asp:Label ID="lblManagerId" runat="server" Text='<%# Eval("ManagerId") %>'></asp:Label>
                                            </td>
                                            <td style="width:20%">
                                                <asp:Label ID="ManagerName" runat="server" Text='<%# Eval("ManagerName") %>'></asp:Label>
                                            </td>
                                             <td>
                                                    <asp:Label ID="lblAllProjectList" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                   <asp:Label ID="lblProposedList" runat="server" Text="0"></asp:Label> 
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblApprovedList" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblRMList" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                    <asp:Label ID="lblRCList" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblCompletedList" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblRejectedList" runat="server" Text="0"></asp:Label>
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
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
                </div>
        </div>
    </div>
   

</asp:Content>
