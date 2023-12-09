<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_SendGeneralMailNotice.aspx.cs" Inherits="Pages_Admin_Admin_SendGeneralMailNotice" ValidateRequest="false" %>

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
        <script type="text/javascript" src="//tinymce.cachefly.net/4.0/tinymce.min.js"></script>
    <script type="text/javascript">
        tinymce.init({ selector: 'textarea#ContentPlaceHolder1_txtMessage', });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="panel">
            <div class="panel-heading">
                <h3>General Mail Sending
                 
                </h3>
            </div>
            <div class="panel-body">
                <div class="container-fluid">
                <div class="row" data-plugin="matchHeight" data-by-row="true">
                    <div class="col-lg-12">
                       
                        <div class="row">
                         
                            <div class="col-md-3">
                                  <label>Mail Id &nbsp;
                                       <span class="text-danger" style="font-size:x-small">Enter key After one Mail Id</span></label>
                            
                               <asp:TextBox ID="txtMailId"  CssClass="form-control" Height="265px" TextMode="MultiLine" Rows="1" Columns="20" placeholder="Enter Mail Id" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-9">
                            <div class="row">
                                  <div class="col-md-6">
                                    <label>Subject</label>
                                    <span class="pull-right text-muted text-info" style="font-size: medium;"></span>
                                    <asp:TextBox ID="txtSubject" CssClass="form-control" placeholder="Enter Subject Here" runat="server"></asp:TextBox>
                                </div>
                            </div>
                               
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Message</label>
                                    <span class="pull-right text-muted text-info" style="font-size: medium;"></span>
                                    <asp:TextBox ID="txtMessage" CssClass="form-control" Height="200px" TextMode="MultiLine" placeholder="Enter Mail body Here" runat="server"></asp:TextBox>
                                </div>
                            </div>
                             </div>
                        </div>                
                   <br />
                        <div class="row">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnSendMaild" onclick="btnSendMaild_Click" CssClass="btn btn-sm btn-success btn-block" OnClientClick="this.disabled = true; this.value = 'Please Wait SMS Sending...';" UseSubmitBehavior="false" runat="server" Text="Send Mail" />
                               </div>
                        </div>
                    </div>
                </div>
                    </div>
            </div>
        </div>
</asp:Content>

