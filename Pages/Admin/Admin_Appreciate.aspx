<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_Appreciate.aspx.cs" Inherits="Pages_Admin_Admin_Appreciate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/CommonJS/toster.js"></script>
    <script src="../../JS/CommonJS/bootstrap-datepicker_fun.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
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
    <style>
        .bg{
            background-color:#C2D69B;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnShow').click(function () {

                //$("#frame").attr("src", "../../Certificate/Appreciate/3 Star_MG01040_mallikarjun.tech@dfmail.org.pdf");
                window.open("../../Certificate/Appreciate/3 Star_MG01040_mallikarjun.tech@dfmail.org.pdf", "width=500,height=500,top=100,left=500");
            });
            function myFunction(filename) {
                window.open("../../Certificate/Appreciate/" + filename + ".pdf", "width=500,height=500,top=100,left=500");
            }
        });
    </script>
    <script type="text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {
                        inputList[i].checked = true;

                        //  row.addClass("bg");

                    }
                    else {
                        //  row.removeClass("bg");
                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            //  row.style.backgroundColor = "#C2D69B";

                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
         <%-- <a href="#" id="btnShow">this link</a> --%>
        <div id="dialog" style="display: none;">
            <div>
                <iframe id="frame"></iframe>
            </div>
        </div>
        <ul class="nav nav-tabs md-tabs nav-justified primary-color" role="tablist">
            <li class="nav-item">
                <asp:LinkButton ID="btnTabSelectAward" role="tab" OnClick="btnTabSelectAward_Click" CssClass="text-center brandFont" runat="server"><span class="menu-active animated"><i class="fa fa-user-secret"></i></span>&nbsp; Appreciation Selection &nbsp;
                </asp:LinkButton>

            </li>
            <li class="nav-item">
                <asp:LinkButton ID="btnTabSendCertificate" role="tab" OnClick="btnTabSendCertificate_Click" CssClass="text-center brandFont" runat="server"><span class="menu-active animated"><i class="fa fa-user-secret"></i></span>&nbsp; Send Certificate &nbsp;
                </asp:LinkButton>

            </li>
        </ul>
        <div class="tab-content">

            <!-- Panel 1 -->
            <div id="Selection" runat="server" role="tabpanel">
                <div class="row">
                    <div class="col-md-3">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>From Date
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ValidationGroup="Certificate"
                                                ForeColor="DarkRed" ControlToValidate="txtFromDate" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtFromDate" CssClass="form-control form-control-feedback datepicker" autocomplete="off" placeholder="dd-mm-yyyy" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>To Date
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" ValidationGroup="Certificate"
                                                ForeColor="DarkRed" ControlToValidate="txtToDate" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtToDate" CssClass="form-control form-control-feedback datepicker" autocomplete="off"  placeholder="dd-mm-yyyy" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <div class="col-md-12">
                                        <label>Appreciation Star
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" ValidationGroup="Certificate"
                                                ForeColor="DarkRed" ControlToValidate="txtToDate" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Select Star" Value=""></asp:ListItem>
                                            <asp:ListItem Text="3 Star" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="2 Star" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="row form-group">
                            <div class="col-md-12">
                                <label for="ddlProjectType">Select Program</label>
                                <asp:DropDownList ID="ddlprogramId"  OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                  
                                </asp:DropDownList>
                            </div>
                        </div>

                                <div class="row form-group">
                                    <div class="col-md-12">
                                        <label>Select Manager</label>
                                        <asp:DropDownList ID="ddlManager" OnSelectedIndexChanged="ddlManager_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <div class="col-md-12">
                                        <label>Select College</label>
                                        <asp:DropDownList ID="ddlCollegeName" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                               <%--   <div class="row form-group">
                                    <div class="col-md-12">
                                        <label>
                                            Program
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" ValidationGroup="Certificate"
                                                    ForeColor="DarkRed" ControlToValidate="txtToDate" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:DropDownList ID="ddlclgtype" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Select " Value=""></asp:ListItem>
                                            <asp:ListItem Text="Skillplus-R" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Skillplus" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>

                              <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>--%>
                                        <div class="row form-group">
                                                <div class="col-md-6">
                                                <asp:LinkButton ID="btnListingReport" OnClick="btnListingReport_Click" ValidationGroup="Certificate" CssClass="btn btn-block btn-primary" runat="server">Report </asp:LinkButton>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:LinkButton ID="btnSearch" OnClick="btnSearch_Click" ValidationGroup="Certificate" CssClass="btn btn-block btn-info" runat="server">Search </asp:LinkButton>
                                            </div>
                                        </div>
                                <%--    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <div class="row form-group">
                                        
                                    <div class="col-md-12">
                                        <asp:Button ID="bntGenerateCertificate" OnClick="bntGenerateCertificate_Click" ValidationGroup="Certificate" CssClass="btn btn-block btn-danger" Text="Generate Certificate" runat="server" />
                                    </div>
                              
                                </div>
                                 <div class="row ">
                                          
                                        </div>
                               <!-- <br /> -->
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <h3 class="text-center">Total Students</h3>
                                            </div>
                                            <div class="panel-body">
                                                <h3 class="text-center">
                                                    <asp:Label ID="lblTotal_Students" runat="server" Text="0" Font-Size="Larger"  Font-Bold="true"></asp:Label>
                                                </h3>
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-9">
                        <div class="panel">
                            <div class="panel-body" style="overflow: auto; height: 650px;">

                                <div class="row">
                        
                                    <div class="col-md-12">

                                        <asp:CheckBox ID="ChkSelectAll" CssClass="checkbox checkbox-danger" Text="Select All Students" onclick="checkAll(this);" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="rptStudents" runat="server">
                                                    <HeaderTemplate>

                                                        <table class="table table-hover" id="SearchList">
                                                            <thead>
                                                                <tr style="background-color: #13c4f5; color: #fff">
                                                                    <td>Slno</td>

                                                                    <td><strong><b>Student Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></strong>
                                                                    <td><strong><b>LeadId</b></strong>
                                                                    </td>
                                                                    <td><strong><b>Mobile No</b></strong></td>
                                                                    <td><strong><b>Mail Id</b></strong></td>
                                                                    <td><strong><b>College Name</b></strong></td>
                                                                    <td><strong><b>Manager Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></strong></td>

                                                                    <td><strong><b>Projects</b></strong></td>
                                                                      <td><strong><b>Semester</b></strong></td>
                                                                </tr>
                                                            </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <%#Container.ItemIndex+1 %>
                                                                </td>

                                                                <td style="display: none;">

                                                                    <asp:Label ID="lblRegistrationId" runat="server" Text='<% #Eval("RegistrationId") %>'></asp:Label>

                                                                </td>

                                                                <td style="letter-spacing: 2px;">
                                                                    <asp:CheckBox ID="ChkCertificate" Font-Size="Smaller" ForeColor="Black" Text='<% #Eval("StudentName") %>' runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblLead_Id" runat="server" Text='<% #Eval("lead_id") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblMobileNo" runat="server" Text='<% #Eval("MobileNo") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblEmailId" runat="server" Text='<% #Eval("mailid") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblInstituteName" runat="server" Text='<% #Eval("College_Name") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblManagerName" runat="server" Text='<% #Eval("ManagerName") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblProjectCount" runat="server" Text='<% #Eval("Project_Count") %>'></asp:Label>
                                                                </td>
                                                                  <td>
                                                                    <asp:Label ID="lblSemester" runat="server" Text='<% #Eval("SemName") %>'></asp:Label>
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

            <div id="Send" runat="server" role="tabpanel">
                <div class="row">
                    <div class="col-md-1">
                        <label>Select Year</label>
                        <asp:DropDownList ID="ddlAcademicYear" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-md-2"> 
                        <label for="ddlProjectType"> Program</label>
                        <asp:DropDownList ID="ddlprogram"  OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"> </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>Appreciation Star</label>
                        <asp:DropDownList ID="ddlAppreciate_Type" OnSelectedIndexChanged="ddlAppreciate_Type_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                            <asp:ListItem Text="[All]" Value="[All]"></asp:ListItem>
                            <asp:ListItem Text="3 Star" Value="3"></asp:ListItem>
                            <asp:ListItem Text="2 Star" Value="2"></asp:ListItem>
                            <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                       <div class="col-md-2">
                        <label>Mail Status</label>
                        <asp:DropDownList ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                            <asp:ListItem Text="[All]" Value="[All]"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Sent" Value="2"></asp:ListItem>
                           
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label>Add Mobile Notification text</label>
                        <asp:TextBox ID="txtNotificationText" CssClass="form-control" Text="Leadership Certificate for your initiative" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                          <br />
                        <asp:Button ID="btnSend" OnClick="SendEmail" CssClass="btn btn-block btn-info" Text="Send Email" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <br />
                        <asp:LinkButton ID="btnSendCertificateReport" OnClick="btnSendCertificateReport_Click" CssClass="btn btn-block btn-primary" runat="server">Report </asp:LinkButton>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-2">
                        <br />
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h4>Total Mails  (<asp:Label ID="lblTotal_Sending_Summary" runat="server" Text="0" CssClass="text-center" Font-Size="Large" Font-Bold="true"></asp:Label>) </h4>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h4>Pending -    
                                     
                                            <asp:Label ID="lblPending" runat="server" Font-Size="Larger" Text="0" Font-Bold="true"></asp:Label>
                                       
                                            </h4>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <h4>Sent -     
                                     
                                            <asp:Label ID="lblSent" runat="server"  Font-Size="Larger" Text="0" Font-Bold="true"></asp:Label>
                                         </h4>
                                    </div>
                                </div>
                             
                                <div class="row" style="display:none;">
                                       <hr />
                                    <div class="col-md-12">
                                        <h4>Failed -      
                                    
                                            <asp:Label ID="lblError" runat="server"  Font-Size="Larger" Text="0" Font-Bold="true"></asp:Label>
                                       </h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-10">
                        <div class="panel">
                            <div class="panel-body" style="overflow: auto; height: 650px;">

                                <div class="row">
                                    <div class="col-md-12">

                                        <asp:CheckBox ID="CheckBox1" CssClass="checkbox checkbox-danger" Text="Select All Students" onclick="checkAll(this);" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="rptSendCertificate" OnItemDataBound="rptSendCertificate_ItemDataBound" OnItemCommand="rptSendCertificate_ItemCommand" runat="server">
                                                    <HeaderTemplate>

                                                        <table class="table table-hover" id="SearchList">
                                                            <thead>
                                                                <tr style="background-color: #13c4f5; color: #fff">
                                                                    <td>Slno</td>
                                                                    <td><strong><b>Student Name</b></strong>
                                                                    <td><strong><b>LeadId</b></strong></td>
                                                                    <td><strong><b>Mobile No</b></strong></td>
                                                                    <td><strong><b>Mail Id</b></strong></td>
                                                                    <td><strong><b>College Name</b></strong></td>
                                                                    <td style="width:12%"><strong><b>Manager Name</b></strong></td>
                                                                    <td><strong><b>Projects</b></strong></td>
                                                                     <td><strong><b>Level&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></strong></td>
                                                                      <td style="display:none;"><strong><b>Status</b></strong></td>
                                                                     <td><strong><b>View/Status</b></strong></td>
                                                                </tr>
                                                            </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <%#Container.ItemIndex+1 %>
                                                                </td>

                                                                <td style="display: none;">
                                                                    <asp:Label ID="lblSlno" runat="server" Text='<% #Eval("slno") %>'></asp:Label>
                                                                    <asp:Label ID="lblRegistrationId" runat="server" Text='<% #Eval("Student_Id") %>'></asp:Label>
                                                                    <asp:Label ID="lblStatus" Text='<% #Eval("Certificate_Status") %>' runat="server"></asp:Label>
                                                                     <asp:Label ID="lblAppreciation_Type" runat="server" Text='<% #Eval("Appreciation_Type") %>'></asp:Label>
                                                                </td>

                                                                <td style="letter-spacing: 2px;">
                                                                    <asp:CheckBox ID="ChkCertificate" Font-Size="Smaller" ForeColor="Black" Text='<% #Eval("StudentName") %>' runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblLead_Id" runat="server" Text='<% #Eval("lead_id") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblMobileNo" runat="server" Text='<% #Eval("MobileNo") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblEmailId" runat="server" Text='<% #Eval("mailid") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblInstituteName" runat="server" Text='<% #Eval("College_Name") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblManagerName" runat="server" Text='<% #Eval("ManagerName") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblProjectCount" runat="server" Text='<% #Eval("Projects_Count") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblLevel" runat="server" Text='<% #Eval("Appreciation_Type") %>'></asp:Label>
                                                                </td>
                                                                <td style="display:none;">
                                                                    <asp:Label ID="lblNewStatus" runat="server"></asp:Label>
                                                                </td>
                                                              <%--  <td>
                                                                    <asp:LinkButton ID="btnView" CssClass="btn btn-floating btn-info" CommandArgument='<%# Eval("Appreciation_Type")+"_"+Eval("lead_id")+"_"+Eval("mailid")+"_"+Eval("slno")+"_"+Eval("StudentName") +"_"+Eval("Certificate_Status") %>' runat="server"><span class="fa fa-eye"></span> </asp:LinkButton>
                                                                </td>--%>
                                                                <td>
                                                                <a href='#'  class='<%# Eval("Status_Label")%>'  onclick='window.open("../../Certificate/Appreciate/<%# Eval("Appreciation_Type")+"_"+Eval("lead_id")+"_"+Eval("mailid")%>.pdf","scrollbars=yes,width=500,height=500,top=100,left=500"); return true;'>
                                                                    <%# Eval("Sending_Status")%>
                                                                </a>
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

        </div>




    </div>
    <br />
    <div id="POP_Confirm" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 60%;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h2>Are You Sure You Want to Send Mail ?
                    </h2>
                    <div class="row">
                        <div class="col-md-offset-2 col-md-2">
                            <asp:LinkButton ID="btnNo" OnClick="btnNo_Click" CssClass="btn btn-danger" runat="server">NO</asp:LinkButton>
                        </div>
                        <div class="col-md-offset-3 col-md-2">
                            <asp:Button ID="btnYes" OnClick="btnYes_Click" CssClass="btn btn-info" runat="server" OnClientClick="this.disabled = true;this.value='Sending Mail Please Wait...';" UseSubmitBehavior="False" Text="Send Mail" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="POP_PDFView" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 60%;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Label ID="lblCertificateUserName" runat="server" Text=""></asp:Label></h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <asp:Literal ID="ltEmbed" runat="server" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function POP_Confirm() {
            $('#POP_Confirm').modal({
                show: true
            });
        }
    </script>
    <script type="text/javascript">
        function POP_PDFView() {
            $('#POP_PDFView').modal({
                show: true
            });
        }
    </script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // Date Picker

            jQuery('.datepicker').datepicker({
                format: "dd-mm-yyyy",
                autoclose: true,
                todayHighlight: true,

            });
        });

    </script>
</asp:Content>
