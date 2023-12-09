<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="StudentProfile.aspx.cs" Inherits="Pages_Student_StudentProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
    <link href="../../CSS/StudentCSS/jquery-tagsinput.min.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/chat.css" rel="stylesheet" />
    <script src="../../JS/StudentJS/jquery-tagsinput.min.js"></script>
    <link href="../../CSS/CommonCSS/MultiSelect.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/MultiSelect.js"></script>
    <style>
        .button-badge {
            background-color: #1779ba;
            text-decoration: none;
            padding: 1rem 1.5rem;
            position: relative;
            display: inline-block;
            border-radius: .2rem;
            transition: all ease 0.4s;
        }

            .button-badge:hover {
                box-shadow: 0 1px 5px rgba(0, 0, 0, 0.1);
            }

        .badge {
            position: absolute;
            top: -10px;
            right: -10px;
            font-size: .8em;
        }
 
    </style>
  <%--  <script>
        function resize() {
            if ($(window).width() < 514) {
                $("#tabs").removeClass("tabs").addClass("tabs-container tabs-vertical");
            }
            else { $("#tabs").removeClass("tabs-container tabs-vertical").addClass("tabs"); }
        }
        $(document).ready(function () {
            $(window).resize(resize);
            resize();
        });
    </script>--%>
    <style>
        .tab-con {
            float: left;
            padding: 0px 12px;
            border: 1px solid #ccc;
            width: 70%;
            border-left: none;
            height: 300px;
        }
    </style>
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
        .progress-bar {
            width: 0;
            animation: progress 1.5s ease-in-out forwards;
            margin-left:50px;
        }

        .title {
            opacity: 0;
            animation: show 0.35s forwards ease-in-out 0.5s;
        }


        @keyframes progress {
            from {
                width: 0;
            }

            to {
                width: 100%;
            }
        }

        @keyframes show {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }

        .img-thumbnail {
            background-color: white;
        }

        .progress {
            height: 10px;
        }

        .panel-body {
            padding: 6px;
        }

        .sha {
        }
    </style>

    <script type="text/javascript">
        function PostToNewWindow() {
            originalTarget = document.forms[0].target;
            document.forms[0].target = '_blank';
            window.setTimeout("document.forms[0].target=originalTarget;", 300);
            return true;


        }
    </script>
    <style>
        .panel {
            background-color: #f5f5f5;
        }

        .top-right {
            position: absolute;
            top: -10px;
            right: 40px;
        }

        .top-right-count {
            position: absolute;
            top: 12px;
            right: 37px;
        }

        @media (max-width: 767px) {
            .nav-tabs > li {
                float: none;
            }
        }
    </style>
    <style>
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

        #StudentProjectList th {
            background-color: #3367d6;
            color: white;
        }

        .chat {
            height: 300px;
            overflow-y: scroll;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('[data-role="tags-input"]').tagsInput();
        });
    </script>
    <script>
        $(function () {

            $('.chat').animate({
                scrollTop: $('.chat')[0].scrollHeight
            }, "fast");

        });

    </script>
    <style>
        .tab-content {
            padding: 0px 12px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
     
    <asp:Label ID="ll" runat="server" Text=""></asp:Label>
    <div class="row">
        <div class="col-md-3 hidden-xs " style="background-color: #fff;">
            <div class="panel " style="margin-top: 10px;">

                <div class="panel-body">
                    <div class="text-center">
                        <asp:Image ID="PreviewImage" runat="server" CssClass="center-block img-rounded" Style="width: 100px; height: 100px;"
                            EnableTheming="True" />
                        <div class="top-right">
                            <asp:Image ID="ImgBatch" ImageUrl="~/CSS/Images/Batch.png" Visible="false" runat="server" />

                            <div class="top-right-count">
                                <asp:Label ID="lblFiveStartsCount" Visible="false" Font-Bold="true" runat="server" Font-Size="Medium" ForeColor="#143D59" CssClass="text-info brandFont"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-10">
                            <asp:FileUpload ID="ProfilePic" runat="server" onchange="Profile()" CssClass="btn btn-primary form-control" />
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="btnSaveProfileImage" OnClick="btnSaveProfileImage_Click" CssClass="btn btn-success btn-rounded btn-sm" runat="server"><span class="fa fa-upload"></span> </asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <h4 class="text-center">
                        <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label></h4>
                    <p class="text-center">
                        <asp:Label ID="lblCollegeName" runat="server" Text=""></asp:Label>
                    </p>
                    <h4 class="text-center">
                        <span class="pull-right">
                            <asp:ImageButton ID="btnTshirt" CssClass="Shake_img" OnClick="btnTshirt_Click" Height="30px" Width="30px" ImageUrl="~/CSS/Images/TshirtFront.png" runat="server" />
                        </span>
                        <asp:Label ID="lblLeadId" runat="server" Text=""></asp:Label>

                    </h4>


                    <table class="table table-hover">
                        <tbody>
                            <tr>
                                <td>Status</td>
                                <td><span class="label label-success">
                                    <asp:Label ID="lblStudentType" runat="server" Text=""></asp:Label></span></td>
                            </tr>

                            <tr style="display: none;">
                                <td>Member Since</td>
                                <td>
                                    <asp:Label ID="lblRegistrationDate" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>



                </div>
            </div>
            <!-- panel widget -->

            <div class="panel">
                <div class="panel-body">
                    <h3 class="text-center"><i class="fa fa-user-secret"></i><span style="letter-spacing: 3px;">Your Mentor</span>
                        <br />
                        <br />
                        <asp:Image ID="imgManagerDetailInStudentProfile" alt="Pic" Width="50px" Height="50px" CssClass="img-circle img-thumbnail img-rounded" runat="server" />

                    </h3>
                    <div class="column-xs-2 column-md-3">
                        <div>
                            <h5 class="text-center">
                                <asp:Label ID="lblManagerName" runat="server" Text=""></asp:Label></h5>
                            <h5 class="text-center">
                                <asp:Label ID="lblManagerEmailId" runat="server" Text=""></asp:Label></h5>
                            <h5 class="text-center">
                                <asp:Label ID="lblManagerMobileNo" runat="server" Text=""></asp:Label></h5>

                            <br />
                            <div class="row text-center center-block">
                                <div class="col-md-1"></div>
                                <div class="col-md-2 text-center" id="Facebook" runat="server">
                                    <a id="btnFacebook" target="_blank" runat="server"></a>

                                </div>
                                <div class="col-md-2 text-center" id="Twitter" runat="server">
                                    <a id="btnTwitter" target="_blank" runat="server"></a>

                                </div>
                                <div class="col-md-2 text-center" id="InstaGram" runat="server">
                                    <a id="btnInstaGram" target="_blank" runat="server"></a>

                                </div>
                                <div class="col-md-2 text-center" id="WhataApp" runat="server">
                                    <a id="btnWhataApp" target="_blank" runat="server"></a>

                                </div>
                             
                            </div>
                        </div>

                    </div>

                </div>

            </div>
            <!-- panel widget -->

        </div>
        <div class="col-md-9">


            <div class="tabs" id="tabs">
                <ul class="nav nav-tabs">
                    <li id="tabprofile" runat="server">
                        <asp:LinkButton ID="btnProfile" OnClick="btnProfile_Click" CssClass="text-center brandFont" runat="server"><span class="menu-active animated"><i class="fa fa-user-secret"></i></span>&nbsp; Profile &nbsp;
                           
                        </asp:LinkButton>

                    </li>
                    <li id="tabProject" runat="server">
                        <asp:LinkButton ID="btnProjects" CssClass="text-center brandFont" OnClick="btnProjects_Click" runat="server"><span class="menu-active "><i class="fa fa-bolt"></i></span>&nbsp; Project Details</asp:LinkButton>
                    </li>
                    <li id="tabEvent" runat="server">
                        <asp:LinkButton ID="btnEvent" CssClass="text-center brandFont" OnClick="btnEvent_Click" runat="server"><span class="menu-active "><i class="fa fa-bullhorn"></i></span>&nbsp;Events</asp:LinkButton>
                    </li>
                    <li id="tabTshirt" runat="server">
                        <asp:LinkButton ID="btnTabTshirt" CssClass="text-center brandFont" OnClick="btnTabTshirt_Click" runat="server"><span class="menu-active "><i><img src="../../CSS/Images/Tshirt.png" /> </i></span>&nbsp; T-Shirt</asp:LinkButton>
                    </li>


                </ul>
                <div class="tab-content">
                    <div id="profiletab" runat="server" class="brandFont tab-pane">
                        <div class="visible-lg visible-md">
                            <br />
                        </div>

                        <h3 style="letter-spacing: 3px;"><i class="fa fa-user"></i>&nbsp; Profile &nbsp; 
                            
                                 <span class="pull-right">
                                     <asp:LinkButton ID="btnSaveStudentProfile" OnClick="btnSaveStudentProfile_Click" ValidationGroup="Student" CssClass="btn btn-info" runat="server"> <i class="fa fa-check"></i> &nbsp; Save </asp:LinkButton>
                                 </span>
                            <asp:Panel ID="pnlRequest" runat="server">
                                <asp:LinkButton ID="btnRequestToManager" OnClick="btnRequestToManager_Click" ForeColor="Red" Text="Click On link To Request" runat="server"> <marquee><p class="brandFont" style="font-size: 12pt;margin-right:200px;">If Any Colleges OR Any other Information are Not  <b style="color:darkred"> Listed Please click on the link </b> </p></marquee></asp:LinkButton>
                            </asp:Panel>
                        </h3>
                        <br />
                        <div class="row form-group ">
                            <div class="col-md-6" style="letter-spacing: 3px;">
                                <label for="txtStudentName">
                                    Name
                                    <asp:RequiredFieldValidator ID="rfvtxtManagerName" ControlToValidate="txtStudentName" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>

                                </label>
                                <asp:TextBox ID="txtStudentName" CssClass="form-control" Placeholder="Enter Your Name" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="letter-spacing: 3px;">
                                <label for="txtLeadId">LEAD ID</label>
                                <asp:TextBox ID="txtLeadId" BackColor="Highlight" ForeColor="black" Font-Size="Small" Font-Bold="true" CssClass="form-control uppercase" Enabled="false" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-md-3" style="letter-spacing: 3px;">
                                <label for="txtMobileNo">Mobile No</label>
                                <asp:TextBox ID="txtMobileNo" Enabled="false" BackColor="Highlight" ForeColor="black" Font-Size="Small" Font-Bold="true" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
               
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
             
                            <ContentTemplate>
                            
                                <div class="row form-group">
                                    <div class="col-md-4" style="letter-spacing: 2px;">
                                        <label for="ddlState">
                                         College State 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlState" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlState" CssClass="form-control  " OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>

                                    </div>
                                    <div class="col-md-4" style="letter-spacing: 2px;">
                                        <label for="ddlDistrict">College District</label>
                                        <asp:DropDownList ID="ddlDistrict" CssClass="form-control  " OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>

                                    </div>
                                    <div class="col-md-4" style="letter-spacing: 2px;">
                                        <label for="ddlTaluka">
                                         College City  
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlTaluka" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator></span></label>
                                        <asp:DropDownList ID="ddlTaluka" CssClass="form-control " OnSelectedIndexChanged="ddlTaluka_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>

                                    </div>
                                </div>

                                <div class="row form-group" style="letter-spacing: 3px;">
                                    <div class="col-md-4">
                                        <label for="ddlProgramme">
                                            Stream  
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlProgramme" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlProgramme" CssClass="form-control" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>

                                    </div>
                                    <div class="col-md-4">
                                        <label for="ddlCourse">
                                            Course 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlCourse" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlCourse" CssClass="form-control" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>

                                    </div>
                                    <div class="col-md-4">
                                        <label for="">
                                            Semester 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlSemester" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlSemester" CssClass="form-control" runat="server"></asp:DropDownList>

                                    </div>
                                </div>

                                <div class="row form-group" style="letter-spacing: 3px;">
                                    <div class="col-md-12">
                                        <label for="">
                                            College Name
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlCollege" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlCollege"  CssClass="form-control" runat="server"></asp:DropDownList>

                                    </div>
                                </div>
                                <hr />
                                <div class="row">

                                    <div class="col-md-6">
                                        <label for="">Local Address</label>
                                        <asp:TextBox ID="txtAddress" TextMode="MultiLine" placeholder="Enter your Local Address" Rows="4" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="">
                                                    Alteranate Mobile No   
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="txtAlternativeMobileNo" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Student" ForeColor="DarkRed" runat="server" ErrorMessage="* Numeric Only"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" Font-Size="Small" runat="server" ValidationGroup="Student"
                                                        ControlToValidate="txtAlternativeMobileNo" ErrorMessage="* 10 Digits Required" ForeColor="DarkRed"
                                                        ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>

                                                </label>
                                                <asp:TextBox ID="txtAlternativeMobileNo" CssClass="form-control" onkeypress="NumericOnly()" placeholder="Alternative Mobile No" runat="server" MaxLength="10"></asp:TextBox>

                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    Email ID
                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="Student" Display="Dynamic" SetFocusOnError="true"
                                                         ControlToValidate="txtStudentMailId" ErrorMessage="* InValid Mail Id" ForeColor="DarkRed"
                                                         ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txtStudentMailId" ValidationGroup="Student" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtStudentMailId" CssClass="form-control" placeholder="Email ID" runat="server"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-md-3">

                                                <label for="">
                                                    DOB
                                                    <asp:CompareValidator runat="server" ErrorMessage="* Valid Day" Display="Dynamic" ValidationGroup="Student"
                                                        ControlToValidate="ddlDay" Type="String" Operator="NotEqual"
                                                        ValueToCompare="Day" ForeColor="Red" />

                                                </label>
                                                <asp:DropDownList ID="ddlDay" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Day" Value="Day"></asp:ListItem>
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
                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="hidden-sm hidden-xs" for="">
                                                    &nbsp;
                                                    <asp:CompareValidator runat="server" ErrorMessage="* Valid Month" Display="Dynamic" ValidationGroup="Student"
                                                        ControlToValidate="ddlMonth" Type="String" Operator="NotEqual"
                                                        ValueToCompare="Month" ForeColor="Red" />

                                                </label>
                                                <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Month" Value="Month"></asp:ListItem>
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
                                            <div class="col-md-3">
                                                <label class="hidden-sm hidden-xs" for="">
                                                    &nbsp;
                                                    <asp:CompareValidator runat="server" ErrorMessage="* Valid Year" Display="Dynamic" ValidationGroup="Student"
                                                        ControlToValidate="ddlYear" Type="String" Operator="NotEqual"
                                                        ValueToCompare="Year" ForeColor="Red" />
                                                </label>
                                                <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="">
                                                    Blood Group
                                                </label>
                                                <asp:DropDownList ID="ddlBloodGroup" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                                    <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                                    <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                                    <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                                    <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                                    <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                                    <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                                    <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                                    <asp:ListItem Text="Bombay" Value="Bombay Blood"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <br />
                        <div class="row">
                            <div class="col-md-1">

                                <asp:RadioButton ID="rdoMale" CssClass="radio radio-custom radio-danger" Checked="true" Text="Male" GroupName="Gender" runat="server" />

                            </div>
                            <div class="col-md-1">
                                <asp:RadioButton ID="rdoFemale" Text="Female" CssClass="radio radio-custom radio-primary" GroupName="Gender" runat="server" />
                            </div>


                            <div class="col-md-4">
                                <div class="input-group form-group">
                                    <div class="input-group-addon bg-success">
                                        <span class="input-group-text">Aadhar No

                                        </span>
                                           
                                    </div>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" Display="Dynamic" ValidationGroup="Student" SetFocusOnError="true"
                                                  ControlToValidate="txtAadharNo" ErrorMessage="* atleast 12 to 14 digits Required" ForeColor="DarkRed"
                                                  ValidationExpression="^\d{12,14}$"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtAadharNo" CssClass="form-control" placeholder="Aadhar No" MinLength="12" MaxLength="14" onkeypress="NumericOnly()" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group form-group">
                                    <div class="input-group-addon bg-success">
                                        <span class="input-group-text">Bank</span>

                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtBankName" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Student" ForeColor="Red"  ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtBankName" CssClass="form-control" placeholder="Bank Name" runat="server" ></asp:TextBox>

                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="input-group form-group">
                                    <div class="input-group-addon bg-success">
                                        <span class="input-group-text">A/c No</span>

                                    </div>
                                    <asp:RegularExpressionValidator runat="server" ID="regularexp_accountno"   Display="Dynamic" ValidationGroup="Student" SetFocusOnError="true"
                                                  ControlToValidate="txtAccountNo" ErrorMessage="* atleast 9 to 18 digits Required" ForeColor="DarkRed"
                                                  ValidationExpression="^\d{09,18}$"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtAccountNo" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Student" ForeColor="Red"  ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAccountNo" CssClass="form-control" onkeypress="NumericOnly()"  MinLength="09" MaxLength="18" placeholder="Account No" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group form-group">
                                    <div class="input-group-addon bg-success">
                                        <span class="input-group-text">IFSC</span>

                                    </div>
                                       <asp:RegularExpressionValidator runat="server" ID="regularexp_ifsccode"   Display="Dynamic" ValidationGroup="Student" SetFocusOnError="true"
                                                  ControlToValidate="txtIFSCCode" ErrorMessage="* 11 digits Required" ForeColor="DarkRed"
                                                  ValidationExpression="^[A-Za-z0-9]{11}$"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator  ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtIFSCCode" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Student" ForeColor="Red"  ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtIFSCCode" CssClass="form-control" placeholder="IFSC Code" maxLength="11" runat="server"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="input-group form-group">
                                    <div class="input-group-addon bg-success">
                                        <span class="input-group-text">A/c Holder Name</span>

                                    </div>
                                    <asp:RequiredFieldValidator ID="accholdervalidate" runat="server" ControlToValidate="txtAccountHolderName" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Student" ForeColor="Red"  ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAccountHolderName" CssClass="form-control" placeholder="A/c Holder Name"  maxLength="15" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group form-group">
                                    <div class="input-group-addon bg-success">
                                        <span class="input-group-text">Branch</span>

                                    </div>
                                    <asp:RequiredFieldValidator  ID="validateBankbranch" runat="server" ControlToValidate="txtBankBranch" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Student" ForeColor="Red"  ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtBankBranch" CssClass="form-control" placeholder="Branch Name" maxLength="15" runat="server"></asp:TextBox>

                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>My Talent<span class="text-primary">&nbsp;(Eg : Programming,Photography)</span> </label>
                                <asp:TextBox ID="txtMyTalent" placeholder="Eg : Programming,Photography;Self Management;Networking (Use Enter Key for each item)" CssClass="form-control" TextMode="MultiLine" Rows="4" data-role="tags-input" runat="server"></asp:TextBox>
                            </div>
                        </div>


                    </div>


                    <!-- Project tab -->

                    <div id="ProjectTab" runat="server" class="tab-pane">
                        <div class="visible-lg visible-md">
                            <br />
                        </div>
                        <h3><i class="fa fa-bolt"></i>&nbsp; Project Details
                                 <span class="pull-right">

                                     <asp:LinkButton ID="btnProjectRefresh" OnClick="btnProjectRefresh_Click" data-toggle="tooltip" title="Refresh" CssClass="btn btn-facebook btn-rounded btn-sm" runat="server"> <span class="fa fa-refresh"></span> </asp:LinkButton>
                                     <a href="Student_AddEditProject.aspx" class="btn btn-danger hidden"><span class="fa fa-plus"></span></a>
                                     <asp:LinkButton ID="btnProposedProject" OnClick="btnProposedProject_Click" data-toggle="tooltip" title="Add New Project" CssClass="btn btn-info btn-rounded btn-sm" runat="server"> <span class="fa fa-plus" style="cursor:pointer;"></span> </asp:LinkButton>
                                 </span>
                        </h3>
                        <br />
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Repeater ID="rptStudentProposedList" OnItemDataBound="rptStudentProposedList_ItemDataBound" OnItemCommand="rptStudentProposedList_ItemCommand" runat="server">
                                                <HeaderTemplate>
                                                    <div class="row" style="max-height: 550px; overflow: auto;">
                                                        <table class="table table-striped " id="StudentProjectList">
                                                            <thead class="brandFont1">
                                                                <tr>
                                                                    <th style="display: none">PDId
                                                                    </th>
                                                                    <th style="padding: 10px;"><strong><b>Title </b></strong>
                                                                    </th>
                                                                    <th class="hidden-xs text-center" style="padding: 10px;"><strong><b>Requested</b></strong>
                                                                    </th>
                                                                    <th class="hidden-xs text-center" style="padding: 10px;"><strong><b>Approved</b></strong>
                                                                    </th>
                                                                    <th class="hidden-xs text-center" style="padding: 10px;"><strong><b>Released</b></strong>
                                                                    </th>
                                                                    <th style="display: none; padding: 10px;"><strong><b>Balance</b></strong>
                                                                    </th>
                                                                    <th style="text-align: center; padding: 10px;" class="hidden-xs"><strong><b>Status</b></strong>
                                                                    </th>

                                                                    <th style="text-align: center; padding: 10px;"><strong><b>Action</b></strong>
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <tbody>
                                                        <tr>
                                                            <td style="display: none">
                                                                <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                            </td>

                                                            <td style="width: 40%; padding-left: 10px;">
                                                                <asp:Label ID="lblTitle" Font-Size="Medium" runat="server" Text='<%# Eval("title") %>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: center" class="hidden-xs">
                                                                <asp:Label ID="lblAmount" Font-Size="Medium" runat="server" Text='<%# Eval("Amount") %>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: center" class="hidden-xs">
                                                                <asp:Label ID="lblFundedAmount" Font-Size="Medium" runat="server" Text='<%# Eval("SanctionAmount") %>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: center" class="hidden-xs">
                                                                <asp:Label ID="lblDispersedAmount" Font-Size="Medium" runat="server" />
                                                            </td>
                                                            <td style="width: 10%; text-align: center; display: none;">
                                                                <asp:Label ID="lblBalanceAmount" Font-Size="Medium" runat="server" />
                                                            </td>
                                                            <td style="min-width: 30px; text-align: center;" class="hidden-xs">
                                                                <%-- <asp:LinkButton ID="btnViewProject" Font-Size="Medium"  CommandArgument='<%# Eval("PDId")+"_"+Eval("ProjectStatus")+"_"+("View") %>' Text='<%# Eval("ProjectStatus") %>' runat="server"></asp:LinkButton>--%>
                                                                <asp:Label ID="lblProjectStatus" Font-Size="Small" runat="server" Text='<%# Eval("ProjectStatus") %>' />

                                                                <asp:Label ID="lblRating" Visible="false" runat="server" Text='<%# Eval("rating") %>'></asp:Label>


                                                                <asp:Label ID="lblCompletionProgress" runat="server" Text="" role="progressbar" aria-valuemax="100">
                                                                    <span class="title">
                                                                        <h3>
                                                                            <asp:Label ID="lblCompletionProgressCount" runat="server" Text=""></asp:Label></h3>
                                                                    </span>
                                                                </asp:Label>

                                                            </td>

                                                            <%-- <td style="min-width: 10px; text-align: center;">
                                                    <asp:LinkButton ID="btnview" CssClass="btn btn-floating btn-info" Font-Size="Medium"  CommandArgument='<%# Eval("PDId")+"_"+Eval("ProjectStatus")+"_"+("View") %>' runat="server"><span class="fa fa-eye"></span></asp:LinkButton>
                                                </td>--%>

                                                            <td style="min-width: 10px; white-space: nowrap; text-align: center">
                                                                <asp:LinkButton ID="btnEditProject" runat="server"
                                                                    CommandArgument='<%# Eval("PDId")+"_"+Eval("ProjectStatus")+"_"+("Edit") %>'><span class="fa fa-pencil"></span></asp:LinkButton>

                                                                <asp:LinkButton ID="btnChat" OnCommand="btnChat_Click"
                                                                    CommandArgument='<%# Eval("PDId")+"^"+Eval("title")+"^"+Eval("ProjectStatus") %>' runat="server">                                                              
                                                            <span class="fa fa-comment " style="color: white;"></span>

                                                  <%--              <asp:Panel ID="pnlChatCountVisibility" runat="server">
                                                                    <span class="badge badge-danger">
                                                                        <asp:Label ID="lblChatUnReadCount" runat="server"> 
                                                                        </asp:Label>
                                                                    </span>
                                                                </asp:Panel>--%>
                                                                </asp:LinkButton>
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
                                        <%--   <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnProposedProject" EventName="Click" />
                                    </Triggers>--%>
                                    </asp:UpdatePanel>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel">
                                        <div class="panel-body">

                                            <asp:Panel runat="server" ID="pnlStudent">
                                                <div class="alert alert-info alert-dismissible" role="alert">
                                                    <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                                    <strong><span class="fa fa-child"></span>&nbsp; Student</strong>
                                                    <marquee><p class="brandFont" style="font-size: 18pt">Welcome Student Complete <b>1</b> Project to become a <b> Initiator OR Change Maker OR LEAder </b> </p></marquee>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="pnlMasterLeader" >
                                                <div class="alert alert-warning alert-dismissible hidden" role="alert">
                                                    <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                                    <strong>
                                                        <img src="../../CSS/Images/Brooze_Icon.png" style="width: 30px; height: 30px;" />
                                                        &nbsp; Master Leader</strong>
                                                    <marquee><p class="brandFont" style="font-size: 18pt">You are Eligible for <b> Master Leader</b> Please <a href="#"> <asp:LinkButton ID="btnClickForMasterLeaderApply" OnClick="btnClickForMasterLeaderApply_Click" Text="Click Here To Apply" runat="server"></asp:LinkButton>  </a></p></marquee>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="pnlLeadAmbassador">
                                                <div class="alert alert-danger alert-dismissible" role="alert">
                                                    <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                                    <strong>
                                                        <img src="../../CSS/Images/Silver_Icon.png" style="width: 30px; height: 30px;" />
                                                        &nbsp; LEAD Ambassador</strong>
                                                    <marquee><p class="brandFont" style="font-size: 18pt">You are Eligible for <b> LEAD Ambassador</b> Please<a href="#"> <asp:LinkButton ID="btnClickForLeadAmbassadorApply" OnClick="btnClickForLeadAmbassadorApply_Click" runat="server">Click Here To Apply</asp:LinkButton> </a></p></marquee>
                                                </div>
                                            </asp:Panel>



                                            <%-- <asp:Panel runat="server" ID="pnlLeadIntern">
                        <div class="alert alert-success alert-dismissible" role="alert">
                            <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                            <strong> <img src="../../CSS/Images/Gold_Icon.png" style="width:30px;height:30px;" /> &nbsp; LEAD Intern</strong>
                            <marquee><p class="brandFont" style="font-size: 18pt">You are Eligible for <b> LEAD Intern</b> Please <a href="#"> Click Here To Apply</a></p></marquee>
                        </div>
                    </asp:Panel>--%>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <!-- Event tab -->
                    <div id="EventTab" runat="server" class="tab-pane">
                        <div class="visible-lg visible-md">
                            <br />
                        </div>
                        <h3><i class="fa fa-comments"></i>&nbsp; Events
                       <div class="row">
                           <div class="col-md-8">
                               <ul class="list-item member-list" style="height: 550px; overflow: auto;">
                                   <asp:Repeater ID="rptEvent" OnItemCommand="rptEvent_ItemCommand" runat="server">
                                       <ItemTemplate>
                                           <li>
                                               <asp:Label ID="lblEventId" Visible="false" runat="server" Text='<% # Eval("EventId") %>'></asp:Label>
                                               <div class="user-avatar">
                                                   <%-- <asp:LinkButton ID="btnParticularEvent" CommandArgument='<%# Eval("EventId") %>' runat="server">--%>
                                                   <a href='<%# Eval("EventURL") %>' target="_blank" runat="server">
                                                       <asp:Image ID="imgEventImg" Width="652px" Height="221px" CssClass="img-rounded" ImageUrl='<% # Eval("Image_Path") %>' runat="server" /><%--</asp:LinkButton>--%>
                                                   </a>
                                               </div>

                                               <div class="row">
                                                   <div class="col-md-12">
                                                       <h5>
                                                           <asp:Label ID="lblEventName" runat="server" Text='<% # Eval("EventName") %>'></asp:Label>

                                                       </h5>

                                                       <p>
                                                           from :
                                            <asp:Label ID="lblFromDate" runat="server" Text='<% # Eval("FromDate") %>'></asp:Label>
                                                           to 
                                            <asp:Label ID="lblToDate" runat="server" Text='<% # Eval("ToDate") %>'></asp:Label>

                                                       </p>
                                                       <p style="line-height: 1.6;">
                                                           <asp:Label ID="lblEventDescription" runat="server" Text='<% # Eval("EventDescription") %>'></asp:Label>

                                                           &nbsp;
                                                           <br />
                                                           <a runat="server" class="btn-rounded btn-primary btn-sm" href='<% #Eval("EventApplyURL") %>' target="_blank"><span class="fa fa-send"></span>&nbsp; Apply Now</a>
                                                       </p>
                                                   </div>
                                               </div>
                                           </li>

                                       </ItemTemplate>
                                   </asp:Repeater>
                               </ul>
                           </div>

                       </div>
                        </h3>
                        <br />
                    </div>

                    <!-- Tshirt tab -->
                    <div id="TshirtTab" runat="server" class="tab-pane">
                        <div class="visible-lg visible-md">
                            <br />
                        </div>
                        <h3><i>
                            <img src="../../CSS/Images/Tshirt.png" />
                        </i>&nbsp; Tshirt
                     <span class="pull-right">
                         <asp:LinkButton ID="btnRefreshTshirt" OnClick="btnRefreshTshirt_Click" data-toggle="tooltip" title="Refresh Tshirt List" CssClass="btn btn-primary btn-floating" runat="server"> <span class="fa fa-refresh"></span> </asp:LinkButton>
                         <asp:LinkButton ID="btnRequestNewTshirt" OnClick="btnRequestNewTshirt_Click" data-toggle="tooltip" title="Request New Tshirt" CssClass="btn btn-dribbble btn-floating" runat="server"> <span class="fa fa-plus"></span> </asp:LinkButton>
                     </span>
                        </h3>
                        <br />
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Repeater ID="rptMultipleTshirtRequest" OnItemDataBound="rptMultipleTshirtRequest_ItemDataBound" 
                                        OnItemCommand="rptMultipleTshirtRequest_ItemCommand" runat="server">
                                        <HeaderTemplate>
                                            <div class="row" style="max-height: 550px; overflow: auto;">
                                                <table class="table table-hover " style="max-width: 98%">
                                                    <thead>
                                                        <tr>
                                                            <td align="center" style="display: none">RequestedId
                                                            </td>

                                                            <td style="display: none"><strong><b>LEadID</b></strong>
                                                            </td>
                                                            <td style="display: none"><strong><b>ManagerId</b></strong>
                                                            </td>
                                                            <td><strong><b>TshirtSize</b></strong>
                                                            </td>
                                                            <td><strong><b>Requested Date</b></strong>
                                                            </td>

                                                            <td><strong><b>Sanction Date</b></strong>
                                                            </td>
                                                            <td><strong><b>Rejected Date</b></strong>
                                                            </td>
                                                            <td><strong><b>Exchange Date</b></strong>
                                                            </td>
                                                            <td align="center"><strong><b>Remark</b></strong>
                                                            </td>
                                                            <td align="center"><strong><b>Status</b></strong>
                                                            </td>
                                                            <td align="center"><strong><b>Action</b></strong>
                                                            </td>

                                                        </tr>
                                                    </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tbody>
                                                <tr>
                                                    <td align="center" style="display: none">
                                                        <asp:Label ID="lblRequestedId" runat="server" Text='<%# Eval("requestedId") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label ID="lblLeadId" Font-Size="Medium" runat="server" Text='<%# Eval("Lead_id") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label ID="lblManagerId" Font-Size="Medium" runat="server" Text='<%# Eval("managerid") %>' />
                                                    </td>
                                                    <td style="width: 10%;">
                                                        <asp:Label ID="lblTshirtSize" Font-Size="Medium" runat="server" Text='<% #Eval("TshirtSize") %>' />
                                                    </td>
                                                    <td style="width: 12%;">
                                                        <asp:Label ID="lblRequestedDate" Font-Size="Medium" runat="server" Text='<%# Eval("RequestedDate") %>' />
                                                    </td>
                                                    <td style="width: 12%;">
                                                        <asp:Label ID="lblSanctionDate" Font-Size="Medium" runat="server" Text='<%# Eval("SanctionDate") %>' />
                                                    </td>
                                                    <td style="min-width: 12%;">
                                                        <asp:Label ID="lblRejectedDate" Font-Size="Medium" runat="server" Text='<%# Eval("RejectedDate") %>' />
                                                    </td>
                                                    <td style="min-width: 12%; text-align: center;">
                                                        <asp:Label ID="lblExchangeDate" Font-Size="Medium" runat="server" Text='<%# Eval("ExchangeDate") %>' />
                                                    </td>
                                                    <td style="min-width: 30px; text-align: center;">
                                                        <asp:Label ID="lblRemark" Font-Size="Medium" runat="server" Text='<%# Eval("Remark") %>' />
                                                    </td>
                                                    <td style="min-width: 30px; text-align: center;">
                                                        <asp:Label ID="Label1" Font-Size="Medium" runat="server" Text='<%# Eval("Status") %>' />
                                                    </td>
                                                    <td style="min-width: 30px; text-align: center; display: none;">
                                                        <asp:Label ID="lblSanctionStatus" Font-Size="Medium" runat="server" Text='<%# Eval("SanctionStatus") %>' />
                                                    </td>
                                                    <td style="min-width: 10px; text-align: center;">
                                                        <asp:LinkButton ID="btnEditTshirt" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </div>
                                        </FooterTemplate>
                                    </asp:Repeater>


                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- tab-content -->
            </div>

        </div>
    </div>
    <div id="POP_AddProjectProposal" class="modal fade" role="dialog" style="margin-top: 0px">
        <div class="modal-dialog" style="width: auto; max-width: 90%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h3>
                        <asp:Label ID="lblProposedEventType" Visible="false" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblProposedTitle" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblProjectStatusClickingEvent" Visible="false" runat="server" Text=""></asp:Label>
                        <a class="pull-right text-right" data-dismiss="modal">
                            <asp:Label ID="lblPDIdEditProject" Visible="false" runat="server" Text=""></asp:Label>&nbsp;
                            <i class="fa fa-remove text-primary fa-2x" style="cursor: pointer;"></i>
                        </a>
                    </h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label for="txtProjectTitle">Project Title </label>
                            <asp:TextBox ID="txtProjectTitle" placeholder="What is your idea ?" CausesValidation="true" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvtxtClave" ForeColor="Red" ControlToValidate="txtProjectTitle" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator Display = "Dynamic" SetFocusOnError="true" ControlToValidate = "txtProjectTitle" ID="RegularExpressionValidator7" ForeColor="Red" ValidationExpression = "^[\s\S]{10,}$" runat="server" ErrorMessage="* Minimum 10 Characters ." ValidationGroup="Prop"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-6 ">
                            <label for="ddlProjectType">Project Type</label>
                            <asp:DropDownList ID="ddlProjectType" CssClass="form-control" runat="server"></asp:DropDownList>
                            <span style="display: none;">
                                <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="[Select]" Value="[Select]"></asp:ListItem>
                                </asp:DropDownList>
                            </span>

                            <asp:CompareValidator runat="server" Display="Dynamic" ErrorMessage="* Choose Project Type"
                                ControlToValidate="ddlProjectType" Type="String" SetFocusOnError="true" Operator="NotEqual"
                                ControlToCompare="DropDownList1" ForeColor="Red">

                            </asp:CompareValidator>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label for="txtProjectObjectives">Objectives of the project </label>
                            <asp:TextBox ID="txtProjectObjectives" TextMode="MultiLine" placeholder="Why do you want to implement this idea?" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ForeColor="Red" ControlToValidate="txtProjectObjectives" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator Display = "Dynamic" SetFocusOnError="true" ControlToValidate = "txtProjectObjectives" ID="RegularExpressionValidator6" ForeColor="Red" ValidationExpression = "^[\s\S]{10,}$" runat="server" ErrorMessage="* Minimum 10 Characters ." ValidationGroup="Prop"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-6">
                            <label for="txtProjectObjectives">Action Plan</label>
                            <asp:TextBox ID="txtProjectPlan" TextMode="MultiLine" placeholder="How do you want to implement this idea?" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red" ControlToValidate="txtProjectPlan" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator Display = "Dynamic" SetFocusOnError="true" ControlToValidate = "txtProjectPlan" ID="RegularExpressionValidator8" ForeColor="Red" ValidationExpression = "^[\s\S]{10,}$" runat="server" ErrorMessage="* Minimum 10 Characters ." ValidationGroup="Prop"></asp:RegularExpressionValidator>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 form-group">
                            <label for="txtTotalBeneficiaries">Place of implementation</label>
                            <asp:TextBox ID="txtProposalPlaceofImplementation" placeholder="Place of Implementation" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red" ControlToValidate="txtProposalPlaceofImplementation" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                                  <asp:RegularExpressionValidator Display = "Dynamic" SetFocusOnError="true" ControlToValidate = "txtProposalPlaceofImplementation" ID="RegularExpressionValidator9" ForeColor="Red" ValidationExpression = "^[\s\S]{10,}$" runat="server" ErrorMessage="* Minimum 3 Characters ." ValidationGroup="Prop"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-3 ">
                            <label for="txtTotalBeneficiaries">How many beneficiaries</label>
                            <asp:TextBox ID="txtTotalBeneficiaries" placeholder="How many beneficiaries are there" Text="0" onkeypress="NumericOnly()" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" ControlToValidate="txtTotalBeneficiaries" Display="Dynamic" ErrorMessage="* Numeric Only" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                ControlToValidate="txtTotalBeneficiaries" SetFocusOnError="true"
                                ErrorMessage="Only numeric allowed." ForeColor="Red"
                                ValidationExpression="^[0-9]*$" ValidationGroup="Prop">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-6">
                            <label for="txtBeneficiaries">Who are the beneficiaries</label>
                            <asp:TextBox ID="txtProposedBeneficiaries" placeholder="Who are the beneficiaries" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator23" ForeColor="Red" ControlToValidate="txtProposedBeneficiaries" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                               <asp:RegularExpressionValidator Display = "Dynamic" SetFocusOnError="true" ControlToValidate = "txtProposedBeneficiaries" ID="RegularExpressionValidator11" ForeColor="Red" ValidationExpression = "^[\s\S]{10,}$" runat="server" ErrorMessage="* Minimum 3 Characters ." ValidationGroup="Prop"></asp:RegularExpressionValidator>
                        </div>

                    </div>
                    <div class="row form-group">
                        <div class="col-md-6">
                            <label for="txtCurrentSituation">Current Situation</label>
                            <asp:TextBox ID="txtCurrentSituation" placeholder="Current Situation" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator24" ForeColor="Red" ControlToValidate="txtCurrentSituation" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                               <asp:RegularExpressionValidator Display = "Dynamic" SetFocusOnError="true" ControlToValidate = "txtCurrentSituation" ID="RegularExpressionValidator10" ForeColor="Red" ValidationExpression = "^[\s\S]{10,}$" runat="server" ErrorMessage="* Minimum 10 Characters ." ValidationGroup="Prop"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-2 form-group">
                            <label for="txtProposedStartDate">Start Date</label>
                            <asp:TextBox ID="txtProposedStartDate" placeholder="yyyy-mm-dd" autocomplete="off" CssClass="form-control datepicker" runat="server" onchange="CalculateTargetDays();"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator30" ForeColor="Red" ControlToValidate="txtProposedStartDate" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                            <asp:CompareValidator runat="server" Display="Dynamic" ErrorMessage="* Choose Proper Date"
                                ControlToValidate="txtProposedStartDate" ControlToCompare="txtProposedEndDate" Type="date" Operator="LessThanEqual" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Prop">
                            </asp:CompareValidator>


                        </div>
                        <div class="col-md-2 form-group">
                            <label for="txtProposedEndDate">End Date</label>

                            <asp:TextBox ID="txtProposedEndDate" placeholder="yyyy-mm-dd" autocomplete="off" CssClass="form-control datepicker" runat="server" onchange="CalculateTargetDays();"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator31" ForeColor="Red" ControlToValidate="txtProposedEndDate" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                            <asp:CompareValidator runat="server" Display="Dynamic" ErrorMessage="* Choose Proper Date"
                                ControlToValidate="txtProposedEndDate" ControlToCompare="txtProposedStartDate" Type="date" Operator="GreaterThanEqual" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Prop">

                            </asp:CompareValidator>
                        </div>
                        <div class="col-md-2">
                            <label>Target Days</label>
                            <br />
                            <asp:Label ID="lblProposedProjectTargetDays" CssClass="text-center" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-4">

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <%--      <h4>Add project materials &nbsp;&nbsp;                                          
                                               
                                    </h4>--%>
                                    <div class="row">

                                        <div class="col-md-12" style="max-height: 300px; overflow: auto;">
                                            <asp:Repeater runat="server" ID="rptMeterial">
                                                <HeaderTemplate>
                                                    <table class="table table-hover" id="meterial" style="width: 100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Name</th>
                                                                <th>Cost</th>
                                                                <th style="display: none">add</th>
                                                            </tr>
                                                        </thead>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblSlno" Text='<%# Eval("slno") %>' runat="server" />
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Meterial Type" ID="txtMeterialName" Text='<%# Eval("MeterialName") %>' />
                                                           <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ForeColor="Red" ControlToValidate="txtMeterialName" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Cost" onkeypress="NumericOnly()" ID="txtMeterialCost" 
                                                                Text='<%# Eval("MeterialCost") %>' />
                                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ForeColor="Red" ControlToValidate="txtMeterialCost" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <%--  <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/CSS/Images/add.png" OnClick="btnAddMeterial_Click" />--%>
                                                        </td>
                                                        <td style="width: 6%;">
                                                            <%-- <a onclick="RemoveRows(this)"><span class="fa fa-trash" runat="server" onclick="btnRemoveRow_Click"></span></a>--%>
                                                            &nbsp;
                                                            <asp:ImageButton ID="btnRemoveRow" runat="server" ImageUrl="~/CSS/Images/trash.png" OnClick="btnRemoveRow_Click" />
                                                            <%-- <asp:LinkButton ID="btnRemove" CssClass="btn btn-danger btn-floating " OnClick="btnRemove_Click" runat="server"><span class="fa fa-remove"></span></asp:LinkButton>--%>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <asp:LinkButton ID="btnAddMeterial" CssClass="btn btn-block text-center btn-info" runat="server" OnClick="btnAddMeterial_Click" ValidationGroup="Meterial"><span class="fa fa-plus"></span> Add Meterial</asp:LinkButton>
                                        </div>
                                    </div>

                                    <h3 runat="server" id="lblTotalAmt">Total Amount
                                  &nbsp; :
                                        <asp:Label ID="lblTotalAmount" CssClass="text-danger" runat="server" Text=""></asp:Label>
                                    </h3>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-8">
                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Always" ValidateRequestMode="Enabled">
                                <ContentTemplate>
                                    <%--         <h4>Add team members &nbsp;&nbsp;                                     
                                
                                    </h4>--%>
                                    <div class="row">

                                        <div class="col-md-12" style="max-height: 300px; overflow: auto;">
                                            <asp:Repeater runat="server" ID="rptTeamMembers" OnItemDataBound="rptTeamMembers_ItemDataBound">
                                                <HeaderTemplate>
                                                    <table class="table table-hover" style="width: 100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Name</th>
                                                                <th>Mail Id</th>


                                                                <th>Mobile No</th>
                                                                <th style="display: none;">Gender</th>
                                                                <th style="display: none">add</th>

                                                            </tr>
                                                        </thead>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblSlno" Text='<%# Eval("slno") %>' runat="server" />
                                                            <%--  <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />--%>
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Name" ID="txtName" Text='<%# Eval("MemberName") %>' />
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Mail Id" ID="txtMailId" Text='<%# Eval("MemberMailId") %>' />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="Prop" ControlToValidate="txtMailId" ForeColor="DarkRed" SetFocusOnError="true" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 20%;">

                                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Mobile No" ID="txtMobileNo" onkeypress="NumericOnly()" MaxLength="10" Text='<%# Eval("MemberMobileNo") %>' />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" SetFocusOnError="true" ValidationGroup="Prop"
                                                                ControlToValidate="txtMobileNo" ErrorMessage="* 10 Digits Required" ForeColor="DarkRed"
                                                                ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="Prop" ControlToValidate="txtMobileNo" ForeColor="DarkRed" SetFocusOnError="true" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>


                                                        </td>
                                                        <td style="width: 15%; display: none">
                                                            <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <%-- <td style="width: 2%;">

                                                       
                                                           <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/CSS/Images/add.png" OnClick="btnAddTeamMembers_Click" />
                                                        </td>--%>
                                                        <td style="width: 6%;">
                                                            <asp:ImageButton ID="btnRemoveRow" runat="server" ImageUrl="~/CSS/Images/trash.png" OnClick="btnRemoveTeamMembers_Click" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <asp:LinkButton ID="btnAddTeamMembers" CssClass="btn btn-block btn-facebook text-center" runat="server" OnClick="btnAddTeamMembers_Click"><span class="fa fa-plus"></span> Add Team Members </asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row" runat="server" id="ManagerCommentText">
                        <div class="col-md-12">
                            <h3>
                                <label for="txtManagerComments">Manager Comments</label></h3>
                            <asp:TextBox ID="txtManagerComments" TextMode="MultiLine" Rows="5" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-4 col-md-offset-4">

                            <asp:Button ID="btnSaveProposal" OnClick="btnSaveProposal_Click" CssClass="btn btn-info" ValidationGroup="Prop"  runat="server" Text="Send Proposal" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ErrorModal" class="modal fade" role="dialog" style="margin-top: 200px; width: auto; max-width: 90%">
        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>Message
                          <a class="pull-right text-right" data-dismiss="modal">
                              <i class="fa fa-remove text-primary fa-2x" style="cursor: pointer;"></i>
                          </a>
                    </h3>
                    <p>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </p>

                </div>

            </div>

        </div>
    </div>
    <div id="POP_RequestToManager" class="modal fade" role="dialog" style="margin-top: 150px; width: auto;">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>Request To Manager
                          <a class="pull-right text-right" data-dismiss="modal">
                              <i class="fa fa-remove text-primary fa-2x" style="cursor: pointer;"></i>
                          </a>
                    </h3>
                    <div class="row">
                        <div class="col-md-12">
                            <label for="txtMailId" class="brandFont">
                                Enter Name  
                          
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" ControlToValidate="txtStudentRequestName" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtStudentRequestName" CssClass="form-control brandFont" placeholder="Enter Name" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label for="txtMailId" class="brandFont">
                                Enter MailId  
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true"
                                    ControlToValidate="txtRequestMailId" ErrorMessage="* InValid Mail Id" ForeColor="DarkRed"
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRequestMailId" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtRequestMailId" CssClass="form-control brandFont" TextMode="Email" placeholder="Enter eMailId" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label for="txtStateName" class="brandFont">
                                Enter College State name                                 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ControlToValidate="txtStateName" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtStateName" CssClass="form-control brandFont" placeholder="Enter State Name" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="txtDistrictName" class="brandFont">
                                Enter College District name                                 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ControlToValidate="txtDistrictName" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtDistrictName" CssClass="form-control brandFont" placeholder="Enter State Name" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label for="txtTalukaName" class="brandFont">
                                Enter College Taluka name                                 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ControlToValidate="txtTalukaName" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtTalukaName" CssClass="form-control brandFont" placeholder="Enter Taluka Name" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="txtCollegeName" class="brandFont">
                                Enter College name                                 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ControlToValidate="txtCollegeName" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtCollegeName" CssClass="form-control brandFont" placeholder="Enter College Name" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label for="txtRequestToManager" class="brandFont">
                                Enter Full Description where you are facing to Update your profile 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtRequestToManager" ValidationGroup="Request" Display="Dynamic" SetFocusOnError="true" ForeColor="DarkRed" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtRequestToManager" CssClass="form-control brandFont" TextMode="MultiLine" Rows="4" placeholder="Ex :Enter Description whatever problem facing" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4 col-md-offset-1">
                            <asp:LinkButton ID="btnSendRequestToManager" OnClick="btnSendRequestToManager_Click" ValidationGroup="Request" CssClass="btn btn-primary btn-black brandFont" runat="server"><span class="fa fa-send">&nbsp; <i class="brandFont"> Send Request</i></span>  </asp:LinkButton>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>
    <div id="sharad">
    </div>

    <script type="text/javascript">
        function Profile() {
            var Avatar = document.querySelector('#<%=PreviewImage.ClientID %>');
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
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <script type="text/javascript">
        function ErrorModal() {
            $('#ErrorModal').modal({
                backdrop: 'static',
                keyboard: true,
                show: true
            });

        }
    </script>
    <div id="POP_Tshirt" class="modal fade" role="dialog" style="margin-top: 100px; width: auto;">

        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="modal-body">
                            <h3 style="color: white">Select T-shirt Size
                          <a class="pull-right text-right" data-dismiss="modal">
                              <i class="fa fa-remove text-primary fa-fw" style="color: white; cursor: pointer;"></i>
                          </a>
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblTshirtRequestedId" runat="server" Visible="false" Text=""></asp:Label>
                                    <table class="table ">
                                        <thead>
                                            <tr>
                                                <td>T-shirt Size</td>
                                                <td class="text-center">Chest In Inches</td>
                                                <td class="text-center">Height in Inches</td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>

                                                <td>
                                                    <asp:RadioButton ID="rdoS" GroupName="T" CssClass="radio radio-custom radio-success" Text="S" runat="server" />
                                                </td>
                                                <td class="text-center">36
                                                </td>
                                                <td class="text-center">26
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoM" GroupName="T" CssClass="radio radio-custom radio-success" Text="M" runat="server" />
                                                </td>
                                                <td class="text-center">38
                                                </td>
                                                <td class="text-center">27
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoL" GroupName="T" CssClass="radio radio-custom radio-success" Text="L" runat="server" />
                                                </td>
                                                <td class="text-center">40
                                                </td>
                                                <td class="text-center">28
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoXL" GroupName="T" CssClass="radio radio-custom radio-success" Text="XL" runat="server" />
                                                </td>
                                                <td class="text-center">42
                                                </td>
                                                <td class="text-center">29
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoXXL" GroupName="T" CssClass="radio radio-custom radio-success" Text="XXL" runat="server" />
                                                </td>
                                                <td class="text-center">44
                                                </td>
                                                <td class="text-center">30
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>

                            </div>

                            <br />
                            <div class="row">
                                <div class="col-md-4 col-md-offset-1">
                                    <asp:LinkButton ID="btnSendTshirtRequest" OnClick="btnSendTshirtRequest_Click" CssClass="btn btn-dropbox btn-black brandFont" runat="server"><span class="fa fa-send">&nbsp; <i class="brandFont"> Request T-shirt</i></span>  </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
    <div id="POP_StudentTshirt" class="modal fade" role="dialog" style="margin-top: 200px; width: auto;">

        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="modal-body">
                            <h3 style="color: white">Select T-shirt Size
                          <a class="pull-right text-right" data-dismiss="modal">
                              <i class="fa fa-remove text-primary fa-fw" style="color: white; cursor: pointer;"></i>
                          </a>
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMultipleTshirtRequest" Visible="false" runat="server" Text=""></asp:Label>
                                    <table class="table ">
                                        <thead>
                                            <tr>
                                                <td>T-shirt Size</td>
                                                <td class="text-center">Chest In Inches</td>
                                                <td class="text-center">Height in Inches</td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>

                                                <td>
                                                    <asp:RadioButton ID="rdoS1" GroupName="T" CssClass="radio radio-custom radio-success" Text="S" runat="server" />
                                                </td>
                                                <td class="text-center">36
                                                </td>
                                                <td class="text-center">26
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoM1" GroupName="T" CssClass="radio radio-custom radio-success" Text="M" runat="server" />
                                                </td>
                                                <td class="text-center">38
                                                </td>
                                                <td class="text-center">27
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoL1" GroupName="T" CssClass="radio radio-custom radio-success" Text="L" runat="server" />
                                                </td>
                                                <td class="text-center">40
                                                </td>
                                                <td class="text-center">28
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoXL1" GroupName="T" CssClass="radio radio-custom radio-success" Text="XL" runat="server" />
                                                </td>
                                                <td class="text-center">42
                                                </td>
                                                <td class="text-center">29
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoXXL1" GroupName="T" CssClass="radio radio-custom radio-success" Text="XXL" runat="server" />
                                                </td>
                                                <td class="text-center">44
                                                </td>
                                                <td class="text-center">30
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>
                                <div class="col-md-12">
                                    <label>Select Reson For ReApply</label>
                                    <asp:DropDownList ID="ddlStudentResonForReApply" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="Damage" Value="Damage"></asp:ListItem>
                                        <asp:ListItem Text="Lost" Value="Lost"></asp:ListItem>
                                        <asp:ListItem Text="Size Mismatch" Value="Size Mismatch"></asp:ListItem>
                                        <asp:ListItem Text="Emergency T-Shirt" Value="Emergency T-Shirt"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-md-4 col-md-offset-1">
                                    <asp:LinkButton ID="btnSubmitStudentTshirtRequest" OnClick="btnSubmitStudentTshirtRequest_Click" CssClass="btn btn-dropbox btn-black brandFont" runat="server"><span class="fa fa-send">&nbsp; <i class="brandFont"> Request T-shirt</i></span>  </asp:LinkButton>
                                </div>
                            </div>

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
                                <h4>
                                    <p class="text-info">
                                        <asp:Label ID="lblChatProjectTitle" runat="server" Text=""></asp:Label>
                                    </p>
                                </h4>
                                <span style="display: none;">
                                    <asp:Label ID="lblChatPDID" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblChatProjectStatus" runat="server" Text=""></asp:Label>
                                </span>
                                <ul class="chat" style="margin-top: 10px;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <%--       <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="1000"></asp:Timer>--%>
                                            <asp:Repeater runat="server" ID="rptDiscussionForum">
                                                <ItemTemplate>
                                                    <asp:Panel ID="Panel1" Visible='<%# Eval("User_Type").ToString() == "Manager" ? true : false %>' runat="server">
                                                        <li class="left clearfix"><span class="chat-img pull-left">
                                                            <img src="../../CSS/Images/Chat_U.png" style="width: 40px; height: 40px;" alt="User Avatar" class="img-circle">
                                                        </span>
                                                            <div class="chat-body clearfix">
                                                                <div class="header">
                                                                    <strong class="primary-font"><%#Eval("ManagerName")%></strong>
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
                                                    <asp:Panel ID="Panel2" Visible='<%# Eval("User_Type").ToString() == "Student" ? true : false %>' runat="server">
                                                        <li class="right clearfix">
                                                            <span class="chat-img pull-right">
                                                                <img src="../../CSS/Images/Chat_Me.png" alt="User Avatar" style="width: 40px; height: 40px;" class="img-circle">
                                                            </span>
                                                            <div class="chat-body clearfix">
                                                                <div class="header">
                                                                    <small class="text-muted" style="font-size: xx-small;">
                                                                        <%#Eval("ProjectStatus")%> - <%#Eval("Comment_Type")%>
                                                                    </small>
                                                                    <strong class="pull-right primary-font"><%#Eval("StudentName")%></strong>
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
                                        <%--                   <Triggers >
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
        function POP_Tshirt() {
            $('#POP_Tshirt').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });

        }
    </script>
    <script type="text/javascript">
        function POP_StudentTshirt() {
            $('#POP_StudentTshirt').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });

        }
    </script>
    <script type="text/javascript">
        function POP_AddProjectProposal() {
            $('#POP_AddProjectProposal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
        }
    </script>
    <script type="text/javascript">
        function POP_Chat() {
            $('#POP_Chat').modal({

                show: true
            });
        }
    </script>


    <script type="text/javascript">
        function POP_RequestToManager() {
            $('#POP_RequestToManager').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });

        }


    </script>
    <script>
        function CheckForm() {
            if ($('#<%=ddlProjectType.ClientID %>:selected').eval() == "[Select]") {
                alert('Please input');
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSaveProposal.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script>
        window.onbeforeunload = DisableButton;
        function DisableButton() {
            <%-- document.getElementById('<%= btnSaveProposal.ClientID %>').disabled = "disabled";
            __doPostBack('<%= btnSaveProposal.ClientID %>', '');--%>
            <%--    var e = document.getElementById('<%= ddlProjectType.ClientID %>');
var value = e.options[e.selectedIndex].value;

var text = e.options[e.selectedIndex].text;--%>

            if ($('#ContentPlaceHolder1_txtProjectTitle').val().trim() != '') {
                if ($('#ContentPlaceHolder1_ddlProjectType').val().trim() != '') {
                    if ($('#ContentPlaceHolder1_txtProjectObjectives').val().trim() != '') {
                        if ($('#ContentPlaceHolder1_txtProjectPlan').val().trim() != '') {
                            if ($('#ContentPlaceHolder1_txtProposalPlaceofImplementation').val().trim() != '') {
                                if ($('#ContentPlaceHolder1_txtTotalBeneficiaries').val().trim() != '') {
                                    if ($('#ContentPlaceHolder1_txtProposedBeneficiaries').val().trim() != '') {
                                        if ($('#ContentPlaceHolder1_txtCurrentSituation').val().trim() != '') {
                                            if ($('#ContentPlaceHolder1_txtProposedStartDate').val().trim() != '') {
                                                if ($('#ContentPlaceHolder1_txtProposedEndDate').val().trim() != '') {
                                                    if ($('#ContentPlaceHolder1_ddlProjectType').val().trim() != '[Select]') {
                                                        document.getElementById('<%= btnSaveProposal.ClientID %>').disabled = "disabled";


                                                        }
                                                        else {
                                                            toastr.error('Select Project Type', 'Warning!', { timeOut: 5000 });
                                                        }

                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
        }
       
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
    </script>
    <script>
        function CalculateTargetDays() {

            const date1 = new Date(document.getElementById('<%= txtProposedStartDate.ClientID %>').value);
            const date2 = new Date(document.getElementById('<%= txtProposedEndDate.ClientID %>').value);
            const diffTime = Math.abs(date2.getTime() - date1.getTime());
            const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
            if (isNaN(diffDays)) {
                document.getElementById('<%= lblProposedProjectTargetDays.ClientID %>').textContent = 0;
            }
            else if (diffDays == 0) {
                document.getElementById('<%= lblProposedProjectTargetDays.ClientID %>').textContent = 1;
            }
            else {
                document.getElementById('<%= lblProposedProjectTargetDays.ClientID %>').textContent = diffDays + 1;
            }
        }


    </script>

</asp:Content>