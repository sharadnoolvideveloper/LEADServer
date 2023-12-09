<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="WorkDiary.aspx.cs" Inherits="Pages_Manager_WorkDiary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8"/>
	<title>LEADCampus | Manager Console (LEAD) </title>
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
    <link rel='shortcut icon' type='image/x-icon' href="../../CSS/Images/logo.png" />
    <script src="../../JS/CommonJS/toster.js"></script>
    <script src="../../JS/CommonJS/Numeric.js"></script>
    <script src="../../JS/CommonJS/jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
    <script src="../../JS/CommonJS/toster.js"></script>
    <link href="../../CSS/CommonCSS/Bootstrap_Grid_Advetise.css" rel="stylesheet" />
    <link href="../../CSS/LoginCSS/Style.css" rel="stylesheet" />
    <link href="../../CSS/ManagerCSS/app.v2.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/toster.css" rel="stylesheet" />
    <link href="../../CSS/LoginCSS/font-awesome.min.css" rel="stylesheet" />



    <script type="text/javascript">
        function success(msg) {
            toastr.options.timeOut = 4500; //1.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.success(msg);
        }
        function warning(msg) {
            toastr.options.timeOut = 4000; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.warning(msg);
        }
        function info(msg) {
            toastr.options.timeOut = 4000; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.info(msg);
        }
        function error(msg) {
            toastr.options.timeOut = 4000; //2.0 mili seconds 
            toastr.options.positionClass = "toast-bottom-left";
            toastr.error(msg);
        }
    </script>

    <style>
        .modal.left.fade .modal-dialog {
            left: -320px;
            -webkit-transition: opacity 0.3s linear, left 0.3s ease-out;
            -moz-transition: opacity 0.3s linear, left 0.3s ease-out;
            -o-transition: opacity 0.3s linear, left 0.3s ease-out;
            transition: opacity 0.3s linear, left 0.3s ease-out;
        }

        .modal.left.fade.in .modal-dialog {
            left: 0;
        }

        .modal.left .modal-dialog {
            position: fixed;
            margin: auto;
            width: 400px;
            height: 100%;
            -webkit-transform: translate3d(0%, 0, 0);
            -ms-transform: translate3d(0%, 0, 0);
            -o-transform: translate3d(0%, 0, 0);
            transform: translate3d(0%, 0, 0);
            box-shadow: 10px 4px 10px 0px #ccc;
        }

        .modal.left .modal-content {
            height: 100%;
            overflow-y: auto;
        }

        .modal-open .modal {
            margin-top: -20px;
        }

        .modal.left .modal-body {
            padding: 15px 15px 80px;
        }

     
       
    </style>

    <style>
        .form-control {
            display: block;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        }

            .form-control:focus {
                border-color: #66afe9;
                outline: 0;
                -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
                box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
            }

            .form-control::-moz-placeholder {
                color: #999;
                opacity: 1;
            }

            .form-control:-ms-input-placeholder {
                color: #999;
            }

            .form-control::-webkit-input-placeholder {
                color: #999;
            }
    </style>
    <script>
        $(document).ready(function () {
            $("body").on("contextmenu", function () {
                return false;
            });
        });
    </script>
</head>
<body onkeydown="return (event.keyCode != 116)">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="navbar navbar-inverse ">
            <div class="navbar-header">
                <button type="button" class="btn btn-facebook text-center brandFont2" style="height: 50px;" data-toggle="modal" data-target="#DiaryWork">
                    <i class="fa fa-plus"></i>&nbsp;&nbsp;&nbsp;NEW TASK
                </button>
                <span style="margin-right: 20px; margin-top: -10px;" class="pull-right">
                    <h3 class="brandFont2">&nbsp;&nbsp;&nbsp;&nbsp; Work Diary</h3>
                </span>
            </div>
        </div>
        <br />
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                      <div class="row">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-md-10">
                                                 <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-search"></i> &nbsp; Search</span>
                                            <input type="text" id="txtTaskSearch" onkeyup="SearchTaskDetail()" placeholder="Search" class="form-control" />
                                        </div>
                                            </div>
                                            <div class="col-md-2 hidden-xs hidden-sm">
                                                <asp:LinkButton ID="btnViewAll" CssClass="btn btn-info btn-block" OnClick="btnViewAll_Click" runat="server">View All</asp:LinkButton>
                                            </div>
                                        </div>
                                       
                                    </div>
                                    <div class="panel-body">
                                        <div class="table brandFont" style="height: 650px; overflow: auto;" id="SearchList">
                                            <asp:Repeater ID="dlWorkList" OnItemCommand="dlWorkList_ItemCommand" runat="server">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-12 col-xs-12 hvr-float-shadow ">
                                                            <div class="panel panel-default">
                                                                <div class="panel-footer">
                                                                    <div class="row">
                                                                        <div class="col-md-12 col-xs-12" style="letter-spacing:2px;">
                                                                         <span class="pull-right" style="background-color:#dda94e;border:solid;color:white;padding:3px 3px 2px 3px;"> <%# Container.ItemIndex + 1 %>   </span>
                                                                             <asp:LinkButton ID="btnDisable" CssClass="btn btn-circle" CommandArgument='<%# Eval("slno") %>' runat="server"><span class="fa fa-remove"></span> </asp:LinkButton>
                                                                            &nbsp;  Date : <%# Eval("entry_date") %> &nbsp;&nbsp; Duration : <%# Eval("Spent_Time") %>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row form-group">
                                                                        <div class="col-md-12 col-xs-12">
                                                                            <asp:Label ID="lblSlno" runat="server" Visible="false" Text='<%# Eval("slno") %>' />
                                                                            Group : <%# Eval("Main_CategoryName") %> &nbsp;&nbsp; Category : <%# Eval("Sub_CategoryName") %>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row form-group">
                                                                        <div class="col-md-12 col-xs-12 title">
                                                                           Description :  <%# Eval("description") %>
                                                                        </div>
                                                                    </div>
                                                                     <div class="row form-group">
                                                                        <div class="col-md-12 col-xs-12 title">
                                                                           Status :  <%# Eval("Progress") %>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                </div>
            </div>
          
        </div>

        <div class="modal left fade" id="DiaryWork" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #224fd7; color: white;">
                        <button type="button" class="close" data-dismiss="modal" style="color: white; border-radius: 100px;">&times;</button>
                        <h3 class="modal-title brandFont2"><span class="fa fa-book"></span>&nbsp;&nbsp;Daily Work </h3>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel" style="border: 0px;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Main Category
                                          
                                                        </label>
                                                        <asp:DropDownList ID="ddlMainCategory" OnSelectedIndexChanged="ddlMainCategory_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label>
                                                            Sub Category
                                          
                                                        </label>
                                                        <asp:DropDownList ID="ddlSubCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label>College Name</label>
                                                        <asp:DropDownList ID="ddlCollege" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label>
                                                          Planned Note
                                               <asp:RequiredFieldValidator ID="rfvtxtManagerName" ControlToValidate="txtDescription" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Diary" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <asp:TextBox ID="txtDescription" CssClass="form-control" placeholder="Enter the Planned Note why you are doing this task" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Total Count
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtParticipations" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Diary" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <asp:TextBox ID="txtParticipations" placeholder="Total Count in Number" CssClass="form-control" onkeypress="NumericOnly()" runat="server"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <label>
                                                            Process Status
                                                        </label>
                                                        <asp:DropDownList ID="ddlProgress" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="[Select]" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="Paid Registered" Value="PaidRegistered"></asp:ListItem>
                                                            <asp:ListItem Text="NonPaid Registered" Value="NonPaidRegistered"></asp:ListItem>
                                                            <asp:ListItem Text="Funded" Value="Funded"></asp:ListItem>
                                                            <asp:ListItem Text="Attended" Value="Attended"></asp:ListItem>
                                                            <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                                            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                            <asp:ListItem Text="OnHold" Value="OnHold"></asp:ListItem>
                                                            <asp:ListItem Text="Busy" Value="Busy"></asp:ListItem>
                                                            <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                                            <asp:ListItem Text="Postponed" Value="Postponed"></asp:ListItem>
                                                            <asp:ListItem Text="Scheduled" Value="Scheduled"></asp:ListItem>
                                                            <asp:ListItem Text="Re-Scheduled" Value="ReScheduled"></asp:ListItem>
                                                            <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Time Spent
                                                        </label>
                                                        <asp:DropDownList ID="ddlHH" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="[HH]" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                            <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                            <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                            <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                            <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                            <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                            <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                            <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                            <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                            <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label>
                                                            &nbsp;
                                            
                                                        </label>
                                                        <asp:DropDownList ID="ddlMM" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="[MM]" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                            <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                            <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label>
                                                             Outcome Note
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRemark" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Diary" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <asp:TextBox ID="txtRemark" CssClass="form-control" TextMode="MultiLine" placeholder="Enter the  Outcome Note as conclusion" Rows="4" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="panel-footer">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-lg btn-primary" ValidationGroup="Diary" OnClick="btnSubmit_Click" runat="server" OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Saving...';" UseSubmitBehavior="False" Text="Submit" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="ErrorModal" class="modal fade" role="dialog" style="margin-top: 0px">
            <div class="modal-dialog bg-danger">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-body">
                        <h3>Message</h3>
                        <p>
                            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
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

           <div id="POP_Confirm" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 30%;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h2>Want to Delete ?
                    </h2>
                    <div class="row">
                        <asp:Label ID="lblSlno" runat="server" Visible="false" Text=""></asp:Label>
                        <div class="col-md-offset-2 col-md-2">
                            <asp:LinkButton ID="btnNo" OnClick="btnNo_Click" CssClass="btn btn-danger" runat="server">NO</asp:LinkButton>
                        </div>
                        <div class="col-md-offset-3 col-md-2">
                            <asp:Button ID="btnYes" OnClick="btnYes_Click" CssClass="btn btn-info" runat="server" OnClientClick="this.disabled = true;this.value='Deleting..';" UseSubmitBehavior="False" Text="Delete" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

         <script type="text/javascript">
        function SearchTaskDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtTaskSearch");
            filter = input.value.toUpperCase();
            ul = document.getElementById("SearchList");
            li = ul.getElementsByClassName("hvr-float-shadow") ;
            for (i = 0; i < li.length; i++) {
                a = li[i];
                if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";

                }
            }
        }
    </script>
        <script type="text/javascript">
            function DiaryWork() {

                $('#DiaryWork').modal({
                    backdrop: 'static',
                    keyboard: true,
                    show: true
                });
                //$('#DiaryWork').modal('show');
            }

        </script>
        <script type="text/javascript">
            function ErrorModal() {
                $('#ErrorModal').modal('show');

            }
        </script>
         <script type="text/javascript">
        function POP_Confirm() {
            $('#POP_Confirm').modal({
                show: true
            });
        }
    </script>
        <script>
            $(document).ready(function () {
                $("#myInput").on("keyup", function () {
                    var value = $(this).val().toLowerCase();
                    $(".dropdown-menu li").filter(function () {
                        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                    });
                });
            });
        </script>
    </form>



</body>


</html>
