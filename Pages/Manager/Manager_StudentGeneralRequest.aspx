<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="Manager_StudentGeneralRequest.aspx.cs" Inherits="Pages_Manager_Manager_GeneralRequest" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4 ># Student Request</h4>
    <div style="background-color: whitesmoke">
        <div class="row">
            <div class="col-md-12">
                <div class="panel">
                    <div class="panel-heading" style="background-color: antiquewhite;">
                        <div class="row">
                            <div class="col-md-1">
                                <asp:DropDownList ID="ddlAcademicYear" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <asp:DropDownList ID="ddlRequestStatus" OnSelectedIndexChanged="ddlRequestStatus_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="[All]" Value="[All]"></asp:ListItem>
                                    <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                    <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                                    <asp:ListItem Selected="True" Text="High" Value="High"></asp:ListItem>
                                     <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                                  
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-8">
                                <div class="input-group">
                                    <div class="input-group-addon bg-info">
                                        <span class="input-group-text">Search</span>
                                    </div>
                                    <asp:TextBox ID="txtStudentSearch" onkeyup="SearchStudentDetail()" placeholder="Search for LEAD ID / Ticket No / Request Message / etc." class="form-control" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblRequestCount" runat="server" CssClass="label label-default"></asp:Label>
                            </div>
                         <div class="col-md-1">
                             <asp:LinkButton ID="btnExcelDownload" OnClick="btnExcelDownload_Click" CssClass="btn btn-facebook btn-floating" runat="server">
                                 <span class="fa fa-file-excel-o"></span>
                             </asp:LinkButton>
                            </div>
                      
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12" style="width: 100%; height: 650px; overflow: auto" id="SearchList">
                                <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">

                                    <ContentTemplate>
                                        <asp:Repeater ID="rptStudentRequest" OnItemCommand="rptStudentRequest_ItemCommand" OnItemDataBound="rptStudentRequest_ItemDataBound" runat="server">
                                            <HeaderTemplate>

                                                <table class="table table-hover" style="overflow:auto;">
                                                    <thead>
                                                        <tr style="background-color: #27a9e3; color: #fff">
                                                            <td style="width:4%"><strong><b>Ticket No </b><strong></td>
                                                             <td style="width:16%"><strong><b>Request_Date</b><strong></td>
                                                             <td style="width:15%"><strong><b>Respond_Date</b><strong></td>
                                                            <td><strong><b>lead_id</b><strong></td>
                                                            <td style="width:10%"><strong><b>Student Name</b><strong></td>
                                                            <td><strong><b>Mobile_No</b><strong></td>
                                                            <td style="display:none;"><strong><b>Mailid</b><strong></td>
                                                            <td style="display:none;"><strong><b>Institution</b><strong></td>
                                                            <td><strong><b>Request_Type</b><strong></td>
                                                            <td style="width:20%"><strong><b>Request_Message</b><strong></td>
                                                             <td style="width:15%"><strong><b>Respond_Message</b><strong></td>
                                                               <td class="hidden-xs"><strong><b>Project_Title</b></strong>
                                                                </td>
                                                            <td style="width:4%"><strong><b>Priority</b><strong></td>
                                                           
                                                            <td style="display:none;"><strong><b>Status</b><strong>
                                                            </td>
                                                          
                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                             
                                                        <tr>

                                                            <td>
                                                                <span style="display:none;">
                                                                    # <asp:Label ID="lblRequestId" runat="server" Text='<%# Eval("Ticket_No") %>' />
                                                                </span>
                                                                
                                                               <asp:LinkButton ID="btnRqid" Font-Size="Smaller" Text='<%# "#"+Eval("Ticket_No") %>'   runat="server">
                                                                    
                                                                  
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRequestDate" Font-Size="Smaller" runat="server" Text='<%# Eval("Request_Date") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRespondDate" Font-Size="Smaller" runat="server" Text='<%# Eval("Response_Date") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblLeadId" Font-Size="Smaller" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStudentName" Font-Size="Smaller" runat="server" Text='<%# Eval("StudentName") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMobileNo" Font-Size="Smaller" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                            </td>
                                                            <td style="display: none;">
                                                                <asp:Label ID="lblEmailId" Font-Size="Smaller" runat="server" Text='<%# Eval("MailId") %>' />
                                                            </td>
                                                            <td style="display: none;">
                                                                <asp:Label ID="lblCollegeName" Font-Size="Smaller" runat="server" Text='<%# Eval("College_Name") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRequest_Type" Font-Size="Smaller" runat="server" Text='<%# Eval("Head_Name") %>' />
                                                                <span style="display: none;">
                                                                   <asp:Label ID="lblRequestHead_Id" Font-Size="Smaller" runat="server" Text='<%# Eval("Request_Head_Id") %>' />
                                                                     <asp:Label ID="lblPDID" Font-Size="Small" runat="server" Text='<%# Eval("PDID") %>' />
                                                                </span>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblRequestMessage" Font-Size="Smaller" runat="server" Text='<%# Eval("Request_Message") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRespondMessage" Font-Size="Smaller" runat="server" Text='<%# Eval("Response_Message") %>' />
                                                            </td>
                                                               <td style="width: 20%;" >
                                                               <asp:Label ID="lblProjectTitle" Font-Size="Smaller" runat="server" Text='<%# Eval("ProjectTitle") %>'></asp:Label>
                                                        </td>
                                                            <td style="cursor:pointer;">
                                                                      <asp:LinkButton ID="btnResponse" Font-Size="Medium"  runat="server">
                                                                              <asp:Label ID="lblRequestPriority" Font-Size="Smaller" runat="server" Text='<%# Eval("Request_Priority") %>' />
                                                                </asp:LinkButton>
                                                            
                                                            </td>
                                                            <td style="width:4%;display: none;">
                                                                <asp:Label ID="lblStatus" Font-Size="Smaller" runat="server" Text='<%# Eval("Status") %>' />
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
    </div>

    <div id="POP_ManagerResponse" class="modal fade " role="dialog" style="margin-top: 0px; margin-left: 10px;">
        <div class="modal-dialog " style="width: 60%; overflow: auto; max-height: 700px;">
         
                     <div class="modal-content">
                <div class="modal-header">
                    <h5>Ticket No :&nbsp; [<asp:Label ID="lblRequestId_POP" Font-Bold="true" runat="server" Text=""></asp:Label>]
                         <a class="pull-right text-right">Lead_Id :  
                             <asp:Label ID="lblLeadId_POP" Font-Size="Medium" Font-Bold="true" runat="server" Text=""></asp:Label>
                             &nbsp; <span class="fa fa-close" data-dismiss="modal" style="cursor:pointer;"></span>

                         </a>
                    </h5>
                </div>
                <div class="modal-body">
                    <div class="row form-group">
                        <asp:Label ID="lblProjectId" Visible="false" runat="server" Text="Label"></asp:Label>
                         <asp:Label ID="lblRequestHeadId_POP" Visible="false" runat="server" Text=""></asp:Label>
                       
                        <div class="col-md-3">
                              <label>Request Date</label>
                             <asp:TextBox ID="txtRequestDate" BackColor="ivory" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox> 
                        </div>
                        <div class="col-md-3">
                                <label>Student Name</label>
                             <asp:TextBox ID="txtStudentName" BackColor="ivory" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">                           
                                <label>Mobile No</label>
                              <asp:TextBox ID="txtMobileNo" BackColor="ivory" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>
                 
                        </div>
                            <div class="col-md-4">                           
                                <label>Mail id</label>
                              <asp:TextBox ID="txtMailId" BackColor="ivory" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>
                 
                        </div>
                   
                    </div>
                    <div class="row form-group">
                        <div class="col-md-12">
                               <label>College Name</label>
                              <asp:TextBox ID="txtCollegeName" BackColor="ivory" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                    </div>
                          
                    <div class="row form-group" id="Project" runat="server">
                        <div class="col-md-6">
                            <label>Project Title &nbsp;  <asp:CheckBox ID="ChkCreateDoc"  runat="server" Text="Generate Doc" /></label>
                              <asp:TextBox ID="txtProjectTitle" BackColor="ivory" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                           <div class="col-md-3">
                            <label>Valid From Date</label>
                              <asp:TextBox ID="txtFromDate" autocomplete="off"  CssClass="form-control datepicker" Font-Size="Small"  runat="server"></asp:TextBox>
                            </div>
                           <div class="col-md-3">
                            <label>Valid To Date</label>
                              <asp:TextBox ID="txtToDate" autocomplete="off"  CssClass="form-control datepicker" Font-Size="Small"  runat="server"></asp:TextBox>
                            </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-6">
                                <label>Request Message</label>
                              <asp:TextBox ID="txtRequestMessage" BackColor="ivory" CssClass="form-control disabled" TextMode="MultiLine" Rows="4" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                          <div class="col-md-6">
                                <label>Response Message &nbsp; <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtResponse" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Response"></asp:RequiredFieldValidator></label>
                              <asp:TextBox ID="txtResponse" CssClass="form-control" TextMode="MultiLine" Rows="4" Font-Size="Small" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group" >
                        <div class="col-md-4 pull-right">
                            
                            <asp:LinkButton ID="btnCloseTicket" CssClass="btn btn-block btn-facebook" OnClick="btnCloseTicket_Click" ValidationGroup="Response" runat="server">Close Ticket</asp:LinkButton>
                        </div>
                   
                    </div>
                </div>
            </div>

            <!-- Modal content-->
           

        </div>
    </div>

     <script type="text/javascript">
        function SearchStudentDetail() {
            var input, filter, ul, li, a, i;
            input =document.getElementById("<%= txtStudentSearch.ClientID %>");
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
          <script type="text/javascript">
              jQuery(document).ready(function () {
                  // Date Picker
                  var today = new Date();
                  var dd = today.getDate();

                  var mm = today.getMonth() + 1;
                  var yyyy = today.getFullYear();
                  var startdates = yyyy + '-' + mm + '-' + dd;

                  jQuery('.datepicker').datepicker({
                      format: "yyyy-mm-dd",
                      autoclose: true,
                      todayHighlight: true,
                      startDate: startdates

                  });
              });

        //function RemoveRows(ids)
        //{
        //    $(ids).parent('td').parent('tr').remove();           
        //}
    </script>
      <script type="text/javascript">
          function POP_ManagerResponse() {
              $('#POP_ManagerResponse').modal({
                  backdrop: 'static',
                  keyboard: true,
                  show: true
              });

          }
    </script>
</asp:Content>

