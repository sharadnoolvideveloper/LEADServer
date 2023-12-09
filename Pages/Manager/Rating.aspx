<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="Rating.aspx.cs" Inherits="Pages_Manager_Rating" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/toster.js"></script>

    <script src="../../JS/CommonJS/rating.js"></script>
    <link href="../../CSS/CommonCSS/rating.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
   
    <style type="text/css">
        .Star {
            background-image:url("../../CSS/Images/Star.gif");
            height: 17px;
            width: 17px;
        }

        .WaitingStar {
            background-image: url('../../CSS/Images/WaitingStar.gif');
            height: 17px;
            width: 17px;
           
        }

        .FilledStar {
            background-image: url('../../CSS/Images/FilledStar.gif');
            height: 17px;
            width: 17px;
        }
        </style>
   
        <script type="text/javascript">
        $(function ()
        {
            $('.rating').rating();

            $('.ratingEvent').rating({ rateEnd: function (v) { $('#result').text(v); } });
        });
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <asp:Rating ID="Rating1" runat="server" AutoPostBack="true" OnChanged="Rating1_Changed"
            StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
            FilledStarCssClass="FilledStar">
        </asp:Rating>
        <br />
            <asp:Label ID="lbresult" runat="server" Text=""></asp:Label>
        <br />
        <asp:TextBox runat="server" ID="txtreview" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Button runat="server" Text="Submit Review" ID="btnsubmit" OnClick="btnsubmit_Click" />
     <p><b>Example</b></p>
    Base example<br />
    <input type="text" class="rating rating10" />

    <br />

    With a initial value 3<br />
    <input type="text" class="rating rating18" value="3" />

    <br />

    Readonly, just for display result.<br />
    <input type="text" class="rating rating8" readonly="readonly" value="3" />

    <br />

    Public Method, After voted trige the event "rateEnd"
    <input type="text" class="ratingEvent rating9" value="5" />
    <div><b id="result">5</b> start(s)</div>

    <p>&nbsp;</p>

    <b>How to use</b>
    <p>1. Include necessary JS and CSS files</p>
    <pre style="width:700px; background-color:#CCCCCC;">
        <code>
            &lt;script type="text/javascript" src="rating.js"&gt;&lt;/script&gt;
            &lt;link rel="stylesheet" type="text/css" href="rating.css" /&gt;
        </code>
    </pre>

    <p>2. Create a text box element (the class "rating10" contain a num "10", this will display 10 starts)</p>
    <pre style="width:700px; background-color:#CCCCCC;">
        <code>
            &lt;input type="text" class="rating rating10" /&gt;
        </code>
    </pre>

    <p>3. Fire plugin using jQuery selector</p>
    <pre style="width:700px; background-color:#CCCCCC;">
        <code>
           $(function ()
            {
                $('.rating').rating();
            });
        </code>
    </pre>

  
 
  
</asp:Content>

