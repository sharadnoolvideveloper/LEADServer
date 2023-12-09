<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="ProgressBar.aspx.cs" Inherits="Pages_Student_ProgressBar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

      <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />


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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel">
                <div class="panel-body">
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

     <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
</asp:Content>

