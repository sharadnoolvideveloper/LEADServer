<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8"/>
	<title>LEADCampus | Home Page</title>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width,initial-scale=1" />
	<meta http-equiv="Copyright" content="Copyright (c) 2019 Deshpande Foundation India"/>
	<meta name="distribution" content="global"/>
	<meta name="language" content="english"/>
	<meta http-equiv="content-language" content="en"/>
	<meta name="rating" content="safe for kids"/>
	<meta name="web_author" content="Deshpande Foundation India"/>
	<meta name="resource-type" content="document"/>
	<meta name="classification" content="Education"/>
	<meta name="doc-type" content="public"/>
	<meta http-equiv="cahe-control" content="cache"/>
	<meta name="contactName" content="Vivek Pawar"/>
	<meta name="contactOrganization" content="Deshpande Foundation India"/>
    <meta name="og:latitude" content="15.3709"/>
    <meta name="og:longitude" content="75.1234"/>
	<meta name="contactCity" content="Hubballi"/>
	<meta name="contactState" content="Karnataka"/>
	<meta name="contactCountry" content="India"/>
	<meta name="contact NetworkAddress" content="leadmis{@}dfmail{.}org"/>
	<meta name="linkage" content="http://mis.leadcampus.org"/>
	<meta property="og:site_name" content="http://mis.leadcampus.org"/>
	<meta property="og:type" content="website"/>
	<meta property="og:title" content="LEADCampus | Manager Console (LEAD)"/>
	<meta property="og:url" content="http://mis.leadcampus.org"/>
	<meta property="og:image" content="http://mis.leadcampus.org/css/images/logo.png"/>
	<meta property="og:image" content="http://mis.leadcampus.org/CSS/LoginCSS/Images/banner.jpg"/>
	<meta name="google-analytics" content="UA-126729086-1"/>
	<meta name="author" content="Deshpande Foundation India: www.deshpandefoundationindia.org"/>
	<meta property="og:description" content="The Leaders Accelerating Development (LEAD) Program of Deshpande Foundation, Hubballi, Karnataka Complaints... Start with THEY, Solutions... start with I"/>
	<meta name="description" content="The Leaders Accelerating Development (LEAD) Program of Deshpande Foundation, Hubballi, Karnataka The Leaders Accelerating Development (LEAD) Program of Deshpande Foundation, Hubballi, Karnataka Complaints... Start with THEY, Solutions... start with I"/>
	<meta name="keywords" content="Student Programme,Lead Adda, Lead Programme hubli, leaders in hubli,leadership,deshpande foundation hubli,lead deshpande foundation,challenge, school ,lead colleges,lead mobile app,lead android app"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="mobile-web-app-capable" content="yes" />
   <meta name="theme-color" content="#182848" />
    <meta name="msapplication-navbutton-color" content="#0072ff" />
    <meta name="apple-mobile-web-app-status-bar-style" content="#0072ff" />
    <link rel='shortcut icon' type='image/x-icon' href="../../CSS/Images/logo.png" />
  
    <link href="CSS/LoginCSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/LoginCSS/Style.css" rel="stylesheet" />
    <link href="CSS/CommonCSS/toster.css" rel="stylesheet" />  
    <script src="JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="JS/LoginJS/bootstrap.js"></script>  
    <script src="JS/CommonJS/toster.js"></script>
    <link href="CSS/LoginCSS/font-awesome-animation.css" rel="stylesheet" />
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
        .parallax {
            min-height: 800px;
            color: white;
            text-shadow: 0px 1px 1px #000;

        }
        body,html {
            margin: 0;
            padding: 0;
            overflow: hidden;
            background: url("CSS/LoginCSS/Images/banner.jpg");
            background-size: cover;
            position: fixed;
                top: -1vh;
            left: -1vw;
            width: 102vw;
            height: 102vh;
        }
   
        #img_logo {
            margin-top: 10px;
            height: 80px;

        }
     
        .fun {
            border-radius: 22px;
            background: #fff;
            padding: 10px;
            margin: 20px;
        }
        .form-control {
            border: none;
        }
   
    </style>

</head>
<body class="brandFont back">

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-158699154-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-158699154-1');
    </script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <%-- <div id="loader-overlay">
            <img src="css/Images/loader.gif" alt="Loading" />
        </div>
     --%>
             
        <div class="parallax hidden-xs">
            <br />
            <div class="row">
                <div class="col-lg-12 fa faa-float animated">
                    <img id="img_logo" class="img-responsive img-circle center-block z-depth-2 hoverable" src="CSS/Images/logo.png" />
                </div>
            </div>
            <h3 class="text-center brandFont">
                <!--<span id="lblName">Lead leadership programme</span>-->
                <a style="letter-spacing: 5px; color: white">Complaints... Start with THEY, Solutions... start with I</a>
            </h3>
        </div>
       
        <div class="parallax visible-xs" style="min-height: 680px;">
            <br />
            <div class="row ">
                <div class="col-lg-12 fa faa-float animated">
                    <img id="img_logo1" style="width: 50px; height: 50px;left:25px;position:relative;" class="img-responsive img-circle 
                       z-depth-2 hoverable center-block" src="CSS/Images/logo.png" />
                </div>
            </div>
            <h4 class="brandFont" style="left:15px;position:relative;">
                <!--<span id="lblName">Lead leadership programme</span>-->
                <a style="letter-spacing: 3px; color: white;font-size:small">Complaints... Start with THEY, Solutions... start with I</a>
            </h4>
        </div>
        <div class="container-fluid">
            <div class="row text-center " style="margin-top: -570px">
                <div class="col-md-4 fun hoverable">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel ">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-10 text-center">
                                            <asp:Label ID="lblLoginError" ForeColor="Red" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                         <div class="col-md-9">
                                            <asp:RequiredFieldValidator ID="rfvtxtLeadId" ControlToValidate="txtUserName" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>

                                            <asp:TextBox ID="txtUserName" CssClass="form-control text-uppercase" autocomplete="off" placeholder="LEAD Id" runat="server"></asp:TextBox>
                                        </div>                                       
                                    </div>
                                    <hr />
                                    <div class="row">
                                         <div class="col-md-9 form-group">
                                            <asp:RequiredFieldValidator ID="rfvtxtMobileNo" ControlToValidate="txtPassword" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>

                                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control " runat="server"  placeholder="Mobile No" ></asp:TextBox>

                                        </div>
                                       
                                        <div class="col-md-2">

                                            <asp:LinkButton ID="btnStudentLogin" CssClass="btn btn-sm btn-primary hoverable"  OnClick="btnStudentLogin_Click" ValidationGroup="Student" runat="server">Login </asp:LinkButton>
                                            <asp:LinkButton ID="btnGoogle_Login" CssClass="btn btn-sm btn-info hoverable" Visible="false"  OnClick="btnGoogle_Login_Click"  runat="server">  Google Login  &nbsp; <img src="CSS/Images/Google_Logo.png" style="height:30px;width:30px;" /> </asp:LinkButton>
                                        </div>

                                    </div>
                                    <div class="row pull-left" style="cursor:pointer;">
                                            &nbsp;&nbsp;
                                            <asp:LinkButton ID="btnForgottonPassword" OnClick="btnForgottonPassword_Click" runat="server">Forgot Password? </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="row">
                        <div class="col-md-12">
                         
                        </div>
                    </div>
                    
                </div>
                <div class="col-md-4 pull-right hidden-sm hidden-xs brandFont" style="margin-top: -18px; margin-left: 25px;">
                    <h1 class="text-center" style="color: white; font-size: 40pt; letter-spacing: 1px;">New to LEAD ?</h1>
                    <h3 class="text-center" style="color: white; letter-spacing: 3px;">Give Miss Call to Register</h3>
                    <h1 class="text-center" style="color: white; font-size: 40pt; letter-spacing: 3px;">08047091456</h1>
                    <span style="right: 10px; cursor: pointer; text-align: center;" class="fa faa-float animated">
                        <asp:LinkButton ID="btnAppDownload" OnClick="btnAppDownload_Click" runat="server">
                             <img src="CSS/Images/Icon_Android.png" style="width: 90px; height: 90px;" alt="Icon" />
                        </asp:LinkButton>

                        <%--<a href="http://ec2-18-138-98-10.ap-southeast-1.compute.amazonaws.com:9090/CSS/leadcampus.apk">
                            <img src="CSS/Images/Icon_Android.png" style="width: 90px; height: 90px;" alt="Icon" />
                        </a>--%>
                    </span>
                </div>
                <div class="col-md-4  hidden-md hidden-lg text-center brandFont" style="color: white; margin-top: 10px; margin-left: 15px;">
                    <h3 style="color: white; font-size: 15pt; letter-spacing: 3px;">New to LEAD ?</h3>
                    <h4 style="color: white;">Give Miss Call to Register</h4>
                    <h3 style="color: white; font-size: 15pt; letter-spacing: 3px;">08047091456</h3>

                    <span style="right: 10px; cursor: pointer; text-align: center;" class="fa faa-float animated">
                        <asp:LinkButton ID="LinkButton1" OnClick="btnAppDownload_Click" runat="server">
      <img src="CSS/Images/Icon_Android.png" style="width: 90px; height: 90px;" alt="Icon" />
                        </asp:LinkButton>
                        <%--   <a href="http://ec2-18-138-98-10.ap-southeast-1.compute.amazonaws.com:9090/CSS/leadcampus.apk" target="_blank" >
                       <img src="CSS/Images/Icon_Android.png"  style="width:90px;height:90px;" alt="Icon"/>
                     </a>--%>
                    </span>
                 



                </div>
                <div class="row visible-xs">
                    <marquee style="color: white;"><p class="brandFont" style="font-size: 16pt">HI, Students for any <b> Query</b> Call <strong><b>9686654748</b></strong> and Mail <strong><b>LEADMIS@dfmail.org</b></strong></p></marquee>
                </div>
            </div>
            <br />
            <br />
            <div class="row hidden-xs">
                 <marquee style="color:white;" class="hidden-xs"><p class="brandFont" style="font-size: 18pt">HI, Students <b> Help Line</b> Call <strong><b>9686654748</b></strong> and Mail <strong><b>LEADMIS@dfmail.org</b></strong> </p></marquee>
            </div>
            <span class="hidden-xs">
                <br />
                <br />
                 <br />

            </span>         
            <div class="row">
                <div class="text-center">
                   <span style="color:white;" class="brandFont text-center">Handcrafted By </span> 
                    <br />
                    <img src="CSS/Images/df_Small.png" class="hoverable" />
               
                </div>
                <span class="pull-right brandFont">
                    <strong style="color:white;padding-right:65px;">Version 15.0.1 </strong>
                </span>
            </div>
            <script type="text/javascript">
                function ErrorModal() {
                    $('#ErrorModal').modal('show');

                }
            </script>
            <div id="ErrorModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" style="margin-top: 0px">
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
        </div>
     
      <div id="POP_ForgotPassword" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>Enter EmailID
                          <a class="pull-right text-right" data-dismiss="modal" >
                              <i class="fa fa-remove fa-fw" style="cursor:pointer;">x</i>
                          </a>
                    </h3>
                    <div class="row">
                        <div class="col-md-12">
                            <label for="txtMailId" class="text-danger brandFont">
                              
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true"
                                    ControlToValidate="txtMailId" ErrorMessage="* InValid Mail Id" ForeColor="DarkRed"
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMailId" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtMailId" autocomplete="off" CssClass="form-control brandFont" TextMode="Email" placeholder="Enter Registered eMailId" runat="server"></asp:TextBox>
                        </div>
                    </div>
                  
                    <br />
                       <br />
                    <div class="row">
                        <div class="col-md-4 col-md-offset-1">
                            <asp:LinkButton ID="btnSendForgotPassword" OnClick="btnSendForgotPassword_Click" ValidationGroup="Request" CssClass="btn btn-primary btn-black brandFont" runat="server"><span class="fa fa-send">&nbsp; <i class="brandFont"> Request Password</i></span>  </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <div id="POP_ErrorMsg" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h2>
                    <span class="fa fa-warning"></span>&nbsp; <asp:Label ID="lblErroMessage" runat="server"></asp:Label>
                    </h2>
                </div>
            </div>
        </div>
    </div>
       <script type="text/javascript">
         function POP_ForgotPassword() {
            $('#POP_ForgotPassword').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
        }
       </script>
         <script type="text/javascript">
             function POP_ErrorMsg() {
                 $('#POP_ErrorMsg').modal({
                show: true
            });
        }
       </script>

        
    </form>
</body>
</html>
