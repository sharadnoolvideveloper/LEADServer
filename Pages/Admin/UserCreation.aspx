<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="UserCreation.aspx.cs" Inherits="Pages_Admin_UserCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
      <script src="../../JS/StudentJS/bootstrap.min.js"></script> 
     <script src="../../JS/CommonJS/toster.js"></script>
      <script src="../../JS/CommonJS/bootstrap-datepicker_fun.min.js"></script>
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
    <style>
        .list-group-item
        {
            padding:1px 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2 hidden">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel">
                            <div class="panel-heading">
                                <h4>Managers List</h4>
                            </div>
                            <div class="panel-body">
                                <ul class="list-group" style="overflow: auto; height: 600px;">
                                    <asp:Repeater ID="rptManagersList" OnItemCommand="rptManagersList_ItemCommand" runat="server">
                                        <ItemTemplate>
                                            <li class="list-group-item">
                                                <asp:LinkButton ID="btnManagerClick" runat="server">
                                                    <asp:Label ID="lblManagerid" Visible="false" runat="server" Text='<% #Eval("ManagerId") %>'></asp:Label>
                                                    <asp:Label ID="lblManagerName" runat="server" Text='<% #Eval("ManagerName") %>'></asp:Label>
                                                    <span class="badge m-r bg-primary">
                                                        <asp:Label ID="lblManagerDistrict" runat="server" Text='<% #Eval("StateName") %>'></asp:Label></span>
                                                </asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-12">

                        <div class="panel">
                            <div class="panel-heading">
                                <h4>User Creation
                        <span class="pull-right">
                            <asp:LinkButton ID="btnCreateUser" ValidationGroup="Manager" CssClass="btn btn-success" OnClick="btnCreateUser_Click" runat="server">  <span class="fa fa-save"></span> &nbsp;Save </asp:LinkButton>
                            <asp:LinkButton ID="btnClear" CssClass="btn btn-primary" OnClick="btnClear_Click" runat="server">  <span class="fa fa-remove"></span> &nbsp;Clear </asp:LinkButton>
                        </span>
                                </h4>
                            </div>
                            <br />
                            <div class="panel-body">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-md-5">
                                              
                                            <div class="input-group form-group">
                                                <div class="input-group-addon ">
                                                    <span class="input-group-text">Manager Name</span>
                                                    <asp:RequiredFieldValidator ID="rfvtxtManagerName" ControlToValidate="txtManagerName" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                </div>
                                                <asp:TextBox ID="txtManagerName" CssClass="form-control" placeholder="Enter Manager Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                             
                                            <div class="input-group form-group">
                                                <div class="input-group-addon ">
                                                    <span class="input-group-text">Mail Id</span>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMailId" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                </div>
                                                <asp:TextBox ID="txtMailId" CssClass="form-control" placeholder="Enter Manager Mail Id" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                          
                                           
                                                       
                                            <div class="input-group form-group">
                                                <div class="input-group-addon ">
                                                    <span class="input-group-text">Mobile No
                                                        </span>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMobileNo" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" SetFocusOnError="True" ValidationGroup="Manager" ForeColor="DarkRed"
                                                            ControlToValidate="txtMobileNo" ErrorMessage="* 10 Digit"
                                                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                </div>
                                                <asp:TextBox ID="txtMobileNo" MaxLength="10" CssClass="form-control" placeholder="Enter Mobile No" onkeypress="NumericOnly();" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="input-group form-group">
                                                <div class="input-group-addon ">
                                                    <span class="input-group-text">Address</span>

                                                </div>
                                                <asp:TextBox ID="txtAddress" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Manager Address" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group form-group">
                                                <div class="input-group-addon ">
                                                    <span class="input-group-text">Select Gender</span>

                                                </div>
                                                <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group form-group">
                                                <div class="input-group-addon ">
                                                    <span class="input-group-text">Select Blood Group</span>

                                                </div>
                                                <asp:DropDownList ID="ddlBloodGroup" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                                    <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                                    <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                                    <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                                    <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                                    <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                                    <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                                    <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <div class="input-group form-group">
                                                <div class="input-group-addon ">
                                                    <span class="input-group-text">Select State</span>

                                                </div>
                                                <asp:DropDownList ID="ddlState" CssClass="form-control fun selectpicker" runat="server"></asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton ID="btnListDistrict" CssClass="btn btn-info btn-block" OnClick="btnListDistrict_Click" runat="server">List <span class="pull-right"><i class="fa fa-arrow-down"></i> </span>  </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="panel">

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-3">
                                       
                                        <asp:LinkButton ID="btnListThalukas" CssClass="btn btn-info btn-floating text-center pull-right" OnClick="btnListThalukas_Click" runat="server"> <span class="fa fa-chevron-right"></span></asp:LinkButton>
                                     
                                        <ul class="list-group" style="overflow: auto; height: 360px;">
                                           
                                    <asp:Repeater ID="rptDistrict"  runat="server">
                                        <ItemTemplate>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblDistinctCode" Visible="false" runat="server" Text='<% #Eval("DistrictId") %>'></asp:Label>
                                                <asp:CheckBox ID="ChkDistrict" Text='<% #Eval("DistrictName") %>' runat="server" />
                                                   
                                             
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:LinkButton ID="btnListColleges" CssClass="btn btn-success btn-floating text-center pull-right" OnClick="btnListColleges_Click" runat="server"> <span class="fa fa-chevron-right"></span></asp:LinkButton>
                                          <ul class="list-group" style="overflow: auto; height: 360px;">

                                            <asp:Repeater ID="rptTaluka"  runat="server">
                                                <ItemTemplate>
                                                    <li class="list-group-item">
                                                        <asp:Label ID="lblTalukaId" Visible="false" runat="server" Text='<% #Eval("Id") %>'></asp:Label>
                                                        <asp:CheckBox ID="ChkTaluk" Text='<% #Eval("Taluk_name") %>' runat="server" />
                                                      

                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                           </ul>
                                      
                                    </div>
                                    <div class="col-md-6">
                                         <ul class="list-group" style="overflow: auto; height: 360px;">

                                            <asp:Repeater ID="rptColleges"   runat="server">
                                                
                                                <ItemTemplate>
                                                    <li class="list-group-item">
                                                        <asp:Label ID="lblCollegeCode" Visible="false" runat="server" Text='<% #Eval("CollegeId") %>'></asp:Label>
                                                         <asp:Label ID="lblTalukaId" Visible="false" runat="server" Text='<% #Eval("TalukId") %>'></asp:Label>

                                                        <asp:CheckBox ID="ChkCollege" Font-Size="Small" Text='<% #Eval("College_Name") %>' runat="server" />
                                                         <span class="badge m-r bg-primary">
                                                        <asp:Label ID="lblManagerDistrict" runat="server" Text='<% #Eval("taluk_name") %>'></asp:Label></span>

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




    </div>




    <script type="text/javascript">

        jQuery(document).ready(function () {
            // Date Picker
            jQuery('.datepicker').datepicker({
                format: "dd-mm-yyyy",
                autoclose: true,
                todayHighlight: true
            });
        });
    </script>

     <script type="text/javascript">
         function UnSavedColleges() {
             $('#UnSavedColleges').modal({
                 backdrop: 'static',
                 keyboard: true,
                 show: true
             });

        }
    </script>
    <div id="UnSavedColleges" class="modal fade" role="dialog" style="margin-top: 0px">
        <div class="modal-dialog bg-danger" style="width:60%">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>Alloted Colleges List</h3>
                    <p>
                        <asp:Label ID="lblUnsavedColleges" runat="server" Text=""></asp:Label>
                    </p>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <a class="pull-right text-right" data-dismiss="modal">
                                <i class="fa fa-arrow-up text-primary fa-2x"></i>
                            </a>

                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
</asp:Content>

