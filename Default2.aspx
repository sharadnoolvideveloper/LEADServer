<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>Animated Login Page</title>

        <script src="JS/CommonJS/1.11.1jquery.min.js"></script>
    <style>
        body {
            margin: 0;
            padding: 0;
            overflow: hidden;
        }

        .back {
            background: url("CSS/LoginCSS/Images/banner.jpg");
            background-size: cover;
            position: absolute;
            top: -1vh;
            left: -1vw;
            width: 102vw;
            height: 102vh;
        }

        .front {
            background: url("CSS/Images/logo.png");
            background-size: cover;
            position: absolute;
            bottom: -1vh;
            right: 3vw;
            width: 50vw;
            padding-bottom: 30%;
        }
         #scroll {
   width: 200px;
   height: 300px;
   overflow-y: scroll;
 }
    </style>
         
     <script>
         $(function () {
         
             $('#scroll').animate({
                 scrollTop: $('#scroll')[0].scrollHeight
                 }, "slow");
            
         });
 </script>


    <style>
       
    </style>
</head>
<body>
  
    <form id="form1" runat="server">
        <asp:Button ID="btnDownload" runat="server" Text="Download LEAD" OnClick="btnDownload_Click" />
 
    </form>
    <br />
<div id="scroll">
    <br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/>blah<br/>halb<br/> <center><b>Voila!! You have already reached the bottom :)<b></center>
</div>


</body>
</html>
