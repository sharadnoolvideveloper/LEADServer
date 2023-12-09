<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="ManagerSocialLinks.aspx.cs" Inherits="Pages_Manager_ManagerSocialLinks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
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
    <br />
    <div class="container-fluid">
        <div class="panel">
           
              
           
            <div class="panel-body">
                 <h2 class="text-center text-uppercase text-warning"><b> Social Network Link </b></h2>
                 <div class="row">
            <div class="col-md-12">
                <div class="container">
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <label for="txtFacebook"><span class="fa fa-facebook fa-2x"></span>&nbsp;  Facebook</label>
                            <asp:TextBox ID="txtFacebook" CssClass="form-control" placeholder="Paste Your Facebook Page URL" runat="server"></asp:TextBox>

                            <a id="a_Facebook" target="_blank" runat="server">Click to Check <span class="fa fa-facebook"></span> Facebook Link</a>
                        </div>
                    </div>
                       <br />
                     <div class="row form-group">
                        <div class="col-md-12">
                            <label for="txtTwitter"><span class="fa fa-twitter fa-2x"></span>&nbsp; Twitter</label>
                            <asp:TextBox ID="txtTwitter" CssClass="form-control" placeholder="Paste Your Twitter Page URL" runat="server"></asp:TextBox>

                            <a id="a_Twitter" target="_blank" runat="server">Click to Check <span class="fa fa-twitter"></span> Twitter Link</a>
                        </div>
                    </div>
                    <br />
                     <div class="row form-group">
                         <div class="col-md-12">
                            <label for="txtInstaGram"><span class="fa fa-instagram fa-2x"></span>&nbsp; Insta Gram</label>
                            <asp:TextBox ID="txtInstaGram" CssClass="form-control" placeholder="Paste Your InstaGram URL" runat="server"></asp:TextBox>
                             <a id="a_Instagram" target="_blank" runat="server">Click to Check <span class="fa fa-instagram"></span> InstaGram Link</a>
                        </div>
                    </div>
                    <br />
                      <div class="row form-group">
                         <div class="col-md-12">
                            <label for="txtWhatsApp"><span class="fa fa-whatsapp fa-2x"></span>&nbsp; WhatsApp</label>
                            <asp:TextBox ID="txtWhatsApp" CssClass="form-control" placeholder="Enter Your WhatsApp Number" runat="server"></asp:TextBox>
                             <a id="a_WhatsApp" target="_blank" runat="server">Click to Check <span class="fa fa-whatsapp"></span> Whats App Link</a>
                        </div>
                    </div>
                    <br />
                         <br />
                    <div class="row form-group">
                        <div class="col-md-12">
                            <asp:LinkButton ID="btnSave" CssClass="btn btn-facebook btn-block" OnClick="btnSave_Click" runat="server"><span class="fa fa-save"></span> &nbsp; Save </asp:LinkButton>
                        </div>
                    </div>
                </div>
                 <div style="height:250px;"></div>
            </div>
        </div>
            </div>
        </div>
       
    </div>
   
</asp:Content>

