<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_SendDetails_Managers.aspx.cs" Inherits="Pages_Admin_Admin_SendDetails_Managers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
      <script src="../../JS/StudentJS/bootstrap.min.js"></script> 
     <script src="../../JS/CommonJS/toster.js"></script>
      <script src="../../JS/CommonJS/bootstrap-datepicker_fun.min.js"></script>
      <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />

  
  
  
    <style>
        .list-group-item {
            padding: 1px 1px;
        }
    </style>

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

                        //  row.style.backgroundColor = "aqua";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            //  row.style.backgroundColor = "#C2D69B";

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <div class="container-fluid">
    <div style="background-color: whitesmoke">
        <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel">
                            <div class="pillbox">
                                <h4>SMS / E-MAIL SERVICE
                                  
                                </h4>

                            </div>
                            <div class="panel-body">                             
                              <div class="row">
                                  <div class="col-md-12">

                                 
                                  <label for="ddlSandbox">Select Sandbox</label>
                                  <asp:DropDownList ID="ddlSandbox" CssClass="form-control" OnSelectedIndexChanged="ddlSandbox_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                      <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                       <asp:ListItem Text="KARNATAKA" Value="KARNATAKA"></asp:ListItem>
                                       <asp:ListItem Text="NIZAMABAD" Value="NIZAMABAD"></asp:ListItem>
                                       <asp:ListItem Text="NALGONDA" Value="NALGONDA"></asp:ListItem>
                                       <asp:ListItem Text="EKSOCH" Value="EKSOCH"></asp:ListItem>

                                  </asp:DropDownList>
                                       </div>
                              </div>
                               
                            </div>
                            <div class="pillbox">
                                  <br />
                                <div class="row">
                                    <div class="col-sm-12" >
                                        <div style="padding:5px;">
                                            <asp:LinkButton ID="btnMailMain" CssClass="btn btn-danger btn-block" OnClick="btnMailMain_Click" runat="server">Send Mail &nbsp;  <span class="fa fa-envelope glyphicon-envelope"></span> </asp:LinkButton>
                                        </div>
                                    </div>
                                      </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-12">
                                         <div style="padding:5px;">
                                        <asp:LinkButton ID="btnSMSMain" CssClass="btn btn-primary btn-block" OnClick="btnSMSMain_Click" runat="server"> Send Messages &nbsp; <span class="fa fa-phone"></span> </asp:LinkButton>
                                             </div>
                                    </div>
                                </div>
                                  <br />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel">
                            <div class="pillbox">
                                <h4>Managers LIST
                                    <span class="pull-right"><b>
                                        <asp:Label ID="lblTotalManagersCount" runat="server" Text="0"></asp:Label></b></span>
                                </h4>
                            </div>
                              <div class="input-group">
                                    <div class="input-group-addon bg-info">
                                        <span class="input-group-text">Search</span>
                                    </div>
                                    <input type="text" id="txtManagerSearch" onkeyup="SearchManagerDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." class="form-control" />
                                </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row" style="overflow: auto; height: 620px;">
                                            <ul class="list-group" id="SearchList"  style="padding:15px;">
                                                <asp:Repeater ID="rptManagers" runat="server">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="ChkSelectAll" CssClass="checkbox checkbox-circle checkbox-danger" Text="All" onclick="checkAll(this);" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <li class="list-group-item" >
                                                            <a href="#" class="list-group-item">
                                                            <asp:CheckBox ID="ChkManager" Font-Size="Smaller" ForeColor="Black" Text='<% #Eval("ManagerName") %>' runat="server" />
                                                            <asp:Label ID="lblManagerCode" Visible="false" runat="server" Text='<% #Eval("ManagerId") %>'></asp:Label>
                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<% #Eval("MobileNo") %>'></asp:Label>
                                                            <asp:Label ID="lblMailId" runat="server" Text='<% #Eval("MailId") %>'></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel">
                            <div class="pillbox">
                                <h4>
                                    <asp:Label ID="lblsmsmail" runat="server" Text="SMS / E-MAIL"></asp:Label>
                                    <span class="pull-right text-warning">
                                        <asp:Label ID="lblcredits" Font-Size="Smaller" runat="server" Text=""></asp:Label>
                                    </span>
                                </h4>
                            </div>
                            <div class="panel-body" style="overflow: auto; height: 670px;">
                                <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View ID="vwSMS" runat="server">
                                        <legend><b>(<asp:Label ID="lblSMSCount" runat="server" Text=""></asp:Label>)</b></legend>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="txtToSMS">Message </label>
                                                <asp:TextBox ID="txtToSMS" Visible="false" runat="server" CssClass="form-control"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:TextBox ID="txtSMSMessage" runat="server" placeholder="Enter content" CssClass="form-control" Rows="6" Columns="6"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lblCountMobile" runat="server" Font-Bold="true"
                                                ForeColor="Purple" Text=""></asp:Label>
                                            <asp:Label ID="lbl_to_sms" Visible="false" runat="server" Font-Bold="true" Text="To"></asp:Label>
                                        </div>
                                        <div class="panel-footer">
                                           <div class="row">
                                                 <div class="col-md-12">
                                                    <asp:Button ID="BtnSendNotification" OnClick="BtnSendNotification_Click" CssClass="btn btn-success btn-block" OnClientClick="this.disabled = true;this.value='Notification Sending Please Wait...';" UseSubmitBehavior="False" runat="server" Text="Send Notification" />
                                                </div>
                                            </div>
                                             <br />
                                              <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Button ID="btnSendSMS" OnClick="btnSendSMS_Click" CssClass="btn btn-primary btn-block" OnClientClick="this.disabled = true;this.value='Sending Message Please Wait...';" UseSubmitBehavior="False" runat="server" Text="Send SMS" />
                                                </div>                               
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwMail" runat="server">
                                        <legend><b>Send Email</b></legend>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <label for="txtSubject">Subject</label>
                                                <asp:TextBox ID="txtSubject" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <label for="txtMailMessage">Message</label>
                                                <asp:TextBox ID="txtMailMessage" runat="server" CssClass="form-control" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:Label ID="lblMres" ForeColor="Purple" Font-Bold="true" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="panel-footer">
                                            <asp:LinkButton ID="btnSendMail" runat="server" OnClick="btnSendMail_Click" CssClass="btn btn-success">Send Mail &nbsp;  <span class="fa fa-envelope"></span> </asp:LinkButton>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>      
    </div>
      <div id="POP_Confirm" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-body">
                        <h2>Are You Sure You Want to Send SMS ?
                        </h2>
                        <div class="row">
                            <div class="col-md-offset-2 col-md-2">
                                <asp:LinkButton ID="btnNo" OnClick="btnNo_Click" CssClass="btn btn-danger" runat="server">NO</asp:LinkButton>
                            </div>
                            <div class="col-md-offset-3 col-md-2">
                                <asp:LinkButton ID="btnYes" OnClick="btnYes_Click" CssClass="btn btn-info" runat="server">YES</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
      <script type="text/javascript">
        function SearchManagerDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtManagerSearch");
            filter = input.value.toUpperCase();
            ul = document.getElementById("SearchList");
            li = ul.getElementsByTagName("li");
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
         function POP_Confirm() {
             $('#POP_Confirm').modal({
                show: true
            });
        }
       </script>
</asp:Content>

