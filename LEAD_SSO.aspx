<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEAD_SSO.aspx.cs" Inherits="LEAD_SSO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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

</head>
<body>
    <form id="form1" runat="server">
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
             function POP_ErrorMsg() {
                 $('#POP_ErrorMsg').modal({
                show: true
            });
        }
       </script>
    </form>

    
</body>
</html>
