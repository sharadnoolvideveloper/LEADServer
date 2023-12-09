<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_SandboxWiseTshirtList.aspx.cs" Inherits="Pages_Admin_Admin_SandboxWiseTshirtList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/CommonJS/toster.js"></script>
    <script src="../../JS/CommonJS/bootstrap-datepicker_fun.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/components.css" rel="stylesheet" />

    <script type="text/javascript">
        function success(msg) {
            toastr.options.timeOut = 3500; //1.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.success(msg);
        }
        function warning(msg) {
            toastr.options.timeOut = 3500; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.warning(msg);
        }
        function info(msg) {
            toastr.options.timeOut = 3500; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.info(msg);
        }
        function error(msg) {
            toastr.options.timeOut = 3500; //2.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.error(msg);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container-fluid" style="background-color: white;">
      <div class="row">
           <div class="col-md-2">
                                <label for="ddlProgramType">Select Program</label>
                                <asp:DropDownList ID="ddlprogramId"  OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                  
                                </asp:DropDownList>
                            </div>
          <div class="col-md-4">
              <label for="">Select Academic Year</label>
              <asp:DropDownList ID="ddlAcademicYear" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
          </div>
          <div class="col-md-2">
            <br />
              <asp:LinkButton ID="btnExcelCount" OnClick="btnExcelCount_Click" CssClass="btn btn-success" runat="server"> SanboxWise Count&nbsp; <span class="fa fa-file-excel-o"></span></asp:LinkButton>
          </div>
           <div class="col-md-2">
            <br />
              <asp:LinkButton ID="btnExcelList" OnClick="btnExcelList_Click" CssClass="btn btn-primary" runat="server"> SanboxWise List&nbsp; <span class="fa fa-file-excel-o"></span></asp:LinkButton>
          </div>
      </div>
         <br />
        <div class="panel-group" id="accordion">
            <div style="max-height:650px;overflow:auto;">
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
                                            <tr style="background-color:#28b779;color:whitesmoke;">
                                                <td>Sand box</td>                                              
                                                    <td >Total</td>
                                                      <td >Used</td>
                                                     <td >Bal</td>
                                                  <td >Requests</td>
                                                <td >S</td>
                                                <td >M</td>
                                                <td >L</td>
                                                <td >XL</td>
                                                <td >XXL</td>
                                                
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                 <td style="width:20%">
                                                      <asp:Label ID="SandboxName" runat="server" Text='<%# Eval("Sandbox") %>'></asp:Label>
                                                 </td>
                                                <td >
                                                    <asp:Label ID="lblTopAllTshirt" runat="server" Text="0"></asp:Label>
                                                </td>
                                                  <td >
                                                    <asp:Label ID="lblTopTotalUsed" runat="server" Text="0"></asp:Label>
                                                </td>
                                                   <td >
                                                    <asp:Label ID="lblTopTotalBalance" runat="server" Text="0"></asp:Label>
                                                </td>
                                                     <td >
                                                    <asp:Label ID="lblTopRequests" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                  <asp:Label ID="lblUsedTopS" runat="server" Text="0"></asp:Label> /
                                                       <asp:Label ID="lblAllotedTopS" runat="server" Text="0"></asp:Label> 
                                                      
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblUsedTopM" runat="server" Text="0"></asp:Label> /
                                                     <asp:Label ID="lblAllotedTopM" runat="server" Text="0"></asp:Label>
                                                     
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblUsedTopL" runat="server" Text="0"></asp:Label>/
                                                     <asp:Label ID="lblAllotedTopL" runat="server" Text="0"></asp:Label>
                                                    
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblUsedTopXL" runat="server" Text="0"></asp:Label>/
                                                    <asp:Label ID="lblAllotedTopXL" runat="server" Text="0"></asp:Label>
                                                      
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblUsedTopXXL" runat="server" Text="0"></asp:Label>/
                                                     <asp:Label ID="lblAllotedTopXXL" runat="server" Text="0"></asp:Label>
                                                     
                                                </td>                                              
                                            </tr>
                                        </tbody>
                                    </table>
                                </a>
                            </h4>
                        </div>
                        <div id='<%# Eval("Sandbox") %>' class="panel-collapse collapse">
                              <div style="max-height:650px;overflow:auto;">
                            <asp:Repeater ID="rptManagerList" OnItemDataBound="rptManagerList_ItemDataBound"  runat="server">
                                <HeaderTemplate>
                                    <table class="table table-hover">
                                         <thead>
                                            <tr style="background-color:aliceblue">
                                                <td>Manager Name</td>                                              
                                                <td >Total</td>
                                                  <td >Used</td>
                                                 <td >Bal</td>
                                                 <td >Requests</td>
                                                <td >S</td>
                                                <td >M</td>
                                                <td >L</td>
                                                <td >XL</td>
                                                <td >XXL</td>
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
                                                 <td >
                                                    <asp:Label ID="lblAllTshirt" runat="server" Text="0"></asp:Label>
                                                </td>
                                             <td >
                                                    <asp:Label ID="lblUsedTotal" runat="server" Text="0"></asp:Label>
                                                </td>
                                               <td >
                                                    <asp:Label ID="lblBalance" runat="server" Text="0"></asp:Label>
                                                </td>
                                              <td >
                                                    <asp:Label ID="lblRequestCount" runat="server" Text="0"></asp:Label>
                                                </td>
                                                 <td >
                                                  <asp:Label ID="lblUsedS" runat="server" Text="0"></asp:Label> /
                                                       <asp:Label ID="lblAllotedS" runat="server" Text="0"></asp:Label> 
                                                      
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblUsedM" runat="server" Text="0"></asp:Label> /
                                                     <asp:Label ID="lblAllotedM" runat="server" Text="0"></asp:Label>
                                                     
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblUsedL" runat="server" Text="0"></asp:Label>/
                                                     <asp:Label ID="lblAllotedL" runat="server" Text="0"></asp:Label>
                                                    
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblUsedXL" runat="server" Text="0"></asp:Label>/
                                                    <asp:Label ID="lblAllotedXL" runat="server" Text="0"></asp:Label>
                                                      
                                                </td>
                                                 <td >
                                                     <asp:Label ID="lblUsedXXL" runat="server" Text="0"></asp:Label>/
                                                     <asp:Label ID="lblAllotedXXL" runat="server" Text="0"></asp:Label>
                                                     
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

