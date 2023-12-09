<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="AdminProjectDetails.aspx.cs" Inherits="Pages_Admin_AdminProjectDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>

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
    <div class="container-fluid">

        <div class="row">
            <div class="col-md-2 ">
                <div class="panel panel-info ">
                    <div class="panel-heading">
                        <h4>
                            <asp:Label ID="lblPageTitle" runat="server" Text="All Projects"></asp:Label>

                        </h4>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="list-group">



                                    <a href="AdminProjectDetails.aspx?vwType=AllProjects" class="list-group-item">All Projects</a>
                                    <a href="AdminProjectDetails.aspx?vwType=Proposed" class="list-group-item">Proposed Projects</a>
                                    <a href="AdminProjectDetails.aspx?vwType=Approved" class="list-group-item">Approved Projects</a>
                                    <a href="AdminProjectDetails.aspx?vwType=Completed" class="list-group-item">Completed Projects</a>
                                    <a href="AdminProjectDetails.aspx?vwType=Cancelled" class="list-group-item">Cancelled Projects</a>


                                </div>
                                <div class="alert alert-danger text-center">
                                    <h3>Import Information</h3>
                                    hello admin, if you want any help line please contact Deshpande Foundation Tech Team 
                   
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-lg-10">
                <div class="row">
                    <div class="col-md-12">

                        <asp:MultiView ID="MainView" runat="server">
                            <asp:View ID="vwAllProjects" runat="server">


                                <div class="panel">
                                    <div class="panel-heading ">
                                        <h5>

                                        All Project Filter Data <span class="fa fa-hand-o-right"></span>
                                          
                                                 <button type="button" class="btn btn-info pull-right" data-toggle="collapse" data-target="#demo"><span class="fa fa-filter"></span> </button>
                                         
                                        </h5>
                                    </div>
                                    <div id="demo" class="panel-body" >
                                          <div class="row">
                                                <div class="col-md-6">
                                                    <label for="ddlManagerName">Select Manager</label>
                                                    <asp:DropDownList ID="ddlAllProjectManagerName" CssClass="form-control fun selectpicker" runat="server">
                                                        <asp:ListItem>Abhinanada Chavate</asp:ListItem>
                                                        <asp:ListItem>18-19</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <label for="ddlAllProjectYear">Select Year</label>
                                                    <asp:TextBox ID="TextBox1" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlAllProjectYear" CssClass="form-control" runat="server">
                                                        <asp:ListItem>17-18</asp:ListItem>
                                                        <asp:ListItem>18-19</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <br />
                                                    <asp:LinkButton ID="btnAllProjectList" CssClass="btn btn-primary btn-block" runat="server">List &nbsp; <span class="fa fa-arrow-right pull-right"></span> </asp:LinkButton>
                                                </div>
                                            </div>
                                        <div class="row">

                                            <div class="quick-actions_homepage">
                                                <ul class="quick-actions text-center">

                                                    <li class="bg_lb col-md-3"><a href="#"><i class="fa fa-bell fa-2x"></i><span class="label label-danger">20444449999999</span>
                                                        <br />
                                                        All Projects</a></li>



                                                    <li class="bg_lg col-md-2"><a href="#"><i class="fa fa-list fa-2x"></i><span class="label label-danger">40</span><br />
                                                        Proposed</a> </li>
                                                    <li class="bg_ly  col-md-2"><a href="#"><i class="fa fa-list-ol fa-2x"></i><span class="label label-success">101</span><br />
                                                        Approved </a></li>
                                                    <li class="bg_lo  col-md-2"><a href="#"><i class="fa fa-chain"></i><span class="label label-success">104</span><br />
                                                        Completed</a> </li>
                                                    <li class="bg_ls  col-md-2"><a href="#"><i class="fa fa-eraser"></i><span class="label label-success">10</span><br />
                                                        Canceled</a> </li>


                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                            </asp:View>
                            <asp:View ID="vwProposedProjects" runat="server">
                            </asp:View>

                            <asp:View ID="vwApprovedProjects" runat="server">
                            </asp:View>
                            <asp:View ID="vwUnApprovedProjects" runat="server">
                            </asp:View>
                            <asp:View ID="vwCompletedProjects" runat="server"></asp:View>
                            <asp:View ID="vwStudentDeactivation" runat="server"></asp:View>

                        </asp:MultiView>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        jQuery(document).ready(function () {
            // Date Picker
            jQuery('.datepicker').datepicker({
                format: "dd-mm-yyyy",
                autoclose: true,
                todayHighlight: true
            });
        });
    </script>
</asp:Content>

