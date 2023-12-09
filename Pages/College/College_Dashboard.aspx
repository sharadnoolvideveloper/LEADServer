<%@ Page Language="C#" AutoEventWireup="true" CodeFile="College_Dashboard.aspx.cs" Inherits="Pages_College_College_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>LEADCampus | Student Console (Lead) </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <meta http-equiv="Copyright" content="Copyright (c) 2019 Deshpande Foundation India" />
    <meta name="distribution" content="global" />
    <meta name="language" content="english" />
    <meta http-equiv="content-language" content="en" />
    <meta name="rating" content="safe for kids" />
    <meta name="web_author" content="Deshpande Foundation India" />
    <meta name="resource-type" content="document" />
    <meta name="classification" content="Education" />
    <meta name="doc-type" content="public" />
    <meta http-equiv="cahe-control" content="cache" />
    <meta name="contactName" content="Vivek Pawar" />
    <meta name="contactOrganization" content="Deshpande Foundation India" />
    <meta name="og:latitude" content="15.3709" />
    <meta name="og:longitude" content="75.1234" />
    <meta name="contactCity" content="Hubballi" />
    <meta name="contactState" content="Karnataka" />
    <meta name="contactCountry" content="India" />
    <meta name="contact NetworkAddress" content="leadmis{@}dfmail{.}org" />
    <meta name="linkage" content="http://mis.leadcampus.org" />
    <meta property="og:site_name" content="http://mis.leadcampus.org" />
    <meta property="og:type" content="website" />
    <meta property="og:title" content="LEADCampus | Manager Console (LEAD)" />
    <meta property="og:url" content="http://mis.leadcampus.org" />
    <meta property="og:image" content="http://mis.leadcampus.org/css/images/logo.png" />
    <meta property="og:image" content="http://mis.leadcampus.org/CSS/LoginCSS/Images/banner.jpg" />
    <meta name="google-analytics" content="UA-126729086-1" />
    <meta name="author" content="Deshpande Foundation India: www.deshpandefoundationindia.org" />
    <meta property="og:description" content="The Leaders Accelerating Development (LEAD) Program of Deshpande Foundation, Hubballi, Karnataka Complaints... Start with THEY, Solutions... start with I" />
    <meta name="description" content="The Leaders Accelerating Development (LEAD) Program of Deshpande Foundation, Hubballi, Karnataka The Leaders Accelerating Development (LEAD) Program of Deshpande Foundation, Hubballi, Karnataka Complaints... Start with THEY, Solutions... start with I" />
    <meta name="keywords" content="Student Programme,Lead Adda, Lead Programme hubli, leaders in hubli,leadership,deshpande foundation hubli,lead deshpande foundation,challenge, school ,lead colleges,lead mobile app,lead android app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="mobile-web-app-capable" content="yes" />
    <meta name="theme-color" content="#182848" />
    <meta name="msapplication-navbutton-color" content="#0072ff" />
    <meta name="apple-mobile-web-app-status-bar-style" content="#0072ff" />
    <link rel='shortcut icon' type='image/x-icon' href="../../CSS/Images/logo.png" />

    <link href="../../CSS/StudentCSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/clevex-core.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/clevex-forms.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/entypo.css" rel="stylesheet" />
    <link href="../../CSS/LoginCSS/font-awesome.min.css" rel="stylesheet" />
    <link href="../../CSS/LoginCSS/Style.css" rel="stylesheet" />
    <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script>
        function resize() {
            if ($(window).width() < 514) { $("#tabs").removeClass("tabs").addClass("tabs-container tabs-vertical"); }
            else { $("#tabs").removeClass("tabs-container tabs-vertical").addClass("tabs"); }
        }
        $(document).ready(function () {
            $(window).resize(resize);
            resize();
        });
    </script>
    <style>
        .navbar-default {
            padding: 8px;
            background-color: #16a085;
            border-bottom: solid 1px #a6dad0;
            height: 50px;
        }

        .profile-info.dropdown .dropdown-menu {
            margin-top: 5px;
        }

        .gray-bg {
            background-color: #f0ffff !important;
        }

    </style>
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
                width: 90%;
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
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <div class="page-container">
            <div class="main-container gray-bg">
                <div class="nav navbar-default navbar-fixed-top">
                    <div class="col-xs-10">
                        <!-- Site header  -->
                        <div class="header-logo brandFont">
                            <div class="menu-toggle" style="display: none;"><a class="menu-icon" href="#menu"><i class="icon-menu"></i></a></div>
                            <div class="site-logo">
                                <span class="visible-xs visible-md" style="color: white;">
                                    <img src="../../CSS/Images/logo.png" style="width: 30px; height: 30px;" />
                                    &nbsp;Lead
                                </span>
                                <h3 class="visible-lg">
                                    <a href="#" style="padding-top: 5px;">
                                        <span style="color: white" class="text-capitalize">Welcome :
                                            <asp:Label ID="lblMailId" runat="server" Text=""></asp:Label>
                                        </span>
                                    </a></h3>
                            </div>
                        </div>
                        <!-- /site header -->
                    </div>

                    <div class="col-xs-2">
                        <div class="pull-right">


                            <!-- User info -->
                            <ul class="user-info" style="margin-top: 1px">
                                <li class="profile-info dropdown">
                                    <a data-toggle="dropdown" class="dropdown-toggle" href="#" aria-expanded="false">
                                        <img id="ProfilePic" class="center-block img-circle" src="../../CSS/Images/NoImage.png" style="width: 40px; height: 40px;">
                                        <!-- User action menu -->
                                    </a>
                                    <ul class="dropdown-menu">
                                        <a data-toggle="dropdown" class="dropdown-toggle" href="#" aria-expanded="false"></a>
                                     
                                        <li>
                                            <asp:LinkButton ID="btnTopLogOut" OnClick="btnTopLogOut_Click" runat="server"><i class="fa fa-sign-out"></i>&nbsp; Logout

                                            </asp:LinkButton>
                                          
                                        </li>
                                    </ul>
                                    <!-- /user action menu -->

                                </li>
                            </ul>
                            <!-- /user info -->
                        </div>
                    </div>
                </div>
                <div class="main-content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="tabs" id="tabs">
                                <ul class="nav nav-tabs">
                                    <li id="tabSummary" runat="server">
                                        <asp:LinkButton ID="btnSummaryCount" OnClick="btnSummaryCount_Click" CssClass="text-center" runat="server"><span class="menu-active fa fa animated"><i class="fa fa-dashcube"></i></span>&nbsp; Summary
                                        </asp:LinkButton>
                                    </li>
                                    <li id="tabFiveStarProject" runat="server" visible="false">
                                        <asp:LinkButton ID="btnFiveStarProject" OnClick="btnFiveStarProject_Click" CssClass="text-center" runat="server"><span class="menu-active "><i class="fa fa-bolt"></i></span>&nbsp; Top Project</asp:LinkButton>
                                    </li>
                                    <li id="tabEvent" runat="server" visible="false">
                                        <asp:LinkButton ID="btnEvent" CssClass="text-center" runat="server"><span class="menu-active "><i class="fa fa-bullhorn"></i></span>&nbsp;Events</asp:LinkButton>
                                    </li>

                                    <li class="pull-right text-center">
                                        <asp:DropDownList ID="ddlAcademicCode" OnSelectedIndexChanged="ddlAcademicCode_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div id="SummaryTab" runat="server" class="brandFont">
                                        <div class="visible-lg visible-md visible-sm">
                                            <br />
                                        </div>
                                        <div class="panel">

                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row text-center">
                                                            <div class="col-md-3">
                                                                <div class="panel  hoverable z-depth-1-half" style="background-color:#007bff;">
                                                                    <div class="panel-heading">

                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <h4 style="color: whitesmoke;">Student Registrations</h4>
                                                                                <br />
                                                                                <h3 style="color: whitesmoke;">
                                                                                    <asp:Label ID="lblTotalCount" runat="server" Text=""></asp:Label></h3>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="panel  hoverable z-depth-1-half" style="background-color: #17a2b8;">
                                                                    <div class="panel-heading">

                                                                        <div class="row">

                                                                            <div class="col-md-12">
                                                                                <h4 style="color: whitesmoke;">Total Projects</h4>
                                                                                <br />
                                                                                <h3 style="color: whitesmoke;">
                                                                                    <asp:Label ID="lbltotalProjects" runat="server" Text=""></asp:Label></h3>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="panel hoverable z-depth-1-half" style="background-color:#28a745;">
                                                                    <div class="panel-heading">

                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <h4 style="color: whitesmoke;"> Proposed Projects</h4>
                                                                                <br />
                                                                                <h3 style="color: whitesmoke;">
                                                                                    <asp:Label ID="lblTotalProposed" runat="server" Text=""></asp:Label></h3>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="panel hoverable z-depth-1-half" style="background-color:#F6BB43;">
                                                                    <div class="panel-heading">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <h4 style="color: whitesmoke;">Approved Projects</h4>
                                                                                <br />
                                                                                <h3 style="color: whitesmoke;">
                                                                                    <asp:Label ID="lblTotalApproved" runat="server" Text=""></asp:Label></h3>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row text-center">
                                                            <div class="col-md-3">
                                                                <div class="panel hoverable z-depth-1-half" style="background-color:#343a40;">
                                                                    <div class="panel-heading">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <h4 style="color: whitesmoke;">Project Completion Request</h4>
                                                                                <br />
                                                                                <h3 style="color: whitesmoke;">
                                                                                    <asp:Label ID="lblTotalRequestForCompletion" runat="server" Text=""></asp:Label></h3>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="panel hoverable z-depth-1-half" style="background-color:#6c757d;">
                                                                    <div class="panel-heading">

                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <h4 style="color: whitesmoke;">Project Completed</h4>
                                                                                <br />
                                                                                <h3 style="color: whitesmoke;">
                                                                                    <asp:Label ID="lblTotalCompleted" runat="server" Text=""></asp:Label></h3>
                                                                            </div>


                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="panel panel-primary hoverable z-depth-1-half">
                                                                    <div class="panel-heading">

                                                                        <div class="row">

                                                                            <div class="col-md-12">
                                                                                <h4 style="color: whitesmoke;">Project Modification Requests</h4>
                                                                                <br />
                                                                                <h3 style="color: whitesmoke;">
                                                                                    <asp:Label ID="lblTotalRequestForModification" runat="server" Text=""></asp:Label></h3>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="panel hoverable z-depth-1-half" style="background-color:#dc3545;">
                                                                    <div class="panel-heading">

                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <h4 style="color: whitesmoke;">Rejected Projects</h4>
                                                                                <br />
                                                                                <h3 style="color: whitesmoke;">
                                                                                    <asp:Label ID="lblTotalRejected" runat="server" Text=""></asp:Label></h3>
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
                                                        <div class="panel panel-info hoverable z-depth-1-half">
                                                            <div class="panel-heading">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <h4 style="color: #0c516f;">Project Requested Amount</h4>
                                                                        <br />
                                                                        <h3 style="color: #0c516f;">
                                                                            <asp:Label ID="lblRequestedAmount" runat="server" Text=""></asp:Label></h3>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="panel panel-warning hoverable z-depth-1-half">
                                                            <div class="panel-heading">

                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <h4 style="color: #793820;">Project Approved Amount</h4>
                                                                        <br />
                                                                        <h3 style="color: #793820;">
                                                                            <asp:Label ID="lblSanctionAmount" runat="server" Text=""></asp:Label></h3>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="panel panel-success hoverable z-depth-1-half">
                                                            <div class="panel-heading">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <h4 style="color: #155010;">Project Released Amount</h4>
                                                                        <br />
                                                                        <h3 style="color: #155010;">
                                                                            <asp:Label ID="lblReleaseAmount" runat="server" Text=""></asp:Label></h3>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="panel panel-danger hoverable z-depth-1-half">
                                                            <div class="panel-heading">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <h4 style="color: #4A274F;">Project Balance Amount</h4>
                                                                        <br />
                                                                        <h3 style="color: #4A274F;">
                                                                            <asp:Label ID="lblBalanceAmount" runat="server" Text=""></asp:Label></h3>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="panel">

                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="panel z-depth-1-half" style="background-color: #8AAAE5;">

                                                            <div class="panel-heading">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <h3 class="text-center" style="color: #FFFFFF;"><i class="fa fa-user-secret"></i><span style="letter-spacing: 3px;">&nbsp;College Mentor</span>
                                                                            <br />
                                                                            <br />
                                                                            <asp:Image ID="imgCollegeMentor" alt="Manger Image" Width="80px" Height="80px" CssClass="img-circle img-thumbnail img-rounded" runat="server" />
                                                                        </h3>
                                                                        <h2 class="text-center " style="color: #FFFFFF;">
                                                                            <asp:Label ID="lblManagerName" runat="server" Text=""></asp:Label></h2>
                                                                        <h3 class="text-center" style="color: #FFFFFF;">
                                                                            <asp:Label ID="lblManagerEmailId" runat="server" Text=""></asp:Label></h3>
                                                                        <h4 class="text-center" style="color: #FFFFFF;">
                                                                            <asp:Label ID="lblManagerMobileNo" runat="server" Text=""></asp:Label></h4>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <ul class="list-group z-depth-1-half">
                                                                    <li class="list-group-item list-group-item-info hoverable" id="Facebook" runat="server">
                                                                        <div class="row">
                                                                            <div class="col-md-1">Facebook:</div>

                                                                            <div class="col-md-11" style="cursor: pointer;">
                                                                                <a id="btnFacebook" target="_blank" runat="server">facebook</a>

                                                                            </div>
                                                                        </div>


                                                                    </li>
                                                                    <li class="list-group-item list-group-item-success hoverable" id="Twitter" runat="server">
                                                                        <div class="row">
                                                                            <div class="col-md-1">Twitter:</div>

                                                                            <div class="col-md-11" style="cursor: pointer;">
                                                                                <a id="btnTwitter" target="_blank" runat="server">twitter</a>

                                                                            </div>
                                                                        </div>
                                                                    </li>
                                                                    <li class="list-group-item list-group-item-warning hoverable" id="InstaGram" runat="server">
                                                                        <div class="row">
                                                                            <div class="col-md-1">InstaGram:</div>
                                                                            <div class="col-md-11" style="cursor: pointer;">
                                                                                <a id="btnInstaGram" target="_blank" runat="server">instagram</a>
                                                                            </div>
                                                                        </div>
                                                                    </li>
                                                                    <li class="list-group-item list-group-item-danger hoverable" id="WhataApp" runat="server">
                                                                        <div class="row">
                                                                            <div class="col-md-1">WhataApp:</div>

                                                                            <div class="col-md-11" style="cursor: pointer;">
                                                                                <a id="btnWhatsApp" target="_blank" runat="server">whatsapp</a>

                                                                            </div>
                                                                        </div>

                                                                    </li>
                                                                </ul>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Project tab -->

                                    <div id="FiveStarProjectTab" runat="server" class="tab-pane">
                                         <div class="visible-lg visible-md visible-sm">
                                            <br />
                                         </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="panel">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <asp:Repeater ID="Repeater1" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="progress">
                                                                            <div class="progress-bar progress-bar-success" id="RatingProgress" runat="server" role="progressbar" aria-valuemax="100">
                                                                                40% Complete (success)
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                               
                                                             <%--   <div class="progress">
                                                                    <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%">
                                                                        50% Complete (info)
                                                                    </div>
                                                                </div>
                                                                <div class="progress">
                                                                    <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                                        60% Complete (warning)
                                                                    </div>
                                                                </div>
                                                                <div class="progress">
                                                                    <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100" style="width: 70%">
                                                                        70% Complete (danger)
                                                                    </div>
                                                                </div>--%>
                                                         </div>
                                                     </div>
                                                    </div>

                                                </div>
                                            </div>
                                             <div class="col-md-8">
                                                <div class="panel">
                                                    <div class="panel-body">
                                                     <div class="row">
                                                         <div class="col-md-12">

                                                         </div>
                                                     </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                       
                                    </div>


                                    <!-- Event tab -->
                                    <div id="EventTab" runat="server" class="tab-pane">
                                        <div class="hidden-xs">
                                            <br />
                                        </div>
                                        <h3><i class="fa fa-comments"></i>&nbsp; Events
                       <div class="row">
                           <div class="col-md-8">
                               <ul class="list-item member-list" style="height: 550px; overflow: auto;">
                                   <asp:Repeater ID="rptEvent" runat="server">
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
                                                       <p style="line-height: 1.6;">
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
                           <div class="col-md-4">
                               <div class="row">
                                   <div class="col-md-12">
                                       <div class="embed-section">
                                           <div class="embed-responsive embed-responsive-16by9">
                                               <%-- <iframe class="embed-responsive-item" src="https://www.youtube.com/embed/DI4zp7yeuMU" allowfullscreen=""></iframe>--%>

                                               <%-- <iframe src="../../Documents/Videos/1.mp4?rel=0" allowfullscreen=""></iframe>--%>

                                               <video controls="true">
                                                   <source src="../../Documents/Videos/1.mp4" type="video/mp4" />
                                               </video>
                                           </div>
                                       </div>
                                   </div>
                               </div>
                               <div class="row">
                                   <div class="col-md-12">
                                       <div class="embed-section">
                                           <div class="embed-responsive embed-responsive-16by9">

                                               <video controls="true">
                                                   <source src="../../Documents/Videos/2.mp4" type="video/mp4" />
                                               </video>

                                           </div>
                                       </div>
                                   </div>
                               </div>
                               <div class="row">
                                   <div class="col-md-12">
                                       <div class="embed-section">
                                           <div class="embed-responsive embed-responsive-16by9">
                                               <video controls="true">
                                                   <source src="../../Documents/Videos/3.mp4" type="video/mp4" />
                                               </video>
                                           </div>
                                       </div>
                                   </div>
                               </div>
                           </div>
                       </div>
                                        </h3>
                                        <br />
                                    </div>


                                </div>

                            </div>


                        </div>
                    </div>
                </div>

                <br />
                <br />
                <br />
                <br />
                <br />
                <footer class="footer-main">
                    <small>Handcrafted by</small><br>
                    <strong>Deshpade Foundation</strong>  <a target="_blank" href="#/">Hubli</a>
                </footer>
            </div>

        </div>
    </form>
</body>
</html>
