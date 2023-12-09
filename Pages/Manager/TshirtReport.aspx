<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="TshirtReport.aspx.cs" Inherits="Pages_Manager_TshirtReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/toster.js"></script>

    <style>
        .table-fixed thead {
            width: 97%;
        }

        .table-fixed tbody {
            height: 230px;
            overflow-y: auto;
            width: 100%;
        }

        .table-fixed thead, .table-fixed tbody, .table-fixed tr, .table-fixed td, .table-fixed th {
            display: block;
        }

            .table-fixed tbody td, .table-fixed thead > tr > th {
                float: left;
                border-bottom-width: 0;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <br />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-3">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="txtFromDate">
                                    <asp:RequiredFieldValidator ValidationGroup="Report" ControlToValidate="txtFromDate" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="* Select From Date" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtFromDate" CssClass="form-control datepicker" autocomplete="off" placeholder="From Date" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtToDate">
                                    <asp:RequiredFieldValidator ValidationGroup="Report" ControlToValidate="txtToDate" ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="* Select To Date" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                <asp:TextBox ID="txtToDate" CssClass="form-control datepicker" autocomplete="off" placeholder="To Date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">

                        <div class="row form-group">
                            <div class="col-md-12">
                                <label for="ddlProjectType">Select tshirt size
                                     
                                </label>

                                <asp:DropDownList ID="ddlSize" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="[All]" Value="[All]"></asp:ListItem>
                                    <asp:ListItem Text="S" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="L" Value="L"></asp:ListItem>
                                    <asp:ListItem Text="XL" Value="XL"></asp:ListItem>
                                    <asp:ListItem Text="XXL" Value="XXL"></asp:ListItem>                               
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label for="ddlStudentType">Select College</label>
                                <asp:DropDownList ID="ddlCollege" CssClass="form-control" runat="server">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="btnGenerate" CssClass="btn btn-primary btn-block" ValidationGroup="Report" OnClick="btnGenerate_Click" runat="server">Generate <span class="fa fa-arrow-right pull-right"></span> </asp:LinkButton>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <asp:LinkButton ID="btnPDFReport" OnClick="btnPDFReport_Click" ValidationGroup="Report" CssClass="btn btn-danger  btn-block" runat="server"><span class="fa fa-file-pdf-o"></span> &nbsp; PDF </asp:LinkButton>
                            </div>
                            <div class="col-md-6">
                                <asp:LinkButton ID="btnExcelReport" OnClick="btnExcelReport_Click" ValidationGroup="Report" CssClass="btn btn-success  btn-block" runat="server"><span class="fa  fa-file-excel-o"></span> &nbsp; Excel </asp:LinkButton>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="col-md-9">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <div class="row">
                           
                            <div class="col-md-12">
                                <div class="input-group pull-right">
                                    <div class="input-group-addon bg-info">
                                        <span class="input-group-text">Search</span>
                                    </div>
                                    <input type="text" id="txtStudentSearch" onkeyup="SearchStudentDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12"  style="width: 100%; height: 650px; overflow: auto" id="SearchList">
                                      <asp:Repeater ID="rptTshirt"  runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-hover" style="width: 200%">
                                                                    <thead>
                                                                        <tr style="background-color: #27a9e3; color: #fff">
                                                                                                                                               
                                                                         
                                                                            <td style="text-align: center"><strong><b>LEAD ID</b><strong>
                                                                            </td>
                                                                            <td><strong><b>Student Name</b><strong>
                                                                            </td>
                                                                            <td><strong><b>MobileNo</b><strong>
                                                                            </td>
                                                                         
                                                                            <td><strong><b>College Name</b><strong>
                                                                            </td>
                                                                               <td><strong><b>Course Name</b><strong>
                                                                            </td>
                                                                               <td><strong><b>Semester Name</b><strong>
                                                                            </td>
                                                                            <td><strong><b>Tshirt Size</b><strong>
                                                                            </td>
                                                                            <td><strong><b>Status</b><strong>
                                                                            </td>
                                                                            <td><strong><b>Remark
                                                                                        </b><strong>
                                                                            </td>
                                                                            <td style="text-align: center"><strong><b>Requested Date</b><strong>
                                                                            </td>
                                                                               <td style="text-align: center"><strong><b>Sanction Date</b><strong>
                                                                            </td>
                                                                               <td style="text-align: center"><strong><b>Rejected Date</b><strong>
                                                                            </td>
                                                                               <td style="text-align: center"><strong><b>Exchange Date</b><strong>
                                                                            </td>
                                                                               <td style="text-align: center"><strong><b>Reapply reson</b><strong>
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tbody>
                                                                    <tr>
                                                                       
                                                                    
                                                                        <td style="width:auto; text-align: center;">
                                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                        </td>
                                                                        <td style="width:auto;">
                                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                        </td>
                                                                        <td style="width: auto;">
                                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("mobileno") %>' />
                                                                        </td>
                                                                        <td style="width:auto;">
                                                                            <asp:Label ID="lblCollegeName" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                        </td>
                                                                         <td style="width:auto;">
                                                                            <asp:Label ID="lblCourseName" Font-Size="Small" runat="server" Text='<%# Eval("CourseName") %>' />
                                                                        </td>
                                                                         <td style="width:auto;">
                                                                            <asp:Label ID="lblSemName" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />
                                                                        </td>
                                                                        <td style="width:auto; text-align: center;">
                                                                            <asp:Label ID="lblTshirtSize" Font-Size="Small" runat="server" Text='<%# Eval("tshirtsize") %>' />
                                                                        </td>
                                                                       
                                                                        <td style="width:auto;">
                                                                            <asp:Label ID="lblStatus" Font-Size="Small" runat="server" Text='<%# Eval("status") %>' />
                                                                        </td>
                                                                        <td style="width:auto;">
                                                                            <asp:Label ID="lblRemark" Font-Size="Small" runat="server" Text='<%# Eval("remark") %>' />
                                                                        </td>
                                                                        <td style="width:auto;">
                                                                            <asp:Label ID="lblRequestedDate" Font-Size="Small" runat="server" Text='<%# Eval("RequestedDate") %>' />
                                                                        </td>
                                                                        <td style="width:auto;">
                                                                            <asp:Label ID="lblSanctionDate" Font-Size="Small" runat="server"  Text='<%# Eval("SanctionDate") %>'/>
                                                                        </td>
                                                                        <td style="width:auto; text-align: center;">
                                                                            <asp:Label ID="lblRejectedDate" Font-Size="Small" runat="server" Text='<%# Eval("RejectedDate") %>' />
                                                                        </td>
                                                                          <td style="width: auto; text-align: center;">
                                                                            <asp:Label ID="lblExchangeDate" Font-Size="Small" runat="server" Text='<%# Eval("ExchangeDate") %>' />
                                                                        </td>
                                                                          <td style="width: auto; text-align: center;">
                                                                            <asp:Label ID="lblReapplyReson" Font-Size="Small" runat="server" Text='<%# Eval("ReapplyReson") %>' />
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
        jQuery(document).ready(function () {
            // Date Picker

            jQuery('.datepicker').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true,
                todayHighlight: true,

            });
        });

    </script>

     <script type="text/javascript">
        function SearchStudentDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtStudentSearch");
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

