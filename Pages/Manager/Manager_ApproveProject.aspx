<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="Manager_ApproveProject.aspx.cs" Inherits="Pages_Manager_Manager_ApproveProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
      <script src="../../JS/CommonJS/toster.js"></script>
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
    <div class="row " >
        <div class="col-md-1" >
            <div class="row" >
                <div class="col-md-12 col-xs-3 pull-right form-group">
                    <asp:LinkButton ID="btnApproved" OnClick="btnApproved_Click" ValidationGroup="Student" CssClass="btn btn-info form-control" runat="server"> <span class="fa fa-check"></span>
                       <br />
                       Approve&nbsp;
                     
                    </asp:LinkButton>
                      <span style="display: none;">
                           <asp:Label ID="lblPDID" runat="server" ></asp:Label>
                           <asp:Label ID="lblProjectStatus" runat="server" ></asp:Label>
                            <asp:Label ID="lblLead_Id" runat="server" ></asp:Label>
                       </span>
                </div>
                <div class="col-md-12 col-xs-3 form-group pull-right">
                    <asp:LinkButton ID="btnRequestModification" OnClick="btnRequestModification_Click" ValidationGroup="Student" CssClass="btn btn-primary form-control" runat="server"> <span class="fa fa-reply"></span> 
                          <br />
                          Modify &nbsp;
                    </asp:LinkButton>
                </div>
                <div class="col-md-12 col-xs-3 pull-right  form-group" style="display:none;">
                    <asp:LinkButton ID="btnReject" OnClick="btnReject_Click" ValidationGroup="Student" CssClass="btn btn-danger form-control" runat="server"> <span class="fa fa-remove"></span>
                     <br />
                     Reject &nbsp;
                    </asp:LinkButton>
                </div>
              
                   <div class="col-md-12 col-xs-3 pull-right  form-group">
                    <asp:Panel ID="pnlReject" runat="server">
                        <a href="#" data-toggle="modal" data-target="#POP_Reject" class="btn btn-danger">
                            <span class="fa fa-remove"></span>
                            <br />
                            Reject &nbsp;
                        </a>
                    </asp:Panel>
                </div>

                <div class="col-md-12 col-xs-3 pull-right">
                    
                    <asp:LinkButton ID="btnClose" OnClick="btnClose_Click" CssClass="btn btn-white form-control" runat="server"> <span class="fa fa-remove"></span>
                     <br />
                     Close &nbsp;&nbsp;&nbsp;
                    </asp:LinkButton>
                </div>
             
            </div>
        </div>
        <div class="col-md-10">
            <div class="panel panel-default">
                <div class="panel-body" style="background-color: white;">
                     <div class="row">
                                <asp:Label ID="lblProjectId" Visible="false" runat="server" Text="Label"></asp:Label>
                                <div class="col-md-4">

                                    <label>Student Name</label>
                                    <asp:TextBox ID="txtStudentName" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-5">

                                    <label>College Name</label>
                                    <asp:TextBox ID="txtCollegeName" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>


                                </div>
                                <div class="col-md-3">

                                    <label>Mobile No</label>
                                    <asp:TextBox ID="txtMobileNo" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">

                                    <label>
                                        Project Title &nbsp;
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtProjectTitle" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtProjectTitle" TextMode="MultiLine" CssClass="form-control" placeholder="Project Title" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-6">

                                    <label>Current Situation</label>
                                    <asp:TextBox ID="txtCurrentSituation" TextMode="MultiLine" placeholder="Current Situation" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Place of Implemention</label>
                                    <asp:TextBox ID="txtPlaceofImplement" TextMode="MultiLine" placeholder="Place of Implemention" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-6">

                                            <label>Total Beneficiaries</label>
                                            <asp:TextBox ID="txtTotalBeneficiaries" onkeypress="NumericOnly()" CssClass="form-control" runat="server"></asp:TextBox>

                                        </div>
                                        <div class="col-md-6">

                                            <label>Duration</label>
                                            <asp:TextBox ID="txtDuration" CssClass="form-control" placeholder="Duration" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <label>Beneficiaries</label>
                                            <asp:TextBox ID="txtBeneficiaries" placeholder="Beneficiaries" CssClass="form-control" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">


                                <div class="col-md-3">

                                    <label>Requested Amount</label>
                                    <asp:TextBox ID="txtBudget" Enabled="false" CssClass="form-control disabled" placeholder="Request Amt" onkeypress="NumericOnly()" runat="server"></asp:TextBox>

                                </div>

                                <div class="col-md-3">

                                    <label>
                                        Approved Amount
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtAmount" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtAmount" CssClass="form-control" onkeypress="NumericOnly()" placeholder="Sanction Amt" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-3">
                                    <label>Theme of The project</label>
                                    <asp:DropDownList ID="ddlTheme" CssClass="form-control" runat="server"></asp:DropDownList>

                                </div>
                                  <div class="col-md-3">
                                    <label>Sem / cohorts</label>
                                      <asp:TextBox ID="txtSemName" CssClass="form-control disabled" Font-Size="Small" Enabled="false" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">

                                    <label>Project Objectives</label>
                                    <asp:TextBox ID="txProjectObjective" TextMode="MultiLine" placeholder="Objective Of the Project" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-6">


                                    <label>Action Plan</label>
                                    <asp:TextBox ID="txtActionPlan" TextMode="MultiLine" placeholder="Action Plan" CssClass="form-control" runat="server"></asp:TextBox>


                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">

                                    <label>
                                        Manager Comments
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtManagerComments" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Student" runat="server" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtManagerComments" placeholder="Manager Comments" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-2 form-group">
                                    <label for="txtProposedStartDate">Start Date</label>
                                    <asp:TextBox ID="txtProposedStartDate" placeholder="yyyy-mm-dd" CssClass="form-control datepicker" autocomplete="off" runat="server" onchange="CalculateTargetDays();"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator30" ForeColor="Red" ControlToValidate="txtProposedStartDate" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Student"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" Display="Dynamic" ErrorMessage="* Choose Proper Date"
                                        ControlToValidate="txtProposedStartDate" ControlToCompare="txtProposedEndDate" Type="date" Operator="LessThanEqual" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Student">
                                    </asp:CompareValidator>
                                </div>
                                <div class="col-md-2 form-group">
                                    <label for="txtProposedEndDate">End Date</label>

                                    <asp:TextBox ID="txtProposedEndDate" placeholder="yyyy-mm-dd" CssClass="form-control datepicker" autocomplete="off" runat="server" onchange="CalculateTargetDays();"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator31" ForeColor="Red" ControlToValidate="txtProposedEndDate" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Student"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" Display="Dynamic" ErrorMessage="* Choose Proper Date"
                                        ControlToValidate="txtProposedEndDate" ControlToCompare="txtProposedStartDate" Type="date" Operator="GreaterThanEqual" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Student">

                                    </asp:CompareValidator>
                                </div>
                                <div class="col-md-1">
                                    <label>Days</label>
                                    <br />
                                    <asp:Label ID="lblProposedProjectTargetDays" CssClass="text-center" runat="server" Text="0"></asp:Label>
                                </div>
                                <div class="col-md-1 text-center">
                                       <label class="text-info">isImpact?</label>
                                    <br />
                                    <asp:CheckBox ID="ChkIsImpact"   runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:UpdatePanel runat="server">

                                        <ContentTemplate>
                                            <span>
                                                <h4>Add Project Materials &nbsp;&nbsp;
                                          
                                                <asp:LinkButton ID="btnAddMeterial" CssClass="" runat="server" OnClick="btnAddMeterial_Click"><span class="fa fa-plus text-primary"></span> </asp:LinkButton>

                                                </h4>

                                            </span>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12" style="max-height: 300px; overflow: auto;">
                                                    <asp:Repeater runat="server" ID="rptMeterial">
                                                        <HeaderTemplate>

                                                            <table class="table table-hover" style="width: 100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Meterial Name</th>
                                                                        <th>Meterial Cost</th>
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
                                                                </td>
                                                                <td style="width: 20%;">

                                                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Cost" onkeypress="NumericOnly()" ID="txtMeterialCost" Text='<%# Eval("MeterialCost") %>' 
                                                                    AutoPostBack="true" OnTextChanged="txtMeterialCost_TextChanged"     
                                                                        />
                                                                </td>

                                                                <td style="width: 4%;">

                                                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/CSS/Images/add.png" OnClick="btnAddMeterial_Click" />
                                                                </td>
                                                                <td style="width: 6%;">

                                                                    <asp:ImageButton ID="btnRemoveRow" runat="server" ImageUrl="~/CSS/Images/trash.png" OnClick="btnRemoveRow_Click" />

                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <h3 runat="server" id="lblTotalAmt" >Total Amount
                                  &nbsp; :
                                        <asp:Label ID="lblTotalAmount" CssClass="text-danger" runat="server" Text=""></asp:Label>
                                            </h3>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-7" style="max-height: 300px; overflow: auto;">                               
                                        <h4>Project Team Members &nbsp;&nbsp;</h4>                             
                                    <br />
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
                                                <td style="width: 30%;">
                                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Name" ID="txtName" Text='<%# Eval("MemberName") %>' />
                                                </td>
                                                <td style="width: 30%;">
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
                                                    <%--  <asp:RadioButton ID="rdoTeamMemberMale" CssClass="radio radio-custom radio-info" Checked="true" GroupName="TeamGender" Text="Male"  runat="server" />
                                                            &nbsp;
                                                            <asp:RadioButton ID="rdoTeamMemberFemale" GroupName="TeamGender" CssClass="radio radio-custom radio-danger"  Text="Female" runat="server" />--%>
                                                    <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                </div>
            </div>
        </div>
        <div class="col-md-1">
             <div class="bg_ss z-depth-2 hidden-xs" style="position: fixed; top: 0; right: 0; height: 100%; width: 50px;">
                <h3 class="fa-rotate-90 text-muted text-nowrap" style="letter-spacing: 10px; margin-top: 50px;color:white;">&nbsp;&nbsp;&nbsp;
                 <span> Project</span>    <span > Approval</span>
                </h3>
            </div>
        </div>
    </div>
     <div id="ErrorModal" class="modal fade" role="dialog" style="margin-top: 0px">
        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>Message</h3>
                    <p>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
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

      <div class="modal fade" id="POP_Reject" tabindex="-1" role="dialog" data-toggle="modal" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Do you want to Reject the Project ? </h5>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-6 col-lg-6 col-xs-12">
                                <label>
                                    Enter Reject Comments

  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtRCRejectComments" ValidationGroup="Reject" ErrorMessage="* Required" ForeColor="Red" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRCRejectComments" ID="RegularExpressionValidator14" ForeColor="Red" SetFocusOnError="true" ValidationExpression="^[\s\S]{16,}$" runat="server" ErrorMessage="* Minimum 16 Characters ." ValidationGroup="Reject"></asp:RegularExpressionValidator>
                                </label>
                                <asp:TextBox ID="txtRCRejectComments" TextMode="MultiLine" Rows="2" CssClass="form-control" placeholder="Comments" ValidationGroup="Reject" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                              <button type="button" class="btn btn-success" data-dismiss="modal">   No &nbsp; <span class="fa fa-thumbs-down"></span></button>

                 
                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-danger" ValidationGroup="Reject" runat="server" OnClick="btnRejectConfirm_Click">
                             Yes Reject &nbsp; <span class="fa fa-thumbs-up"></span>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function ErrorModal() {
            $('#ErrorModal').modal('show');

        }
    </script>
        <script type="text/javascript">
         function POP_Reject() {
             $('#POP_Reject').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
        }
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

