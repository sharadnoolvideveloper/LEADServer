<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="NewUserCreateion.aspx.cs" Inherits="Pages_Admin_NewUserCreateion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        .btn
        {
            text-align:left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel">
                            <div class="panel-heading">
                                <h4>Managers List</h4>
                            </div>
                            <div class="panel-body">
                                <ul class="list-group" style="overflow: auto; height: 650px;">
                                    <asp:Repeater ID="rptManagersList" OnItemCommand="rptManagersList_ItemCommand" OnItemDataBound="rptManagersList_ItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <li class="list-group-item" style="padding:0px;">
                                                <asp:LinkButton ID="btnManagerClick" CssClass="btn btn-success form-control" runat="server">
                                                    <asp:Label ID="lblManagerid" Visible="false" runat="server" Text='<% #Eval("ManagerId") %>'></asp:Label>
                                                    <asp:Label ID="lblManagerName" runat="server" Text='<% #Eval("ManagerName") %>'></asp:Label>
                                                    <span class="badge m-r bg-primary pull-right">
                                                        <asp:Label ID="lblManagerDistrict" Visible="false" runat="server" Text='<% #Eval("StateName") %>'></asp:Label>
                                                         <asp:Label ID="lblProjectCount" runat="server"></asp:Label>
                                                    </span>
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
            <div class="col-md-9">
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
                                              <label for="txtManagerName">Manager Name &nbsp;
                                                   <asp:RequiredFieldValidator ID="rfvtxtManagerName" ControlToValidate="txtManagerName" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                              </label>
                                             <asp:TextBox ID="txtManagerName" CssClass="form-control" placeholder="Enter Manager Name" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                             <label for="txtMailId">eMail Id &nbsp;
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMailId" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                             </label>
                                             <asp:TextBox ID="txtMailId" CssClass="form-control" placeholder="Enter Manager Mail Id" runat="server"></asp:TextBox>
                                            
                                        </div>
                                        <div class="col-md-3">
                                            <label for="txtMobileNo">Mobile No &nbsp;
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMobileNo" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Manager" runat="server" ForeColor="Red" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" SetFocusOnError="True" ValidationGroup="Manager" ForeColor="DarkRed"
                                                            ControlToValidate="txtMobileNo" ErrorMessage="* 10 Digit"
                                                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                            </label>
                                              <asp:TextBox ID="txtMobileNo" MaxLength="10" CssClass="form-control" placeholder="Enter Mobile No" onkeypress="NumericOnly();" runat="server"></asp:TextBox>
                                          
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5">
                                            <label for="txtAddress">Address</label>
                                            <asp:TextBox ID="txtAddress" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Manager Address" runat="server"></asp:TextBox>
                                          
                                        </div>
                                        <div class="col-md-4">
                                            <label for="ddlGender">Gender</label>
                                               <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                </asp:DropDownList>
                                          
                                        </div>
                                        <div class="col-md-3">
                                            <label for="ddlBloodGroup">Blood Group</label>
                                              <asp:DropDownList ID="ddlBloodGroup" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                                    <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                                    <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                                    <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                                    <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                                    <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                                    <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                                    <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                                    <asp:ListItem Text="Bombay" Value="Bombay"></asp:ListItem>
                                                </asp:DropDownList>
                                           
                                        </div>

                                    </div>
                                    
                                    <div class="row">
                                        <div class="col-md-12">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>

                                                <div class="row">
                                                    <div class="col-md-4" style="letter-spacing: 2px;">
                                                        <label for="ddlState">
                                                            State 
                                                        </label>
                                                        <asp:DropDownList ID="ddlState" CssClass="form-control  " OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-4" style="letter-spacing: 2px;">
                                                        <label for="ddlDistrict">District</label>
                                                        <asp:DropDownList ID="ddlDistrict" CssClass="form-control  " OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-4" style="letter-spacing: 2px;">
                                                        <label for="ddlTaluka">
                                                            City  
                                                        </label>
                                                        <asp:DropDownList ID="ddlTaluka" CssClass="form-control " OnSelectedIndexChanged="ddlTaluka_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>

                                                    </div>
                                                </div>

                                                <div class="row" style="letter-spacing: 3px;">
                                                    <div class="col-md-2">
                                                        <label for="ddlProgramme">
                                                            College Type  
                                                        </label>
                                                        <asp:DropDownList ID="ddlProgramme" CssClass="form-control" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <label for="">
                                                            College  
                                                        </label>
                                                        <asp:DropDownList ID="ddlCollege" CssClass="form-control" runat="server"></asp:DropDownList>

                                                    </div>
                                               
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="max-height: 300px; overflow: auto;">
                                                        <asp:Repeater runat="server" ID="rptCollegeList">
                                                            <HeaderTemplate>
                                                                <table class="table table-hover" style="width: 100%">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>College Id</th>
                                                                             <th>Slno</th>
                                                                            <th>College Name</th>

                                                                        </tr>
                                                                    </thead>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="display: none;">
                                                                       
                                                                          <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                    </td>
                                                                    <td style="width: 70%;">
                                                                        <asp:Label ID="lblCollegeId" Visible="false" Text='<% #Eval("CollegeId") %>' runat="server"></asp:Label>
                                                                        <asp:Label ID="lblTalukaId" Visible="false" runat="server" Text='<% #Eval("TalukId") %>'></asp:Label>
                                                                        <asp:CheckBox ID="ChkCollege" Font-Size="Small" Text='<% #Eval("College_Name") %>' runat="server" />
                                                                        <span class="badge m-r bg-primary">
                                                                            <asp:Label ID="lblManagerDistrict" runat="server" Text='<% #Eval("taluk_name") %>'></asp:Label></span>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                             
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
</asp:Content>

