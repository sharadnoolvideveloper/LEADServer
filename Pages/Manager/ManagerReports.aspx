<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="ManagerReports.aspx.cs" Inherits="Pages_Manager_ManagerReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <asp:Label ID="ll" runat="server" Text=""></asp:Label>
    <div class="container-fluid">
       <div class="panel">
           <div class="panel-body">
               <div class="row">
                   <div class="col-md-2">
                       <h4>Manager Reports
                       </h4>
                   </div>
                   <div class="col-md-2">
                      <asp:DropDownList ID="ddlAcademicYear" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                   </div>
               </div>

               <br />
                <div class="row">
                    <div class="col-md-3 hidden">
                        <div class="panel panel-danger">
                            <div class="panel-heading">
                                <h4>Funding Report                                   
                                </h4>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="btnGenerateListingReports" OnClick="btnGenerateListingReports_Click" CssClass="btn btn-info" runat="server">Listing Report</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                     <div class="col-md-4 hidden">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h4>Project Type Report
                                    <span class="pull-right">
                                        <asp:DropDownList ID="ddlProjectType" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="[All]" Value="[All]"></asp:ListItem>
                                             <asp:ListItem Text="Proposed" Value="Proposed"></asp:ListItem>
                                             <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                             <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                             <asp:ListItem Text="RequestForModification" Value="RequestForModification"></asp:ListItem>
                                            <asp:ListItem Text="RequestForCompletion" Value="RequestForCompletion"></asp:ListItem>
                                            <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </h4>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="btnGenerateProjectStatusWiseReport" OnClick="btnGenerateProjectStatusWiseReport_Click" CssClass="btn btn-Primary" runat="server">Listing Report</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                     <div class="col-md-4 hidden">
                        <div class="panel panel-warning">
                            <div class="panel-heading">
                                <h4>Document Create
                                   
                                </h4>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="btnGenerateDocuments" OnClick="btnGenerateDocuments_Click" CssClass="btn btn-Primary" runat="server">Listing Report</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                   

                    
                </div>
                 <div class="row">
                      <div class="col-md-4">
                         <div class="panel panel-success">
                            <div class="panel-heading">
                                <h4>Download Student All Documents
                                   
                                </h4>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtFolderFromDate" placeholder="From Date" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                        </div>
                                         <div class="col-md-6">
                                            <asp:TextBox ID="txtFolderToDate" placeholder="To Date" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="btnGenerateFolderWiseData" OnClick="btnGenerateFolderWiseData_Click" CssClass="btn btn-info" runat="server">Folder Wise List</asp:LinkButton>
                                        </div>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                     </div>
                    <div class="col-md-4">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h4>Funding Report 
                                   
                                </h4>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtFromDate" placeholder="From Date" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                        </div>
                                         <div class="col-md-6">
                                            <asp:TextBox ID="txtToDate" placeholder="To Date" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="btnGenerateFundingBetweenDates" OnClick="btnGenerateFundingBetweenDates_Click" CssClass="btn btn-info" runat="server">Get Report</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                 </div>
              
           </div>
       </div>
             
    </div>
     <div id="ErrorModal" class="modal fade" role="dialog" style="margin-top: 0px">
        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>Message</h3>
                    <h2>
                        <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
                    </h2>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <a class="pull-right text-right" data-dismiss="modal">
                                <i class="fa fa-arrow-up text-primary fa-2x"></i>
                            </a>

                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
      <script type="text/javascript">
        function ErrorModal() {
            $('#ErrorModal').modal('show');

        }
    </script>
      <script type="text/javascript">

        jQuery(document).ready(function () {
            // Date Picker
            jQuery('.datepicker').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true,
                todayHighlight: true
            });
        });
    </script>
</asp:Content>

