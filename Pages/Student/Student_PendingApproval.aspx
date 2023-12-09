<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="Student_PendingApproval.aspx.cs" Inherits="Pages_Student_Student_PendingApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
   
    <script>
        function resize() {
            if ($(window).width() < 514) {
                $("#tabs").removeClass("tabs").addClass("tabs-container tabs-vertical");
            }
            else { $("#tabs").removeClass("tabs-container tabs-vertical").addClass("tabs"); }
            if (($(window).width() > 0 && $(window).width() < 1000)) {
                $("#fixed").removeClass("fixed-leftbutton");
            }
            else { $("#fixed").addClass("fixed-leftbutton"); }
        }
        $(document).ready(function () {
            $(window).resize(resize);
            resize();
        });
    </script>
    <style>
             .fixed-leftbutton {
            position: fixed;
        }
    </style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-1">
            <div class="row fixed" id="fixed">
                <div class="col-md-12 col-xs-6 pull-right form-group">
                    <asp:LinkButton ID="btnSaveProposal" OnClick="btnSaveProposal_Click" runat="server" CssClass="btn btn-info text-center" ValidationGroup="Prop" OnClientClick="DisableButton();" UseSubmitBehavior="false"><span class="fa fa-check"> </span>
                       <br />
                        Save&nbsp;&nbsp;
                    </asp:LinkButton>
                </div>
                 <div class="col-md-12 col-xs-6 pull-right">
                    <a href="StudentProfile.aspx" class="btn  text-center">
                        <span class="fa fa-times"></span>
                        <br />
                        Close
                    </a>
                    
                </div>
            </div>
           

        </div>
        <div class="col-md-10">
            <div class="panel panel-default">
               
                <div class="panel-body" style="background-color:white;">
                     <div class="row">
                        <div class="col-md-6 form-group">
                            <label for="txtProjectTitle">Project Title </label>
                            <asp:TextBox ID="txtProjectTitle" placeholder="What is your idea ?" CausesValidation="true" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvtxtClave" ForeColor="Red" ControlToValidate="txtProjectTitle" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-6 ">
                            <label for="ddlProjectType">Project Type</label>
                            <asp:DropDownList ID="ddlProjectType" CssClass="form-control" runat="server"></asp:DropDownList>
                         <span style="display:none;">
                          <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                              <asp:ListItem Text="[Select]" Value="[Select]"></asp:ListItem>
                          </asp:DropDownList>
                         </span>
                                              
                         <asp:CompareValidator runat="server" Display="Dynamic"   ErrorMessage="* Choose Project Type" 
                                ControlToValidate="ddlProjectType" Type="String" SetFocusOnError="true" Operator="NotEqual"
                                ControlToCompare="DropDownList1" ForeColor="Red" >

                         </asp:CompareValidator>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label for="txtProjectObjectives">Objectives of the project </label>
                            <asp:TextBox ID="txtProjectObjectives" TextMode="MultiLine" placeholder="Why do you want to implement this idea?" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ForeColor="Red" ControlToValidate="txtProjectObjectives" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-6">
                            <label for="txtProjectObjectives">Action Plan</label>
                            <asp:TextBox ID="txtProjectPlan" TextMode="MultiLine" placeholder="How do you want to implement this idea?" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red" ControlToValidate="txtProjectPlan" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 form-group">
                            <label for="txtTotalBeneficiaries">Place of implementation</label>
                            <asp:TextBox ID="txtProposalPlaceofImplementation" placeholder="Place of Implementation" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red" ControlToValidate="txtProposalPlaceofImplementation" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3 ">
                            <label for="txtTotalBeneficiaries">How many beneficiaries</label>
                            <asp:TextBox ID="txtTotalBeneficiaries" placeholder="How many beneficiaries are there" Text="0" onkeypress="NumericOnly()" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" ControlToValidate="txtTotalBeneficiaries" Display="Dynamic" ErrorMessage="* Numeric Only" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                ControlToValidate="txtTotalBeneficiaries" SetFocusOnError="true"
                                ErrorMessage="Only numeric allowed." ForeColor="Red"
                                ValidationExpression="^[0-9]*$" ValidationGroup="Prop">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-6">
                            <label for="txtBeneficiaries">Who are the beneficiaries</label>
                            <asp:TextBox ID="txtProposedBeneficiaries" placeholder="Who are the beneficiaries" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator23" ForeColor="Red" ControlToValidate="txtProposedBeneficiaries" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                        </div>

                    </div>
                    <div class="row form-group">
                        <div class="col-md-6">
                            <label for="txtCurrentSituation">Current Situation</label>
                            <asp:TextBox ID="txtCurrentSituation" placeholder="Current Situation" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator24" ForeColor="Red" ControlToValidate="txtCurrentSituation" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2 form-group">
                            <label for="txtProposedStartDate">Start Date</label>
                            <asp:TextBox ID="txtProposedStartDate" placeholder="yyyy-mm-dd" autocomplete="off" CssClass="form-control datepicker" runat="server" onchange="CalculateTargetDays();"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator30" ForeColor="Red" ControlToValidate="txtProposedStartDate" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                                <asp:CompareValidator runat="server" Display="Dynamic"   ErrorMessage="* Choose Proper Date" 
                                ControlToValidate="txtProposedStartDate" ControlToCompare="txtProposedEndDate" Type="date" Operator="LessThanEqual" ForeColor="Red"  SetFocusOnError="true" ValidationGroup="Prop">
                         </asp:CompareValidator>

            
                        </div>
                        <div class="col-md-2 form-group">
                            <label for="txtProposedEndDate">End Date</label>

                            <asp:TextBox ID="txtProposedEndDate" placeholder="yyyy-mm-dd" autocomplete="off" CssClass="form-control datepicker" runat="server" onchange="CalculateTargetDays();"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator31" ForeColor="Red" ControlToValidate="txtProposedEndDate" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Prop"></asp:RequiredFieldValidator>

                             <asp:CompareValidator runat="server" Display="Dynamic"   ErrorMessage="* Choose Proper Date" 
                                ControlToValidate="txtProposedEndDate" ControlToCompare="txtProposedStartDate" Type="date" Operator="GreaterThanEqual" ForeColor="Red"  SetFocusOnError="true" ValidationGroup="Prop">

                         </asp:CompareValidator>
                        </div>
                        <div class="col-md-2">
                            <label>Target Days</label>
                            <br />
                            <asp:Label ID="lblProposedProjectTargetDays" CssClass="text-center" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-4">

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                              <%--      <h4>Add project materials &nbsp;&nbsp;                                          
                                               
                                    </h4>--%>
                                    <div class="row">
                                      
                                        <div class="col-md-12" style="max-height: 300px; overflow: auto;">
                                            <asp:Repeater runat="server" ID="rptMeterial">
                                                <HeaderTemplate>
                                                    <table class="table table-hover" id="meterial" style="width: 100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Name</th>
                                                                <th>Cost</th>
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
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="txtMeterialName" ValidationGroup="Prop" runat="server" ErrorMessage="* Required" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Cost" onkeypress="NumericOnly()" ID="txtMeterialCost" AutoPostBack="true" OnTextChanged="txtMeterialCost_TextChanged"                                 
                                                                 Text='<%# Eval("MeterialCost") %>' />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtMeterialCost" 
                                                                 ValidationGroup="Prop" runat="server" ErrorMessage="* Amount" ForeColor="DarkRed">

                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 2%;">
                                                          <%--  <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/CSS/Images/add.png" OnClick="btnAddMeterial_Click" />--%>
                                                        </td>
                                                        <td style="width: 6%;">
                                                            <%-- <a onclick="RemoveRows(this)"><span class="fa fa-trash" runat="server" onclick="btnRemoveRow_Click"></span></a>--%>
                                                            &nbsp;
                                                            <asp:ImageButton ID="btnRemoveRow" runat="server" ImageUrl="~/CSS/Images/trash.png" OnClick="btnRemoveRow_Click"  />
                                                            <%-- <asp:LinkButton ID="btnRemove" CssClass="btn btn-danger btn-floating " OnClick="btnRemove_Click" runat="server"><span class="fa fa-remove"></span></asp:LinkButton>--%>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                             <asp:LinkButton ID="btnAddMeterial" CssClass="btn btn-block text-center btn-info" runat="server" OnClick="btnAddMeterial_Click" ValidationGroup="Meterial"><span class="fa fa-plus"></span> Add Meterial</asp:LinkButton>
                                        </div>
                                    </div>

                                    <h3 runat="server" id="lblTotalAmt">Total Amount
                                  &nbsp; :
                                        <asp:Label ID="lblTotalAmount" CssClass="text-danger" runat="server" Text=""></asp:Label>
                                    </h3>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-8">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                           <%--         <h4>Add team members &nbsp;&nbsp;                                     
                                
                                    </h4>--%>
                                    <div class="row">
                                    
                                        <div class="col-md-12" style="max-height: 300px; overflow: auto;">
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
                                                        <td style="width: 20%;">
                                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Name" ID="txtName" Text='<%# Eval("MemberName") %>' />
                                                        </td>
                                                        <td style="width: 40%;">
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
                                                               <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <%-- <td style="width: 2%;">

                                                       
                                                           <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/CSS/Images/add.png" OnClick="btnAddTeamMembers_Click" />
                                                        </td>--%>
                                                        <td style="width: 6%;">
                                                            <asp:ImageButton ID="btnRemoveRow" runat="server" ImageUrl="~/CSS/Images/trash.png" OnClick="btnRemoveTeamMembers_Click" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                                   <asp:LinkButton ID="btnAddTeamMembers"  CssClass="btn btn-block btn-facebook text-center" runat="server" OnClick="btnAddTeamMembers_Click"><span class="fa fa-plus"></span> Add Team Members </asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row"  runat="server" id="ManagerCommentText" >
                        <div class="col-md-12">
                            <h3>
                                <label for="txtManagerComments" class="text-danger">Manager Comments</label>
                          <asp:TextBox ID="txtManagerComments" TextMode="MultiLine" Rows="3" CssClass="form-control" BackColor="darksalmon"  ForeColor="WhiteSmoke" Font-Size="Medium" Enabled="false" runat="server"></asp:TextBox></h3>
                        </div>
                    </div>
                </div>
         
            </div>
        </div>
       <div class="col-md-1">
            <div class="bg-success z-depth-2 hidden-xs" style="position: fixed; top: 0; right: 0; height: 100%; width: 50px">
                <h3 class="fa-rotate-90 text-muted text-nowrap brandFont" style="letter-spacing: 10px; margin-top: 50px">&nbsp;&nbsp;&nbsp;
                 <span class="text-primary"> Proposed</span>   Project <span class="text-primary"> Modification</span>
                </h3>
            </div>
       </div>
    </div>
        <asp:HiddenField ID="PDID" runat="server" />
            <asp:HiddenField ID="ProjectStatus" runat="server" />
            <asp:HiddenField ID="SumofAmount" runat="server" />
    <script>
      function DisableButton() {
            <%-- document.getElementById('<%= btnSaveProposal.ClientID %>').disabled = "disabled";
            __doPostBack('<%= btnSaveProposal.ClientID %>', '');--%>
      <%--    var e = document.getElementById('<%= ddlProjectType.ClientID %>');
var value = e.options[e.selectedIndex].value;
var text = e.options[e.selectedIndex].text;--%>
            if ($('#ContentPlaceHolder1_txtProjectTitle').val().trim() != '') {
                if ($('#ContentPlaceHolder1_ddlProjectType').val().trim() != '') {
                    if ($('#ContentPlaceHolder1_txtProjectObjectives').val().trim() != '') {
                        if ($('#ContentPlaceHolder1_txtProjectPlan').val().trim() != '') {
                            if ($('#ContentPlaceHolder1_txtProposalPlaceofImplementation').val().trim() != '') {
                                if ($('#ContentPlaceHolder1_txtTotalBeneficiaries').val().trim() != '') {
                                    if ($('#ContentPlaceHolder1_txtProposedBeneficiaries').val().trim() != '') {
                                        if ($('#ContentPlaceHolder1_txtCurrentSituation').val().trim() != '') {
                                            if ($("#elementId :selected").text() == '[Select]') {
                                              toastr.error('Select Project Type', 'Warning!', { timeOut: 5000 })
                                                if ($('#ContentPlaceHolder1_txtProposedStartDate').val().trim() != '') {
                                                    if ($('#ContentPlaceHolder1_txtProposedEndDate').val().trim() != '') {
                                                        if ($('#ContentPlaceHolder1_rptMeterial_txtMeterialName_1').val().trim() != ''){
                                                            if ($('#ContentPlaceHolder1_rptMeterial_txtMeterialCost_1').val().trim() != '') {
                                                                document.getElementById('<%= btnSaveProposal.ClientID %>').disabled = "disabled";
                                                            }
                                                        }
                                                            
                                                         
                                                    }
                                                    
                                                }
                                               
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    </script>

    <script type="text/javascript">
        jQuery(document).ready(function () {
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
        });

        //function RemoveRows(ids)
        //{
        //    $(ids).parent('td').parent('tr').remove();           
        //}
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
                    document.getElementById('<%= lblProposedProjectTargetDays.ClientID %>').textContent = diffDays+1;
            }
        }


    </script>
  
</asp:Content>

