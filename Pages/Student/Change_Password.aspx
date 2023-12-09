<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="Change_Password.aspx.cs" Inherits="Pages_Student_Change_Password" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="vmMain" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading">
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label>
                                                    Enter Captch
                                                     <asp:CustomValidator Display="Dynamic" ErrorMessage="Invalid. Please try again." OnServerValidate="ValidateCaptcha"
                                                         runat="server" />
                                                </label>
                                                <asp:TextBox ID="txtCaptcha" CssClass="form-control" placeholder="Enter Captcha" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Medium" CaptchaLength="5"
                                                    CaptchaHeight="60" CaptchaWidth="200" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                                    FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:ImageButton ImageUrl="~/CSS/Images/refresh.png" runat="server" CausesValidation="false" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                   <asp:Button ID="btnSubmit" CssClass="btn btn-black btn-info" runat="server" Text="Submit" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vmSub" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading">
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>

            </div>
        </div>
    </div>
</asp:Content>

