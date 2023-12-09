<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reg.aspx.cs" Inherits="reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>LEADCampus | Reg Page</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server" class="brandFont2">
        <div class="row">
            <div class="col-md-12">
                <h2>Student Registration</h2>
            </div>
        </div>
    </form>
</body>
</html>
