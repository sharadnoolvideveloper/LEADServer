<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="ListringReport.aspx.cs" Inherits="Pages_Manager_ListringReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
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
                                <label for="ddlProjectType">Select Project Type</label>
                                <asp:DropDownList ID="ddlProjectType" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="[All]" Value="[All]"></asp:ListItem>
                                    <asp:ListItem Text="Proposed" Value="Proposed"></asp:ListItem>
                                    <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                    <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                    <asp:ListItem Text="Modification Request" Value="RequestForModification"></asp:ListItem>
                                    <asp:ListItem Text="Completion Request" Value="RequestForCompletion"></asp:ListItem>
                                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                     <asp:ListItem Text="Impact Projects" Value="Impact"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label for="ddlStudentType">Select Student Status</label>
                                <asp:DropDownList ID="ddlStudentType" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="[All]" Value="[All]"></asp:ListItem>
                                    <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                                     <asp:ListItem Text="Initiator" Value="Initiator"></asp:ListItem>
                                     <asp:ListItem Text="Change Maker" Value="Change Maker"></asp:ListItem>
                                    <asp:ListItem Text="LEADer" Value="LEADer"></asp:ListItem>
                                    <asp:ListItem Text="Master Leader" Value="Master Leader"></asp:ListItem>
                                    <asp:ListItem Text="Lead Ambassador" Value="Lead Ambassador"></asp:ListItem>

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
                            <div class="col-md-3">
                                <h4>Student Listing &nbsp; (
                            <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                                    )  </h4>
                            </div>
                            <div class="col-md-9">
                                <div class="input-group pull-right">
                                    <div class="input-group-addon bg-info">
                                        <span class="input-group-text">Search</span>
                                    </div>
                                    <input type="text" id="txtStudentSearch" placeholder="Search for LEAD ID / Title / Institute / etc." class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12" id="main" runat="server">

                                    <div id="Listing_table" style="max-height: 680px; overflow: auto;" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $("#txtStudentSearch").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#Listing tr:not(:first)").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
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