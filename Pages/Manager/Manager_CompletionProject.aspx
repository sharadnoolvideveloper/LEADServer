<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="Manager_CompletionProject.aspx.cs" Inherits="Pages_Manager_Manager_CompletionProject" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>

    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/toster.js"></script>
    <link href="../../CSS/CommonCSS/MultiSelect.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/MultiSelect.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.lstbox').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>

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

        .lstbox {
        }

        .hover-shadow:hover {
            box-shadow: 0 80px 80px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        .fixed-leftbutton {
            position: fixed;
        }
    </style>
    <script>
        function resize() {
            if (($(window).width() > 0 && $(window).width() < 1000)) {
                $("#fixed").removeClass("fixed-leftbutton");
            }
            else { $("#fixed").addClass("fixed-leftbutton"); }
        }
        $(document).ready(function () {
            $(window).resize(resize);
            resize();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            if ($(".pnl_impactdiv").length) {
                toastr.info("Check Impact Project");
                toastr.options.positionClass = "toast-top-right";
                $(<%= ChkCollabration.ClientID %>).click(function () {
                    if ($(this).is(":checked")) {
                        $("#Collabration").show();
                    } else {

                        $("#Collabration").hide();
                    }

                });
                if ($(<%= ChkCollabration.ClientID %>).is(":checked")) {
                    $("#Collabration").show();
                }
                $(<%= ChkAgainst_Tide.ClientID %>).click(function () {
                    if ($(this).is(":checked")) {
                        $("#Against_Tide").show();
                    } else {

                        $("#Against_Tide").hide();
                    }
                });
                if ($(<%= ChkAgainst_Tide.ClientID %>).is(":checked")) {
                    $("#Against_Tide").show();
                }
                $(<%= ChkCross_Hurdles.ClientID %>).click(function () {
                    if ($(this).is(":checked")) {
                        $("#Cross_Hurdles").show();
                    } else {

                        $("#Cross_Hurdles").hide();
                    }
                });
                if ($(<%= ChkCross_Hurdles.ClientID %>).is(":checked")) {
                    $("#Cross_Hurdles").show();
                }
                $(<%= ChkEntrepreneurial_Venture.ClientID %>).click(function () {
                    if ($(this).is(":checked")) {
                        $("#Entrepreneurial_Venture").show();
                    } else {

                        $("#Entrepreneurial_Venture").hide();
                    }
                });
                if ($(<%= ChkEntrepreneurial_Venture.ClientID %>).is(":checked")) {
                    $("#Entrepreneurial_Venture").show();
                }
                $(<%= ChkGovernment_Awarded.ClientID %>).click(function () {
                    if ($(this).is(":checked")) {
                        $("#Government_Awarded").show();
                    } else {

                        $("#Government_Awarded").hide();
                    }
                });
                if ($(<%= ChkGovernment_Awarded.ClientID %>).is(":checked")) {
                    $("#Government_Awarded").show();
                }
                $(<%= ChkLeadership_Roles.ClientID %>).click(function () {
                    if ($(this).is(":checked")) {
                        $("#Leadership_Roles").show();
                    } else {
                        $("#Leadership_Roles").hide();
                    }
                });
                if ($(<%= ChkLeadership_Roles.ClientID %>).is(":checked")) {
                    $("#Leadership_Roles").show();
                }
            }
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="row">
        <div class="col-md-1">
            <div class="row fixed-leftbutton" id="fixed">
                <div class="col-md-12 col-xs-3 pull-right form-group">
                    <asp:LinkButton ID="btnMangerCompletionComfirmation" OnClick="btnMangerCompletionComfirmation_Click" ValidationGroup="MangerCompletion" CssClass="btn btn-primary" runat="server"><span class="fa fa-check"></span>
                       <br />
                       Submit &nbsp;  </asp:LinkButton>
                </div>
                <div class="col-md-12 col-xs-3 pull-right form-group">
                    <asp:LinkButton ID="btnClose" OnClick="btnClose_Click" CssClass="btn btn-white" runat="server"> <span class="fa fa-remove"></span>
                     <br />
                     Close &nbsp;&nbsp;&nbsp;
                    </asp:LinkButton>

                    <span style="display: none;">
                        <asp:Label ID="lblPDID" runat="server"></asp:Label>
                        <asp:Label ID="lblProjectStatus" runat="server"></asp:Label>
                        <asp:Label ID="lblLead_Id" runat="server"></asp:Label>
                    </span>
                </div>
                <div class="col-md-12 col-xs-3 pull-right form-group">
                    <asp:LinkButton ID="btnManagerImpactProject" OnClick="btnManagerImpactProject_Click" 
                        CssClass="btn btn-info" runat="server"><span class="fa fa-magic"></span>
                       <br />
                      Impact  &nbsp;  </asp:LinkButton>
                      <span style="display: none;">
                        <asp:Label ID="lblImpact" runat="server" Text=""></asp:Label>
                    </span>
                </div>
              
                <div class="col-md-12 col-xs-3 pull-right  form-group">
                    <asp:Panel ID="pnlReject" runat="server">
                        <a href="#" data-toggle="modal" data-target="#POP_Reject" class="btn btn-danger">
                            <span class="fa fa-remove"></span>
                            <br />
                            Reject &nbsp;
                        </a>
                    </asp:Panel>
                </div>
                <div class="col-md-12 col-xs-3">
                    <asp:LinkButton ID="btnDownloadStudentDocument" CssClass="btn btn-success" OnClick="btnDownloadStudentDocument_Click" runat="server"><span class="fa fa-download fa-3x ">&nbsp;&nbsp;</span>
                    </asp:LinkButton>
                </div>


            </div>
        </div>
        <div class="col-md-10" style="height: 750px; overflow: auto;">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-6">
                                    <label for="txtCompletionStudentName">Student Name</label>
                                    <asp:TextBox ID="txtCompletionStudentName" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label for="txtCompletionMobileNo">Mobile No</label>
                                    <asp:TextBox ID="txtCompletionMobileNo" onkeypress="NumericOnly()" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>
                                </div>
                                  <div class="col-md-3">
                                    <label>Sem / cohorts</label>
                                      <asp:TextBox ID="txtSemName" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>

                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label for="txtCompletionProjectTitle">Title of the Project </label>
                                    <asp:TextBox ID="txtCompletionProjectTitle" TextMode="MultiLine" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtCompletionProjectObjective">Objective of Project</label>
                                    <asp:TextBox ID="txtCompletionProjectObjective" TextMode="MultiLine" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">

                                    <label for="txtCompletionBeneficiary">Total Beneficiaries </label>
                                    <asp:TextBox ID="txtCompletionBeneficiary" placeholder="Total No Beneficiaries" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-3">
                                    <label for="txtCompletionActualBeneficiaries">Actual Beneficiaries </label>
                                    <asp:TextBox ID="txtCompletionActualBeneficiaries" placeholder="Actual Beneficiaries" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txtCompletionPlaceofImplement">Place of Implementation</label>
                                    <asp:TextBox ID="txtCompletionPlaceofImplement" placeholder="Place of Implementation" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txtCompletionFundRaised">Fund Raised</label>
                                    <asp:TextBox ID="txtCompletionFundRaised" placeholder="Fund Raised" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlCompletionTheme">Theme</label>
                                    <asp:DropDownList ID="ddlCompletionTheme" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="txtCompletionRequestedAmount">Requested Amount</label>
                                    <asp:TextBox ID="txtCompletionRequestedAmount" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtCompletionApprovedAmount">Approved Amount</label>
                                    <asp:TextBox ID="txtCompletionApprovedAmount" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label for="txtRCStartDate">Start Date</label>
                                    <asp:TextBox ID="txtRCStartDate" placeholder="yyyy-mm-dd" autocomplete="off" CssClass="form-control datepicker" runat="server">

                                    </asp:TextBox>

                                </div>
                                <div class="col-md-2 form-group">
                                    <label for="txtRCEndDate">End Date</label>

                                    <asp:TextBox ID="txtRCEndDate" placeholder="yyyy-mm-dd" autocomplete="off" CssClass="form-control datepicker" runat="server">

                                    </asp:TextBox>

                                </div>
                                <div class="col-md-2">
                                    <label>Target Days</label>
                                    <br />
                                    <asp:Label ID="lblRCTargetDays" CssClass="text-center" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label for="">
                                        Challenges Faced During Project &nbsp;
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtCompletionChanllengesFaced" ValidationGroup="Completion" ErrorMessage="* Required" ForeColor="Red" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCompletionChanllengesFaced" placeholder="Challenges Faced During Projects" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtCompletionChanllengesFaced" ID="RegularExpressionValidator7" ForeColor="Red" ValidationExpression="^[\s\S]{10,}$" runat="server" ErrorMessage="* Minimum 10 Characters ." ValidationGroup="Completion"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtCompletionLearningFromProject">
                                        learning from this project
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCompletionLearningFromProject" ValidationGroup="Completion" ErrorMessage="* Required" ForeColor="Red" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCompletionLearningFromProject" placeholder="learning from this project" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCompletionLearningFromProject" ID="RegularExpressionValidator8" ForeColor="Red" SetFocusOnError="true" ValidationExpression="^[\s\S]{20,}$" runat="server" ErrorMessage="* Minimum 20 Characters ." ValidationGroup="Completion"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label for="txtCompletionProjectStory">
                                        Your project as a Story 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtCompletionProjectStory" ValidationGroup="Completion" ErrorMessage="* Required" ForeColor="Red" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCompletionProjectStory" placeholder="Enter Your Project as a Story" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCompletionProjectStory" ID="RegularExpressionValidator6" ForeColor="Red" SetFocusOnError="true" ValidationExpression="^[\s\S]{100,}$" runat="server" ErrorMessage="* Minimum 100 Characters ." ValidationGroup="Completion"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-4">
                                    <label for="txtCompletionResourceUtilize">Resources Utilization 
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCompletionResourceUtilize" ValidationGroup="Completion" ErrorMessage="* Required" ForeColor="Red" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCompletionResourceUtilize" placeholder="Resources Utilization" TextMode="MultiLine" Rows="1" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="txtCompletionResourceAmount">Resources Worth Amount 
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCompletionResourceAmount" ValidationGroup="Completion" ErrorMessage="* Required" ForeColor="Red" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCompletionResourceAmount" placeholder="Total Amount" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Panel ID="pnl_ImpactQuestions" runat="server">
                                        <div class="pnl_impactdiv">
                                            <div class="row form-group">
                                                <div class="col-md-6">
                                                    <div class="panel panel-warning z-depth-1 hoverable">

                                                        <div class="panel-body">
                                                            <label for="txtCompletionCollabration" style="color: #F66634;">
                                                                with whom and in what way collaboration is supported to your project? &nbsp;
                                                    <asp:CheckBox ID="ChkCollabration" runat="server" />
                                                           
                                                            </label>
                                                            <span id="Collabration" style="display: none;">
                                                                <asp:TextBox ID="txtCompletionCollabration" placeholder="with whom and in what way collaboration is supported to your project?" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                                                      <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionCollabration" ID="RegularExpressionValidator1" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required."></asp:RegularExpressionValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">

                                                    <label for="txtCompletionProcedure" style="color: #F66634;">Explain in 100 words the project details. (Procedure, permission and activities done)</label>
                                                    <asp:TextBox ID="txtCompletionProcedure" placeholder="Procedure, permission and activities done" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>

                                                          <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionProcedure" ID="RegularExpressionValidator3" ValidationExpression = "^[\s\S]{100,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 100 characters required."></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label style="color: #F66634;">What has been the most memorable experience of initiative?</label>
                                                    <asp:TextBox ID="txtCompletionInitiativeExperience" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                        placeholder="What has been the most memorable experience of initiative?" runat="server"></asp:TextBox>
                                                          <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionInitiativeExperience" ID="RegularExpressionValidator4" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required."></asp:RegularExpressionValidator>

                                                </div>
                                                <div class="col-md-6">
                                                    <label style="color: #F66634;">What is lacking in your initiative and how you think LEAD can help overcome it?</label>
                                                    <asp:TextBox ID="txtCompletionOvercomeLacking" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                        placeholder="What is lacking in your initiative and how you think LEAD can help overcome it?" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionOvercomeLacking" ID="RegularExpressionValidator5" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required."></asp:RegularExpressionValidator>

                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-md-6">
                                                    <div class="panel panel-warning z-depth-1 hoverable">

                                                        <div class="panel-body">
                                                            <label style="color: #F66634;">
                                                                Did your project required to brave the consequences of going against the tide - wrong norms of society/beliefs/superstitions? &nbsp;
                                                <asp:CheckBox ID="ChkAgainst_Tide" runat="server" /></label>
                                                            <span id="Against_Tide" style="display: none;">
                                                                <asp:TextBox ID="txtCompletionAgainst_Tide" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                                    placeholder="Did your project required to brave the consequences of going against the tide - wrong norms of society/beliefs/superstitions?" runat="server"></asp:TextBox>
                                                                                 <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionAgainst_Tide" ID="RegularExpressionValidator9" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required."></asp:RegularExpressionValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="panel panel-warning z-depth-1 hoverable">

                                                        <div class="panel-body">
                                                            <label style="color: #F66634;">
                                                                Did your project required to cross real hurdles (permissions/resources/resistance from interest groups)? &nbsp;
                                                <asp:CheckBox ID="ChkCross_Hurdles" runat="server" /></label>
                                                            <span id="Cross_Hurdles" style="display: none;">
                                                                <asp:TextBox ID="txtCompletionCross_Hurdles" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                                    placeholder="Did your project required to cross real hurdles (permissions/resources/resistance from interest groups)?" runat="server"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionCross_Hurdles" ID="RegularExpressionValidator10" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required."></asp:RegularExpressionValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-md-6">
                                                    <div class="panel panel-warning z-depth-1 hoverable">

                                                        <div class="panel-body">
                                                            <label style="color: #F66634;">
                                                                Does you project or initiative leads to an entrepreneurial venture? &nbsp;
                                                <asp:CheckBox ID="ChkEntrepreneurial_Venture" runat="server" /></label>
                                                            <span id="Entrepreneurial_Venture" style="display: none;">
                                                                <asp:TextBox ID="txtCompletionEntrepreneurial_Venture" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                                    placeholder="Does you project or initiative leads to an entrepreneurial venture?" runat="server"></asp:TextBox>
                                                                     <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionEntrepreneurial_Venture" ID="RegularExpressionValidator11" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required."></asp:RegularExpressionValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="panel panel-warning z-depth-1 hoverable">

                                                        <div class="panel-body">
                                                            <label style="color: #F66634;">
                                                                Is your project appreciated/awarded by Government bodies? &nbsp;
                                                <asp:CheckBox ID="ChkGovernment_Awarded" runat="server" /></label>
                                                            <span id="Government_Awarded" style="display: none;">
                                                                <asp:TextBox ID="txtCompletionGovernment_Awarded" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                                    placeholder="Is your project appreciated/awarded by Government bodies?" runat="server"></asp:TextBox>
                                                                       <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionGovernment_Awarded" ID="RegularExpressionValidator12" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required."></asp:RegularExpressionValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-md-12">
                                                    <div class="panel panel-warning z-depth-1 hoverable">

                                                        <div class="panel-body">
                                                            <label style="color: #F66634;">
                                                                Is your project helped you to get a formal leadership roles - (In college setup/ any other set up in society/ LEAD) &nbsp;
                                                <asp:CheckBox ID="ChkLeadership_Roles" runat="server" /></label>
                                                            <span id="Leadership_Roles" style="display: none;">
                                                                <asp:TextBox ID="txtCompletionLeadership_Roles" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                                    placeholder="Is your project helped you to get a formal leadership roles - (In college setup/ any other set up in society/ LEAD)" runat="server"></asp:TextBox>
                                                          <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionLeadership_Roles" ID="RegularExpressionValidator13" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required."></asp:RegularExpressionValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <label>
                                        Project Level
                                    </label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ForeColor="Red" ControlToValidate="ddlCompletionStudentLevel" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Completion"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCompletionStudentLevel" Font-Size="Larger" CssClass="form-control form-group-lg bg-primary" runat="server">
                                        <asp:ListItem Text="----Project Level----" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Initiator" Value="Initiator"></asp:ListItem>
                                        <asp:ListItem Text="Change Maker" Value="Change Maker"></asp:ListItem>
                                        <asp:ListItem Text="LEADer" Value="LEADer"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtCompletionHoursSpend">
                                        Hours Spent
                                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" ControlToValidate="txtCompletionHoursSpend" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true"
                                             ValidationGroup="Completion"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCompletionHoursSpend" onkeypress="NumericOnly()" placeholder="Hours Spent" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        SDG goals &nbsp; <a href="https://www.undp.org/content/undp/en/home/sustainable-development-goals.html" target="_blank">
                                            <span class="fa fa-info-circle  text-info"></span></a>
                                    </label>
                                    <br />
                                    <asp:ListBox ID="LstSDG_Goals" CssClass="lstbox form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <label for="txtCompletionManagerComments" class="text-danger">
                                        Manager Comments 
                                         &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtCompletionManagerComments" runat="server" ErrorMessage="* Required" ForeColor="Red" ValidationGroup="MangerCompletion"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCompletionManagerComments" placeholder="Manager Comments" TextMode="MultiLine" Rows="2" CssClass="form-control" BackColor="DarkSalmon" ForeColor="WhiteSmoke" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCompletionManagerComments" ID="RegularExpressionValidator2" ForeColor="Red" SetFocusOnError="true" ValidationExpression="^[\s\S]{50,}$" runat="server" ErrorMessage="* Minimum 50 Characters ." ValidationGroup="Completion"></asp:RegularExpressionValidator>

                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-6">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <table class="table table-bordered">

                                                <tbody>
                                                    <tr>
                                                        <td style="width: 2px;" class="text-center">1
                                                        </td>
                                                        <td>Innovation 
                                                 
                                                        </td>
                                                        <td>

                                                            <asp:Rating ID="RatingInnovation" runat="server" AutoPostBack="true" OnChanged="RatingInnovation_Changed"
                                                                StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                                                                FilledStarCssClass="FilledStar">
                                                            </asp:Rating>
                                                            <asp:Label ID="lblInnovationStars" runat="server" Text=""></asp:Label>
                                                            <span class="pull-right">
                                                                <asp:Label ID="lblInnovationresult" runat="server" Text=""></asp:Label></span>


                                                        </td>
                                                        <td rowspan="5" class="text-center">

                                                            <h3 class="brandFont1 text-info">
                                                                <label>Final Rating </label>
                                                                <br />
                                                                <asp:Label ID="lblFinalRatingStars" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="lblFinalRatingResult" runat="server" Text=""></asp:Label>
                                                            </h3>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2px;" class="text-center">2
                                                        </td>
                                                        <td>Leadership 
                                                   
                                                        </td>
                                                        <td>
                                                            <asp:Rating ID="RatingLeadership" runat="server" AutoPostBack="true" OnChanged="RatingLeadership_Changed"
                                                                StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                                                                FilledStarCssClass="FilledStar">
                                                            </asp:Rating>
                                                            <asp:Label ID="lblLeadershipStarts" runat="server" Text=""></asp:Label>
                                                            <span class="pull-right">
                                                                <asp:Label ID="lblLeadershipResult" runat="server" Text=""></asp:Label></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2px;" class="text-center">3
                                                        </td>
                                                        <td>Risk Taken 
                                                   
                                                        </td>
                                                        <td>

                                                            <asp:Rating ID="RatingRiskTaken" runat="server" AutoPostBack="true" OnChanged="RatingRiskTaken_Changed"
                                                                StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                                                                FilledStarCssClass="FilledStar">
                                                            </asp:Rating>
                                                            <asp:Label ID="lblRiskTakenStars" runat="server" Text=""></asp:Label>
                                                            <span class="pull-right">
                                                                <asp:Label ID="lblRiskTakenResult" runat="server" Text=""></asp:Label></span>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2px;" class="text-center">4
                                                        </td>
                                                        <td>Impact
                                                        </td>
                                                        <td>
                                                            <asp:Rating ID="RatingImpact" runat="server" AutoPostBack="true" OnChanged="RatingImpact_Changed"
                                                                StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                                                                FilledStarCssClass="FilledStar">
                                                            </asp:Rating>
                                                            <asp:Label ID="lblImpactStars" runat="server" Text=""></asp:Label>
                                                            <span class="pull-right">
                                                                <asp:Label ID="lblImpactResult" runat="server" Text=""></asp:Label>
                                                            </span>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2px;" class="text-center">5
                                                        </td>
                                                        <td>Fund raised
                                                        </td>
                                                        <td>
                                                            <asp:Rating ID="RatingFundraised" runat="server" AutoPostBack="true" OnChanged="RatingFundraised_Changed"
                                                                StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                                                                FilledStarCssClass="FilledStar">
                                                            </asp:Rating>

                                                            <asp:Label ID="lblFundRaisedStars" runat="server" Text=""></asp:Label>
                                                            <span class="pull-right">
                                                                <asp:Label ID="lblFundraisedResult" runat="server" Text=""></asp:Label>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblFinalRatingCountLable" runat="server" Text="Total"></asp:Label></td>
                                                        <td colspan="1" class="pull-right bg_ss">
                                                            <asp:Label ID="lblFinalRatingOverAllCount" ForeColor="WhiteSmoke" runat="server" Text="0"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                </tfoot>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-md-6" style="height: 200px; overflow: auto;">
                                    <h4>Images</h4>

                                    <%--                          OnItemCommand="DstStudentImgDocumentList_ItemCommand" OnItemDataBound="DstStudentImgDocumentList_ItemDataBound" --%>
                                    <asp:DataList ID="DstStudentImgDocumentList" runat="server" RepeatDirection="Horizontal" RepeatColumns="10" HorizontalAlign="Left">
                                        <ItemTemplate>

                                            <%--                                                           <asp:LinkButton ID="btnDeleteImage" runat="server" data-toggle="tooltip" title="Delete Image" ><span  class="fa fa-trash text-danger"></span></asp:LinkButton>--%>

                                            <span style="display: none;">
                                                <asp:Label ID="lblImageSlno" runat="server" Text='<%# Eval("slno") %>'></asp:Label>
                                            </span>
                                            <asp:Image ID="Image1" Height="100px" Width="200px" ImageUrl='<%# Eval("Document_Path") %>' onclick="openModal();currentSlide(1)" runat="server" />
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-5" style="height: 200px; overflow: auto;">
                                    <h4>Documents</h4>
                                    <asp:DataList ID="DstStudentDOCLst" runat="server" RepeatDirection="Vertical">
                                        <ItemTemplate>
                                            <a href='<%# Eval("Document_Path")%>' runat="server" target="_blank">'<%# Eval("Document_Path") %>'</a>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-1">
            <div class="bg_ss z-depth-2 hidden-xs" style="position: fixed; top: 0; right: 0; height: 100%; width: 50px;">
                <h3 class="fa-rotate-90 text-muted text-nowrap brandFont" style="letter-spacing: 10px; color: whitesmoke;">&nbsp;&nbsp;&nbsp;
                 <span class="text-warning">Project</span> Completion  
                </h3>
            </div>
        </div>
    </div>

    <div id="myModal" class="modal" role="dialog" style="width: auto; max-width: 90%; margin-top: 100px;">
        <div class="modal-dialog">
            <span class="close cursor" onkeydown="closeModal()" onclick="closeModal()">&times;</span>
            <asp:DataList ID="rptDocument2" runat="server" RepeatColumns="4">
                <ItemTemplate>
                    <div class="mySlides">
                        <asp:Image ID="Image2" Style="width: 800px; height: 500px;" ImageUrl='<%# Eval("Document_Path") %>' runat="server" />
                        <%-- ImageUrl='<%#"~/Handlers/GetInwardDocuments.ashx?ID=" + Eval("SLNO")+ "&code=" + Eval("code")+ "&year=" + Eval("academicyear")%>'--%>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <a class="prev" onclick="plusSlides(-1)">&#10094;</a> <a class="next" onclick="plusSlides(1)">&#10095;</a>
        </div>
    </div>
    <div class="modal fade" id="POP_Confirmation" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">All Mandatory Fileds are Checked ? </h5>
                </div>
                <div class="modal-body">
                    <div style="height: 220px; overflow: auto;">
                        <h3>Role and Responsibilities</h3>
                        <p>
                            1.  Check the Project  <span class="text-danger">Theme</span>
                        </p>
                        <p>
                            2. Check <span class="text-danger">SDG Goals</span>   for Impact Area Indicator.
                        </p>
                        <p>
                            3.  Please check  All <span class="text-danger">Impact Questions</span> if it is Impact Project
                        </p>

                        <p>
                            4. Select <span class="text-danger">Project Level</span>   Compulsory.
                        </p>
                        <p>
                            5. Enter valuable <span class="text-danger">comments</span>   for projects.
                        </p>
                        <p>
                            6. Give Proper <span class="text-danger">Rating</span>  for Project.
                        </p>
                        <br />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnNotCheck" CssClass="btn btn-danger" runat="server">
                             Not Checked &nbsp; <span class="fa fa-thumbs-down"></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnYesCheck" CssClass="btn btn-success" runat="server" OnClick="btnYesCheck_Click">
                             Yes Checked &nbsp; <span class="fa fa-thumbs-up"></span>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
       <div class="modal fade" id="POP_Reject" tabindex="-1" role="dialog" data-toggle="modal" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Are you want to Reject The Project ? </h5>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-6 col-lg-6 col-xs-12">
                                <label>
                                    Enter Reject Comments

  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtCompletionRejectComments" ValidationGroup="Reject" ErrorMessage="* Required" ForeColor="Red" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCompletionRejectComments" ID="RegularExpressionValidator14" ForeColor="Red" SetFocusOnError="true" ValidationExpression="^[\s\S]{16,}$" runat="server" ErrorMessage="* Minimum 16 Characters ." ValidationGroup="Reject"></asp:RegularExpressionValidator>
                                </label>
                                <asp:TextBox ID="txtCompletionRejectComments" TextMode="MultiLine" Rows="2" CssClass="form-control" placeholder="Comments" ValidationGroup="Reject" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
             <button type="button" class="btn btn-success" data-dismiss="modal">   No &nbsp; <span class="fa fa-thumbs-down"></span></button>
                    <asp:LinkButton ID="btnReject" CssClass="btn btn-danger" ValidationGroup="Reject" runat="server" OnClick="btnReject_Click">
                             Yes Reject &nbsp; <span class="fa fa-thumbs-up"></span>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
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
            dots[slideIndex - 1].className += "active";
            captionText.innerHTML = dots[slideIndex - 1].alt;
        }
    </script>
    <script>
        function CalculateTargetDays1() {

            const date1 = new Date(document.getElementById('<%= txtRCStartDate.ClientID %>').value);
            const date2 = new Date(document.getElementById('<%= txtRCEndDate.ClientID %>').value);
            const diffTime = Math.abs(date2.getTime() - date1.getTime());
            const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
            if (isNaN(diffDays)) {
                document.getElementById('<%= lblRCTargetDays.ClientID %>').textContent = 0;
            }
            else {
                document.getElementById('<%= lblRCTargetDays.ClientID %>').textContent = diffDays;
            }
        }
    </script>
    <script type="text/javascript">
        function POP_Confirmation() {
            $('#POP_Confirmation').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
        }
    </script>
     <script type="text/javascript">
         function POP_Reject() {
             $('#POP_Reject').modal({
                 backdrop: 'static',
                 keyboard: false,
                 show: true
             });
         }
     </script>
    
</asp:Content>
