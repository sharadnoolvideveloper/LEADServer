<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="Manager_Scheduling_Task.aspx.cs" Inherits="Pages_Manager_Manager_Scheduling_Task" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      
          <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>

    <script src="../../JS/ManagerJS/app.v2.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/toster.js"></script>
       <link href="../../CSS/CommonCSS/MultiSelect.css" rel="stylesheet" />
       <script src="../../JS/CommonJS/MultiSelect.js"></script>
   
   
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
            margin-top: -10px;
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
            .lstbox{

            }
    </style>
    <script type="text/javascript">
        function cnt(text) {
            var a = text.value;
            var type = $("#<%=lstScheduleType.ClientID%>  OPTION:selected").val();
            if (type == "SMS") {
                text.parentNode.getElementsByTagName('span')[0].innerHTML = 149 - a.length + "/149";
                if (text.value.length > 149) {
                    $("#<%=txtSchedule_Message.ClientID%>").css("color", "red");
                    $("#<%=txtSchedule_Message.ClientID%>").css("border-color", "red");

                }
                else {
                    $("#<%=txtSchedule_Message.ClientID%>").css("color", "#000");
                    $("#<%=txtSchedule_Message.ClientID%>").css("border-color", "#66afe9");
                }
            }
            else {
                text.parentNode.getElementsByTagName('span')[0].innerHTML = 0 + a.length;
            }
        }
    </script>
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
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="navbar navbar-inverse ">
            <div class="navbar-header">
                <button type="button" class="btn btn-facebook text-center brandFont1" style="height: 50px;" data-toggle="modal" data-target="#Message_Schedule">
                    <i class="fa fa-plus"></i>&nbsp;&nbsp;&nbsp;New Schedule
                </button>
                <span style="margin-right: 20px; margin-top: 5px;" class="pull-right visible-lg visible-md">
                    <h4 class="brandFont">&nbsp;&nbsp;&nbsp;&nbsp;Message Scheduler</h4>
                </span>
                <a href="DashBoard.aspx?vwType=DashBoard" style="margin-top: 10px;" class="pull-right">&nbsp;&nbsp;&nbsp;<i class="fa fa-home fa-2x"></i></a>
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
                                                            <span class="input-group-addon"><i class="fa fa-search"></i>&nbsp; Search</span>
                                                            <input type="text" id="txtTaskSearch" onkeyup="SearchTaskDetail()" placeholder="Search" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2 hidden-xs hidden-sm">
                                                        <asp:DropDownList ID="ddlAcademicYear" CssClass="form-control" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="table brandFont" style="height: 650px; overflow: auto;" id="SearchList">
                                                        <div class="col-sm-6">
                                                            <h3 class="text-danger text-center brandFont2">
                                                                <b>Up-coming Schedules</b>
                                                            </h3>
                                                            <asp:Repeater ID="rptSchedules" OnItemCommand="rptSchedules_ItemCommand" OnItemDataBound="rptSchedules_ItemDataBound" runat="server">
                                                                <ItemTemplate>
                                                                    <div class="row">
                                                                        <div class="col-md-12 col-xs-12 hvr-float-shadow ">
                                                                            <div class="panel panel-primary">
                                                                                <asp:Panel ID="Panel1" runat="server">
                                                                                    <div class="panel-footer">

                                                                                        <div class="row">
                                                                                            <div class="col-md-12 col-xs-12" style="letter-spacing: 2px;">
                                                                                                <span class="pull-right" style="background-color: #dda94e; border: solid; color: white; padding: 3px 3px 2px 3px;"><%# Container.ItemIndex + 1 %>   </span>
                                                                                                <asp:LinkButton ID="btnStatus" CssClass="btn btn-circle" CommandArgument='<%# Eval("ScheduleId")+"_"+("Delete") %>' runat="server"><span class="fa fa-remove"></span> </asp:LinkButton>
                                                                                                <asp:LinkButton ID="btnEdit" CssClass="btn btn-circle" CommandArgument='<%# Eval("ScheduleId")+"_"+("Edit") %>' runat="server"><span class="fa fa-pencil"></span> </asp:LinkButton>
                                                                                                &nbsp;  Date : <%# Eval("Schedule_Date") %>  &nbsp;&nbsp;&nbsp;  Status :  
                                                                                              <asp:Label ID="lblProgress" Font-Size="Small" runat="server" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row form-group">
                                                                                            <div class="col-md-12 col-xs-12">
                                                                                                <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status") %>' />
                                                                                                <asp:Label ID="lblScheduleId" runat="server" Visible="false" Text='<%# Eval("ScheduleId") %>' />
                                                                                                Category : <%# Eval("Schedule_Type") %> &nbsp;&nbsp;  Type: <%# Eval("Parameter_Type") %>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-12 col-xs-12 title">
                                                                                                Message :  <%# Eval("Schedule_Message") %>
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>

                                                        <div class="col-sm-6">
                                                               <h3 class="text-success text-center brandFont2">
                                                             <b>   Completed Schedules</b>
                                                            </h3>
                                                            <asp:Repeater ID="rptCompletedSchedule" OnItemDataBound="rptCompletedSchedule_ItemDataBound" runat="server">
                                                                <ItemTemplate>
                                                                    <div class="row">
                                                                        <div class="col-md-12 col-xs-12 hvr-float-shadow ">
                                                                            <div class="panel panel-primary">
                                                                                <asp:Panel ID="Panel1" runat="server">
                                                                                    <div class="panel-footer">

                                                                                        <div class="row">
                                                                                            <div class="col-md-12 col-xs-12" style="letter-spacing: 2px;">
                                                                                                <span class="pull-right" style="background-color: forestgreen; border: solid; color: white; padding: 3px 3px 2px 3px;"><%# Container.ItemIndex + 1 %>   </span>
                                                                                                <asp:LinkButton ID="btnStatus" Visible="false" CssClass="btn btn-circle" CommandArgument='<%# Eval("ScheduleId") %>' runat="server"><span class="fa fa-remove"></span> </asp:LinkButton>
                                                                                                Date : <%# Eval("Schedule_Date") %>  &nbsp;&nbsp;&nbsp;  Status :  
                                                                                    <asp:Label ID="lblProgress" Font-Size="Small" runat="server" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row form-group">
                                                                                            <div class="col-md-12 col-xs-12">
                                                                                                <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status") %>' />
                                                                                                <asp:Label ID="lblScheduleId" runat="server" Visible="false" Text='<%# Eval("ScheduleId") %>' />
                                                                                                Category : <%# Eval("Schedule_Type") %> &nbsp;&nbsp;  Type: <%# Eval("Parameter_Type") %>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-12 col-xs-12 title">
                                                                                                Message :  <%# Eval("Schedule_Message") %>
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>
                                                                                </asp:Panel>
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
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal  fade brandFont" id="Message_Schedule" role="dialog" style="height:650px;overflow:auto;">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #224fd7; color: white;">
                        <button type="button" class="close" data-dismiss="modal" style="color: white; border-radius: 100px;">&times;</button>
                        <h3 class="modal-title brandFont1"><span class="fa fa-calendar"></span>&nbsp; Scheduler                   
                        </h3>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel" style="border: 0px;">

                                            <div class="panel-body">
                                                <asp:Label ID="lblScheduleId" Visible="false" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="lblActionType" Visible="false" runat="server" Text=""></asp:Label>
                                                <div class="row">
                                                    <div class="col-md-6 col-sm-6 col-xs-6 form-group">
                                                             <asp:LinkButton ID="btnScheduleSave" OnClick="btnScheduleSave_Click" ValidationGroup="Schedule" CssClass="btn btn-block btn-facebook" runat="server"><span class="fa fa-save"></span>&nbsp; Schedule</asp:LinkButton>                                 
                                                    </div>
                                                    <div class="col-md-6 col-sm-6 col-xs-6 form-group">
                                                          <asp:LinkButton ID="btnReShedule" OnClick="btnReShedule_Click" ValidationGroup="Schedule" CssClass="btn btn-block btn-primary" runat="server"><span class="fa fa-save"></span>&nbsp;Re-Schedule</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <label for="drop_stype">Select Student Type *</label>
                                                   
                                                        <asp:ListBox ID="lstStudentType" CssClass="lstbox" runat="server" SelectionMode="Multiple">
                                                            <asp:ListItem Text="Student"  Value="Student"></asp:ListItem>
                                                            <asp:ListItem Text="Leader" Value="Leader"></asp:ListItem>
                                                            <asp:ListItem Text="Master Leader" Value="Master Leader"></asp:ListItem>
                                                            <asp:ListItem Text="Lead Ambassador" Value="Lead Ambassador"></asp:ListItem>
                                                            <asp:ListItem Text="Lead Intern" Value="Lead Intern"></asp:ListItem>
                                                        </asp:ListBox>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label for="drop_status">Select Staus *</label>
                                                        <asp:DropDownList ID="ddlProjectStatus" runat="server" CssClass="form-control">
                                                            <asp:ListItem>[All]</asp:ListItem>
                                                            <asp:ListItem Text="Proposed" Value="Proposed"></asp:ListItem>
                                                            <asp:ListItem  Text="Approved" Value="Approved"></asp:ListItem>
                                                            <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                                            <asp:ListItem Text="Modification" Value="RequestForModification"></asp:ListItem>
                                                            <asp:ListItem Text="Completion Request" Value="RequestForCompletion"></asp:ListItem>
                                                            <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                                        
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>
                                                                    Schedule Date
                                                <asp:RequiredFieldValidator runat="server" ID="rfvtxtClave" ForeColor="Red" ControlToValidate="txtScheduleDate" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Schedule"></asp:RequiredFieldValidator>
                                                                </label>
                                                                <asp:TextBox ID="txtScheduleDate" autocomplete="off" CausesValidation="true" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <label class="text-center">&nbsp;HH</label>
                                                                <asp:DropDownList ID="ddlHours" CssClass="form-control" runat="server">
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
                                                            <div class="col-sm-2">
                                                                <label class="text-center">&nbsp;MM</label>
                                                                <asp:DropDownList ID="ddlMinutes" CssClass="form-control" runat="server">
                                                                      <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                    <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                                    <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                    <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                                                    <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <label>Am/Pm</label>
                                                                    <asp:DropDownList ID="ddlAMPM" CssClass="form-control" runat="server">
                                                                      <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                    <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                           <div class="col-sm-4">
                                                                                   <label>Schedule Type</label><br />
                                                                <asp:ListBox ID="lstScheduleType" CssClass="lstbox"   runat="server"  SelectionMode="Multiple">
                                                                    <asp:ListItem Text="SMS" Value="SMS"></asp:ListItem>
                                                                    <asp:ListItem Text="Mail" Value="Mail"></asp:ListItem>
                                                                    <asp:ListItem  Text="Notificatin" Selected="True" Value="Notification"></asp:ListItem>
                                                                </asp:ListBox>
                                                           </div>

                                                        </div>
                                                      
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>College</label>
                                                               
                                                                <asp:DropDownList ID="ddlCollege" CssClass="form-control" runat="server">
                                                                    
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>
                                                                    Schedule Description
                                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtScheduleDescription" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Schedule"></asp:RequiredFieldValidator>
                                                                </label>
                                                                <asp:TextBox ID="txtScheduleDescription" MaxLength="40" placeholder="Enter Description" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>
                                                                    Schedule Message
                                                                </label>
                                                                <span class="pull-right text-muted text-info" style="font-size: medium;"></span>
                                                                <asp:TextBox ID="txtSchedule_Message" CssClass="form-control" TextMode="MultiLine" autocomplete="off" placeholder="Enter Message Here" MaxLength="149" onkeyup="cnt(this);" onkeydown="cnt(this);" Rows="3" runat="server"></asp:TextBox>
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
            </div>
        </div>
        <script type="text/javascript">
            function SearchTaskDetail() {
                var input, filter, ul, li, a, i;
                input = document.getElementById("txtTaskSearch");
                filter = input.value.toUpperCase();
                ul = document.getElementById("SearchList");
                li = ul.getElementsByClassName("hvr-float-shadow");
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
            function Message_Schedule() {
                $('#Message_Schedule').modal({
                    backdrop: 'static',
                    keyboard: true,
                    show: true
                });
                  $("#<%=txtSchedule_Message.ClientID%>").val = "";
                $("#<%=txtScheduleDescription.ClientID%>").val = "";
                $("#<%=txtScheduleDate.ClientID%>").val = "";
              
                //$('#Message_Schedule').modal('show');
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

       
        <script type="text/javascript">
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
        </script>
</asp:Content>

