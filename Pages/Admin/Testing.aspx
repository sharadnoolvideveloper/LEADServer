<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Testing.aspx.cs" Inherits="Pages_Admin_Testing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstEmployee]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div class="input-group color-picker colorpicker-element focused" title="Using format option" data-options="{&quot;format&quot;: null}">
                                <input type="text" class="form-control input-lg" value="#305AA2">
                                <span class="input-group-append">
                        <span class="input-group-text add-on white">
                            <i class="circle" style="background-color: rgb(80, 51, 134);"></i>
                      </span>
                    </span>
                            </div>

    <asp:ListBox ID="lstEmployee" runat="server" CssClass="multiselect" SelectionMode="Multiple">

        <asp:ListItem Text="Nikunj Satasiya" Value="1" />
        <asp:ListItem Text="Dev Karathiya" Value="2" />
        <asp:ListItem Text="Hiren Dobariya" Value="3" />
        <asp:ListItem Text="Vivek Ghadiya" Value="4" />
        <asp:ListItem Text="Pratik Pansuriya" Value="5" />
    </asp:ListBox>

    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
  <!-- Indicators -->
  <ol class="carousel-indicators">
    <li data-target="#carousel-example-generic" data-slide-to="0"
      class="active"></li>
    <li data-target="#carousel-example-generic" data-slide-to="1"></li>
    <li data-target="#carousel-example-generic" data-slide-to="2"></li>
  </ol>
  <!-- Wrapper for slide content -->
  <div class="carousel-inner" role="listbox">
    <div class="item">
      <img src="../../CSS/Images/PrayanaBottom_EmailTemplate.jpg" alt="...">
      <div class="carousel-caption">
        ...
      </div>
    </div>
    ... Only one slide for brevity.
  </div>
  <!-- Controls -->
  <a class="left carousel-control" href="#carousel-example-generic" role="button"
    data-slide="prev">
    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="right carousel-control" href="#carousel-example-generic" role="button"
    data-slide="next">
    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>
</asp:Content>

