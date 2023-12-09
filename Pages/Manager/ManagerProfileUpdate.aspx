<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="ManagerProfileUpdate.aspx.cs" Inherits="Pages_Manager_ManagerProfileUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="container-fluid">
        <div class="panel panel-info">

            <div class="panel-body">
                <h3>Profile Edit
                    <span class="pull-right">
                        <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" ValidationGroup="Manager" runat="server"><span class="fa fa-check"></span>&nbsp; Save </asp:LinkButton>
                    </span>
                </h3>
                <br />
                <div class="row">
                    <div class="col-md-3">
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
                    <div class="col-md-9">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="txtName">
                                    Enter Name
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Enter Name" ValidationGroup="Manager" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row form-group">
                            <div class="col-md-6">
                                <label for="txtMobileNo">
                                    Enter Mobileno
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMobileNo" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtMobileNo" CssClass="form-control" placeholder="Enter Mobileno" ValidationGroup="Manager" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEmailId">
                                    Enter eMailid
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEmailId" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtEmailId" CssClass="form-control" placeholder="Enter eMailid" ValidationGroup="Manager" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row form-group">

                            <div class="col-md-3">
                                <label>Select Male</label>
                                <div class="row">

                                    <div class="col-md-1"></div>
                                    <div class="col-md-10">
                                        <asp:RadioButton ID="rdoMale" GroupName="Gender" Checked="true" CssClass="radio radio-custom radio-primary" Text="Male" runat="server" />
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-3">
                                <label>Select Female</label>
                                <div class="row">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-10">
                                        <asp:RadioButton ID="rdoFemale" GroupName="Gender" CssClass="radio radio-custom radio-success" Text="Female" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-12">
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

                        <div class="row form-group">
                            <div class="col-md-12">
                                <label for="txtMobileNo">
                                    Address
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtAddress" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Enter Address" ValidationGroup="Manager" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row form-group">
                            <div class="col-md-6">
                                <label for="txtMobileNo">
                                    Facebook
                                    
                                </label>
                                <asp:TextBox ID="txtFacebook" CssClass="form-control" placeholder="Enter Facebook Id" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEmailId">
                                    Twitter
                                    
                                </label>
                                <asp:TextBox ID="txtTwitter" CssClass="form-control" placeholder="Enter Twitter Id" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-6">
                                <label for="txtMobileNo">
                                    InstaGram
                                    
                                </label>
                                <asp:TextBox ID="txtInstaGram" CssClass="form-control" placeholder="Enter InstaGram Id" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="txtEmailId">
                                    WhatsApp
                                   
                                </label>
                                <asp:TextBox ID="txtWhatsApp" CssClass="form-control" placeholder="Enter WhatsApp no" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ddlManagerRecordSet">
                                    Filter Records
                                   
                                </label>
                                <asp:DropDownList ID="ddlManagerRecordSet" CssClass="form-control" runat="server">

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
                        </div>

                    </div>
                </div>


            </div>
        </div>
    </div>


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

