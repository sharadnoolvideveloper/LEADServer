<%@ Page Title="Manager Console" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="Pages_Manager_DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .padder {
            box-shadow: 0 5px 5px 0 rgba(0, 0, 0, 0.16), 0 2px 10px 0 rgba(0, 0, 0, 0.12);
        }
    </style>

    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/toster.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/chat.css" rel="stylesheet" />
    <style type="text/css">
        .Star {
            background-image: url("../../CSS/Images/Star.gif");
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
    <style>
        .row > .column {
            padding: 0 8px;
        }

        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        .quick-actions_homepage {
            margin-top: 0px;
        }

        .quick-actions li {
            min-width: 13%;
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
            margin-top: -115px;
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
    </style>
    <style>
        table {
            border-collapse: collapse;
            border-spacing: 0;
            width: 100%;
            border: 1px solid #ddd;
        }

        th, td {
            text-align: left;
            padding: 8px;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
    <style>
        #SearchList th {
            background-color: #1900ff;
            color: white;
        }

        #ProposedList th {
            background-color: #1900ff;
            color: white;
        }

        #ApprovedList th {
            background-color: #1900ff;
            color: white;
        }

        #RequestForModificationList th {
            background-color: #1900ff;
            color: white;
        }

        #RequestForCompletionList th {
            background-color: #1900ff;
            color: white;
        }

        #CompletionList th {
            background-color: #1900ff;
            color: white;
        }

        #RejectProjectList th {
            background-color: #1900ff;
            color: white;
        }

        #FundAmountProjectList th {
            background-color: #1900ff;
            color: white;
        }

        #FeesPaidUnPaidList th {
            background-color: #1900ff;
            color: white;
        }

        .chat {
            height: 300px;
            overflow-x: scroll;
        }
    </style>

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
            toastr.options.positionClass = "toast-top-right";
            toastr.error(msg);
        }
    </script>
    <script type="text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        row.style.backgroundColor = "aqua";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            row.style.backgroundColor = "#C2D69B";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

    </script>
    <script>



</script>
    <script>

        function FundingAmount(PDID, BalAmt, leadid, Title) {
            if (BalAmt > 0) {

                $("#<%=lblFundingAmountPDID.ClientID%>").val(PDID).hide();
                $("#<%=lblFundingAmountBalance.ClientID%>").val(BalAmt).hide();
                $("#<%=lblFundingLeadId.ClientID%>").val(leadid).hide();
                $("#<%=txtFundingAmount.ClientID%>").val(BalAmt);
                $("#<%=lblFundingProjectTitle.ClientID%>").val(Title).hide();

                $('#POP_FundAmount').modal({
                    show: true
                });

            }
            else {
                toastr.error('Released Full Fund', 'Warning!', { timeOut: 5000 })
            }


        }

    </script>
    <script>
        $(function () {
            $('.chat').animate({
                scrollTop: $('.chat')[0].scrollHeight
            }, "fast");
        });
    </script>
    <%--
    <script>
        $(function () {
            $("#<%=btnSaveCommentChat.ClientID%>").click(function () {
                $("label[for=<%= txtChatMessage.ClientID %>]").text("sharad");
                $('.chat').animate({
                    scrollTop: $('.chat')[0].scrollHeight
                }, "fast");
            });
        });
       
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />


    <section id="content" class="content-sidebar bg-white" style="height: 750px; overflow: auto;">
        <asp:Label ID="lblErrorfield" runat="server" Text=""></asp:Label>
        <!-- .sidebar -->

        <div class="row">
            <div class="col-md-2">

                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="bg-lighter">
                                    <div class="text-center bg-white hidden-xs">
                                        <asp:Image ID="imgManagerProfilePicLeft" ImageUrl="~/CSS/Images/NoImage.png" Width="220px" Height="240px" runat="server" />

                                    </div>
                                    <div class="bg-light padder padder-v">
                                        <span class="h4">
                                            <asp:Label ID="lblManagerNameLeft" Font-Size="Small" runat="server" Text=""></asp:Label></span>
                                        <small class="block m-t-mini">Programme Manager</small>

                                        <asp:LinkButton ID="btnChangePassword" OnClick="btnChangePassword_Click" runat="server" CssClass="btn btn-primary btn-block "> Change &nbsp;<span class="fa fa-lock"></span></asp:LinkButton>

                                    </div>

                                    <div class="list-group">

                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=DashBoard" runat="server" id="leftDashboard" class="list-group-item  hoverable "><span class="badge m-r bg-primary ">
                                            <asp:Label ID="lblTotalProjectLeft" runat="server"></asp:Label></span>Dashboard</a>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=Proposed" id="leftProposed" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-primary ">
                                            <asp:Label ID="lblProposedProjectLeft" Text="0" runat="server"></asp:Label></span>Proposed Projects   </a>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=Approved" id="leftApproved" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-danger">
                                            <asp:Label ID="lblApprovedProjectsLeft" Text="0" runat="server"></asp:Label></span>Approved Projects</a>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=RequestForModification" id="leftRequestForModification" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-info">
                                            <asp:Label ID="lblRequestForModification" Text="0" runat="server"></asp:Label></span>Modification Requests  </a>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=RequestForCompletion" id="leftRequestForCompletion" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-success">
                                            <asp:Label ID="lblRequestForCompletionProjectsLeft" Text="0" runat="server"></asp:Label></span>Completion Requests</a>
                                       <%-- changed code --%>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=Draft" id="leftdrafted" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-danger">
                                            <asp:Label ID="lbldraftedprojectsleft" Text="0" runat="server"></asp:Label></span>Draft Projects</a>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=Completed" id="leftCompleted" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-primary">
                                            <asp:Label ID="lblCompletedProjectsLeft" runat="server" Text="0"></asp:Label></span> Completed Projects</a>

                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=DeActivate" id="lefDeActive" runat="server" class="list-group-item hoverable hidden"><span class="badge m-r bg-info">
                                            <asp:Label ID="lblDeActivatedStudents" runat="server" Text="0"></asp:Label></span> De-Activated Student </a>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=Rejected" id="leftRejected" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-primary">
                                            <asp:Label ID="lblRejectedProjectLeft" runat="server" Text="0"></asp:Label></span> Rejected Projects</a>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=FundAmount" id="leftFundAmount" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-primary">
                                            <asp:Label ID="lblFundAmountLeft" runat="server" Text="0"></asp:Label></span> Fund Amount</a>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=Registration" id="A1" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-primary">
                                            <asp:Label ID="lblRegistration" runat="server" Text="0"></asp:Label></span>Fees Details</a>
                                        <a href="~/Pages/Manager/DashBoard.aspx?vwType=Tshirt" id="LeftTshirt" runat="server" class="list-group-item hoverable"><span class="badge m-r bg-primary">
                                            <asp:Label ID="lblTotalTshirt" runat="server" Text="0"></asp:Label></span>T-shirt</a>
                                        <a class="hoverable">
                                            <asp:LinkButton ID="btnLogoutDashboard" CssClass="list-group-item" OnClick="btnLogoutDashboard_Click" runat="server"><span class="badge m-r bg-danger"><i class="fa fa-sign-out"></i></span>Logout</asp:LinkButton>
                                        </a>
                                    </div>

                                </div>

                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-lg-10 col-xs-12">
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="vwDashboard" runat="server">
                        <div class="row">
                            <div class="quick-actions_homepage hidden-xs ">
                                <ul class="quick-actions text-center">
                                    <li class="bg_lb col-md-1 hoverable"><a href="DashBoard.aspx?vwType=DashBoard"><i class="fa fa-bell"></i><span class="label label-danger">
                                        <asp:Label ID="lblAllProjectsCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                    </span>
                                        <br />
                                        <br />
                                        All</a></li>
                                    <li class="bg_ly col-md-1 hoverable"><a href="DashBoard.aspx?vwType=Proposed"><i class="fa fa-list"></i><span class="label label-info">
                                        <asp:Label ID="lblProposedProjectsCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                    </span>
                                        <br />
                                        <br />
                                        Proposed</a> </li>
                                    <li class="bg_lg col-md-1 hoverable"><a href="DashBoard.aspx?vwType=Approved"><i class="fa fa-list-ol"></i><span class="label label-success">
                                        <asp:Label ID="lblApprovedCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                    </span>
                                        <br />
                                        <br />
                                        Approved </a></li>
                                    <li class="bg_ss col-md-1 hoverable"><a href="DashBoard.aspx?vwType=RequestForModification"><i class="fa fa-reply"></i><span class="label label-danger">
                                        <asp:Label ID="lblRequestForModificationCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                    </span>
                                        <br />

                                        Request  Modificatin</a></li>
                                    <li class="bg_ls  col-md-1 hoverable"><a href="DashBoard.aspx?vwType=RequestForCompletion"><i class="fa fa-recycle"></i><span class="label label-success">
                                        <asp:Label ID="lblRequestForCompletionCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                    </span>
                                        <br />
                                        Request Completion</a> </li>
                                    <li class="bg_lv  col-md-1 hoverable"><a href="DashBoard.aspx?vwType=Completed"><i class="fa fa-chain"></i><span class="label label-primary">
                                        <asp:Label ID="lblCompletedProjectsCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                    </span>
                                        <br />

                                        <br />
                                        Completed</a> </li>
                                    <li class="bg_lr col-md-1 hoverable"><a href="DashBoard.aspx?vwType=Rejected"><i class="fa fa-eraser"></i><span class="label label-danger">
                                        <asp:Label ID="lblRejectedProjectCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                    </span>
                                        <br />
                                        <br />
                                        Rejected
                                                <br />
                                    </a></li>
                                    <!-- added draft project count -->
                                     <li class="bg_lv  col-md-1 hoverable"><a href="DashBoard.aspx?vwType=Draft"><i class="fa fa-chain"></i><span class="label label-primary">
                                        <asp:Label ID="lbldraftedProjectCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                    </span>
                                        <br />

                                        <br />
                                        Draft</a> </li>
                                </ul>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading hoverable">
                                        <div class="row">
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlAllProjectAcademicYear" OnSelectedIndexChanged="ddlAllProjectAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlAllRecordCount" OnSelectedIndexChanged="ddlAllRecordCount_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="[Record]" Value="[Record]"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                    <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                                    <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                                    <asp:ListItem Text="25000" Value="25000"></asp:ListItem>
                                                    <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-info">
                                                        <span class="input-group-text">Search</span>
                                                    </div>
                                                    <asp:TextBox ID="txtStudentSearch" onkeyup="SearchStudentDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-primary">
                                                        <span class="input-group-text">Search on</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDashboardSearchOn" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SR.LEAD_Id" Text="LEAD Id"></asp:ListItem>
                                                        <asp:ListItem Value="SR.StudentName" Text="Student Name"></asp:ListItem>
                                                        <asp:ListItem Value="SR.MobileNo" Text="Mobile No"></asp:ListItem>
                                                        <asp:ListItem Value="SR.Mailid" Text="Mailid"></asp:ListItem>
                                                        <asp:ListItem Value="PD.Title" Text="Project Title"></asp:ListItem>
                                                        <asp:ListItem Value="CLG.College_Name" Text="College Name"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="btnDashboardSearch" OnClick="btnDashboardSearch_Click" CssClass="btn btn-info" runat="server"><span class="fa fa-search"></span> </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12" style="width: 100%; height: 550px; overflow: auto" id="SearchList">
                                                <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">

                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rptAllProjects" OnItemCommand="rptAllProjects_ItemCommand" OnItemDataBound="rptAllProjects_ItemDataBound" runat="server">
                                                            <HeaderTemplate>
                                                                <div class="table-responsive text-small">
                                                                    <table class="table table-striped table-bordered table-hover b-t text-small ">
                                                                        <thead class="hoverable">
                                                                            <tr>
                                                                                <th>Slno
                                                                                </th>
                                                                                <th style="display: none; text-align: center;">PDId
                                                                                </th>
                                                                                <th style="display: none">
                                                                                    <strong><b>Proposed_Date</b><strong>
                                                                                </th>
                                                                                <th style="text-align: center">
                                                                                    <strong><b>LEAD ID</b><strong>
                                                                                </th>
                                                                                <th><strong><b>Student Name</b><strong>
                                                                                </th>
                                                                                <th><strong><b>Title</b><strong>
                                                                                </th>
                                                                                <th style="display: none">Institution
                                                                                </th>
                                                                                <th style="display: none">Location
                                                                                </th>
                                                                                <th class="hidden-xs">Mobile No
                                                                                </th>
                                                                                <th><strong><b>Req_Amt</b><strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>San_Amt</b><strong>
                                                                                </th>
                                                                                <th><strong><b>Disperse</b><strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>Bal</b><strong>
                                                                                </th>
                                                                                   <th style="text-align: center"><strong><b>Status</b><strong>
                                                                                </th>
                                                                                  <th><strong><b>SEM</b><strong>
                                                                                </th>
                                                                             
                                                                                <th style="text-align: center"><strong><b><span class="fa fa-rupee"></span></b><strong></th>
                                                                            </tr>
                                                                        </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tbody>
                                                                    <tr class="z-depth-2">
                                                                        <td>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                                        </td>
                                                                        <%--  <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>--%>
                                                                        <td style="min-width: 40px; display: none">
                                                                            <asp:Label ID="lblProposedDate" Font-Size="Small" runat="server" Text='<%# Eval("ProposedDate") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px; text-align: center;">
                                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;">
                                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <asp:Label ID="lblTitle" Font-Size="Small" runat="server" Text='<%# Eval("title") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; display: none;">
                                                                            <asp:Label ID="lblCollegeName" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center; display: none">
                                                                            <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("Taluk_Name") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center;" class="hidden-xs">
                                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                                        </td>
                                                                        <td style="min-width: 30px;">
                                                                            <asp:Label ID="lblRequestedAmount" Font-Size="Small" runat="server" Text='<%# Eval("Amount") %>' />
                                                                        </td>
                                                                        <td style="min-width: 30px;" class="hidden-xs">
                                                                            <asp:Label ID="lblSacntionAmount" Font-Size="Small" runat="server" Text='<%# Eval("SanctionAmount") %>' />
                                                                        </td>
                                                                        <td style="min-width: 30px;">
                                                                            <asp:Label ID="lblDisperseAmt" Font-Size="Small" runat="server" />
                                                                        </td>
                                                                        <td style="min-width: 30px;" class="hidden-xs">
                                                                            <asp:Label ID="lblBalance" Font-Size="Small" runat="server" />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center; font-size: xx-small;">
                                                                            <asp:Label ID="lblProjectStatus" Font-Size="Small" runat="server" Text='<%# Eval("ProjectStatus") %>' />
                                                                        </td>
                                                                            <td style="min-width: 50px; text-align: center; font-size: xx-small;">
                                                                            <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />
                                                                        </td>
                                                                        <td style="min-width: 10px; text-align: center;">
                                                                            <asp:LinkButton ID="btnPayee" Font-Size="Small" CommandArgument='<%# Eval("PDId")+"_"+Eval("ProjectStatus")+"_"+("Payee") %>' runat="server"></asp:LinkButton>
                                                                        </td>

                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwProposedProjects" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading hoverable">
                                        <div class="row">
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlProposedAcademicYear" OnSelectedIndexChanged="ddlProposedAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlProposedRecordCount" OnSelectedIndexChanged="ddlProposedRecordCount_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="[Record]" Value="[Record]"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                    <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                                    <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                                    <asp:ListItem Text="25000" Value="25000"></asp:ListItem>
                                                    <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-primary">
                                                        <span class="input-group-text">Search</span>
                                                    </div>
                                                    <asp:TextBox ID="txtProposedSearch" onkeyup="ProposedSearchDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-info">
                                                        <span class="input-group-text">Search on</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddlProposedSearchOn" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SR.LEAD_Id" Text="LEAD Id"></asp:ListItem>
                                                        <asp:ListItem Value="SR.StudentName" Text="Student Name"></asp:ListItem>
                                                        <asp:ListItem Value="SR.MobileNo" Text="Mobile No"></asp:ListItem>
                                                        <asp:ListItem Value="SR.Mailid" Text="Mailid"></asp:ListItem>
                                                        <asp:ListItem Value="PD.Title" Text="Project Title"></asp:ListItem>
                                                        <asp:ListItem Value="CLG.College_Name" Text="College Name"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="btnSearchProposed" OnClick="btnSearchProposed_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12" style="width: 100%; max-height: 650px; overflow: auto" id="ProposedList">

                                                <asp:UpdatePanel ID="UpdatePanel11" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">

                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rptProposedProjectsList" runat="server">
                                                            <HeaderTemplate>
                                                                <div class="table-responsive">


                                                                    <table class="table table-striped table-bordered table-hover b-t text-small">
                                                                        <thead>
                                                                            <tr style="background-color: #28b779; color: #fff">
                                                                                <th>Slno</th>
                                                                                <th style="display: none">PDId
                                                                                </th>

                                                                                <%--  <td ><b><strong> Pic</strong></b>
                                                                                    </td>--%>

                                                                                <th style="text-align: center;" class="hidden-xs"><strong><b>Proposed_Date</b><strong>
                                                                                </th>
                                                                                <th style="text-align: center;"><strong><b>LEAD ID</b><strong>
                                                                                </th>
                                                                                <th><strong><b>Student_Name</b><strong>
                                                                                </th>
                                                                                <th class="hidden-xs">Mobile No
                                                                                </th>
                                                                                <th><strong><b>Title</b><strong>
                                                                                </th>

                                                                                <th class="hidden-xs"><strong><b>Req_Amt</b><strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>Institution</b><strong> 
                                                                                </th>
                                                                                <th style="display: none">Location
                                                                                </th>

                                                                                <th class="hidden"><strong><b>Manager_Comment</b><strong>
                                                                                </th>
                                                                                <th><strong><b>SEM</b><strong>
                                                                                </th> 
                                                                                <th class="text-center"><strong><b>Status</b><strong>
                                                                                </th>

                                                                            </tr>
                                                                        </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <tbody>
                                                                    <tr class="z-depth-2">
                                                                        <%--   <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                                        </td>
                                                                        <td style="min-width: 30px;" class="hidden-xs">
                                                                            <asp:Label ID="lblProposedDate" Font-Size="Small" runat="server" Text='<%# Eval("ProposedDate") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px;">
                                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;">
                                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center;" class="hidden-xs">
                                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <asp:Label ID="lblTitle" Font-Size="Small" runat="server" Text='<%# Eval("title") %>' />
                                                                        </td>

                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblRequestedAmount" Font-Size="Small" runat="server" Text='<%# Eval("Amount") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblCollegeName" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center; display: none">
                                                                            <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("Taluk_Name") %>' />
                                                                        </td>

                                                                        <td style="min-width: 50px;" class="hidden">
                                                                            <asp:Label ID="lblManagerComments" Font-Size="Small" runat="server" Text='<%# Eval("ManagerComments") %>' />
                                                                        </td>
                                                                  <td style="min-width: 50px; text-align: center; font-size: xx-small;">                                     <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />                                                </td>
                                                                        <td style="min-width: 50px; text-align: center; white-space: nowrap;">
                                                                            <asp:LinkButton ID="btnProposedProject" Font-Size="Small" OnCommand="btnProposedProject_Command"
                                                                                CommandArgument='<%# Eval("PDId")+"_"+Eval("Lead_Id") %>'
                                                                                CssClass="btn btn-info btn-circle btn-small" runat="server"><span class="fa fa-pencil-square-o "></span></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnChat" CssClass="btn btn-circle btn-small btn-flickr" OnCommand="btnChat_Click"
                                                                                CommandArgument='<%# Eval("PDId")+"^"+Eval("title")+"^"+("Proposed") %>' runat="server">
                                                                <span class="fa fa-comment "></span></asp:LinkButton>

                                                                        </td>

                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                                 </div>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

<!-- added vwdraftedprojects -->
           <asp:View ID="vwdraftedProjects" runat="server">
                       <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-body">
                                        <div class="row hoverable">
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddldraftedAcademicYear" OnSelectedIndexChanged="ddldraftAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddldraftedRecordCount" OnSelectedIndexChanged="ddldraftRecordCount_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="[Record]" Value="[Record]"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                    <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                                    <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                                    <asp:ListItem Text="25000" Value="25000"></asp:ListItem>
                                                    <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-info">
                                                        <span class="input-group-text">Search</span>
                                                    </div>
                                                    <asp:TextBox ID="TextBox1" onkeyup="SearchStudentDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-primary">
                                                        <span class="input-group-text">Search on</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddldraftedsearchon" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SR.LEAD_Id" Text="LEAD Id"></asp:ListItem>
                                                        <asp:ListItem Value="SR.StudentName" Text="Student Name"></asp:ListItem>
                                                        <asp:ListItem Value="SR.MobileNo" Text="Mobile No"></asp:ListItem>
                                                        <asp:ListItem Value="SR.Mailid" Text="Mailid"></asp:ListItem>
                                                        <asp:ListItem Value="PD.Title" Text="Project Title"></asp:ListItem>
                                                        <asp:ListItem Value="CLG.College_Name" Text="College Name"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="LinkButton1" OnClick="btnDashboardSearch_Click" CssClass="btn btn-info" runat="server"><span class="fa fa-search"></span> </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12" style="width: 100%; height: 650px; overflow: auto" id="SearchListdraft">
                                                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">

                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rptdraftedProject" OnItemCommand="rptAllProjects_ItemCommand" OnItemDataBound="rptAllProjects_ItemDataBound" runat="server">
                                                            <HeaderTemplate>
                                                                <div class="table-responsive text-small">
                                                                    <table class="table table-striped table-bordered table-hover b-t text-small ">
                                                                        <thead class="hoverable">
                                                                            <tr>
                                                                                <th>Slno
                                                                                </th>
                                                                                <th style="display: none; text-align: center;">PDId
                                                                                </th>
                                                                                <th style="display: none">
                                                                                    <strong><b>Proposed_Date</b><strong>
                                                                                </th>
                                                                                <th style="text-align: center">
                                                                                    <strong><b>LEAD ID</b><strong>
                                                                                </th>
                                                                                <th><strong><b>Student Name</b><strong>
                                                                                </th>
                                                                                <th><strong><b>Title</b><strong>
                                                                                </th>
                                                                                <th style="text-align: center">Institution
                                                                                </th>
                                                                                <th style="text-align: center">Location
                                                                                </th>
                                                                                <th class="hidden-xs">Mobile No
                                                                                </th>
                                                                                 <th style="text-align: center"> currentsituation
                                                                                </th>
                                                                               
                                                                                <th><strong><b>Req_Amt</b><strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>San_Amt</b><strong>
                                                                                </th>
                                                                                <th><strong><b>Disperse</b><strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>Bal</b><strong>
                                                                                </th>
                                                                                   <th style="text-align: center"><strong><b>Status</b><strong>
                                                                                </th>
                                                                                  <th><strong><b>SEM</b><strong>
                                                                                </th>
                                                                               </th>
                                                                                 <th class="text-center"><strong><b>Status</b></strong>
                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tbody>
                                                                    <tr class="z-depth-2">
                                                                        <td>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                                        </td>
                                                                        <%--  <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>--%>
                                                                        <td style="min-width: 40px; display: none">
                                                                            <asp:Label ID="lblProposedDate" Font-Size="Small" runat="server" Text='<%# Eval("ProposedDate") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px; text-align: center;">
                                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;">
                                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <asp:Label ID="lblTitle" Font-Size="Small" runat="server" Text='<%# Eval("title") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;">
                                                                            <asp:Label ID="lblCollegeName" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center;">
                                                                            <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("Taluk_Name") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center;" class="hidden-xs">
                                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                                        </td>
                                                                         <td style="min-width: 50px; text-align: center;">
                                                                            <asp:Label ID="lblcurrentsituation" Font-Size="Small" runat="server" Text='<%# Eval(" CurrentSituation") %>' />
                                                                        </td>
                                                                       

                                                                        <td style="min-width: 30px;">
                                                                            <asp:Label ID="lblRequestedAmount" Font-Size="Small" runat="server" Text='<%# Eval("Amount") %>' />
                                                                        </td>
                                                                        <td style="min-width: 30px;" class="hidden-xs">
                                                                            <asp:Label ID="lblSacntionAmount" Font-Size="Small" runat="server" Text='<%# Eval("SanctionAmount") %>' />
                                                                        </td>
                                                                        <td style="min-width: 30px;">
                                                                            <asp:Label ID="lblDisperseAmt" Font-Size="Small" runat="server" />
                                                                        </td>
                                                                        <td style="min-width: 30px;" class="hidden-xs">
                                                                            <asp:Label ID="lblBalance" Font-Size="Small" runat="server" />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center; font-size: xx-small;">
                                                                            <asp:Label ID="lblProjectStatus" Font-Size="Small" runat="server" Text='<%# Eval("ProjectStatus") %>' />
                                                                        </td>
                                                                            <td style="min-width: 50px; text-align: center; font-size: xx-small;">
                                                                            <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />
                                                                        </td>
                                                                       
                                                                        <td style="min-width: 50px; white-space: nowrap;">
                                                                            <asp:LinkButton ID="btndraftedProjectStatus" CssClass="btn btn-circle btn-info btn-small" Font-Size="Small" OnCommand="btndraftedProjectStatus" CommandArgument='<%# Eval("PDId")+"_"+Eval("Lead_Id") %>' runat="server">
                                                                                <span class="fa fa-pencil-square-o"></span>
                                                                            </asp:LinkButton>

                                                                            <asp:LinkButton ID="btnChat" CssClass="btn btn-circle btn-small btn-flickr" OnCommand="btnChat_Click"
                                                                                CommandArgument='<%# Eval("PDId")+"^"+Eval("title")+"^"+("Draft") %>' runat="server">
                                                                <span class="fa fa-comment "></span></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>


                    <asp:View ID="vwApprovedProjects" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-body">
                                        <div class="row hoverable">
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlApprovedAcademicYear" OnSelectedIndexChanged="ddlApprovedAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlApprovedRecordCount" OnSelectedIndexChanged="ddlApprovedRecordCount_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="[Record]" Value="[Record]"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                    <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                                    <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                                    <asp:ListItem Text="25000" Value="25000"></asp:ListItem>
                                                    <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-primary">
                                                        <span class="input-group-text">Search</span>
                                                    </div>
                                                    <asp:TextBox ID="txtApprovedSearch" onkeyup="ApprovedSearchDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-info">
                                                        <span class="input-group-text">Search on</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddlApprovedSearchOn" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SR.LEAD_Id" Text="LEAD Id"></asp:ListItem>
                                                        <asp:ListItem Value="SR.StudentName" Text="Student Name"></asp:ListItem>
                                                        <asp:ListItem Value="SR.MobileNo" Text="Mobile No"></asp:ListItem>
                                                        <asp:ListItem Value="SR.Mailid" Text="Mailid"></asp:ListItem>
                                                        <asp:ListItem Value="PD.Title" Text="Project Title"></asp:ListItem>
                                                        <asp:ListItem Value="CLG.College_Name" Text="College Name"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="btnSearchApproved" OnClick="btnSearchApproved_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="max-height: 650px; overflow: auto" id="ApprovedList">
                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">

                                    <ContentTemplate>
                                        <asp:Repeater ID="rptApprovedProjects" runat="server">
                                            <HeaderTemplate>
                                                <div class="table-responsive">
                                                    <table class="table table-striped table-bordered table-hover b-t text-small">
                                                        <thead>
                                                            <tr style="background-color: #da542e; color: #fff">
                                                                <th>Slno
                                                                </th>
                                                                <th style="display: none">PDId
                                                                </th>
                                                                <%-- <td style="text-align:center"><strong><b>Pic</b></strong> 
                                                                                    </td>--%>
                                                                <th style="display: none"><strong><b>Prop_Date</b></strong>
                                                                </th>
                                                                <th class="hidden-xs"><strong><b>Approved_Date</b></strong> </th>
                                                                <th style="text-align: center"><strong><b>LEAD ID</b></strong> </th>
                                                                <th><strong><b>Student_Name</b></strong>
                                                                </th>
                                                                <th class="hidden-xs"><strong><b>Mobile No</b></strong>
                                                                </th>
                                                                <th><strong><b>Title</b></strong>
                                                                </th>
                                                                <th class="hidden-xs"><strong><b>Institute</b></strong>
                                                                </th>
                                                                <th style="display: none">Location
                                                                </th>
                                                                <th class="hidden-xs"><strong><b>Manager Comment</b></strong>
                                                                </th>
                                                              
                                                                <th class="hidden-xs"><strong><b>Req</b></strong>
                                                                </th>
                                                                <th class="hidden-xs"><strong><b>Sanction</b></strong></th>
                                                                  <th><strong><b>SEM</b><strong></th>  
                                                                <th class="text-center"><strong><b>Status</b></strong>
                                                                </th>


                                                            </tr>
                                                        </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr class="z-depth-2">
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                        </td>
                                                        <%-- <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>--%>

                                                        <td style="display: none">
                                                            <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                        </td>
                                                        <td style="min-width: 40px; display: none;">
                                                            <asp:Label ID="lblProposedDate" Font-Size="Small" runat="server" Text='<%# Eval("ProposedDate") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;" class="hidden-xs">
                                                            <asp:Label ID="lblApprovedDate" Font-Size="Small" runat="server" Text='<%# Eval("ApprovedDate") %>' />
                                                        </td>
                                                        <td style="min-width: 40px; text-align: center;">
                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                        </td>
                                                        <td style="min-width: 50px; text-align: center;" class="hidden-xs">
                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                        </td>
                                                        <td style="width: 15%;">
                                                            <asp:Label ID="lblTitle" Font-Size="Small" runat="server" Text='<%# Eval("title") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                            <asp:Label ID="lblCollegeName" Font-Size="Smaller" runat="server" Text='<%# Eval("College_Name") %>' />
                                                        </td>
                                                        <td style="min-width: 50px; text-align: center; display: none">
                                                            <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("Taluk_Name") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                            <asp:Label ID="lblManagerComments" Font-Size="Small" runat="server" Text='<%# Eval("ManagerComments") %>' />
                                                        </td>
                                                     
                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                            <asp:Label ID="lblRequestedAmount" Font-Size="Small" runat="server" Text='<%# Eval("Amount") %>' />
                                                        </td>

                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                            <asp:Label ID="lblSanctionAmount" Font-Size="Small" runat="server" Text='<%# Eval("SanctionAmount") %>' />
                                                        </td>
                                                           <td style="min-width: 50px; text-align: center; font-size: xx-small;">                                     <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />                                                </td>
                                                        <td style="min-width: 50px; white-space: nowrap;">

                                                            <asp:LinkButton ID="btnApproveProject" OnCommand="btnApproveProject_Command" Font-Size="Small" CssClass="btn btn-info btn-circle btn-small" runat="server"
                                                                CommandArgument='<%# Eval("PDId")+"_"+Eval("Lead_Id") %>'>
                                                                <span class="fa fa-pencil-square-o"></span>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="btnChat" CssClass="btn btn-circle btn-small btn-flickr" OnCommand="btnChat_Click"
                                                                CommandArgument='<%# Eval("PDId")+"^"+Eval("title")+"^"+("Approved") %>' runat="server">
                                                                <span class="fa fa-comment "></span></asp:LinkButton>
                                                            <tr>
                                                            </tr>
                                                        </td>

                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                                </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </asp:View>
                    <asp:View ID="vwRequestForModification" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading hoverable">
                                        <div class="row">
                                            <div class="col-md-1">

                                                <asp:DropDownList ID="ddlRequestForModificationAcademicYear" OnSelectedIndexChanged="ddlRequestForModificationAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlRMRcordCount" OnSelectedIndexChanged="ddlRMRcordCount_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="[Record]" Value="[Record]"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                    <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                                    <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                                    <asp:ListItem Text="25000" Value="25000"></asp:ListItem>
                                                    <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-danger">
                                                        <span class="input-group-text">Search</span>
                                                    </div>
                                                    <asp:TextBox ID="txtRequestForModificationSearch" onkeyup="RequestForModificationSearchDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-info">
                                                        <span class="input-group-text">Search on</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRequestForModificationSearchOn" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SR.LEAD_Id" Text="LEAD Id"></asp:ListItem>
                                                        <asp:ListItem Value="SR.StudentName" Text="Student Name"></asp:ListItem>
                                                        <asp:ListItem Value="SR.MobileNo" Text="Mobile No"></asp:ListItem>
                                                        <asp:ListItem Value="SR.Mailid" Text="Mailid"></asp:ListItem>
                                                        <asp:ListItem Value="PD.Title" Text="Project Title"></asp:ListItem>
                                                        <asp:ListItem Value="CLG.College_Name" Text="College Name"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="btnSearchRequestForModification" OnClick="btnSearchRequestForModification_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">

                                        <div class="row">
                                            <div class="col-md-12" style="width: 100%; height: 600px; overflow: auto" id="RequestForModificationList">
                                                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">

                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rptRequestForModifiation" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-striped table-bordered table-hover b-t text-small">
                                                                    <thead>
                                                                        <tr style="background-color: #5191d1; color: #fff">
                                                                            <th>Slno
                                                                            </th>
                                                                            <th style="display: none">PDId
                                                                            </th>
                                                                            <%-- <td style="text-align:center"><strong><b>Pic</b></strong> 
                                                                                    </td>--%>
                                                                            <th style="display: none;"><strong><b>Prop Date</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Requested_Date </b></strong></th>
                                                                            <th><strong><b>LEAD ID</b></strong></th>
                                                                            <th><strong><b>Student_Name</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs">Mobile No
                                                                            </th>
                                                                            <th><strong><b>Title</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Institution</b></strong>
                                                                            </th>
                                                                            <th style="display: none">Location
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Manager Comment</b></strong>
                                                                            </th>
                                                                          
                                                                            <th style="display: none"><strong><b>Req_Amt</b></strong>
                                                                            </th>
                                                                            <th style="display: none"><strong><b>San_Amt</b></strong>
                                                                            </th>
                                                                              <th><strong><b>SEM</b><strong></th> 
                                                                            <th style="text-align: center"><strong><b>Status</b></strong>
                                                                            </th>

                                                                        </tr>
                                                                    </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tbody class="z-depth-2">
                                                                    <tr>

                                                                        <td>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                        </td>
                                                                        <%-- <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>--%>
                                                                        <td style="display: none">
                                                                            <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px; display: none;">
                                                                            <asp:Label ID="lblProposedDate" Font-Size="Small" runat="server" Text='<%# Eval("ProposedDate") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px;" class="hidden-xs">
                                                                            <asp:Label ID="lblRequestformodification" Font-Size="Small" runat="server" Text='<%# Eval("RequestForModificationDate") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px; text-align: center;">
                                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;">
                                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center;" class="hidden-xs">
                                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <asp:Label ID="lblTitle" Font-Size="Small" runat="server" Text='<%# Eval("title") %>' />
                                                                        </td>


                                                                        <td style="min-width: 50px" class="hidden-xs">
                                                                            <asp:Label ID="lblCollegeName" Font-Size="Smaller" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center; display: none">
                                                                            <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("Taluk_Name") %>' />
                                                                        </td>

                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblManagerComments" Font-Size="Small" runat="server" Text='<%# Eval("ManagerComments") %>' />

                                                                        </td>
                                                                 
                                                                        <td style="min-width: 50px; display: none;">
                                                                            <asp:Label ID="lblRequestedAmount" Font-Size="Small" runat="server" Text='<%# Eval("Amount") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; display: none;">
                                                                            <asp:Label ID="lblSanctionAmount" Font-Size="Small" runat="server" Text='<%# Eval("SanctionAmount") %>' />
                                                                        </td>
                                                                               <td style="min-width: 50px; text-align: center; font-size: xx-small;">                                     <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />                                                </td>
                                                                        <td style="min-width: 50px; white-space: nowrap;">
                                                                            <asp:LinkButton ID="btnRequestForModification" OnCommand="btnRequestForModification_Command"
                                                                                CommandArgument='<%# Eval("PDId")+"_"+Eval("Lead_Id") %>'
                                                                                Font-Size="Small" CssClass="btn btn-circle btn-info btn-small" runat="server">
                                                                                <span class="fa fa-pencil-square-o"></span>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="btnChat" CssClass="btn btn-circle btn-small btn-flickr" OnCommand="btnChat_Click"
                                                                                CommandArgument='<%# Eval("PDId")+"^"+Eval("title")+"^"+("RequestForModification") %>' runat="server">
                                                                <span class="fa fa-comment "></span></asp:LinkButton>

                                                                        </td>

                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>


                    </asp:View>
                    <asp:View ID="vwRequestForCompletion" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading hoverable">
                                        <div class="row">
                                            <div class="col-md-1 ">

                                                <asp:DropDownList ID="ddlRequestForCompletionSearchAcademicYear" OnSelectedIndexChanged="ddlRequestForCompletionSearchAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlRCRecordCount" OnSelectedIndexChanged="ddlRCRecordCount_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="[Record]" Value="[Record]"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                    <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                                    <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                                    <asp:ListItem Text="25000" Value="25000"></asp:ListItem>
                                                    <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-warning">
                                                        <span class="input-group-text">Search</span>
                                                    </div>
                                                    <asp:TextBox ID="txtRequestForCompletionSearch" onkeyup="RequestForCompletionSearchDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-info">
                                                        <span class="input-group-text">Search on</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRequestForCompletionSearchOn" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SR.LEAD_Id" Text="LEAD Id"></asp:ListItem>
                                                        <asp:ListItem Value="SR.StudentName" Text="Student Name"></asp:ListItem>
                                                        <asp:ListItem Value="SR.MobileNo" Text="Mobile No"></asp:ListItem>
                                                        <asp:ListItem Value="SR.Mailid" Text="Mailid"></asp:ListItem>
                                                        <asp:ListItem Value="PD.Title" Text="Project Title"></asp:ListItem>
                                                        <asp:ListItem Value="CLG.College_Name" Text="College Name"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="btnSearchRequestForCompletion" OnClick="btnSearchRequestForCompletion_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">

                                        <div class="row">
                                            <div class="col-md-12" style="width: 100%; height: 650px; overflow: auto" id="RequestForCompletionList">
                                                <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">

                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rptRequestForCompletion" OnItemDataBound="rptRequestForCompletion_ItemDataBound" runat="server">
                                                            <HeaderTemplate>
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped table-bordered table-hover b-t text-small">
                                                                        <thead>
                                                                            <tr style="background-color: #13c4a5; color: #fff">
                                                                                <th>Slno
                                                                                </th>

                                                                                <th style="display: none">PDId
                                                                                </th>
                                                                                <%-- <td style="text-align:center"><strong><b>Pic</b></strong> 
                                                                                    </td>--%>
                                                                                <th style="display: none;"><strong><b>Prop_Date</b></strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>Request_Date</b></strong></th>
                                                                                <th>
                                                                                    <strong><b>LEAD ID</b></strong></th>
                                                                                <th><strong><b>Student_Name</b></strong>
                                                                                </th>
                                                                                <th class="hidden-xs">Mobile_No
                                                                                </th>
                                                                                <th><strong><b>Title</b></strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>Institution</b></strong>
                                                                                </th>
                                                                                <th style="display: none">Location
                                                                                </th>

                                                                                <th class="hidden"><strong><b>Comment</b></strong>
                                                                                </th>
                                                                            
                                                                                <th class="hidden-xs"><strong><b>Req_Amt</b></strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>Sanc_Amt</b></strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>Disp_Amt</b></strong>
                                                                                </th>
                                                                                <th class="hidden-xs"><strong><b>Bal_Amt</b></strong>
                                                                                </th>
                                                                                    <th><strong><b>SEM</b><strong></th>  
                                                                                <th style="text-align: center"><strong><b>Status</b></strong>
                                                                                </th>

                                                                            </tr>
                                                                        </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tbody>
                                                                    <tr class="z-depth-2">
                                                                        <td>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                                        </td>
                                                                        <td style="width: 40px; display: none;">
                                                                            <asp:Label ID="lblProposedDate" Font-Size="Small" runat="server" Text='<%# Eval("ProposedDate") %>' />
                                                                        </td>
                                                                        <td style="width: 40px;" class="hidden-xs">
                                                                            <asp:Label ID="lblRequestedForCompletion" Font-Size="Small" runat="server" Text='<%# Eval("RequestForCompletionDate") %>' />
                                                                        </td>
                                                                        <td style="width: 40px;">
                                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                        </td>
                                                                        <td style="width: 50px;">
                                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                        </td>
                                                                        <td style="width: 50px; text-align: center;" class="hidden-xs">
                                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <asp:Label ID="lblTitle" Font-Size="Small" runat="server" Text='<%# Eval("title") %>' />
                                                                        </td>
                                                                        <td style="width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblCollegeName" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                        </td>
                                                                        <td style="width: 50px; text-align: center; display: none">
                                                                            <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("Taluk_Name") %>' />
                                                                        </td>

                                                                        <td style="width: 50px;" class="hidden">
                                                                            <asp:Label ID="lblManagerComments" Font-Size="Small" runat="server" Text='<%# Eval("ManagerComments") %>' />
                                                                        </td>
                                                                  

                                                                        <td style="width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblRequestedAmount" Font-Size="Small" runat="server" Text='<%# Eval("Amount") %>' />
                                                                        </td>
                                                                        <td style="width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblsanctionAmount" Font-Size="Small" runat="server" Text='<%# Eval("SanctionAmount") %>' />
                                                                        </td>
                                                                        <td style="width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblDispAmount" Font-Size="Small" runat="server" />
                                                                        </td>
                                                                        <td style="width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblBalAmount" Font-Size="Small" runat="server" />
                                                                        </td>
                                                                              <td style="min-width: 50px; text-align: center; font-size: xx-small;">                                     <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />                                                </td>
                                                                        <td style="font-size: smaller; white-space: nowrap;">
                                                                            <asp:LinkButton ID="lblProjectStatus" OnCommand="lblProjectStatus_Command"
                                                                                CommandArgument='<%# Eval("PDId")+"_"+Eval("Lead_Id") %>' CssClass="btn btn-dropbox btn-circle btn-small"
                                                                                Font-Size="Small" runat="server">
                                                                                <span class="fa fa-pencil-square-o"></span>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="btnChat" CssClass="btn btn-circle btn-small btn-flickr" OnCommand="btnChat_Click"
                                                                                CommandArgument='<%# Eval("PDId")+"^"+Eval("title")+"^"+("RequestForCompletion") %>' runat="server">
                                                                <span class="fa fa-comment "></span></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </asp:View>
                    <asp:View ID="vwCompletedProjects" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading hoverable">
                                        <div class="row">
                                            <div class="col-md-1">

                                                <asp:DropDownList ID="ddlCompletionAcademicYear" OnSelectedIndexChanged="ddlCompletionAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlCompletedRecordCount" OnSelectedIndexChanged="ddlCompletedRecordCount_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="[Record]" Value="[Record]"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                    <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                                    <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                                    <asp:ListItem Text="25000" Value="25000"></asp:ListItem>
                                                    <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-primary">
                                                        <span class="input-group-text">Search</span>
                                                    </div>
                                                    <asp:TextBox ID="txtCompletionSearch" onkeyup="CompletionSearchDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-info">
                                                        <span class="input-group-text">Search on</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCompletionSearchOn" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SR.LEAD_Id" Text="LEAD Id"></asp:ListItem>
                                                        <asp:ListItem Value="SR.StudentName" Text="Student Name"></asp:ListItem>
                                                        <asp:ListItem Value="SR.MobileNo" Text="Mobile No"></asp:ListItem>
                                                        <asp:ListItem Value="SR.Mailid" Text="Mailid"></asp:ListItem>
                                                        <asp:ListItem Value="PD.Title" Text="Project Title"></asp:ListItem>
                                                        <asp:ListItem Value="CLG.College_Name" Text="College Name"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="btnSearchCompletion" OnClick="btnSearchCompletion_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">

                                        <div class="row">
                                            <div class="col-md-12" style="width: 100%; height: 650px; overflow: auto" id="CompletionList">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="Server">

                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rptCompletedProject" OnItemDataBound="rptCompletedProject_ItemDataBound" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-striped table-bordered table-hover b-t text-small">
                                                                    <thead>
                                                                        <tr style="background-color: #13c4a5; color: #fff">
                                                                            <th>Slno
                                                                            </th>
                                                                            <th style="display: none">PDId
                                                                            </th>
                                                                            <th style="display: none;"><strong><b>Prop_Date </b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Complete_Date</b></strong> </th>
                                                                            <th><strong><b>LEAD ID</b></strong> </th>
                                                                            <th><strong><b>Student_Name</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs">Mobile No
                                                                            </th>
                                                                            <th><strong><b>Title</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Institution</b></strong>
                                                                            </th>
                                                                            <th style="display: none">Location
                                                                            </th>

                                                                            <th class="hidden-xs"><strong><b>Comments</b></strong>
                                                                            </th>
                                                                            
                                                                            <th class="hidden-xs"><strong><b>Req_Amt</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>San_Amt</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Disp_Amt</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Bal_Amt</b></strong>
                                                                            </th>
                                                                            <th><strong><b>SEM</b><strong></th>  
                                                                            <th style="text-align: center"><strong><b>Status</b></strong>
                                                                            </th>

                                                                        </tr>
                                                                    </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tbody>
                                                                    <tr class="z-depth-2">
                                                                        <td>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                        </td>
                                                                        <%--  <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>--%>
                                                                        <td style="display: none">
                                                                            <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                                        </td>
                                                                        <td style="min-width: 15%; display: none;">
                                                                            <asp:Label ID="lblProposedDate" Font-Size="Small" runat="server" Text='<%# Eval("ProposedDate") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px;" class="hidden-xs">
                                                                            <asp:Label ID="lblCompletedDate" Font-Size="Small" runat="server" Text='<%# Eval("CompletedDate") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px; text-align: center;">
                                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;">
                                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center;" class="hidden-xs">
                                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <asp:Label ID="lblTitle" Font-Size="Small" runat="server" Text='<%# Eval("title") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblCollegeName" Font-Size="Smaller" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center; display: none">
                                                                            <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("Taluk_Name") %>' />
                                                                        </td>

                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblManagerComments" Font-Size="Small" runat="server" Text='<%# Eval("ManagerComments") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblRequestedAmount" Font-Size="Small" runat="server" Text='<%# Eval("Amount") %>' />
                                                                        </td>
                                                               
                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblSanctionAmount" Font-Size="Small" runat="server" Text='<%# Eval("SanctionAmount") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblDispAmount" Font-Size="Small" runat="server" />
                                                                        </td>
                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblBalAmount" Font-Size="Small" runat="server" />
                                                                        </td>
                                                                                 <td style="min-width: 50px; text-align: center; font-size: xx-small;">                                     <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />                                                </td>
                                                                        <td style="min-width: 50px; white-space: nowrap;">
                                                                            <asp:LinkButton ID="btnCompletedProjectStatus" CssClass="btn btn-circle btn-info btn-small" Font-Size="Small" OnCommand="btnCompletedProjectStatus_Command" CommandArgument='<%# Eval("PDId")+"_"+Eval("Lead_Id") %>' runat="server">
                                                                                <span class="fa fa-pencil-square-o"></span>
                                                                            </asp:LinkButton>

                                                                            <asp:LinkButton ID="btnChat" CssClass="btn btn-circle btn-small btn-flickr" OnCommand="btnChat_Click"
                                                                                CommandArgument='<%# Eval("PDId")+"^"+Eval("title")+"^"+("Completed") %>' runat="server">
                                                                <span class="fa fa-comment "></span></asp:LinkButton>
                                                                        </td>

                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwStudentDeactivation" runat="server">
                    </asp:View>
                    <asp:View ID="vwRejectedProjects" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading hoverable">
                                        <div class="row">
                                            <div class="col-md-1">

                                                <asp:DropDownList ID="ddlRejectedProjectAcademicYear" OnSelectedIndexChanged="ddlRejectedProjectAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:DropDownList ID="ddlRejectedRecordCount" OnSelectedIndexChanged="ddlRejectedRecordCount_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="[Record]" Value="[Record]"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                    <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                                    <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                                    <asp:ListItem Text="25000" Value="25000"></asp:ListItem>
                                                    <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-danger">
                                                        <span class="input-group-text">Search</span>
                                                    </div>
                                                    <asp:TextBox ID="txtRejectedProjectSearch" onkeyup="RejectedProjectSearchDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-info">
                                                        <span class="input-group-text">Search on</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRejectSearchOn" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SR.LEAD_Id" Text="LEAD Id"></asp:ListItem>
                                                        <asp:ListItem Value="SR.StudentName" Text="Student Name"></asp:ListItem>
                                                        <asp:ListItem Value="SR.MobileNo" Text="Mobile No"></asp:ListItem>
                                                        <asp:ListItem Value="SR.Mailid" Text="Mailid"></asp:ListItem>
                                                        <asp:ListItem Value="PD.Title" Text="Project Title"></asp:ListItem>
                                                        <asp:ListItem Value="CLG.College_Name" Text="College Name"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="btnSearchRejected" OnClick="btnSearchRejected_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">

                                        <div class="row">
                                            <div class="col-md-12" style="width: 100%; height: 650px; overflow: auto" id="RejectProjectList">
                                                <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">
                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rptRejected" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-striped table-bordered table-hover b-t text-small">
                                                                    <thead>
                                                                        <tr style="background-color: #5191d1; color: #fff">
                                                                            <th>Slno
                                                                            </th>
                                                                            <th style="display: none">PDId
                                                                            </th>
                                                                            <th style="display: none;"><strong><b>Prop_Date</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Rejected Date </b></strong></th>
                                                                            <th><strong><b>LEAD ID</b></strong></th>
                                                                            <th><strong><b>Student Name</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs">Mobile No
                                                                            </th>
                                                                            <th><strong><b>Title</b></strong>
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Institution</b></strong>
                                                                            </th>
                                                                            <th style="display: none">Location
                                                                            </th>
                                                                            <th class="hidden-xs"><strong><b>Manager Comment</b></strong>
                                                                            </th>
                                                                        
                                                                            <th class="hidden-xs"><strong><b>Req_Amt</b></strong>
                                                                            </th>
                                                                                <th><strong><b>SEM</b><strong></th> 
                                                                            <th style="text-align: center;">
                                                                                <span class="fa fa-comment fa-2x" style="color: white;"></span>
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tbody>
                                                                    <tr class="z-depth-2">

                                                                        <td>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                        </td>
                                                                        <%--  <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>--%>
                                                                        <td style="display: none">
                                                                            <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px; display: none;">
                                                                            <asp:Label ID="lblProposedDate" Font-Size="Small" runat="server" Text='<%# Eval("ProposedDate") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px;" class="hidden-xs">
                                                                            <asp:Label ID="lblRejectedDate" Font-Size="Small" runat="server" Text='<%# Eval("RejectedDate") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px; text-align: center;">
                                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;">
                                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center;" class="hidden-xs">
                                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <asp:Label ID="lblTitle" Font-Size="Small" runat="server" Text='<%# Eval("title") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblCollegeName" Font-Size="Smaller" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                        </td>
                                                                        <td style="min-width: 50px; text-align: center; display: none">
                                                                            <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("Taluk_Name") %>' />
                                                                        </td>

                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblManagerComments" Font-Size="Small" runat="server" Text='<%# Eval("ManagerComments") %>' />

                                                                        </td>
                                                                   
                                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                                            <asp:Label ID="lblRequestedAmount" Font-Size="Small" runat="server" Text='<%# Eval("Amount") %>' />
                                                                        </td>
                                                                             <td style="min-width: 50px; text-align: center; font-size: xx-small;">                                     <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />                                                </td>
                                                                        <td style="text-align: center;">

                                                                            <asp:LinkButton ID="btnChat" CssClass="btn btn-circle btn-small btn-flickr" OnCommand="btnChat_Click"
                                                                                CommandArgument='<%# Eval("PDId")+"^"+Eval("title")+"^"+("Rejected") %>' runat="server">
                                                                <span class="fa fa-comment "></span></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>




                    <asp:View ID="vwFundAmount" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading hoverable">
                                        <div class="row">
                                            <div class="col-md-1">

                                                <asp:DropDownList ID="ddlFundAmountAcademicYear" OnSelectedIndexChanged="ddlFundAmountAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                            <%--              <div class="col-md-1">
                                                        <asp:DropDownList ID="ddlFundAmountRecordCount"  AutoPostBack="true" CssClass="form-control" runat="server">
                                                             <asp:ListItem Text="[Record]" Value="[Record]"></asp:ListItem>
                                                             <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                             <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                             <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                             <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                             <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                             <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                             <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                             <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                            <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                                             <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                                             <asp:ListItem Text="25000" Value="25000"></asp:ListItem>
                                                             <asp:ListItem Text="50000" Value="50000"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                                            <div class="col-md-9">
                                                <div class="input-group">
                                                    <div class="input-group-addon bg-danger">
                                                        <span class="input-group-text">Search</span>
                                                    </div>
                                                    <asp:TextBox ID="txtFundAmountSearchDetail" onkeyup="FundAmountSearchDetail()" placeholder="Search for LEAD ID / Title / Status / etc." CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>

                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblFundAmountCount" Font-Size="Medium" runat="server" CssClass="label label-default" Text=""></asp:Label>
                                            </div>
                                            <%--                  <div class="col-md-3">
                                                          <div class="input-group">
                                                            <div class="input-group-addon bg-info">
                                                                <span class="input-group-text">Search on</span>
                                                            </div>
                                                           <asp:DropDownList ID="ddlFundAmountSearchOn"  CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="SR.LEAD_Id"  Text="LEAD Id"></asp:ListItem>
                                                             <asp:ListItem Value="SR.StudentName"  Text="Student Name"></asp:ListItem>
                                                             <asp:ListItem Value="SR.MobileNo"  Text="Mobile No"></asp:ListItem>
                                                             <asp:ListItem Value="SR.Mailid"  Text="Mailid"></asp:ListItem>
                                                             <asp:ListItem Value="PD.Title"  Text="Project Title"></asp:ListItem>
                                                             <asp:ListItem Value="CLG.College_Name"  Text="College Name"></asp:ListItem>
                                                        </asp:DropDownList>
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1">
                                                         <asp:LinkButton ID="btnSearchFundAmount" CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                                    </div>--%>
                                        </div>
                                    </div>
                                    <div class="panel-body">

                                        <div class="row">
                                            <div class="col-md-12" style="width: 100%; height: 650px; overflow: auto">
                                                <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="Server">

                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rptFundAmount" OnItemDataBound="rptFundAmount_ItemDataBound" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-striped table-bordered table-hover b-t text-small" id="FundAmountProjectList">
                                                                    <thead>
                                                                        <tr style="background-color: #5191d1; color: #fff">
                                                                            <th>Slno</th>
                                                                            <th style="display: none">PDId
                                                                            </th>
                                                                            <th style="width: 2%;"><strong><b>Lead_Id </b></strong></th>
                                                                            <th style="width: 15%;"><strong><b>Student_Name </b></strong></th>
                                                                            <th style="width: 8%; text-align: center;"><strong><b>Mobile No </b></strong></th>
                                                                            <th style="display: none;"><strong><b>EmailId </b></strong></th>
                                                                            <th style="width: 30%;"><strong><b>Project Title </b></strong></th>
                                                                            <th style="width: 8%;"><strong><b>Req Amt </b></strong></th>
                                                                            <th style="width: 8%; text-align: center;" class="hidden-xs"><strong><b>San Amt </b></strong></th>
                                                                            <th style="width: 8%;"><strong><b>Rel Amt </b></strong></th>
                                                                            <th style="width: 8%;" class="hidden-xs"><strong><b>Bal Amt </b></strong></th>
                                                                            <th style="width: 8%;"><strong><b>Status </b></strong></th>


                                                                        </tr>
                                                                    </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tbody style="font-size: 11px;" onclick="FundingAmount(<%# Eval("PDId") %>,<%# Eval("Balance") %>,'<%# Eval("Lead_Id") %>','<%# Eval("Title") %>')" class="z-depth-2">
                                                                    <tr id="FundingPOP" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                                        </td>
                                                                        <td style="width: 2%;">
                                                                            <asp:Label ID="lblLeadId" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                        </td>
                                                                        <td style="width: 8%; text-align: center;">
                                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                                        </td>
                                                                        <td style="min-width: 40px; display: none;">
                                                                            <asp:Label ID="lblMailId" runat="server" Text='<%# Eval("MailId") %>' />
                                                                        </td>
                                                                        <td style="width: 30%;">
                                                                            <asp:Label ID="lblProjectTitle" runat="server" Text='<%# Eval("Title") %>' />
                                                                        </td>
                                                                        <td style="width: 3%;">
                                                                            <asp:Label ID="lblRequestAmount" runat="server" Text='<%# Eval("RequestedAmount") %>' />
                                                                        </td>
                                                                        <td style="width: 3%; text-align: center;" class="hidden-xs">
                                                                            <asp:Label ID="lblSanctionAmount" runat="server" Text='<%# Eval("SanctionAmount") %>' />
                                                                        </td>
                                                                        <td style="width: 3%;">
                                                                            <asp:Label ID="lblReleaseAmount" runat="server" Text='<%# Eval("ReleaseAmount") %>' />
                                                                        </td>
                                                                        <td style="width: 3%;" class="hidden-xs">
                                                                            <asp:Label ID="lblBalanceAmount" runat="server" Text='<%# Eval("Balance") %>' />
                                                                        </td>
                                                                        <td style="width: 8%;">
                                                                            <asp:Label ID="lblProjectStatus" runat="server" Text='<%# Eval("ProjectStatus") %>' />
                                                                        </td>

                                                                    </tr>
                                                                </tbody>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwRegistration" runat="server">
                        <div class="panel">
                            <div class="panel-heading hoverable">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="input-group">
                                            <div class="input-group-addon bg-primary">
                                                <span class="input-group-text">Select College</span>
                                            </div>
                                            <asp:Panel ID="pnlColleges" runat="server">
                                                <asp:DropDownList ID="ddlRegistrationCollegeSearch" AutoPostBack="true" OnSelectedIndexChanged="ddlRegistrationCollegeSearch_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="btnFeesUnpaidExcelSheet" CssClass="btn btn-success btn-block" OnClick="btnFeesUnpaidExcelSheet_Click" runat="server"><span class="fa fa-file-excel-o"></span> &nbsp;Unpaid  </asp:LinkButton>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="btnFeePaidExcelSheet" CssClass="btn btn-primary btn-block" OnClick="btnFeePaidExcelSheet_Click" runat="server"> <span class="fa fa-file-excel-o"></span> &nbsp; Paid   </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <div class="input-group-addon bg-info">
                                                <span class="input-group-text">Search</span>
                                            </div>
                                            <input type="text" id="txtFeePaidUnPaidDetails" onkeyup="SearchFeePaidUnPaidDetails()" placeholder="Search for Registration Date/ LEAD ID / Student Name / etc." class="form-control" />
                                        </div>
                                        <br />
                                    </div>
                                </div>
                                <div class="row" id="FeesPaidUnPaidList">
                                    <div class="col-md-6" style="height: 650px; overflow: auto">
                                        <div class="row">

                                            <div class="col-md-6">
                                                Un Paid List - &nbsp;
                                                 <asp:Label ID="lblTotalUnPaidStrenth" Font-Size="Larger" Font-Bold="true" runat="server" Text=""></asp:Label>

                                            </div>
                                            <span class="pull-right" style="margin-right: 20px;">
                                                <asp:LinkButton ID="btnupdateToPaid" OnClick="btnupdateToPaid_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-arrow-right"></span> </asp:LinkButton>
                                            </span>
                                        </div>
                                        <asp:Repeater ID="rptisNotPaid" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-striped b-t text-small">
                                                    <thead>
                                                        <tr style="background-color: #28b779; color: #fff">
                                                            <td>Slno</td>
                                                            <td style="display: none;">RegistrationID
                                                            </td>
                                                            <td><strong><b>Registration Date</b><strong>
                                                            </td>
                                                            <td><strong><b>LEAD ID</b><strong>
                                                            </td>
                                                            <td><strong><b>Student Name</b><strong>
                                                            </td>
                                                              <td><strong><b>Semester</b><strong>
                                                            </td>
                                                            <td style="text-align: center"><strong><b>
                                                                <asp:CheckBox ID="ChkUnPaidSelectAll" CssClass="checkbox checkbox-custom checkbox-circle checkbox-primary" Text="All" onclick="checkAll(this);" runat="server" /></b><strong>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr class="z-depth-2">
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblRegistrationId" runat="server" Text='<%# Eval("RegistrationId") %>' />
                                                        </td>
                                                        <%--  <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>    --%>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblRegistrationDate" Font-Size="Small" runat="server" Text='<%# Eval("RegistrationDate") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                        </td>
                                                             <td style="min-width: 10px;">
                                                            <asp:Label ID="Label1" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:CheckBox ID="ChkPaid" CssClass="checkbox checkbox-danger checkbox-circle" Text="Paid" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div class="col-md-6" style="height: 650px; overflow: auto">
                                        <div class="row">
                                            <div class="col-md-6">
                                                Paid List - &nbsp;
                                                                <asp:Label ID="lblTotalPaidStrenth" Font-Size="Larger" Font-Bold="true" runat="server" Text=""></asp:Label>

                                            </div>
                                            <span class="pull-right" style="margin-right: 20px;">
                                                <asp:LinkButton ID="btnUpdateToUnpaid" OnClick="btnUpdateToUnpaid_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-arrow-left"></span> </asp:LinkButton>
                                            </span>
                                        </div>
                                        <asp:Repeater ID="rptisPaid" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-hover">
                                                    <thead>
                                                        <tr style="background-color: #28b779; color: #fff">
                                                            <td>Slno</td>
                                                            <td style="display: none;">RegistrationID
                                                            </td>
                                                            <%-- <td ><b><strong> Pic</strong></b>
                                                                                    </td>  --%>
                                                            <td><strong><b>Registration Date</b><strong>
                                                            </td>
                                                            <td><strong><b>LEAD ID</b><strong>
                                                            </td>
                                                            <td><strong><b>Student Name</b><strong>
                                                                  <td><strong><b>Semester</b><strong>
                                                            </td>
                                                            <td style="text-align: center"><strong><b>
                                                                <asp:CheckBox ID="ChkPaidSelectAll" CssClass="checkbox checkbox-custom checkbox-circle checkbox-primary" Text="All" runat="server" /></b><strong>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr class="z-depth-2">

                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblRegistrationId" runat="server" Text='<%# Eval("RegistrationId") %>' />
                                                        </td>
                                                        <%--  <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>      --%>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblRegistrationDate" Font-Size="Small" runat="server" Text='<%# Eval("RegistrationDate") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                        </td>
                                                           <td style="min-width: 10px;">
                                                            <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:CheckBox ID="ChkUnPaid" CssClass="checkbox checkbox-danger checkbox-circle" Text="Unpaid" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </asp:View>
                    <asp:View ID="vwTshirtList" runat="server">
                        <div class="panel">
                            <div class="row">
                                <div class="col-md-10">
                                    <div class="input-group">
                                        <div class="input-group-addon bg-primary">
                                            <span class="input-group-text">Select College</span>
                                        </div>
                                        <asp:Panel ID="pnlTshirtCollege" runat="server">
                                            <asp:DropDownList ID="ddlTshirtCollege" AutoPostBack="true" OnSelectedIndexChanged="ddlTshirtCollege_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="quick-actions_homepage">
                                        <asp:Label ID="lblClickedSize" Visible="false" runat="server" Text=""></asp:Label>
                                        <ul class="quick-actions text-center">
                                            <li class="bg_lb col-md-1" style="cursor: unset;">
                                                <asp:LinkButton ID="btnAllTshirtList" OnClick="btnAllTshirtList_Click" runat="server">
                                                    <b>All</b><span class="label label-danger">
                                                        <asp:Label ID="lblTopAllTshirtCollegeWiseCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>

                                                    </span>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblTotalTshirtUsed" runat="server" Text="400"></asp:Label>
                                                    /
                                                                    <asp:Label ID="lblTotalTshirtAlloted" runat="server" Text="600"></asp:Label>
                                                </asp:LinkButton></li>
                                            <li class="bg_ly col-md-1">
                                                <asp:LinkButton ID="btnSTshirtList" OnClick="btnSTshirtList_Click" runat="server">
                                                    <b>S</b><span class="label label-info">
                                                        <asp:Label ID="lblTopSTshirtCollegeWiseCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                                    </span>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblSTshirtUsedCount" runat="server" Text="0"></asp:Label>
                                                    /
                                                        <asp:Label ID="lblSTshirtAlloted" runat="server" Text="0"></asp:Label>

                                                </asp:LinkButton>

                                            </li>
                                            <li class="bg_lg col-md-1">
                                                <asp:LinkButton ID="btnMTshirtList" OnClick="btnMTshirtList_Click" runat="server">
                                                    <b>M</b><span class="label label-success">
                                                        <asp:Label ID="lblTopMTshirtCollegeWiseCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                                    </span>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMTshirtUsedCount" runat="server" Text="0"></asp:Label>
                                                    /
                                                        <asp:Label ID="lblMTshirtAllotedCount" runat="server" Text="0"></asp:Label>

                                                </asp:LinkButton></li>
                                            <li class="bg_ss col-md-1">
                                                <asp:LinkButton ID="btnLTshirtList" OnClick="btnLTshirtList_Click" runat="server">
                                                    <b>L</b><span class="label label-danger">
                                                        <asp:Label ID="lblTopLTshirtCollegeWiseCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                                    </span>
                                                    <br />
                                                    <br />

                                                    <asp:Label ID="lblLTshirtCountUsedCount" runat="server" Text="0"></asp:Label>
                                                    /
                                                        <asp:Label ID="lblLTshirtAllotedCount" runat="server" Text="0"></asp:Label>

                                                </asp:LinkButton></li>
                                            <li class="bg_ls  col-md-1">
                                                <asp:LinkButton ID="btnXLTshirtList" OnClick="btnXLTshirtList_Click" runat="server">
                                                    <b>XL</b><span class="label label-success">
                                                        <asp:Label ID="lblTopXLTshirtCollegeWiseCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                                    </span>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblXLTshirtCountUsedCount" runat="server" Text="0"></asp:Label>
                                                    /
                                                        <asp:Label ID="lblXLTshirtAllotedCount" runat="server" Text="0"></asp:Label>

                                                </asp:LinkButton></li>

                                            <li class="bg_lv  col-md-1">
                                                <asp:LinkButton ID="btnXXLTshirtList" OnClick="btnXXLTshirtList_Click" runat="server">
                                                    <b>XXL</b><span class="label label-primary">
                                                        <asp:Label ID="lblTopXXLTshirtCollegeWiseCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                                    </span>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblXXLTshirtCountUsedCount" runat="server" Text="0"></asp:Label>
                                                    /
                                                        <asp:Label ID="lblXXLTshirtAllotedCount" runat="server" Text="0"></asp:Label>
                                                </asp:LinkButton>
                                            </li>
                                            <li class="bg_lr col-md-1">
                                                <asp:LinkButton ID="btnRejectedTshirtList" OnClick="btnRejectedTshirtList_Click" runat="server">
                                                    <b><span class="fa fa-eraser"></span></b><span class="label label-danger">
                                                        <asp:Label ID="lblTopRejectedTshirtCollegeWiseCount" Font-Size="Medium" Text="0" runat="server"></asp:Label>
                                                    </span>
                                                    <br />
                                                    <br />
                                                    <b>Rejected</b>
                                                    <br />
                                                </asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" runat="server" id="TshirtAll" visible="false" style="height: 650px; overflow: auto">
                                        <asp:Repeater ID="rptTshirtAll" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-hover">
                                                    <thead>
                                                        <tr style="background-color: #28b779; color: #fff">
                                                            <td>Slno</td>
                                                            <td style="display: none;">RequestedId
                                                            </td>
                                                            <td><strong><b>LEAD ID</b><strong>
                                                            </td>
                                                            <td><strong><b>Student Name</b><strong>
                                                            </td>
                                                            <td class="hidden-xs"><strong><b>College Name</b><strong>
                                                            </td>
                                                            <td><strong><b>Size</b><strong>
                                                            </td>
                                                            <td class="hidden-xs"><strong><b>Requested Date</b><strong>
                                                            </td>
                                                            <td class="hidden-xs"><strong><b>Sanction Date</b><strong>
                                                            </td>
                                                            <td class="hidden-xs"><strong><b>Rejected Date</b><strong>
                                                            </td>
                                                              <td><strong><b>Semester</b><strong>
                                                            </td>
                                                            <td><strong><b>Status</b><strong>
                                                            </td>
                                                            <td><strong><b>Remark
                                                            </td>
                                                            <td><strong><b>Re-ApplyReson
                                                            </td>
                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblRequestedId" runat="server" Text='<%# Eval("RequestedId") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;" class="hidden-xs">
                                                            <asp:Label ID="lblCollegeName" Font-Size="Small" runat="server" Text='<%# Eval("college_Name") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblSize" Font-Size="Small" runat="server" Text='<%# Eval("TshirtSize") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;" class="hidden-xs">
                                                            <asp:Label ID="lblRequestedDate" Font-Size="Small" runat="server" Text='<%# Eval("RequestedDate") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;" class="hidden-xs">
                                                            <asp:Label ID="lblSanctionDate" Font-Size="Small" runat="server" Text='<%# Eval("SanctionDate") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;" class="hidden-xs">
                                                            <asp:Label ID="lblRejectedDate" Font-Size="Small" runat="server" Text='<%# Eval("RejectedDate") %>' />
                                                        </td>
                                                             <td style="min-width: 40px;">
                                                            <asp:Label ID="lblSemester" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblStatus" Font-Size="Small" runat="server" Text='<%# Eval("Status") %>' />
                                                        </td>

                                                        <td style="width: 20%;">
                                                            <asp:Label ID="lblRemark" Font-Size="Small" runat="server" Text='<%# Eval("Remark") %>' />
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:Label ID="lblReapplyReson" Font-Size="Small" runat="server" Text='<%# Eval("ReapplyReson") %>' />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>


                                <div class="row" runat="server" id="tshirtapprovelabels">
                                    <div class="col-md-6">
                                        Requested List - &nbsp;
                                                                <asp:Label ID="lblRequestedTshirtCount" Font-Size="Larger" Font-Bold="true" runat="server" Text=""></asp:Label><span class="pull-right">
                                                                    <asp:LinkButton ID="btnTshirtApproval" OnClick="btnTshirtApproval_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-arrow-right"></span> </asp:LinkButton>
                                                                </span>
                                    </div>
                                    <div class="col-md-6">
                                        Approved & Issue List - &nbsp;
                                                                <asp:Label ID="lblApprovedTshirtCount" Font-Size="Larger" Font-Bold="true" runat="server" Text=""></asp:Label>
                                        <span class="pull-right">
                                            <asp:LinkButton ID="btnTshirtNonApproval" OnClick="btnTshirtNonApproval_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-arrow-left"></span> </asp:LinkButton>
                                        </span>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" runat="server" id="Requesting" style="height: 650px; overflow: auto">
                                        <asp:Repeater ID="rptTshirtRequestedList" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-hover">
                                                    <thead>
                                                        <tr style="background-color: #28b779; color: #fff">
                                                            <td>Slno</td>

                                                            <td style="display: none;">RequestedId
                                                            </td>
                                                            <td>
                                                                <strong><b>
                                                                    <asp:CheckBox ID="ChkApproveAll" Text="All" onclick="checkAll(this);" runat="server" /></b><strong>
                                                            </td>
                                                            <td><strong><b>Requested Date</b><strong>
                                                            </td>
                                                            <td><strong><b>LEAD ID</b><strong>
                                                            </td>
                                                            <td><strong><b>Student Name</b><strong>
                                                            </td>
                                                                 <td><strong><b>Semester</b><strong>
                                                            </td>
                                                            <td><strong><b><span class="fa fa-close" style="cursor: pointer;"></span></b><strong></td>
                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkTshirtApprove" Font-Size="Small" Text='<%# Eval("TshirtSize") %>' runat="server" />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblRequestedId" runat="server" Text='<%# Eval("RequestedId") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblRequestedDate" Font-Size="Small" runat="server" Text='<%# Eval("RequestedDate") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                        </td>
                                                                 <td style="min-width: 10px;">
                                                            <asp:Label ID="lblSemester" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:LinkButton ID="btnTshirtReject" OnClick="btnTshirtReject_Click" runat="server"><span class="fa fa-remove text-danger"></span> </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="col-md-6" runat="server" id="Sanction" style="height: 650px; overflow: auto">
                                        <asp:Repeater ID="rptTshirtSanctionedList" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-hover">
                                                    <thead>
                                                        <tr style="background-color: #28b779; color: #fff">
                                                            <td>Slno</td>
                                                            <td style="display: none;">RequestedId
                                                            </td>
                                                            <td>
                                                                <strong><b>
                                                                    <asp:CheckBox ID="ChkApproveAll" Text="All" onclick="checkAll(this);" runat="server" /></b><strong>
                                                            </td>

                                                            <td><strong><b>Sanction Date</b><strong>
                                                            </td>
                                                            <td><strong><b>LEAD ID</b><strong>
                                                            </td>
                                                            <td><strong><b>Student Name</b><strong>
                                                            </td>
                                                              <td><strong><b>Semester</b><strong>
                                                            </td>
                                                            <td style="text-align: center">
                                                                <strong><b><span class="fa fa-exchange"></span></b><strong></td>
                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblRequestedId" runat="server" Text='<%# Eval("RequestedId") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkTshirtSanctionReturn" Font-Size="Small" Text='<%# Eval("TshirtSize") %>' runat="server" />
                                                        </td>

                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblSanctionDate" Font-Size="Small" runat="server" Text='<%# Eval("SanctionDate") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                        </td>
                                                           <td style="min-width: 10px;">
                                                            <asp:Label ID="lblSem" Font-Size="Small" runat="server" Text='<%# Eval("SemName") %>' />
                                                        </td>
                                                        <td style="width: 20%; text-align: center;">
                                                            <asp:LinkButton ID="btnExchange" OnClick="btnExchange_Click" runat="server"><span class="fa fa-exchange"></span> </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="col-md-6" runat="server" id="rejected" visible="false" style="height: 650px; overflow: auto">
                                        <asp:Repeater ID="rptTshirtRejected" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-hover">
                                                    <thead>
                                                        <tr style="background-color: #28b779; color: #fff">
                                                            <td>Slno</td>
                                                            <td style="display: none;">RegistrationID
                                                            </td>
                                                            <%-- <td ><b><strong> Pic</strong></b>
                                                                                    </td>  --%>
                                                            <td><strong><b>Size</b><strong>
                                                            </td>
                                                            <td><strong><b>Rejected Date</b><strong>
                                                            </td>
                                                            <td><strong><b>LEAD ID</b><strong>
                                                            </td>
                                                            <td><strong><b>Student Name</b><strong>
                                                            </td>
                                                            <td style="text-align: center"><strong><b>Remark
                                                            </td>
                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblRequestedId" runat="server" Text='<%# Eval("RequestedId") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblSize" Font-Size="Small" runat="server" Text='<%# Eval("TshirtSize") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblRejectedDate" Font-Size="Small" runat="server" Text='<%# Eval("RejectedDate") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:Label ID="lblRemark" Font-Size="Small" runat="server" Text='<%# Eval("Remark") %>' />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>





    </section>

    <script type="text/javascript">
        function ErrorModal() {
            $('#ErrorModal').modal('show');

        }
    </script>
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


    <div id="POP_SchedulePay" class="modal fade" style="margin-top: 0px">
        <div class="m-b-none">
            <div class="modal-dialog" style="width: 80%; overflow: auto">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal"><i class="fa fa-times"></i></button>
                                <h4 class="modal-title">
                                    <asp:Label ID="lblPop_ScheduleTitle" runat="server" Text=""></asp:Label>
                                    <a class="pull-right text-right" data-dismiss="modal">
                                        <asp:Label ID="lblPop_ScheduleLeadId" Visible="false" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblPop_ScheduleManagerId" Visible="false" runat="server" Text=""></asp:Label>
                                    </a>
                                </h4>
                            </div>

                            <div class="modal-body">
                                <p>
                                    <asp:Label ID="lblPop_SchedulePDId" Visible="false" runat="server" Text=""></asp:Label>
                                </p>
                                <div class="row">
                                    <div class="col-md-12 ">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="lblSchedule_ProjectTitle">Project Title</label>
                                                <asp:Label ID="lblSchedule_ProjectTitle" CssClass="form-control bg-success" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="lblSchedule_ProjectTitle">Student Name</label>
                                                <asp:Label ID="lblSchedule_StudentName" CssClass="form-control bg-success" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-10">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="txtSchedule_GivingAmount">
                                                    Amount
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtSchedule_GivingAmount" SetFocusOnError="true" ForeColor="DarkRed" ValidationGroup="Schedule" runat="server" ErrorMessage="* NumericOnly"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtSchedule_GivingAmount" placeholder="Giving Amount" onkeypress="NumericOnly()" CssClass="form-control " runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-7">
                                                <label for="txtSchedule_ManagerRemark">
                                                    Manager Remark
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtSchedule_ManagerRemark" SetFocusOnError="true" ForeColor="DarkRed" ValidationGroup="Schedule" runat="server" ErrorMessage="* required"></asp:RequiredFieldValidator></label>
                                                <asp:TextBox ID="txtSchedule_ManagerRemark" placeholder="Manager Comments" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <br />
                                                <div class="row form-group">
                                                    <div class="col-md-12">
                                                        <asp:LinkButton ID="btnSchedule_SaveAmount" ValidationGroup="Schedule" OnClick="btnSchedule_SaveAmount_Click" CssClass="btn btn-info" runat="server"><span class="fa fa-check"></span>&nbsp; Save </asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <div class="col-md-12">
                                                        <button type="button" class="btn btn-danger" data-dismiss="modal"><span class="fa fa-times"></span>&nbsp; Close</button>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12" style="height: 200px; overflow: auto;">
                                                        <asp:Repeater runat="server" ID="rptScheduledDetails">
                                                            <HeaderTemplate>
                                                                <table class="table table-hover" style="width: 100%">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Date</th>
                                                                            <th>Amount</th>
                                                                            <th>Manager Remark</th>
                                                                            <th>Received Status</th>
                                                                        </tr>
                                                                    </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="display: none;">
                                                                        <asp:Label ID="lblFundId" Text='<%# Eval("Fund_Id") %>' runat="server" />
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        <asp:Label ID="lblGivenDate" Text='<%# Eval("GivenDate") %>' runat="server" />
                                                                    </td>
                                                                    <td style="width: 10%;">
                                                                        <asp:Label ID="lblAmount" Text='<%# Eval("Amount") %>' runat="server" />
                                                                    </td>
                                                                    <td style="width: 50%;">

                                                                        <asp:Label ID="lblManagerRemark" Text='<%# Eval("ManagerRemark") %>' runat="server" />
                                                                    </td>
                                                                    <td style="width: 15%;">

                                                                        <asp:Label ID="lblStudentResponse" Text='<%# Eval("ReceivedStatus") %>' runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="row">
                                            <ul class="list-group">
                                                <li class="list-group-item">
                                                    <label for="lblSchedule_RequestedAmount">Requested Amount</label>
                                                    <asp:Label ID="lblSchedule_RequestedAmount" CssClass="form-control bg-info" runat="server" Text=""></asp:Label></li>
                                                <li class="list-group-item">
                                                    <label for="lblSchedule_SanctionAmount">Approved amount</label>
                                                    <asp:Label ID="lblSchedule_SanctionAmount" CssClass="form-control bg-info" runat="server" Text=""></asp:Label></li>
                                                <li class="list-group-item">
                                                    <label for="lblSchedule_TotalGivenAmount">Released amount</label>
                                                    <asp:Label ID="lblSchedule_TotalGivenAmount" CssClass="form-control bg-info" runat="server" Text=""></asp:Label></li>
                                                <li class="list-group-item">
                                                    <label for="lblSchedule_BalanceAmount">Balance Amount</label>
                                                    <asp:Label ID="lblSchedule_BalanceAmount" CssClass="form-control bg-info" runat="server" Text=""></asp:Label></li>
                                            </ul>

                                        </div>
                                    </div>

                                </div>

                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- /.modal-content -->
            </div>
        </div>
    </div>
    <div id="Pop_ChangePassword" class="modal fade" role="dialog" style="margin-top: 0px">
        <div class="modal-dialog bg-danger" style="width: 60%;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h3>
                        <asp:Label ID="lblChangePasswordTitle" runat="server" Text=""></asp:Label>
                    </h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="row">
                                <div class="col-lg-12 ">
                                    <div class="text-center bg-white">
                                        <asp:Image ID="ImgManagerProfilePic" ImageUrl="~/CSS/Images/NoImage.png" CssClass="center-block" EnableTheming="True" Width="240px" Height="240px" runat="server" />
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
                        </div>
                        <div class="col-md-8">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="input-group form-group">
                                        <div class="input-group-addon bg-focus">
                                            <span class="input-group-text">Old Password 
                                               
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtOldPassword" CssClass="form-control" placeholder="Old Password" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="input-group form-group">
                                        <div class="input-group-addon bg-focus">
                                            <span class="input-group-text">New Password 
                                               
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtNewPassword" CssClass="form-control" placeholder="New Password" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <asp:LinkButton ID="btnChangePasswordEvent" CssClass="btn btn-facebook" OnClick="btnChangePasswordEvent_Click" runat="server"><span class="fa fa-lock"></span> &nbsp; Change Password </asp:LinkButton>
                                </div>
                                <div class="col-md-6">
                                    <button data-dismiss="modal" class="btn btn-danger"><span class="fa fa-close" style="cursor: pointer;"></span>&nbsp; Close </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="POP_TshirtReject" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body" style="background-color: indianred; color: white;">
                    <h3>Enter Reson for T-shirt Reject
                          <a class="pull-right text-right" data-dismiss="modal" style="color: white;">
                              <i class="fa fa-remove fa-fw" style="cursor: pointer;"></i>
                          </a>
                    </h3>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblRejectedTshirtPOPSize" Visible="false" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblRejectedTshirtPOPRequestedId" Visible="false" runat="server" Text=""></asp:Label>
                            <label for="txtTshirtRejectReson" class="text-danger brandFont">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTshirtRejectReson" ValidationGroup="TshirtReject" ForeColor="DarkRed" Display="Dynamic" SetFocusOnError="true" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtTshirtRejectReson" CssClass="form-control brandFont" TextMode="MultiLine" placeholder="Enter Reson for Reject" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-md-4 col-md-offset-1">
                            <asp:LinkButton ID="btnRejectRequestedTshirt" OnClick="btnRejectRequestedTshirt_Click" ValidationGroup="TshirtReject" CssClass="btn btn-danger btn-black" runat="server"><span class="fa fa-close" style="cursor:pointer;">&nbsp; <i class="brandFont"> Reject T-Shirt Request</i></span>  </asp:LinkButton>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>
    <div id="POP_TshirtExchange" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body" style="background-color: cornflowerblue; color: white;">
                    <h3><b>
                        <asp:Label ID="lblTshirtExchangeSize" runat="server" Text=""></asp:Label></b>&nbsp;  T-Shirt Exchange
                          <a class="pull-right text-right" data-dismiss="modal" style="color: white;">
                              <i class="fa fa-remove fa-fw" style="cursor: pointer;"></i>
                          </a>
                    </h3>

                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblTshirtExchangeRequestedId" Visible="false" runat="server" Text=""></asp:Label>
                            <label for="ddlTshirtExchange">Select Reson for Exchange</label>
                            <asp:DropDownList ID="ddlTshirtExchangeReson" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Size Variation" Value="Size Variation"></asp:ListItem>
                                <asp:ListItem Text="Low Printing Quality" Value="Low Printing Quality"></asp:ListItem>
                                <asp:ListItem Text="Teared" Value="Teared"></asp:ListItem>
                                <asp:ListItem Text="Dusty" Value="Dusty"></asp:ListItem>
                                <asp:ListItem Text="Old Stock" Value="Old Stock"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">

                            <label for="ddlTshirtExchange">Select New Size for Exchange</label>
                            <asp:DropDownList ID="ddlTshirtExchangeSize" CssClass="form-control" runat="server">
                                <asp:ListItem Text="S - Small" Value="S"></asp:ListItem>
                                <asp:ListItem Text="M - Medium" Value="M"></asp:ListItem>
                                <asp:ListItem Text="L - Large" Value="L"></asp:ListItem>
                                <asp:ListItem Text="XL - Extra Large" Value="XL"></asp:ListItem>
                                <asp:ListItem Text="XXL - double Extra Large" Value="XXL"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>


                    <br />
                    <div class="row">
                        <div class="col-md-4 col-md-offset-1">
                            <asp:LinkButton ID="btnTshirtExChangeByManager" OnClick="btnTshirtExChangeByManager_Click" CssClass="btn btn-primary btn-black" runat="server"><span class="fa fa-exchange">&nbsp; <i class="brandFont"> Exchange T-Shirt</i></span>  </asp:LinkButton>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>
    <div id="POP_FundAmount" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h3><b>
                                <asp:TextBox ID="lblFundingAmountPDID" ForeColor="White" runat="server"></asp:TextBox>
                                <asp:TextBox ID="lblFundingAmountBalance" ForeColor="White" runat="server"></asp:TextBox>
                                <asp:TextBox ID="lblFundingLeadId" ForeColor="White" runat="server"></asp:TextBox>
                                <asp:TextBox ID="lblFundingProjectTitle" ForeColor="White" runat="server"></asp:TextBox>
                            </b>&nbsp;  Fund Releasing 
                                <br />
                         <a class="pull-right text-right" data-dismiss="modal">
                             <i class="fa fa-remove" style="cursor: pointer;"></i>
                         </a>
                            </h3>
                        </div>
                    </div>


                    <div class="row">
                        <span style="display: none;">
                            <asp:TextBox ID="txtFundComments" placeholder="Funding Comments" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                        </span>
                        <div class="col-md-6">
                            <label for="txtMailId" class="brandFont">
                                Fund Amount
 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" ControlToValidate="txtFundingAmount" ValidationGroup="Fund" Display="Dynamic" SetFocusOnError="true" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtFundingAmount" placeholder="Funding Amount" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-md-2">
                              <asp:Button ID="btnFundingAmountWithoutComments" OnClick="btnFundingAmountWithoutComments_Click" CssClass="btn btn-info" ValidationGroup="Fund"  runat="server" Text="Ok" />
                            <%--<asp:LinkButton ID="btnFundingAmountWithoutComments" OnClick="btnFundingAmountWithoutComments_Click" CssClass="btn btn-info" runat="server">Ok</asp:LinkButton>--%>
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>


                </div>

            </div>

        </div>
    </div>


    <div id="POP_Chat" class="modal fade" role="dialog" style="margin-top: 40px;">
        <div class="modal-dialog">
            <div class="container">
                <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-5 col-xs-12">
                        <div class="panel" style="overflow-y: scroll; height: 480px;">

                            <div class="panel-body">
                                <span class="fa fa-comment"></span>Chat
       <a class="pull-right text-right" data-dismiss="modal">&nbsp;
                            <i class="fa fa-remove text-primary" style="cursor: pointer;"></i>
       </a>
                                <h4 class="text-info form-control">Title :  
                                    <asp:Label ID="lblChatProjectTitle" runat="server" Text=""></asp:Label>

                                </h4>
                                <span style="display: none;">
                                    <asp:Label ID="lblChatPDID" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblChatProjectStatus" runat="server" Text=""></asp:Label>
                                </span>
                                <ul class="chat" style="margin-top: 10px;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <%--     <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="1000"></asp:Timer>--%>
                                            <asp:Repeater runat="server" ID="rptDiscussionForum">
                                                <ItemTemplate>
                                                    <asp:Panel ID="Panel1" Visible='<%# Eval("User_Type").ToString() == "Student" ? true : false %>' runat="server">
                                                        <li class="left clearfix"><span class="chat-img pull-left">
                                                            <img src="../../CSS/Images/Chat_U.png" style="width: 40px; height: 40px;" alt="User Avatar" class="img-circle">
                                                        </span>
                                                            <div class="chat-body clearfix">
                                                                <div class="header">
                                                                    <strong class="primary-font"><%#Eval("StudentName")%></strong>
                                                                    <small class="pull-right text-muted" style="font-size: xx-small;">
                                                                        <%#Eval("ProjectStatus")%> - <%#Eval("Comment_Type")%>
                                                                    </small>
                                                                </div>
                                                                <p>
                                                                    <%#Eval("comments")%>
                                                                </p>
                                                                <p>
                                                                    <span class="fa fa-clock-o"></span>&nbsp;<label style="font-size: xx-small; font-style: normal;" class="text-muted">
                                                                        <%#Eval("Reply_Time")%>
                                                                    </label>
                                                                </p>
                                                            </div>
                                                        </li>
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel2" Visible='<%# Eval("User_Type").ToString() == "Manager" ? true : false %>' runat="server">
                                                        <li class="right clearfix">
                                                            <span class="chat-img pull-right">
                                                                <img src="../../CSS/Images/Chat_Me.png" alt="User Avatar" style="width: 40px; height: 40px;" class="img-circle">
                                                            </span>
                                                            <div class="chat-body clearfix">
                                                                <div class="header">
                                                                    <small class="text-muted" style="font-size: xx-small;">
                                                                        <%#Eval("ProjectStatus")%> - <%#Eval("Comment_Type")%>
                                                                    </small>
                                                                    <strong class="pull-right primary-font"><%#Eval("ManagerName")%></strong>
                                                                </div>
                                                                <p>
                                                                    <%#Eval("comments")%>
                                                                </p>
                                                                <p>
                                                                    <span class="fa fa-clock-o"></span>&nbsp;<label style="font-size: xx-small; font-style: normal;" class="text-muted">
                                                                        <%#Eval("Reply_Time")%>
                                                                    </label>
                                                                </p>
                                                            </div>
                                                        </li>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ContentTemplate>
                                        <%--                <Triggers >
                                  <asp:AsyncPostBackTrigger ControlID="btnSaveCommentChat" EventName="Click" />
                            </Triggers>--%>
                                    </asp:UpdatePanel>

                                </ul>
                                <br />
                                <div class="input-group">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" ValidationGroup="Chat" ControlToValidate="txtChatMessage" runat="server" ErrorMessage="* Required">

                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtChatMessage" CssClass="form-control " Font-Size="Medium" autocomplete="off" placeholder="Type your message here..." ValidationGroup="Chat"
                                        TextMode="MultiLine" Rows="2" runat="server">
                                    </asp:TextBox>

                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="btnSaveCommentChat" OnClick="btnSaveCommentChat_Click" CssClass="btn btn-dropbox btn-rounded btn-sm" runat="server" ValidationGroup="Chat"> Send&nbsp;
                            <span class="fa fa-send"> </span></asp:LinkButton>

                                    </span>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>


        </div>
    </div>

     <script type="text/javascript">
         function DisableButton() {
             document.getElementById("<%=btnFundingAmountWithoutComments.ClientID %>").disabled = true;
         }
         window.onbeforeunload = DisableButton;
     </script>

    <script>
        window.onbeforeunload = DisableButton;
        function DisableButton() {
            if ($('#ContentPlaceHolder1_txtFundingAmount').val().trim() != '') {
                document.getElementById('<%= btnFundingAmountWithoutComments.ClientID %>').disabled = "disabled";
            }
            

        }

    </script>

    <script type="text/javascript">
        function openModal() {
            document.getElementById('myModal').style.display = "block";
        }

        function closeModal() {
            document.getElementById('myModal').style.display = "none";
        }

        var slideIndex = 1;
        showSlides(slideIndex);

        function plusSlides(n) {
            showSlides(slideIndex += n);
        }

        function currentSlide(n) {
            showSlides(slideIndex = n);
        }

        function showSlides(n) {
            var i;
            var slides = document.getElementsByClassName("mySlides");
            var dots = document.getElementsByClassName("demo");
            var captionText = document.getElementById("caption");
            if (n > slides.length) { slideIndex = 1 }
            if (n < 1) { slideIndex = slides.length }
            for (i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }
            for (i = 0; i < dots.length; i++) {
                dots[i].className = dots[i].className.replace(" active", "");
            }
            slides[slideIndex - 1].style.display = "block";
            dots[slideIndex - 1].className += " active";
            captionText.innerHTML = dots[slideIndex - 1].alt;
        }
    </script>

    <script type="text/javascript">
        function Pop_ChangePassword() {
            //$('#POP_RequestForCompletion').modal('show');
            $('#Pop_ChangePassword').modal({
                show: true
            });

        }
    </script>
    <script type="text/javascript">
        function POP_SchedulePay() {
            //$('#POP_RequestForCompletion').modal('show');
            $('#POP_SchedulePay').modal({
                backdrop: 'static',
                keyboard: true,
                show: true
            });

        }
    </script>
    <script type="text/javascript">
        function POP_TshirtReject() {
            //$('#POP_RequestForCompletion').modal('show');
            $('#POP_TshirtReject').modal({
                show: true
            });

        }
    </script>
    <script type="text/javascript">
        function POP_TshirtExchange() {
            //$('#POP_RequestForCompletion').modal('show');
            $('#POP_TshirtExchange').modal({
                show: true
            });

        }
    </script>
    <script type="text/javascript">
        function POP_FundAmount() {

            $('#POP_FundAmount').modal({
                backdrop: 'static',
                keyboard: true,
                show: true
            });

        }
    </script>
    <script type="text/javascript">

        jQuery(document).ready(function () {
            // Date Picker
            jQuery('.datepicker').datepicker({
                format: "yyyy-MM-dd",
                autoclose: true,
                todayHighlight: true
            });
        });
    </script>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            // Date Picker
            var today = new Date();
            var dd = today.getDate();

            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            var startdates = yyyy + '-' + mm + '-' + dd;

            jQuery('.datepicker').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true,
                todayHighlight: true,
                startDate: startdates

            });
        });

        //function RemoveRows(ids)
        //{
        //    $(ids).parent('td').parent('tr').remove();           
        //}
    </script>
    <script type="text/javascript">
        function POP_Chat() {
            $('#POP_Chat').modal({

                show: true
            });

        }
    </script>
    <script type="text/javascript">
        function SearchStudentDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("<%= txtStudentSearch.ClientID %>");
            filter = input.value.toUpperCase();
            ul = document.getElementById("SearchList");
            li = ul.getElementsByTagName("tbody");
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
    <script src="../../JS/ManagerJS/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        function ProposedSearchDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("<%=txtProposedSearch.ClientID %>");
            filter = input.value.toUpperCase();
            ul = document.getElementById("ProposedList");
            li = ul.getElementsByTagName("tbody");
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
        function ApprovedSearchDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("<%=txtApprovedSearch.ClientID%>");
            filter = input.value.toUpperCase();
            ul = document.getElementById("ApprovedList");
            li = ul.getElementsByTagName("tbody");
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
        function RequestForModificationSearchDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("<%=txtRequestForModificationSearch.ClientID%>");
            filter = input.value.toUpperCase();
            ul = document.getElementById("RequestForModificationList");
            li = ul.getElementsByTagName("tbody");
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
        function RequestForCompletionSearchDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("<%=txtRequestForCompletionSearch.ClientID %>");
            filter = input.value.toUpperCase();
            ul = document.getElementById("RequestForCompletionList");
            li = ul.getElementsByTagName("tbody");
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
        function CompletionSearchDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("<%=txtCompletionSearch.ClientID %>");
            filter = input.value.toUpperCase();
            ul = document.getElementById("CompletionList");
            li = ul.getElementsByTagName("tbody");
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
        function RejectedProjectSearchDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("<%=txtRejectedProjectSearch.ClientID %>");
            filter = input.value.toUpperCase();
            ul = document.getElementById("RejectProjectList");
            li = ul.getElementsByTagName("tbody");
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
        function SearchFeePaidUnPaidDetails() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtFeePaidUnPaidDetails");
            filter = input.value.toUpperCase();
            ul = document.getElementById("FeesPaidUnPaidList");
            li = ul.getElementsByTagName("tbody");
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
        function FundAmountSearchDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("<%= txtFundAmountSearchDetail.ClientID %>");
            filter = input.value.toUpperCase();
            ul = document.getElementById("FundAmountProjectList");
            li = ul.getElementsByTagName("tbody");
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
        function Profile() {
            var Avatar = document.querySelector('#<%=ImgManagerProfilePic.ClientID %>');
            var file = document.querySelector('#<%=ProfilePic.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                Avatar.src = reader.result;
            }

            if (file) {

                reader.readAsDataURL(file);
            } else {
                Avatar.src = "";
            }
        }
    </script>
</asp:Content>

