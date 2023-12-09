<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="ManagerSummery.aspx.cs" Inherits="Pages_Manager_ManagerSummery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
        <div class="panel">
            <div class="row">
                <div class="col-md-4">
                     <h1 style="margin-left:20px;"> Summary Statement </h1>
                </div>
                <div class="col-md-2">
                    <br />
                    <asp:DropDownList ID="ddlAcademicCode" CssClass="form-control" OnSelectedIndexChanged="ddlAcademicCode_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>
            </div>
       
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="row text-center">
                        <div class="col-md-3">
                            <div class="panel panel-primary">
                                <div class="panel-heading">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4>Student Registrations</h4>
                                            <h3>
                                                <asp:Label ID="lblTotalCount" runat="server" Text=""></asp:Label></h3>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-3">
                            <div class="panel panel-warning">
                                <div class="panel-heading">

                                    <div class="row">

                                        <div class="col-md-12">
                                            <h4>Total Projects</h4>
                                            <h3>
                                                <asp:Label ID="lbltotalProjects" runat="server" Text=""></asp:Label></h3>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-3">
                            <div class="panel panel-success">
                                <div class="panel-heading">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4>Proposed</h4>
                                            <h3>
                                                <asp:Label ID="lblTotalProposed" runat="server" Text=""></asp:Label></h3>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4>Approved</h4>
                                            <h3>
                                                <asp:Label ID="lblTotalApproved" runat="server" Text=""></asp:Label></h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row text-center">
                        <div class="col-md-3">
                            <div class="panel panel-warning">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4>Completion Request</h4>
                                            <h3>
                                                <asp:Label ID="lblTotalRequestForCompletion" runat="server" Text=""></asp:Label></h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="panel panel-success">
                                <div class="panel-heading">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4>Completed</h4>
                                            <h3>
                                                <asp:Label ID="lblTotalCompleted" runat="server" Text=""></asp:Label></h3>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="panel panel-primary">
                                <div class="panel-heading">

                                    <div class="row">

                                        <div class="col-md-12">
                                            <h4>Modification Requested</h4>
                                            <h3>
                                                <asp:Label ID="lblTotalRequestForModification" runat="server" Text=""></asp:Label></h3>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="panel panel-danger">
                                <div class="panel-heading">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4>Rejected Projects</h4>
                                            <h3>
                                                <asp:Label ID="lblTotalRejected" ForeColor="Red" runat="server" Text=""></asp:Label></h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4" style="display: none">
                            <div class="row">
                                <div class="col-md-12">
                                    <span class="fa fa-male fa-2x"></span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMale" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4" style="display: none">
                            <div class="row">
                                <div class="col-md-12">
                                    <span class="fa fa-female fa-2x"></span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblFemale" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>


            </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Requested Amount</h4>
                                    <h3>
                                        <asp:Label ID="lblRequestedAmount" runat="server" Text=""></asp:Label></h3>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-md-3">
                    <div class="panel panel-warning">
                        <div class="panel-heading">

                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Approved Amount</h4>
                                    <h3>
                                        <asp:Label ID="lblSanctionAmount" runat="server" Text=""></asp:Label></h3>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Released Amount</h4>
                                    <h3>
                                        <asp:Label ID="lblReleaseAmount" runat="server" Text=""></asp:Label></h3>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
               
                <div class="col-md-3">
                    <div class="panel parsley-error panel-danger">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Balance Amount</h4>
                                    <h3>
                                        <asp:Label ID="lblBalanceAmount" runat="server" Text=""></asp:Label></h3>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
   
        <div style="height: 500px">
        </div>
    </div>

</asp:Content>

