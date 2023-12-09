<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Pages_Student_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>

    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />

       <style>
        .progress-bar {
            width: 0;
            animation: progress 1.5s ease-in-out forwards;
        }

        .title {
            opacity: 0;
            animation: show 0.35s forwards ease-in-out 0.5s;
        }


        @keyframes progress {
            from {
                width: 0;
            }

            to {
                width: 100%;
            }
        }

        @keyframes show {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }
        .img-thumbnail
        {
           background-color:white;
        }
        .progress
        {
            height:10px;
        }
 
    </style>

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

    <script type="text/javascript">
        function PostToNewWindow() {
            originalTarget = document.forms[0].target;
            document.forms[0].target = '_blank';
            window.setTimeout("document.forms[0].target=originalTarget;", 300);
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="row">
        <div class="col-md-12">
            <div class="panel">
                <div class="panel-heading">
                    <div class="row">

                        <div class="col-md-2">
                           
                            <div style="height: 15px"></div>
                            <span class="fa fa-male fa-2x pull-left"  data-toggle="tooltip" data-placement="top" title="Student"></span>

                            <div class="progress">

                                <div class="progress-bar progress-bar-info progress-bar-striped" id="FromStudentToLeader" runat="server" role="progressbar" aria-valuemax="100">
                                    <span class="title">
                                        <asp:Label ID="lblFromStudentToLeader" runat="server" Text=""></asp:Label></span>
                                </div>
                            </div>
                        </div>

                        <img src="../../CSS/Images/Brooze_Icon.png" alt="Leader Medal" data-toggle="tooltip" data-placement="top" title="Leader" class="img-circle img-thumbnail pull-left" style="width: 40px; height: 40px" />

                        <div class="col-md-4">
                            
                            <div style="height: 15px"></div>

                            <div class="progress">
                                <div class="progress-bar progress-bar-success progress-bar-striped " runat="server" id="FromLeaderToMasterLeader" role="progressbar" aria-valuemax="4" style="max-width: 40%">
                                    <span class="title">
                                        <asp:Label ID="lblFromLeaderToMasterLeader"  runat="server" Text=""></asp:Label></span>
                                </div>
                            </div>
                        </div>
                        
                           
                           
                        <img src="../../CSS/Images/Silver_Icon.png" class="img-circle img-thumbnail pull-left" data-toggle="tooltip" data-placement="top" title="Master Leader" style="width: 40px; height: 40px" />
                        <div class="col-md-4">
                            
                            <div style="height: 15px"></div>
                            <div class="progress">
                                <div class="progress-bar progress-bar-danger progress-bar-striped progress-bar-striped" runat="server" id="FromMasterLeaderToAmbassedor" role="progressbar" aria-valuemax="100">
                                    <%--aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="max-width: 60%"--%>
                                    <span class="title">
                                        <asp:Label ID="lblFromMasterLeaderToAmbassedor" runat="server" Text=""></asp:Label></span>
                                </div>
                            </div>
                        </div>
                        <img src="../../CSS/Images/Gold_Icon.png" class="img-circle img-thumbnail pull-left" data-toggle="tooltip" data-placement="top" title="Lead Ambassador" style="width: 40px; height: 40px" />


                    </div>

                </div>
            </div>

        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="row hidden">
                <div class="col-lg-4">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading no-border clearfix ">
                                    <h2 class="panel-title text-center">Level for <b>Programme Leader</b></h2>

                                </div>
                                <!-- panel body -->
                                <div class="panel-body">
                                    <ul class="list-item todo-list">
                                        <li class="text-center ">

                                            <asp:Image ID="Image1" Width="100px" ImageUrl="~/CSS/Images/Brooze_Medal.png" runat="server" />

                                        </li>


                                    </ul>
                                    <div class="more text-center brandFont">
                                        <h2>
                                            <asp:Label ID="lblBronzeDesc" runat="server" Text="You Need To Achive"></asp:Label>
                                            &nbsp;
                          <asp:Label ID="lblBronzeCount" runat="server" Text="5 Projects"></asp:Label>
                                        </h2>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading no-border clearfix ">
                                    <h2 class="panel-title text-center">Level for <b>Programme Master Leader</b></h2>

                                </div>
                                <!-- panel body -->
                                <div class="panel-body">
                                    <ul class="list-item todo-list">
                                        <li class="text-center">

                                            <asp:Image ID="Image2" Width="100px" ImageUrl="~/CSS/Images/Silver_Medal.png" runat="server" />

                                        </li>


                                    </ul>
                                    <div class="more text-center">
                                        <h2>
                                            <asp:Label ID="lblSilverDesc" runat="server" Text="You Need To Achive"></asp:Label>
                                            &nbsp;
                          <asp:Label ID="lblSilverCount" runat="server" Text="15 Projects"></asp:Label>
                                        </h2>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading no-border clearfix ">
                                    <h2 class="panel-title text-center">Level for <b>Lead Programme Ambassador</b></h2>

                                </div>
                                <!-- panel body -->
                                <div class="panel-body">
                                    <ul class="list-item todo-list">
                                        <li class="text-center">

                                            <asp:Image ID="Image3" Width="100px" ImageUrl="~/CSS/Images/Gold_Medal.png" runat="server" />

                                        </li>


                                    </ul>
                                    <div class="more text-center">
                                        <h2>
                                            <asp:Label ID="lblGoldDesc" runat="server" Text="You Need To Achive"></asp:Label>
                                            &nbsp;
                          <asp:Label ID="lblGoldCount" runat="server" Text="25 Projects"></asp:Label>
                                        </h2>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading no-border clearfix">
                    <h2>Project List
                         
                     <a href="StudentProfile.aspx" class="pull-right"><span class="fa fa-arrow-right"></span> </a>
                    </h2>

                </div>
                <!-- panel body -->
                <div class="panel-body">
                    <ul class="list-item member-list" id="demo_info" style="height: 430px; overflow: auto;">
                        <asp:Repeater ID="rptProjectList" OnItemDataBound="rptProjectList_ItemDataBound" runat="server">
                            <ItemTemplate>
                                <li>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <asp:Label ID="lblProjectTitle" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="lblProjectStatus" runat="server" Text='<%# Eval("ProjectStatus") %>'></asp:Label>
                                            <asp:Label ID="lblRating" runat="server" Visible="false" Text='<%# Eval("rating") %>'></asp:Label>
                                            <%--<asp:LinkButton ID="btnEdit" runat="server">LinkButton</asp:LinkButton>--%>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>

                </div>
            </div>
        </div>
        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading no-border clearfix">
                    <h2 class="panel-title">Notice Board</h2>

                </div>
                <!-- panel body -->
                <div class="panel-body">

                    <ul class="list-item member-list" style="height: 430px; overflow: auto;">
                        <asp:Repeater ID="rptEvent" OnItemCommand="rptEvent_ItemCommand" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:Label ID="lblEventId" Visible="false" runat="server" Text='<% # Eval("EventId") %>'></asp:Label>
                                    <div class="user-avatar">
                                       <%-- <asp:LinkButton ID="btnParticularEvent" CommandArgument='<%# Eval("EventId") %>' runat="server">--%>
                                        <a href='<%# Eval("EventURL") %>' target="_blank" runat="server">
                                            <asp:Image ID="imgEventImg" Width="652px" Height="221px" CssClass="img-rounded" ImageUrl='<% # Eval("Image_Path") %>' runat="server" /><%--</asp:LinkButton>--%>
                                            </a>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <h5>
                                                <asp:Label ID="lblEventName" runat="server" Text='<% # Eval("EventName") %>'></asp:Label>

                                            </h5>

                                            <p>
                                                from :
                                            <asp:Label ID="lblFromDate" runat="server" Text='<% # Eval("FromDate") %>'></asp:Label>
                                                to 
                                            <asp:Label ID="lblToDate" runat="server" Text='<% # Eval("ToDate") %>'></asp:Label>
                                              
                                                <%--<asp:LinkButton ID="btnApplyNow" OnClientClick="return PostToNewWindow();" PostBackUrl='<% #Eval("EventApplyURL") %>' CssClass="btn-rounded btn-primary btn-sm" runat="server"><span class="fa fa-send"></span> Apply Now </asp:LinkButton>--%>
                                            </p>
                                            <p>
                                                <asp:Label ID="lblEventDescription" runat="server" Text='<% # Eval("EventDescription") %>'></asp:Label>

                                                  &nbsp;
                                        <a runat="server" class="btn-rounded btn-primary btn-sm" href='<% #Eval("EventApplyURL") %>' target="_blank"><span class="fa fa-send"></span>&nbsp; Apply Now</a>
                                            </p>
                                        </div>
                                    </div>
                                </li>

                            </ItemTemplate>
                        </asp:Repeater>


                    </ul>

                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="panel">
                <div class="panel-body">
                    <asp:Panel runat="server" ID="pnlStudent">
                        <div class="alert alert-info alert-dismissible" role="alert">
                            <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                            <strong><span class="fa fa-child"></span>&nbsp; Student</strong>
                            <marquee><p class="brandFont" style="font-size: 18pt">Welcome Student Complete <b>1</b> Project to become a <b> Leader </b> </p></marquee>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlMasterLeader">
                        <div class="alert alert-warning alert-dismissible" role="alert">
                            <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                            <strong>
                                <img src="../../CSS/Images/Brooze_Icon.png" style="width: 30px; height: 30px;" />
                                &nbsp; Master Leader</strong>
                            <marquee><p class="brandFont" style="font-size: 18pt">You are Eligible for <b> Master Leader</b> Please <a href="#"> Click Here To Apply</a></p></marquee>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLeadAmbassador">
                        <div class="alert alert-danger alert-dismissible" role="alert">
                            <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                            <strong>
                                <img src="../../CSS/Images/Silver_Icon.png" style="width: 30px; height: 30px;" />
                                &nbsp; LEAD Ambassador</strong>
                            <marquee><p class="brandFont" style="font-size: 18pt">You are Eligible for <b> LEAD Ambassador</b> Please<a href="#"> Click Here To Apply</a></p></marquee>
                        </div>
                    </asp:Panel>
                    <%-- <asp:Panel runat="server" ID="pnlLeadIntern">
                        <div class="alert alert-success alert-dismissible" role="alert">
                            <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                            <strong> <img src="../../CSS/Images/Gold_Icon.png" style="width:30px;height:30px;" /> &nbsp; LEAD Intern</strong>
                            <marquee><p class="brandFont" style="font-size: 18pt">You are Eligible for <b> LEAD Intern</b> Please <a href="#"> Click Here To Apply</a></p></marquee>
                        </div>
                    </asp:Panel>--%>
                </div>


            </div>
        </div>
    </div>

    
  

    <script type="text/javascript">
        function ErrorModal() {
            $('#ErrorModal').modal('show');

        }
    </script>

    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
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
    <div id="ErrorModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="dynamic" style="margin-top: 0px">
        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>Message</h3>
                    <p>
                        <span id="lblErrorMsg"></span>
                    </p>
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

</asp:Content>

