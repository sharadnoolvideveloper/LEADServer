<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="Student_Details.aspx.cs" Inherits="Pages_Student_Student_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/jquery-tagsinput.min.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/hover_home.css" rel="stylesheet" />
    <script src="../../JS/StudentJS/jquery-tagsinput.min.js"></script>

<%--    <script>

        function resize() {
            if ($(window).width() < 514) {
                $("#tabs").removeClass("tabs").addClass("tabs-container tabs-vertical");

            }
            else { $("#tabs").removeClass("tabs-container tabs-vertical").addClass("tabs"); }
        }

        $(document).ready(function () {
            $(window).resize(resize);
            resize();
        });
    </script>--%>

    <style>
        .row > .column {
            padding: 0 8px;
        }

        /*input:focus {
            background-color: #faffbd;
        }

        textarea:focus {
            background-color: #faffbd;
        }*/

        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        .column {
            float: left;
            width: 25%;
        }
        /* The Close Button */ .close {
            color: red;
            background-color: rgba(0, 0, 0, 0.8);
            position: absolute;
            top: 10px;
            right: 25px;
            font-size: 35px;
            font-weight: bold;
            text-decoration: solid;
        }

            .close:hover, .close:focus {
                color: red;
                text-decoration: solid;
                cursor: pointer;
            }

        .mySlides {
            display: none;
        }

        .cursor {
            cursor: pointer;
        }
        /* Next & previous buttons */ .prev, .next {
            cursor: pointer;
            position: absolute;
            top: 50%;
            width: auto;
            padding: 16px;
            margin-top: -125px;
            color: red;
            font-weight: bold;
            font-size: 20px;
            transition: 0.6s ease;
            border-radius: 3px 0 0 3px;
            user-select: none;
            -webkit-user-select: none;
            background-color: rgba(0, 0, 0, 0.8);
        }
        /* Position the "next button" to the right */ .next {
            right: 0;
            border-radius: 3px 0 0 3px;
        }
            /* On hover, add a black background color with a little bit see-through */ .prev:hover, .next:hover {
                background-color: rgba(0, 0, 0, 0.8);
            }
        /* Number text (1/3 etc) */ .numbertext {
            color: #f2f2f2;
            font-size: 12px;
            padding: 8px 12px;
            position: absolute;
            top: 0;
        }

        .caption-container {
            text-align: center;
            background-color: black;
            padding: 2px 16px;
            color: white;
        }

        .demo {
            opacity: 0.6;
        }

            .active, .demo:hover {
                opacity: 1;
            }

        img.hover-shadow {
            transition: 0.3s;
        }

        .hover-shadow:hover {
            box-shadow: 0 80px 80px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        .Shake_img {
            /* Start the shake animation and make the animation last for 0.5 seconds */
            animation: shake 0.5s;
            /* When the animation is finished, start again */
            animation-iteration-count: infinite;
        }

            .Shake_img:hover {
                /* Start the shake animation and make the animation last for 0.5 seconds */
                animation: shake 20s backwards;
                /* When the animation is finished, start again */
                animation-iteration-count: 1;
            }

        @keyframes shake {

            0% {
                transform: scale(1,1);
            }

            50% {
                transform: scale(1.1,1.1);
            }

            100% {
                transform: scale(1,1);
            }
        }
        .z-depth-2 {
            box-shadow: 0 8px 17px 0 rgb(240, 255, 255), 0 6px 20px 0 rgb(240, 255, 255);
        }
    </style>

    <script type="text/javascript">
        function success(msg) {
            toastr.options.timeOut = 4500; //1.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.success(msg);
        }
        function warning(msg) {
            toastr.options.timeOut = 4500; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.warning(msg);
        }
        function info(msg) {
            toastr.options.timeOut = 4500; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.info(msg);
        }
        function error(msg) {
            toastr.options.timeOut = 4500; //2.0 mili seconds 
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

        .img-thumbnail {
            background-color: white;
        }

        .progress {
            height: 10px;
        }
    </style>
    <script type="text/javascript">
        function PostToNewWindow() {
            originalTarget = document.forms[0].target;
            document.forms[0].target = '_blank';
            window.setTimeout("document.forms[0].target=originalTarget;", 300);
            return true;
        }
    </script>
    <style>
        .panel {
            background-color: #f5f5f5;
        }

        .top-right {
            position: absolute;
            top: -20px;
            right: 40px;
        }

        .top-right-count {
            position: absolute;
            top: 30px;
            right: 40px;
        }

        @media (max-width: 767px) {
            .nav-tabs > li {
                float: none;
            }
        }
    </style>
    <script>
        $(document).ready(function () {
            $('[data-role="tags-input"]').tagsInput();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
  

     <div class="row">
      <div class="col-md-3  z-depth-1-half" style="background-color: #fff;">
            <div class="panel " style="margin-top: 10px;">

                <div class="panel-body">
                    <div class="text-center">
                        <asp:Image ID="PreviewImage" runat="server" CssClass="center-block img-rounded" Style="width: 100px; height: 100px;"
                            EnableTheming="True" />
                        <div class="top-right">
                               <asp:Image ID="ImgBatch" ImageUrl="~/CSS/Images/Batch.png" Visible="false" runat="server" />
                    
                            <div class="top-right-count">
                                <asp:Label ID="lblFiveStartsCount" Visible="false" Font-Bold="true" runat="server" Font-Size="Medium" ForeColor="#143D59" CssClass="text-info brandFont"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-10">
                            <asp:FileUpload ID="ProfilePic" runat="server" onchange="Profile()" CssClass="btn btn-primary form-control" />
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="btnSaveProfileImage" OnClick="btnSaveProfileImage_Click" CssClass="btn btn-success btn-floating" runat="server"><span class="fa fa-upload"></span> </asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <h4 class="text-center">
                        <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label></h4>
                    <p class="text-center">
                        <asp:Label ID="lblCollegeName" runat="server" Text=""></asp:Label>
                    </p>
                    <h4 class="text-center">
                            <span class="pull-right">
                        <asp:ImageButton ID="btnTshirt" CssClass="Shake_img" OnClick="btnTshirt_Click" Height="30px" Width="30px" ImageUrl="~/CSS/Images/TshirtFront.png" runat="server" />
                    </span>
                        <asp:Label ID="lblLeadId" runat="server" Text=""></asp:Label>
                         
                    </h4>


                    <table class="table table-hover">
                        <tbody>
                            <tr>
                                <td>Status</td>
                                <td><span class="label label-success">
                                    <asp:Label ID="lblStudentType" runat="server" Text=""></asp:Label></span></td>
                            </tr>

                            <tr style="display:none;">
                                <td>Member Since</td>
                                <td>
                                    <asp:Label ID="lblRegistrationDate" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>

                  

                </div>
            </div>
            <!-- panel widget -->

            <div class="panel">
                <div class="panel-body">
                    <h3 class="text-center"><i class="fa fa-user-secret"></i><span style="letter-spacing: 3px;">Your Mentor</span>
                        <br />
                        <br />
                        <asp:Image ID="imgManagerDetailInStudentProfile" alt="Pic" Width="50px" Height="50px" CssClass="img-circle img-thumbnail img-rounded" runat="server" />

                    </h3>
                    <div class="column-xs-2 column-md-3">
                        <div>
                            <h5 class="text-center">
                                <asp:Label ID="lblManagerName" runat="server" Text=""></asp:Label></h5>
                            <h5 class="text-center">
                                <asp:Label ID="lblManagerEmailId" runat="server" Text=""></asp:Label></h5>
                            <h5 class="text-center">
                                <asp:Label ID="lblManagerMobileNo" runat="server" Text=""></asp:Label></h5>

                            <br />
                            <div class="row text-center center-block">
                                <div class="col-md-1 col-xs-1"></div>
                                <div class="col-md-2 col-xs-2 text-center" id="Facebook" runat="server">
                                    <a id="btnFacebook" target="_blank" runat="server"></a>

                                </div>
                                <div class="col-md-2 col-xs-2 text-center" id="Twitter" runat="server">
                                    <a id="btnTwitter" target="_blank" runat="server"></a>

                                </div>
                                <div class="col-md-2 col-xs-2 text-center" id="InstaGram" runat="server">
                                    <a id="btnInstaGram" target="_blank" runat="server"></a>

                                </div>
                                <div class="col-md-2 col-xs-2 text-center" id="WhataApp" runat="server">
                                    <a id="btnWhataApp" target="_blank" runat="server"></a>

                                </div>
                            </div>
                        </div>

                    </div>

                </div>

            </div>
            <!-- panel widget -->

        </div>
         <div class="col-md-9">
           

             <div class="row" style="display:none;">
                 <div class="col-md-12">

                     <div class="panel z-depth-1-half hoverable" style="background-color: white;">
                         <div class="panel-heading">
                             <div class="row">
                                 <div class="col-md-2">

                                     <div style="height: 15px"></div>
                                     <span class="fa fa-male fa-2x pull-left" data-toggle="tooltip" data-placement="top" title="Student"></span>

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
                                                 <asp:Label ID="lblFromLeaderToMasterLeader" runat="server" Text=""></asp:Label></span>
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
                     <div class="col-md-8">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="panel z-depth-1-half hoverable" style="background-color: white;">
                                     <div class="panel-heading">
                                              <span class="pull-right">
                                             <a href="#" ><span class="fa fa-info-circle"></span></a>
                                         </span>
                                         <asp:Image ID="imgStudentLevel" Width="700px" Height="185px" runat="server" />
                                    
                                         <%--     <img alt="Image Not Found" style="width:700px;height:205px;" src="<%=getPathName()%>"/>--%>
                                     </div>
                                 </div>
                             </div>
                            
                         </div>
                   
                           
                     </div>
                  <div class="col-md-4">
                     <div class="panel z-depth-1-half hoverable" style="background-color: white;">
                         <div class="panel-body">
                             <h3>
                            
                                     <a href="StudentProfile.aspx"> 
                                         Project Ratings
                                         <span class="pull-right fa fa-long-arrow-right"></span>
                                     </a>
                
                             </h3>
                             <div class="row">
                                 <div class="col-xs-1 col-md-1 col-sm-1 col-lg-1">
                                     <div class="pull-left" style="width: 30px; line-height: 1.5;">
                                         <div style="height: 9px; margin: 5px 0;">5 <span class="fa fa-star text-success"></span></div>
                                     </div>
                                 </div>
                                 <div class="col-xs-10">
                                     <div class="progress" style="height: 15px; margin: 8px 0;">
                                         <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="5" aria-valuemin="0" aria-valuemax="5"
                                             data-toggle="tooltip" data-placement="top" title="" data-original-title="5 Star" style="max-width: 100%">
                                             <h3 style="color: whitesmoke"><span class="title">
                                                 <asp:Label ID="lbl5StarCount" runat="server" Text="0"></asp:Label>
                                             </span></h3>
                                         </div>
                                     </div>
                                 </div>
                             </div>

                             <div class="row">
                                 <div class="col-xs-1 col-md-1 col-sm-1 col-lg-1">
                                     <div class="pull-left" style="width: 30px; line-height: 1.5;">
                                         <div style="height: 9px; margin: 5px 0;">4 <span class="fa fa-star text-primary"></span></div>
                                     </div>
                                 </div>
                                 <div class="col-xs-10">
                                     <div class="progress" style="height: 15px; margin: 8px 0;">
                                         <div class="progress-bar progress-bar-primary" role="progressbar" aria-valuenow="5" aria-valuemin="0" aria-valuemax="5"
                                             data-toggle="tooltip" data-placement="top" title="" data-original-title="4 Star" style="max-width: 80%">
                                             <h3 style="color: whitesmoke"><span class="title">
                                                   <asp:Label ID="lbl4StarCount" runat="server" Text="0"></asp:Label>
                                                                           </span></h3>
                                         </div>
                                     </div>
                                 </div>
                             </div>

                             <div class="row">
                                 <div class="col-xs-1 col-md-1 col-sm-1 col-lg-1">
                                     <div class="pull-left" style="width: 30px; line-height: 1.5;">
                                         <div style="height: 9px; margin: 5px 0;">3 <span class="fa fa-star text-info"></span></div>
                                     </div>
                                 </div>
                                 <div class="col-xs-10">
                                     <div class="progress" style="height: 15px; margin: 8px 0;">
                                         <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="5" aria-valuemin="0" aria-valuemax="5"
                                             data-toggle="tooltip" data-placement="top" title="" data-original-title="3 Star" style="max-width: 60%">
                                             <h3 style="color: whitesmoke"><span class="title">
                                                  <asp:Label ID="lbl3StarCount" runat="server" Text="0"></asp:Label>
                                                                           </span></h3>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-xs-1 col-md-1 col-sm-1 col-lg-1">
                                     <div class="pull-left" style="width: 30px; line-height: 1.5;">
                                         <div style="height: 9px; margin: 5px 0;">2 <span class="fa fa-star text-warning"></span></div>
                                     </div>
                                 </div>
                                 <div class="col-xs-10">
                                     <div class="progress" style="height: 15px; margin: 8px 0;">
                                         <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="5" aria-valuemin="0" aria-valuemax="5"
                                             data-toggle="tooltip" data-placement="top" title="" data-original-title="2 Star" style="max-width: 40%">
                                             <h3 style="color: whitesmoke"><span class="title">
                                                   <asp:Label ID="lbl2starCount" runat="server" Text="0"></asp:Label>
                                                                           </span></h3>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-xs-1 col-md-1 col-sm-1 col-lg-1">
                                     <div class="pull-left" style="width: 30px; line-height: 1.5;">
                                         <div style="height: 9px; margin: 5px 0;">1 <span class="fa fa-star text-danger"></span></div>
                                     </div>
                                 </div>
                                 <div class="col-xs-10">
                                     <div class="progress" style="height: 15px; margin: 8px 0;">
                                         <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="5" aria-valuemin="0" aria-valuemax="5"
                                             data-toggle="tooltip" data-placement="top" title="" data-original-title="1 Star" style="max-width: 20%">
                                             <h3 style="color: whitesmoke"><span class="title">
                                                   <asp:Label ID="lbl1StarCount" runat="server" Text="0"></asp:Label>
                                                                           </span></h3>
                                         </div>
                                     </div>
                                 </div>
                             </div>

                         </div>
                     </div>
                 </div>

             </div>
             <div class="row">
                 <asp:Panel ID="pnlLLP" runat="server">
                     <div class="col-md-3 col-xs-6 text-center center-block" style="border-style: none; letter-spacing: 2px;">
                         <div class="panel z-depth-1-half hoverable" style="background-color: white;">
                             <div class="panel-body">
                                 <h5>LLP
                                  <span class="pull-right">
                                      <asp:Label ID="lblLLPBadge" runat="server" Text=""></asp:Label></span>
                                 </h5>
                                 <a class="hovicon effect-8" style="font-size: 12px; width: 80px; height: 80px; cursor: context-menu;" href="#">
                                     <img src="../../CSS/Images/Badges/LLP.png" class="img-responsive img-rounded " style="width: 80px; height: 80px; " />
                                     <p style="margin-top: -25px;">Badge</p>
                                 </a>
                             </div>
                         </div>
                     </div>
                 </asp:Panel>
                 <asp:Panel ID="pnlPrayana" runat="server">
                     <div class="col-md-3 col-xs-6" style="border-style: none; letter-spacing: 2px;">
                         <div class="panel z-depth-1-half hoverable" style="background-color:white;">
                             <div class="panel-body  text-center center-block">
                                 <h5>Prayana
                                  <span class="pull-right">
                                      <asp:Label ID="lblPrayanaBadges" runat="server" Text=""></asp:Label>
                                  </span>
                                 </h5>
                                 <a class="hovicon effect-8 z-depth-2" style="font-size: 12px; width: 80px; height: 80px; cursor: context-menu;" href="#">
                                     <img src="../../CSS/Images/Badges/LP.png" class="img-responsive center-block text-center" style="width: 80px; height: 80px;"/>
                                     <p style="margin-top: -25px;">Badge</p>
                                 </a>
                             </div>
                         </div>
                     </div>
                 </asp:Panel>
                 <asp:Panel ID="pnlYuvaSummit" runat="server">
                     <div class="col-md-3 col-xs-6 " style="border-style: none; letter-spacing: 2px;">
                         <div class="panel z-depth-1-half hoverable" style="background-color:white;">
                              <div class="panel-body text-center center-block">
                                 <h5>Yuva
                                  <span class="pull-right"><asp:Label ID="lblYuvaBadges" runat="server" Text=""></asp:Label></span>
                                 </h5>
                                 <a class=" hovicon effect-8 z-depth-2" style="letter-spacing: 2px; font-size: 12px; width: 80px; 
                                    height: 70px; cursor: context-menu;" href="#">
                                     <img src="../../CSS/Images/Badges/YS.png" class="img-responsiver" style="width: 80px; height: 80px; " />
                                     <p style="margin-top: -35px;">Badge </p>
                                 </a>
                             </div>
                         </div>
                            
                         </div>

         
                 </asp:Panel>
                 <asp:Panel ID="pnlValidatory" runat="server">
                     <div class="col-md-3 col-xs-6 center-block" style="border-style: none; letter-spacing: 2px;">
                         <div class="panel hoverable z-depth-1-half" style="background-color:white;">
                             <div class="panel-body  text-center center-block">
                                 
                                 <h5 style="font-size: smaller">Validator
                                  <span class="pull-right"><asp:Label ID="lblValidatoryBadge" runat="server" Text=""></asp:Label></span>
                                 </h5>
                                 <a class="text-center  hovicon effect-8 z-depth-2" style="letter-spacing: 2px; font-size: 12px; width: 80px; height: 80px; cursor: context-menu;" href="#">
                                     <img src="../../CSS/Images/Badges/VALEDICTORY.png" class="img-responsive center-block text-center" style="width: 80px; height: 80px; margin-top: 0px; margin-bottom: 0px" />
                                     <p style="margin-top: -25px;">Badge</p>
                                 </a>
                             </div>
                         </div>
                     </div>
                 </asp:Panel>
             </div>

             <br />
                 <div class="row">
        <div class="col-md-12">
            <div class="panel">
                <div class="panel-body">
                    <asp:Panel runat="server" ID="pnlStudent">
                        <div class="alert alert-info alert-dismissible" role="alert">
                            <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                            <strong><span class="fa fa-child"></span>&nbsp; Student</strong>
                            <marquee><p class="brandFont" style="font-size: 18pt">Welcome Student Complete <b>1</b> Project to become a <b> Initiator </b> </p></marquee>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlMasterLeader">
                        <div class="alert alert-warning alert-dismissible" role="alert">
                            <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                            <strong>
                                <img src="../../CSS/Images/Brooze_Icon.png" style="width: 30px; height: 30px;" />
                                &nbsp; Master Leader</strong>
                            <marquee><p class="brandFont" style="font-size: 18pt">You are Eligible for <b> Master Leader</b> Please <a href="#"> <asp:LinkButton ID="btnClickForMasterLeaderApply" OnClick="btnClickForMasterLeaderApply_Click" Text="Click Here To Apply" runat="server"></asp:LinkButton>  </a></p></marquee>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLeadAmbassador">
                        <div class="alert alert-danger alert-dismissible" role="alert">
                            <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                            <strong>
                                <img src="../../CSS/Images/Silver_Icon.png" style="width: 30px; height: 30px;" />
                                &nbsp; LEAD Ambassador</strong>
                            <marquee><p class="brandFont" style="font-size: 18pt">You are Eligible for <b> LEAD Ambassador</b> Please<a href="#"> <asp:LinkButton ID="btnClickForLeadAmbassadorApply" OnClick="btnClickForLeadAmbassadorApply_Click" runat="server">Click Here To Apply</asp:LinkButton> </a></p></marquee>
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
         </div>
      </div>
   
    <div id="ErrorModal" class="modal fade" role="dialog" style="margin-top: 200px; width: auto; max-width: 90%">
        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>Message
                          <a class="pull-right text-right" data-dismiss="modal">
                              <i class="fa fa-remove text-primary fa-2x" style="cursor: pointer;"></i>
                          </a>
                    </h3>
                    <p>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </p>

                </div>

            </div>

        </div>
    </div>
        <div id="POP_Tshirt" class="modal fade" role="dialog" style="margin-top: 100px; width: auto;">

        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="modal-body">
                            <h3 style="color: white">Select T-shirt Size
                          <a class="pull-right text-right" data-dismiss="modal">
                              <i class="fa fa-remove text-primary fa-fw" style="color: white; cursor: pointer;"></i>
                          </a>
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblTshirtRequestedId" runat="server" Visible="false" Text=""></asp:Label>
                                    <table class="table ">
                                        <thead>
                                            <tr>
                                                <td>T-shirt Size</td>
                                                <td class="text-center">Chest In Inches</td>
                                                <td class="text-center">Height in Inches</td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>

                                                <td>
                                                    <asp:RadioButton ID="rdoS" GroupName="T" CssClass="radio radio-custom radio-success" Text="S" runat="server" />
                                                </td>
                                                <td class="text-center">36
                                                </td>
                                                <td class="text-center">26
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoM" GroupName="T" CssClass="radio radio-custom radio-success" Text="M" runat="server" />
                                                </td>
                                                <td class="text-center">38
                                                </td>
                                                <td class="text-center">27
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoL" GroupName="T" CssClass="radio radio-custom radio-success" Text="L" runat="server" />
                                                </td>
                                                <td class="text-center">40
                                                </td>
                                                <td class="text-center">28
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoXL" GroupName="T" CssClass="radio radio-custom radio-success" Text="XL" runat="server" />
                                                </td>
                                                <td class="text-center">42
                                                </td>
                                                <td class="text-center">29
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoXXL" GroupName="T" CssClass="radio radio-custom radio-success" Text="XXL" runat="server" />
                                                </td>
                                                <td class="text-center">44
                                                </td>
                                                <td class="text-center">30
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>

                            </div>

                            <br />
                            <div class="row">
                                <div class="col-md-4 col-md-offset-1">
                                    <asp:LinkButton ID="btnSendTshirtRequest" OnClick="btnSendTshirtRequest_Click" CssClass="btn btn-dropbox btn-black brandFont" runat="server"><span class="fa fa-send">&nbsp; <i class="brandFont"> Request T-shirt</i></span>  </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
    <div id="POP_StudentTshirt" class="modal fade" role="dialog" style="margin-top: 200px; width: auto;">

        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="modal-body">
                            <h3 style="color: white">Select T-shirt Size
                          <a class="pull-right text-right" data-dismiss="modal">
                              <i class="fa fa-remove text-primary fa-fw" style="color: white; cursor: pointer;"></i>
                          </a>
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMultipleTshirtRequest" Visible="false" runat="server" Text=""></asp:Label>
                                    <table class="table ">
                                        <thead>
                                            <tr>
                                                <td>T-shirt Size</td>
                                                <td class="text-center">Chest In Inches</td>
                                                <td class="text-center">Height in Inches</td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>

                                                <td>
                                                    <asp:RadioButton ID="rdoS1" GroupName="T" CssClass="radio radio-custom radio-success" Text="S" runat="server" />
                                                </td>
                                                <td class="text-center">36
                                                </td>
                                                <td class="text-center">26
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoM1" GroupName="T" CssClass="radio radio-custom radio-success" Text="M" runat="server" />
                                                </td>
                                                <td class="text-center">38
                                                </td>
                                                <td class="text-center">27
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoL1" GroupName="T" CssClass="radio radio-custom radio-success" Text="L" runat="server" />
                                                </td>
                                                <td class="text-center">40
                                                </td>
                                                <td class="text-center">28
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoXL1" GroupName="T" CssClass="radio radio-custom radio-success" Text="XL" runat="server" />
                                                </td>
                                                <td class="text-center">42
                                                </td>
                                                <td class="text-center">29
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoXXL1" GroupName="T" CssClass="radio radio-custom radio-success" Text="XXL" runat="server" />
                                                </td>
                                                <td class="text-center">44
                                                </td>
                                                <td class="text-center">30
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>
                                <div class="col-md-12">
                                    <label>Select Reson For ReApply</label>
                                    <asp:DropDownList ID="ddlStudentResonForReApply" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="Damage" Value="Damage"></asp:ListItem>
                                        <asp:ListItem Text="Lost" Value="Lost"></asp:ListItem>
                                        <asp:ListItem Text="Size Mismatch" Value="Size Mismatch"></asp:ListItem>
                                        <asp:ListItem Text="Emergency T-Shirt" Value="Emergency T-Shirt"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-md-4 col-md-offset-1">
                                    <asp:LinkButton ID="btnSubmitStudentTshirtRequest" OnClick="btnSubmitStudentTshirtRequest_Click" CssClass="btn btn-dropbox btn-black brandFont" runat="server"><span class="fa fa-send">&nbsp; <i class="brandFont"> Request T-shirt</i></span>  </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>

    <div id="POP_EligibleForMasterLeader" class="modal fade" role="dialog" style="margin-top: 100px; width: auto;">
        <div class="modal-dialog" >
            <!-- Modal content-->
            <div class="modal-content" style="background-color: #125FFF">
                <div class="panel">
                 
                    <div class="panel-body">
                         <div class="modal-body">

                            <h3 class="text-info">Apply for Master Leader
                          <a class="pull-right text-right" data-dismiss="modal">
                              <i class="fa fa-remove text-primary fa-fw" style="cursor: pointer;"></i>
                          </a>
                            </h3>
                             <p>
                                     Welcome to Lead Programme and its associates to become <span class="text-primary"> <b> Master Leader </b></span> subject to the following conditions.
                                       Please read Roles and Responsibilities carefully. 
                                 </p>
                             <div style="height:200px;overflow:auto;">
                                 <h3>Role and Responsibilities</h3>
                                 <p>
                                     1.  Guiding and mentoring initiators and change makers.
                                 </p>
                                 <p>
                                     2. Helping own team members to the next stage transformation.
                                 </p>
                                   <p>
                                     3. Volunteer as a team on LEAD official events.
                                 </p>
                                  <p>
                                     4. Coordination along with LEAD Team (taking part in the official meet, training, college sessions and visits, project participation).
                                 </p>
                                     <p>
                                     5. Assisting in her/his own college or in new college along with LEAD Team.
                                 </p>
                                   <p>
                                     6. Participation in advance level of capacity building programs.
                                 </p>
                                   <p>
                                     7. Representing the LEAD program officially
                                 </p><br />
                               <p>
                                 
                                   <asp:RadioButton ID="rdoMLApproval" CssClass="radio radio-custom radio-primary" 
                                       Text="I Agree above all Role and Responsibilities" GroupName="ML" runat="server" />
                               </p>
                                 <p>
                                   <asp:RadioButton ID="rdoMLNotApproval" CssClass="radio radio-custom radio-primary" 
                                       Text="I dont Agree above all Role and Responsibilities" GroupName="ML"  runat="server" />
                               </p>
                            
                             </div>
                             <br />
                           <div class="row">
                               <div class="col-md-4" id="MLAgreebtn" style="display:none;">
                                   <asp:LinkButton ID="btnMLAgree" OnClick="btnMLAgree_Click" CssClass="btn btn-success" runat="server">I Agree</asp:LinkButton>
                               </div>
                                 <div class="col-md-4" id="MLdisAgreebtn" style="display:none;">
                                   <asp:LinkButton ID="btnMLDisAgree" CssClass="btn btn-default" runat="server">I dont Agree</asp:LinkButton>
                               </div>
                           </div>
                            
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#<%=rdoMLApproval.ClientID%>").change(function () {
                $('#MLAgreebtn').show();
                $("#MLdisAgreebtn").hide();
            })
            $("#<%=rdoMLNotApproval.ClientID%>").change(function () {
                $("#MLdisAgreebtn").show();
                $('#MLAgreebtn').hide();
               
            })

        });
    </script>
      <script type="text/javascript">
          function POP_EligibleForMasterLeader() {
              $('#POP_EligibleForMasterLeader').modal({
                 backdrop: 'static',
                 keyboard: false,
                 show: true
             });

         }
    </script>
     <script type="text/javascript">
        function POP_Tshirt() {
            $('#POP_Tshirt').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });

        }
    </script>
    <script type="text/javascript">
        function POP_StudentTshirt() {
            $('#POP_StudentTshirt').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });

        }
    </script>
</asp:Content>

