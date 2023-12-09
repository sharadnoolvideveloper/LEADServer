<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Testing123.aspx.cs" Inherits="Pages_Student_Testing123" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8"/>
	<title>LEADCampus | Student Console (Lead) </title>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width,initial-scale=1"/>
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



    <link href="../../CSS/StudentCSS/entypo.css" rel="stylesheet" />
    <link href="../../CSS/LoginCSS/font-awesome.min.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/clevex-core.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/clevex-forms.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/perfect-scrollbar.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
  
    <link href="../../CSS/LoginCSS/Style.css" rel="stylesheet" />   
    <link href="../../CSS/CommonCSS/jquery.filer.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/jquery.filer-dragdropbox-theme.css" rel="stylesheet" />  
     <link href="../../CSS/CommonCSS/bootstrap-select.css" rel="stylesheet" />   
     <link href="../../CSS/CommonCSS/toster.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/bootstrap-datepicker_fun.min.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/Custumized.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/jquery.min.js"></script>
    <script src="../../JS/CommonJS/bootstrap.js"></script>
  
    <script src="../../JS/CommonJS/toster.js"></script>
    <script src="../../JS/CommonJS/Loader.js"></script>
    <script src="../../JS/StudentJS/Numeric.js"></script>
    <script src="../../JS/CommonJS/jquery-1.9.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <br />
        <div class="container-fluid text-center">
            <div class="row">
                <div class="col-sm-3 well">
                    <div class="well">
                        <p><a href="#">My Profile</a></p>
                        <img src="bird.jpg" class="img-circle" height="65" width="65" alt="Avatar">
                    </div>
                    <div class="well">
                        <p><a href="#">Interests</a></p>
                        <p>
                            <span class="label label-default">News</span>
                            <span class="label label-primary">W3Schools</span>
                            <span class="label label-success">Labels</span>
                            <span class="label label-info">Football</span>
                            <span class="label label-warning">Gaming</span>
                            <span class="label label-danger">Friends</span>
                        </p>
                    </div>
                    <div class="alert alert-success fade in">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                        <p><strong>Ey!</strong></p>
                        People are looking at your profile. Find out who.
                    </div>
                    <p><a href="#">Link</a></p>
                    <p><a href="#">Link</a></p>
                    <p><a href="#">Link</a></p>
                </div>
                <div class="col-sm-7">

                   <div class="row">
                 <div class="col-md-4">
                     <div class="panel" style="background-color: white;">

                         <div class="panel-body">
                             <h3>Project Ratings
                                 <asp:Button ID="btn" runat="server" OnClick="btn_Click"  Text="Button" />
                             </h3>
                             <div class="row">
                                 <div class="col-xs-1 col-md-1 col-sm-1 col-lg-1">
                                     <div class="pull-left" style="width: 30px; line-height: 1.5;">
                                         <div style="height: 9px; margin: 5px 0;">5 <span class="fa fa-star text-success"></span></div>
                                     </div>
                                 </div>
                                 <div class="col-xs-10">
                                     <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                 

                                     <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                     <div class="progress" style="height: 15px; margin: 8px 0;">
                                         <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="5" aria-valuemin="0" aria-valuemax="5"
                                             data-toggle="tooltip" data-placement="top" title="" data-original-title="5 Star" style="max-width: 100%">
                                             <h3 style="color: whitesmoke"><span class="title">12</span></h3>
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
                                             <h3 style="color: whitesmoke"><span class="title">3</span></h3>
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
                                             <h3 style="color: whitesmoke"><span class="title">7</span></h3>
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
                                             <h3 style="color: whitesmoke"><span class="title">3</span></h3>
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
                                             <h3 style="color: whitesmoke"><span class="title">5</span></h3>
                                         </div>
                                     </div>
                                 </div>
                             </div>

                         </div>
                     </div>
                 </div>
                 <div class="col-md-8">
                     <div class="panel" style="background-color: white;height:235px;overflow:auto;">
                         <div class="panel-body">
                             <div class="row">
                                 <div class="col-md-2">
                                     <img src="../../CSS/Images/Initiator_Medal.png"   />
                                 </div>
                                  <div class="col-md-2">
                                     <img src="../../CSS/Images/Initiator_Medal.png"  />
                                 </div>
                                  <div class="col-md-2">
                                     <img src="../../CSS/Images/Initiator_Medal.png" />
                                 </div>
                                 <div class="col-md-2">
                                     <img src="../../CSS/Images/Initiator_Medal.png" />
                                 </div>
                                 <div class="col-md-2">
                                     <img src="../../CSS/Images/Initiator_Medal.png"  />
                                 </div>
                                 <div class="col-md-2">
                                     <img src="../../CSS/Images/Initiator_Medal.png"  />
                                 </div>
                             </div>
                         </div>
                     </div>
                 </div>
             </div>

                    <div class="row">
                        <div class="col-sm-3">
                            <div class="well">
                                <p>John</p>
                                <img src="bird.jpg" class="img-circle" height="55" width="55" alt="Avatar">
                            </div>
                        </div>
                        <div class="col-sm-9">
                            <div class="well">
                                <p>Just Forgot that I had to mention something about someone to someone about how I forgot something, but now I forgot it. Ahh, forget it! Or wait. I remember.... no I don't.</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="well">
                                <p>Bo</p>
                                <img src="bandmember.jpg" class="img-circle" height="55" width="55" alt="Avatar">
                            </div>
                        </div>
                        <div class="col-sm-9">
                            <div class="well">
                                <p>Just Forgot that I had to mention something about someone to someone about how I forgot something, but now I forgot it. Ahh, forget it! Or wait. I remember.... no I don't.</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="well">
                                <p>Jane</p>
                                <img src="bandmember.jpg" class="img-circle" height="55" width="55" alt="Avatar">
                            </div>
                        </div>
                        <div class="col-sm-9">
                            <div class="well">
                                <p>Just Forgot that I had to mention something about someone to someone about how I forgot something, but now I forgot it. Ahh, forget it! Or wait. I remember.... no I don't.</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="well">
                                <p>Anja</p>
                                <img src="bird.jpg" class="img-circle" height="55" width="55" alt="Avatar">
                            </div>
                        </div>
                        <div class="col-sm-9">
                            <div class="well">
                                <p>Just Forgot that I had to mention something about someone to someone about how I forgot something, but now I forgot it. Ahh, forget it! Or wait. I remember.... no I don't.</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2 well">
                    <div class="thumbnail">
                        <p>Upcoming Events:</p>
                        <img src="paris.jpg" alt="Paris" width="400" height="300">
                        <p><strong>Paris</strong></p>
                        <p>Fri. 27 November 2015</p>
                        <button class="btn btn-primary">Info</button>
                    </div>
                    <div class="well">
                        <p>ADS</p>
                    </div>
                    <div class="well">
                        <p>ADS</p>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
