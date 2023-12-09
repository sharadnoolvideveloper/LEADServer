<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_Consoliated_Report.aspx.cs" Inherits="Pages_Admin_Admin_Consoliated_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="vmTop" runat="server">
                    <div class="container">
                          <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="brandFont2">
                                       Welcome to Consoliated Report
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label>
                                                    Select From Date
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required"
                                                           ForeColor="Red" ValidationGroup="Report" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtFromDate" CssClass="form-control form-control-feedback datepicker"
                                                    autocomplete="off" placeholder="From  Date" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    Select To Date
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required"
                                                        ForeColor="Red" ValidationGroup="Report" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtToDate" CssClass="form-control form-control-feedback datepicker"
                                                    autocomplete="off" placeholder="To  Date" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="ddlProjectType">Program</label>
                                                <asp:DropDownList ID="ddlprogramId"  CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">
                                                <br />
                                                <asp:Button ID="btnGenerateReport" CssClass="btn btn-primary brandFont2" OnClick="btnGenerateReport_Click" runat="server" Text="Generate Report" ValidationGroup="Report" 
                                                    OnClientClick=" if ( Page_ClientValidate() ) { this.value='Report Generating..'; this.disabled=true; }" UseSubmitBehavior="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                  
                </asp:View>
                <asp:View ID="vmContent" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-danger">
                             
                                    <div class="panel-body">
                                            <h4>
                                          
                                            <span class="pull-right">
                                                          <asp:LinkButton ID="btnExcel" OnClick="btnExcel_Click" CssClass="btn btn-warning" runat="server">
                                                     <span class="fa fa-file-excel-o"></span>
                                                </asp:LinkButton>
                                                     <asp:LinkButton ID="btnMultipleSheets" OnClick="btnMultipleSheets_Click" CssClass="btn btn-info" runat="server">
                                                     <span class="fa fa-files-o"></span>
                                                </asp:LinkButton>
                                               

                                            </span>
                                         
                                              <span>
                                                <asp:LinkButton ID="btnBack" OnClick="btnBack_Click" CssClass="btn btn-primary btn-floating" runat="server">
                                                     <span class="fa fa-home"></span>
                                                </asp:LinkButton> &nbsp;
                                          <asp:Label ID="lblContentTitle"  runat="server" Text=""></asp:Label>

                                            </span>
                                         
                                        </h4>
                                        
                                        <div class="row">
                                                   <div class="col-md-12" style="width: 100%; height: 680px; overflow: auto" >
                                                <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">
                                                    <ContentTemplate>
                                                      
                                                        <asp:Repeater ID="rptConsoliatedReport"  runat="server">
                                                            <HeaderTemplate>
                                                                <div class="table-responsive text-small">
                                                                    <table class="table table-striped table-bordered table-hover b-t text-small" border="1">

                                                                        <thead class="hoverable">
                                                                            <tr>
                                                                                <th colspan="2"  style="background-color:#ffff00;">Names
                                                                                </th>
                                                                                <th colspan="2" class="text-center"  style="background-color:#ffff00;">Registered
                                                                                </th>
                                                                                <th colspan="7" class="text-center"  style="background-color:#ffff00;">Project Status
                                                                                </th>
                                                                                <th class="text-center"  style="background-color:#ffff00;">Overall
                                                                                </th>
                                                                                <th colspan="4" class="text-center"  style="background-color:#ffff00;">Project
                                                                                </th>
                                                                                      <th colspan="5" class="text-center"  style="background-color:#ffff00;">Levels of Students
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <th style="background-color:#f2f2f2">slno
                                                                                </th>
                                                                                <th style="background-color:#f2f2f2">Program_Managers
                                                                                </th>
                                                                                <th style="background-color:#daeef3">Paid
                                                                                </th>
                                                                                <th style="background-color:#daeef3">UnPaid
                                                                                </th>
                                                                                <th style="background-color:#fde9d9;">Proposed
                                                                                </th>
                                                                                <th style="background-color:#fde9d9;">Approved
                                                                                </th>
                                                                                <th style="background-color:#fde9d9;">Completed
                                                                                </th>
                                                                                <th style="background-color:#fde9d9;">Rejected
                                                                                </th>
                                                                                <th style="background-color:#fde9d9;">RM
                                                                                </th>
                                                                                <th style="background-color:#fde9d9;">RC
                                                                                </th>
                                                                                <th style="background-color:#fde9d9;">Draft
                                                                                </th>
                                                                                <th style="background-color:#eaf1dd;">P_Total
                                                                                </th>
                                                                                <th style="background-color:#ddd9c3;">Initiator
                                                                                </th>
                                                                                <th style="background-color:#ddd9c3;">CM
                                                                                </th>
                                                                                <th style="background-color:#ddd9c3;">LEADer
                                                                                </th>
                                                                                <th  style="background-color:#ddd9c3;">Impact
                                                                                </th>
                                                                                  <th style="background-color:#e5dfec">Initiator
                                                                                </th>
                                                                                  <th style="background-color:#e5dfec">CM
                                                                                </th >
                                                                                  <th style="background-color:#e5dfec">LEADer
                                                                                </th>
                                                                                  <th style="background-color:#e5dfec">ML
                                                                                </th>
                                                                                  <th style="background-color:#e5dfec">LA
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tbody>
                                                                    <tr>
                                                                         <td style="width: 2%;">
                                                                            <asp:Label ID="lblSlno" Font-Size="Small" runat="server" Text='<%# Container.ItemIndex + 1 %>'/>
                                                                        </td>
                                                                        <td style="min-width: 50px;">
                                                                            <asp:Label ID="lblManagerName" Font-Size="Small" runat="server" Text='<%# Eval("ManagerName") %>' />
                                                                        </td>
                                                                        <td style="width: 5%;background-color:#daeef3;">
                                                                            <asp:Label ID="lblPaid" Font-Size="Small" runat="server" Text='<%# Eval("PaidCount") %>' />
                                                                        </td>
                                                                           <td style="width: 5%;background-color:#daeef3;">
                                                                            <asp:Label ID="lblUnPaid" Font-Size="Small" runat="server" Text='<%# Eval("unpaidcount") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#fde9d9;">
                                                                            <asp:Label ID="lblProposed" Font-Size="Small" runat="server" Text='<%# Eval("ProposedCount") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#fde9d9;">
                                                                            <asp:Label ID="lblApproved" Font-Size="Small" runat="server" Text='<%# Eval("ApprovedCount") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#fde9d9;">
                                                                            <asp:Label ID="lblCompleted" Font-Size="Small" runat="server" Text='<%# Eval("CompltedCount") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#fde9d9;">
                                                                            <asp:Label ID="lblRejected" Font-Size="Small" runat="server" Text='<%# Eval("RejectedCount") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#fde9d9;">
                                                                            <asp:Label ID="lblRM" Font-Size="Small" runat="server" Text='<%# Eval("RequestForModification") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#fde9d9;">
                                                                            <asp:Label ID="lblRC" Font-Size="Small" runat="server" Text='<%# Eval("RequestForCompletion") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#fde9d9;">
                                                                            <asp:Label ID="lblDraft" Font-Size="Small" runat="server" Text='<%# Eval("Drafted") %>' />
                                                                        </td>
                                                                        <td style="width: 5%;background-color:#eaf1dd;">
                                                                            <asp:Label ID="lblGrandTotal" Font-Size="Small" runat="server" Text='<%# Eval("GrandTotal") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#ddd9c3;">
                                                                            <asp:Label ID="lblp_Initiator" Font-Size="Small" runat="server" Text='<%# Eval("p_InitiatorCount") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#ddd9c3;">
                                                                            <asp:Label ID="lblp_ChangeMaker" Font-Size="Small" runat="server" Text='<%# Eval("p_ChangeMakerCount") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#ddd9c3;">
                                                                            <asp:Label ID="lblP_Leader" Font-Size="Small" runat="server" Text='<%# Eval("p_LeaderCount") %>' />
                                                                        </td>
                                                                          <td style="width: 5%;background-color:#ddd9c3;">
                                                                            <asp:Label ID="lbl_Impact" Font-Size="Small" runat="server" Text='<%# Eval("p_impactCount") %>' />
                                                                        </td>

                                                                           <td style="width: 5%;background-color:#e5dfec;">
                                                                            <asp:Label ID="lblL_Initiator" Font-Size="Small" runat="server" Text='<%# Eval("L_InitiatorCount") %>' />
                                                                        </td>
                                                                           <td style="width: 5%;background-color:#e5dfec;">
                                                                            <asp:Label ID="lblL_ChangeMaker" Font-Size="Small" runat="server" 
                                                                                Text='<%# Eval("L_ChangeMakerCount") %>' />
                                                                        </td>
                                                                           <td style="width: 5%;background-color:#e5dfec;">
                                                                            <asp:Label ID="lblL_LEADer" Font-Size="Small" runat="server" Text='<%# Eval("L_LeaderCount") %>' />
                                                                        </td>
                                                                           <td style="width: 5%;background-color:#e5dfec;">
                                                                            <asp:Label ID="lblL_MasterLeader" Font-Size="Small" runat="server" 
                                                                                Text='<%# Eval("LMasterLeaderCount") %>' />
                                                                        </td>
                                                                           <td style="width: 5%;background-color:#e5dfec;">
                                                                            <asp:Label ID="lblL_LeadAmbassador" Font-Size="Small" runat="server" 
                                                                                Text='<%# Eval("L_AmbassadorCount") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <tr style="height: 30px;">
                                                                    <td colspan="2"  style="background-color: #ffc000; ">Grand Total&nbsp;
                                                                    </td>
                                                                    <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalPaid" runat="server" Text='0' />&nbsp;
                                                                    </td>

                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalUnPaid" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalProposed" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalApproved" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalCompleted" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalRejected" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalRM" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalRC" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalDraft" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalProjects" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalPInitiator" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                      <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalPCM" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                       <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalpLeader" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                       <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalpImpact" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                       <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalLInitiator" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                       <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalLCM" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                       <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalL_Leader" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                       <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalL_ML" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                       <td style="background-color: #ffc000; ">
                                                                        <asp:Label ID="lblTotalL_LA" runat="server" Text='0' />&nbsp;
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                                </div>
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
                </asp:View>
            </asp:MultiView>
        </div>
    </div>



          <script type="text/javascript">
              jQuery(document).ready(function () {
                  // Date Picker

                  jQuery('.datepicker').datepicker({
                      format: "yyyy-mm-dd",
                      autoclose: true,
                      todayHighlight: true,

                  });
              });

          </script>
</asp:Content>

