<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="Student_ProjectCompletion.aspx.cs" Inherits="Pages_Student_Student_ProjectCompletion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
    <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
 
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
            toastr.options.timeOut = 4500; //1.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.success(msg);
        }
        function warning(msg) {
            toastr.options.timeOut = 4500; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.warning(msg);
        }
        function info(msg) {
            toastr.options.timeOut = 4500; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.info(msg);
        }
        function error(msg) {
            toastr.options.timeOut = 4500; //2.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.error(msg);
        }
    </script>
    <style>
        .row > .column {
            padding: 0 8px;
        }
        .row:after {
            content: "";
            display: table;
            clear: both;
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
               .lstbox{

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
            margin-top: -125px;
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

        .Shake_img {
            /* Start the shake animation and make the animation last for 0.5 seconds */
            animation: shake 0.5s;
            /* When the animation is finished, start again */
            animation-iteration-count: infinite;
        }

            .Shake_img:hover {
                /* Start the shake animation and make the animation last for 0.5 seconds */
                animation: shake 20s backwards;
                /* When the animation is finished, start again */
                animation-iteration-count: 1;
            }

        @keyframes shake {

            0% {
                transform: scale(1,1);
            }

            50% {
                transform: scale(1.1,1.1);
            }

            100% {
                transform: scale(1,1);
            }
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
                  toastr.info("Fill Impact Questions");
                  toastr.options.positionClass = "toast-top-right";
                  $(<%= ChkCollabration.ClientID %>).click(function () {
                      if ($(this).is(":checked")) {
                          $("#Collabration").show();
                          $("label[for=<%= ChkCollabration.ClientID %>]").text("No");
                      }
                      else {
                          $("#Collabration").hide();
                          $("label[for=<%= ChkCollabration.ClientID %>]").text("Yes");
                      }
                  });
                  if ($(<%= ChkCollabration.ClientID %>).is(":checked")) {
                      $("#Collabration").show();
                  }


                  $(<%= ChkAgainst_Tide.ClientID %>).click(function () {
                      if ($(this).is(":checked")) {
                          $("#Against_Tide").show();
                          $("label[for=<%= ChkAgainst_Tide.ClientID %>]").text("No");
                      }
                      else {
                          $("#Against_Tide").hide();
                          $("label[for=<%= ChkAgainst_Tide.ClientID %>]").text("Yes");
                      }
                  });
                  if ($(<%= ChkAgainst_Tide.ClientID %>).is(":checked")) {
                      $("#Against_Tide").show();
                  }
                  $(<%= ChkCross_Hurdles.ClientID %>).click(function () {
                      if ($(this).is(":checked")) {
                          $("#Cross_Hurdles").show();
                          $("label[for=<%= ChkCross_Hurdles.ClientID %>]").text("No");
                      }
                      else {
                          $("#Cross_Hurdles").hide();
                          $("label[for=<%= ChkCross_Hurdles.ClientID %>]").text("Yes");
                      }
                  });
                  if ($(<%= ChkCross_Hurdles.ClientID %>).is(":checked")) {
                      $("#Cross_Hurdles").show();
                  }
                  $(<%= ChkEntrepreneurial_Venture.ClientID %>).click(function () {
                      if ($(this).is(":checked")) {
                          $("#Entrepreneurial_Venture").show();
                          $("label[for=<%= ChkEntrepreneurial_Venture.ClientID %>]").text("No");
                      }
                      else {
                          $("#Entrepreneurial_Venture").hide();
                          $("label[for=<%= ChkEntrepreneurial_Venture.ClientID %>]").text("Yes");
                      }
                  });
                  if ($(<%= ChkEntrepreneurial_Venture.ClientID %>).is(":checked")) {
                      $("#Entrepreneurial_Venture").show();
                  }
                  $(<%= ChkGovernment_Awarded.ClientID %>).click(function () {
                      if ($(this).is(":checked")) {
                          $("#Government_Awarded").show();
                          $("label[for=<%= ChkGovernment_Awarded.ClientID %>]").text("No");
                      }
                      else {
                          $("#Government_Awarded").hide();
                          $("label[for=<%= ChkGovernment_Awarded.ClientID %>]").text("Yes");
                      }
                  });
                  if ($(<%= ChkGovernment_Awarded.ClientID %>).is(":checked")) {
                      $("#Government_Awarded").show();
                  }
                  $(<%= ChkLeadership_Roles.ClientID %>).click(function () {
                      if ($(this).is(":checked")) {

                          $("#Leadership_Roles").show();
                          $("label[for=<%= ChkLeadership_Roles.ClientID %>]").text("No");
                      }
                      else {
                          $("#Leadership_Roles").hide();
                          $("label[for=<%= ChkLeadership_Roles.ClientID %>]").text("Yes");
                      }
                  });
                  if ($(<%= ChkLeadership_Roles.ClientID %>).is(":checked")) {
                      $("#Leadership_Roles").show();
                  }
              }
         
          });
    
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
           <div class="col-md-1 " >
            <div class="row fixed-leftbutton" id="fixed">
                <div class="col-md-12 col-xs-3 pull-right form-group">
                      <asp:LinkButton ID="btnCompletionSaveAsDraft" OnClick="btnCompletionSaveAsDraft_Click" CssClass="btn btn-warning" runat="server"><span class="fa fa-file"></span>
                          <br />
                          Draft&nbsp;</asp:LinkButton>
               
                </div>
                <div class="col-md-12 col-xs-3 pull-right form-group">
                   <asp:LinkButton ID="btnCompletionSubmit" ValidationGroup="Completion" OnClick="btnCompletionSubmit_Click" CssClass="btn btn-info" runat="server"><span class="fa fa-check"></span> 
                       <br /> Submit </asp:LinkButton>
               
                </div>
                 <div class="col-md-12 col-xs-3 pull-right">
                    <a href="StudentProfile.aspx" class="btn  text-center">
                        <span class="fa fa-times"></span>
                        <br />
                        Close&nbsp;&nbsp;
                    </a>
                    <span style="display:none;">
                        <asp:Label ID="lblPDID" runat="server" Text=""></asp:Label>
                       <asp:Label ID="lblProjectStatus" runat="server" Text=""></asp:Label>
                         <asp:Label ID="lblLead_Id" runat="server" Text=""></asp:Label>
                    </span>
                </div>
                <div class="col-md-12 col-xs-3 pull-right">
                   <asp:LinkButton ID="btnDownloadStudentDocument" OnClick="btnDownloadStudentDocument_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-download fa-2x"></span>
                       <br />
                      
                   </asp:LinkButton>
                </div>                      
            </div>
        </div>

        <div class="col-md-10">
            <div class="panel panel-default">
              
                <div class="panel-body">
                       <div class="row ">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-6 form-group">
                                    <label for="txtCompletionProjectTitle">Project Title </label>
                                    <asp:TextBox ID="txtCompletionProjectTitle" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 form-group">
                                    <label for="txtCompletionProjectObjective">Project Objectives</label>
                                    <asp:TextBox ID="txtCompletionProjectObjective"  Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 form-group">
                                    <label for="txtCompletionPlaceofImplement">Place of Implementation &nbsp; 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtCompletionPlaceofImplement" ValidationGroup="Completion" ErrorMessage="* required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator></label>
                                    <asp:TextBox ID="txtCompletionPlaceofImplement" placeholder="Place of Implementation" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-6 form-group">
                                    <label for="txtCompletionBeneficiary">Total No. of Beneficiaries</label>
                                    <asp:TextBox ID="txtCompletionBeneficiary" placeholder="Total No Beneficiaries" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <label for="txtCompletionRequestedAmount">Requested Amount</label>
                                    <asp:TextBox ID="txtCompletionRequestedAmount" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-3 form-group">
                                    <label for="txtCompletionApprovedAmount">Approved Amount</label>
                                    <asp:TextBox ID="txtCompletionApprovedAmount" Enabled="false" CssClass="form-control disabled" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-3 form-group">
                                    <label for="txtCompletionFundRaised">Fund Raised</label>
                                    <asp:TextBox ID="txtCompletionFundRaised" placeholder="Fund Raised" onkeypress="NumericOnly()" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                    <span class="hidden">
                                        <asp:Label ID="lblDeleteImageCount" runat="server" Text=""></asp:Label>
                                         <asp:Label ID="lblDeleteImgSlno" runat="server" Text=""></asp:Label>
                                    </span>
                                </div>
                                  <div class="col-md-3 form-group">
                                    <label for="txtCompletionHoursSpend">Hours Spent
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator34" ControlToValidate="txtCompletionHoursSpend" ValidationGroup="Completion" ErrorMessage="* Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCompletionHoursSpend" placeholder="Hours Spent" onkeypress="NumericOnly()" CssClass="form-control" runat="server" ></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 form-group">
                                    <label for="txtCompletionChanllengesFaced">Challenges faced during project &nbsp; 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtCompletionChanllengesFaced" ValidationGroup="Completion" ErrorMessage="* Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                                       
                                    </label>
                                    <asp:TextBox ID="txtCompletionChanllengesFaced" placeholder="Challenges faced during project" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                     <asp:RegularExpressionValidator Display = "Dynamic" SetFocusOnError="true" ControlToValidate = "txtCompletionChanllengesFaced" ID="RegularExpressionValidator7" ForeColor="Red" ValidationExpression = "^[\s\S]{10,}$" runat="server" ErrorMessage="* Minimum 10 Characters ." ValidationGroup="Completion"></asp:RegularExpressionValidator>
                                </div>
                                  <div class="col-md-6 form-group">
                                    <label for="">
                                        Major personal learnings from this project &nbsp;
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtCompletionLearningFromProject" ValidationGroup="Completion" ErrorMessage="* Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCompletionLearningFromProject" placeholder="learning from this project" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                     <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtCompletionLearningFromProject" ID="RegularExpressionValidator8" ForeColor="Red" SetFocusOnError="true" ValidationExpression = "^[\s\S]{20,}$" runat="server" ErrorMessage="* Minimum 20 Characters ." ValidationGroup="Completion"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                      
                            <div class="row">
                                <div class="col-md-6 form-group">
                                    <label for="">
                                        Your project as a story &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtCompletionProjectStory" ValidationGroup="Completion" ErrorMessage="* Required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                                        
                                    </label>
                                    <asp:TextBox ID="txtCompletionProjectStory"  placeholder="Enter Your Project as a Story" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                     <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtCompletionProjectStory" ID="RegularExpressionValidator6" ForeColor="Red" SetFocusOnError="true" ValidationExpression = "^[\s\S]{100,}$" runat="server" ErrorMessage="* Minimum 100 Characters ." ValidationGroup="Completion"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-4 form-group">
                                    <label for="txtResourceUtilize">Resources Utilize</label>
                                    <asp:TextBox ID="txtResourceUtilize" placeholder="eg: Paper Printing(40Rs),Internet(20Rs)" TextMode="MultiLine" Rows="1" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtResourceUtilize" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Completion"></asp:RequiredFieldValidator>
                                </div>
                                     <div class="col-md-2 form-group">
                                    <label for="txtResourceUtilize">Resources Worth Amount</label>
                                    <asp:TextBox ID="txtResourceWorthAmount" placeholder="Total Amount" CssClass="form-control" runat="server" onkeypress="NumericOnly()" Text="0"></asp:TextBox>
                                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="txtResourceWorthAmount" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Completion"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                              <div class="row">
                                <div class="col-md-2 form-group">
                                    <label for="txtProposedStartDate">Start Date</label>
                                    <asp:TextBox ID="txtRCStartDate" placeholder="yyyy-mm-dd" autocomplete="off" CssClass="form-control datepicker" runat="server" onchange="CalculateTargetDays1();"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator32" ForeColor="Red" ControlToValidate="txtRCStartDate" Display="Dynamic" ErrorMessage="* Required"  SetFocusOnError="true" ValidationGroup="Completion">
                                    </asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" Display="Dynamic"   ErrorMessage="* Choose Proper Date"
                                ControlToValidate="txtRCStartDate" ControlToCompare="txtRCEndDate" Type="date" Operator="LessThanEqual" ForeColor="Red"  SetFocusOnError="true" ValidationGroup="Completion">
                         </asp:CompareValidator>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label for="txtProposedEndDate">End Date</label>

                                    <asp:TextBox ID="txtRCEndDate" placeholder="yyyy-mm-dd" autocomplete="off" CssClass="form-control datepicker" runat="server" onchange="CalculateTargetDays1();"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator33" ForeColor="Red" ControlToValidate="txtRCEndDate" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Completion"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" Display="Dynamic"   ErrorMessage="* Choose Proper Date" 
                                ControlToValidate="txtRCEndDate" ControlToCompare="txtRCStartDate" Type="date" Operator="GreaterThanEqual" ForeColor="Red"  SetFocusOnError="true" ValidationGroup="Completion">

                         </asp:CompareValidator>
                                </div>
                                <div class="col-md-2">
                                    <label>Target Days</label>
                                    <br />
                                    <asp:Label ID="lblRCTargetDays" CssClass="text-center" runat="server" Text="0"></asp:Label>
                                </div>

                                  <div class="col-md-6">
                                      <label>SDG goals &nbsp; <a href="https://www.undp.org/content/undp/en/home/sustainable-development-goals.html" target="_blank">

                                        <span class="fa fa-info-circle  text-info"></span> </a> </label>
                                      <br />
                                       <asp:ListBox ID="LstSDG_Goals" CssClass="lstbox  form-control" runat="server" SelectionMode="Multiple">                                       
                                      </asp:ListBox>
                                  </div>
                            </div>
                        </div>
                  
                     
                  
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Panel ID="pnl_ImpactQuestions" runat="server">
                                <div class="pnl_impactdiv">
                                <div class="row form-group">
                                    <div class="col-md-6">
                                        <div class="panel panel-danger z-depth-1 hoverable">
                                            <div class="panel-body">
                                                <label for="txtCompletionCollabration" style="color:#F66634;">
                                                    Any collabaration if you have made for the project ? &nbsp;
                                                    <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionCollabration" ID="RegularExpressionValidator2" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required."></asp:RegularExpressionValidator>
                                                <asp:CheckBox ID="ChkCollabration" Text="Yes" runat="server" />


                                                </label>


                                                <span id="Collabration" style="display: none;">

                                                    <asp:TextBox ID="txtCompletionCollabration" placeholder="Any collabaration if you have made for the project ?" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="txtCompletionProcedure" style="color:#F66634;">Explain in 100 words the project details. (Procedure, permission and activities done)
                                                 <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionProcedure" ID="RegularExpressionValidator12" ValidationExpression = "^[\s\S]{100,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 100 characters required."></asp:RegularExpressionValidator>
                                        </label>
                                        <asp:TextBox ID="txtCompletionProcedure" placeholder="Procedure, permission and activities done" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label style="color:#F66634;">What has been the most memorable experience of initiative?
                                                <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionInitiativeExperience" ID="RegularExpressionValidator1" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required.">

                                                </asp:RegularExpressionValidator>

                                        </label>
                                        <asp:TextBox ID="txtCompletionInitiativeExperience" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                            placeholder="What has been the most memorable experience of initiative?" runat="server"></asp:TextBox>

                                    </div>
                                    <div class="col-md-6">
                                        <label style="color:#F66634;">What is lacking in your initiative and how you think LEAD can help overcome it?
                                                   <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionOvercomeLacking" ID="RegularExpressionValidator3" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required.">

                                                </asp:RegularExpressionValidator>
                                        </label>
                                        <asp:TextBox ID="txtCompletionOvercomeLacking" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                            placeholder="What is lacking in your initiative and how you think LEAD can help overcome it?" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="row form-group">
                                    <div class="col-md-6">
                                        <div class="panel panel-danger z-depth-1 hoverable">
                                            <div class="panel-body">
                                                <label style="color:#F66634;">
                                                    Did your project required to brave the consequences of going against the tide - wrong norms of society/beliefs/superstitions? &nbsp;
                                                      <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionAgainst_Tide" ID="RegularExpressionValidator4" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required.">

                                                </asp:RegularExpressionValidator>
                                                <asp:CheckBox ID="ChkAgainst_Tide" Text="Yes" runat="server" /></label>
                                                <span id="Against_Tide" style="display: none;">
                                                    <asp:TextBox ID="txtCompletionAgainst_Tide" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                        placeholder="Did your project required to brave the consequences of going against the tide - wrong norms of society/beliefs/superstitions?" runat="server"></asp:TextBox>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="panel panel-danger z-depth-1 hoverable">
                                            <div class="panel-body">
                                                <label style="color:#F66634;">
                                                    Did your project required to cross real hurdles (permissions/resources/resistance from interest groups)? &nbsp;
                                                        <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionCross_Hurdles" ID="RegularExpressionValidator5" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required.">

                                                </asp:RegularExpressionValidator>
                                                <asp:CheckBox ID="ChkCross_Hurdles" Text="Yes" runat="server" /></label>
                                                <span id="Cross_Hurdles" style="display: none;">
                                                    <asp:TextBox ID="txtCompletionCross_Hurdles" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                        placeholder="Did your project required to cross real hurdles (permissions/resources/resistance from interest groups)?" runat="server"></asp:TextBox>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <div class="col-md-6">
                                         <div class="panel panel-danger z-depth-1 hoverable">
                                            <div class="panel-body">
                                        <label style="color:#F66634;">
                                            Does you project or initiative leads to an entrepreneurial venture? &nbsp;
                                                <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionEntrepreneurial_Venture" ID="RegularExpressionValidator9" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required.">

                                                </asp:RegularExpressionValidator>
                                                <asp:CheckBox ID="ChkEntrepreneurial_Venture" Text="Yes" runat="server" /></label>
                                        <span id="Entrepreneurial_Venture" style="display: none;">
                                            <asp:TextBox ID="txtCompletionEntrepreneurial_Venture" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                placeholder="Does you project or initiative leads to an entrepreneurial venture?" runat="server"></asp:TextBox>
                                        </span>
                                                   </div>
                                                </div>
                                    </div>
                                    <div class="col-md-6">
                                         <div class="panel panel-danger z-depth-1 hoverable">
                                            <div class="panel-body">
                                        <label style="color:#F66634;">
                                            Is your project appreciated/awarded by Government bodies? &nbsp;
                                                  <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionGovernment_Awarded" ID="RegularExpressionValidator10" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required.">

                                                </asp:RegularExpressionValidator>
                                                <asp:CheckBox ID="ChkGovernment_Awarded" Text="Yes" runat="server" /></label>
                                        <span id="Government_Awarded" style="display: none;">
                                            <asp:TextBox ID="txtCompletionGovernment_Awarded" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                placeholder="Is your project appreciated/awarded by Government bodies?" runat="server"></asp:TextBox>
                                        </span>
                                                   </div>
                                                </div>
                                    </div>
                                </div>
                                    <div class="row form-group">
                                        <div class="col-md-12">
                                            <div class="panel panel-danger z-depth-1 hoverable">
                                                <div class="panel-body">
                                                    <label style="color:#F66634;">
                                                        Is your project helped you to get a formal leadership roles - (In college setup/ any other set up in society/ LEAD) &nbsp;
                                                            <asp:RegularExpressionValidator Display = "Dynamic" 
                                                        ControlToValidate = "txtCompletionLeadership_Roles" ID="RegularExpressionValidator11" ValidationExpression = "^[\s\S]{20,}$" runat="server" ForeColor="Red" ErrorMessage="Minimum 20 characters required.">

                                                </asp:RegularExpressionValidator>
                                                <asp:CheckBox ID="ChkLeadership_Roles" Text="Yes" runat="server" /></label>
                                                    <span id="Leadership_Roles" style="display: none;">
                                                        <asp:TextBox ID="txtCompletionLeadership_Roles" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                            placeholder="Is your project helped you to get a formal leadership roles - (In college setup/ any other set up in society/ LEAD)" runat="server"></asp:TextBox>
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
                              <div class="col-lg-4" >
                            <div class="row" style="overflow: auto; height: 300px;">
                                <div class="col-lg-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="card-content text-center" style="overflow: auto; height: 300PX;">
                                                <label style="color: darkred">Add minimum 4 images at a time</label>
                                                <div class="form-group clearfix">
                                                    <div class="col-lg-12">
                                                        <asp:FileUpload ID="FileUpload1" CssClass="sha" AllowMultiple="true" multiple="multiple" runat="server" />

                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <label for="Document">Upload Documents (.doc/.pdf/.docx/.xls/.xlsx)</label>
                                            <asp:RegularExpressionValidator ID="Document" runat="server"
                                                ControlToValidate="DocumentsFileUploader" Display="Dynamic"
                                                ErrorMessage="Only word file  or pdf  are allowed"
                                                ValidationExpression="^.+(.doc|.docx|.DOC|.DOCX|.pdf|.PDF|.xls|.xlsx)$"
                                                Font-Bold="True" Font-Italic="True" ForeColor="#CC3300" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                            <asp:FileUpload ID="DocumentsFileUploader" AllowMultiple="true" runat="server"></asp:FileUpload>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <br />
                          
                        </div>

                             <div class="col-md-8" style="max-height: 250px; overflow: auto;">
                            <h4>Images</h4>
                                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                     <ContentTemplate>
                                         <asp:DataList ID="DstStudentImgDocumentList" runat="server" RepeatDirection="Horizontal"
                                             OnItemCommand="DstStudentImgDocumentList_ItemCommand" OnItemDataBound="DstStudentImgDocumentList_ItemDataBound"
                                             RepeatColumns="10" HorizontalAlign="Left">
                                             <ItemTemplate>
                                                 <div class="card">
                                                     <div class="card-panel">
                                                         <asp:Panel runat="server" ID="pnl">
                                                             <asp:LinkButton ID="btnDeleteImage" CssClass="text-danger" runat="server" data-toggle="tooltip" title="Delete Image">
                                          <span class="fa fa-trash"></span></asp:LinkButton>
                                                             <span style="display: none;">
                                                                 <asp:Label ID="lblImageSlno" runat="server" Text='<%# Eval("slno") %>'></asp:Label>
                                                             </span>
                                                             <asp:Image ID="Image1" Height="100px" Width="100px" ImageUrl='<%# Eval("Document_Path") %>' onclick="openModal();currentSlide(1)" runat="server" />
                                                         </asp:Panel>
                                                     </div>
                                                 </div>
                                             </ItemTemplate>
                                         </asp:DataList>
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                             </div>
                        <div class="col-md-8" style="height: 250px; overflow: auto;">
                            <h4><br />Documents</h4>
                            <asp:DataList ID="DstStudentDOCLst" runat="server" RepeatDirection="Vertical">
                                <ItemTemplate>
                                    <a href='<%# Eval("Document_Path") %>' target="_blank" runat="server">'<%# Eval("Document_Path") %>'</a>

                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>

                    <div class="row">
                   <div class="col-md-4"></div>
                        
                    </div>
                </div>
            </div>
        </div>

          <div class="col-md-1">
            <div class="bg-success z-depth-2 hidden-xs" style="position: fixed; top: 0; right: 0; height: 100%; width: 50px">
                <h3 class="fa-rotate-90 text-muted text-nowrap" style="letter-spacing: 10px; margin-top: 50px">&nbsp;&nbsp;&nbsp;
                 <span class="text-primary"> Completion</span>   Project <span class="text-primary"> Submission</span>
                </h3>
            </div>
       </div>
    </div>


     <div id="myModal" class="modal" role="dialog" style="width: auto; max-width: 90%; margin-top: 100px;">
        <div class="modal-dialog">
            <span class="close cursor" onclick="closeModal()">&times;</span>
            <asp:DataList ID="rptDocument2" runat="server" RepeatColumns="4">
                <ItemTemplate>
                    <div class="mySlides">
                        <asp:Image ID="Image2" Style="width: 800px; height: 500px;" CssClass=" text-center" ImageUrl='<%# Eval("Document_Path") %>' runat="server" />
                        <%-- ImageUrl='<%#"~/Handlers/GetInwardDocuments.ashx?ID=" + Eval("SLNO")+ "&code=" + Eval("code")+ "&year=" + Eval("academicyear")%>'--%>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <a class="prev" onclick="plusSlides(-1)">&#10094;</a> <a class="next" onclick="plusSlides(1)">&#10095;</a>
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
                dots[i].className = dots[i].className.replace("active", "");
            }
            slides[slideIndex - 1].style.display = "block";
            //dots[slideIndex - 1].className += " active";
            //captionText.innerHTML = dots[slideIndex - 1].alt;
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
            else if (diffDays == 0) {
                 document.getElementById('<%= lblRCTargetDays.ClientID %>').textContent = 1;
            }
            else {
                document.getElementById('<%= lblRCTargetDays.ClientID %>').textContent = diffDays+1;
            }
        }


    </script>
   

     
      <script src="../../JS/CommonJS/jquery.filer.min.js"></script>
    <script src="../../JS/CommonJS/jquery.fileuploads.init.js"></script>
</asp:Content>

