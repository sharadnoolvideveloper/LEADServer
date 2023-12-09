<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_SendGeneralNotice.aspx.cs" Inherits="Pages_Admin_Admin_SendGeneralNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
      <script src="../../JS/StudentJS/bootstrap.min.js"></script> 
     <script src="../../JS/CommonJS/toster.js"></script>
      
      <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/Custumized.css" rel="stylesheet" />

    <script type="text/javascript">
        function success(msg) {
            toastr.options.timeOut = 4500; //3.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.success(msg);
        }
        function warning(msg) {
            toastr.options.timeOut = 4500; //3.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.warning(msg);
        }
        function info(msg) {
            toastr.options.timeOut = 4500; //3.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.info(msg);
        }
        function error(msg) {
            toastr.options.timeOut = 4500; //3.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.error(msg);
        }
    </script>
      <script type="text/javascript">
         function cnt(text) {
             var a = text.value;
             text.parentNode.getElementsByTagName('span')[0].innerHTML = 149 - a.length + "/149";
             if (text.value.length > 149)
             {
                 $("#<%=txtMessage.ClientID%>").css("color", "red");
                  $("#<%=txtMessage.ClientID%>").css("border-color", "red");           

             }
             else
             {
                 $("#<%=txtMessage.ClientID%>").css("color", "#000");
                 $("#<%=txtMessage.ClientID%>").css("border-color", "#66afe9");
             }

         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
        <div class="panel">
            <div class="panel-heading">
                <h3>General SMS Sending
                    &nbsp;<asp:Label ID="lblSMSCount" runat="server" Text=""></asp:Label>
                </h3>
            </div>
            <div class="panel-body">
                <div class="container-fluid">
                <div class="row" data-plugin="matchHeight" data-by-row="true">
                    <div class="col-lg-12">
                       
                        <div class="row">
                         
                            <div class="col-md-2">
                                  <label>Mobileno &nbsp;
                                       <span class="text-danger" style="font-size:x-small">Enter key After one Mobile No</span></label>
                            
                               <asp:TextBox ID="txtMobileNo"  CssClass="form-control" Height="200px" TextMode="MultiLine" Rows="1" Columns="20" placeholder="Enter Mobile No" runat="server"></asp:TextBox>
                            </div>
                               <div class="col-md-6">
                                <label>Message</label>
                                 <span class="pull-right text-muted text-info" style="font-size: medium;"></span>
                                <asp:TextBox ID="txtMessage" CssClass="form-control" Height="200px" onkeyup="cnt(this);" onkeydown="cnt(this);" TextMode="MultiLine" placeholder="Enter Message Here" runat="server" MaxLength="149"></asp:TextBox>
                            </div>
                        </div>                
                   <br />
                        <div class="row">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnSendSMS" onclick="btnSendSMS_Click" CssClass="btn btn-sm btn-primary btn-block" OnClientClick="this.disabled = true; this.value = 'Please Wait SMS Sending...';" UseSubmitBehavior="false" runat="server" Text="Send SMS" />
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

   <script type="text/javascript">
         function POP_Confirm() {
             $('#POP_Confirm').modal({
                show: true
            });
        }
       </script>
</asp:Content>

