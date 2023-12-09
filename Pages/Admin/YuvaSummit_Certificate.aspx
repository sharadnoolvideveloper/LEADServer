<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="YuvaSummit_Certificate.aspx.cs" Inherits="Pages_Admin_YuvaSummit_Certificate" %>

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
      <script type="text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                      
                        inputList[i].checked = true;

                    }

                    else {

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
        <div class="row">
            <div class="col-md-3">
                <div class="panel">
                     <div class="panel-heading">
                        <h3 class="text-center text-success">Yuva Summit Certificate</h3>
                    </div>
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-12">
                                <label>Select Academic Year</label>
                                <asp:DropDownList ID="ddlAcademicYear" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                          <div class="row form-group">
                            <div class="col-md-12">
                                <label>Select Type</label>
                                <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-12">
                                <label>Select College</label>
                                <asp:DropDownList ID="ddlCollegeName" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="row form-group">
                                    <div class="col-md-12">
                                        <asp:LinkButton ID="btnSearch" CssClass="btn btn-block btn-info" OnClick="btnSearch_Click" runat="server">Search <span class="fa fa-search fa-2x pull-right"></span> </asp:LinkButton>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Button ID="btnSend" CssClass="btn btn-block btn-primary" Text="Send Email" runat="server" OnClick="SendEmail" />
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnReport" CssClass="btn btn-block btn-danger" Text="Report" runat="server" OnClick="btnReport_Click" />

                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-9">
                <div class="panel">
                    <div class="panel-body" style="overflow: auto; height: 650px;">

                        <div class="row">
                            <div class="col-md-12">

                                <asp:CheckBox ID="ChkSelectAll" CssClass="checkbox checkbox-danger" Text="Select All Students" onclick="checkAll(this);" runat="server" />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Repeater ID="rptStudents" OnItemDataBound="rptStudents_ItemDataBound" OnItemCommand="rptStudents_ItemCommand" runat="server">
                                            <HeaderTemplate>

                                                <table class="table table-hover" id="SearchList">
                                                    <thead>
                                                        <tr style="background-color: #13c4f5; color: #fff">
                                                            <td>Slno</td>
                                                           
                                                            <td><strong><b>Student_Name</b></strong>
                                                                   <td><strong><b>LeadId</b></strong>
                                                            </td>
                                                            <td><strong><b>Mail Id</b></strong></td>
                                                            <td><strong><b>College Name</b></strong></td>
                                                            <td style="display:none;"><strong><b>Mobile No</b></strong></td>
                                                            <td><strong><b>Status</b></strong></td>
                                                            <td><strong><b>View</b></strong></td>
                                                              <td style="display:none;"><strong><b>View</b></strong></td>

                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <%#Container.ItemIndex+1 %>
                                                        </td>

                                                        <td style="display: none;">

                                                            <asp:Label ID="lblSlno" runat="server" Text='<% #Eval("slno") %>'></asp:Label>
                                                        </td>
                                                        <td style="display: none;">

                                                            <asp:Label ID="lblStatus" runat="server" Text='<% #Eval("Status") %>'></asp:Label>
                                                        </td>
                                                        <td style="letter-spacing: 2px;">
                                                            <asp:CheckBox ID="ChkCertificate" Font-Size="Smaller" ForeColor="Black" Text='<% #Eval("StudentName") %>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLead_Id" runat="server" Text='<% #Eval("lead_id") %>'></asp:Label>
                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblEmailId" runat="server" Text='<% #Eval("mailid") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblInstituteName" runat="server" Text='<% #Eval("Institute_Name") %>'></asp:Label>
                                                        </td>
                                                        <td style="display:none;">
                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<% #Eval("MobileNo") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblNewStatus" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="btnView" CssClass="btn btn-floating btn-info" CommandArgument='<%# Eval("StudentName")+"_"+Eval("mailid")+"_"+Eval("Status")+"_"+Eval("slno") %>' runat="server"><span class="fa fa-eye"></span> </asp:LinkButton>
                                                        </td>
                                                           <td style="display:none;">
                                                            <asp:Label ID="lblType" runat="server" Text='<% #Eval("Type") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>

                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div id="POP_Confirm" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 60%;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h2>Are You Sure You Want to Send Mail ?
                    </h2>
                    <div class="row">
                        <div class="col-md-offset-2 col-md-2">
                            <asp:LinkButton ID="btnNo" OnClick="btnNo_Click" CssClass="btn btn-danger" runat="server">NO</asp:LinkButton>
                        </div>
                        <div class="col-md-offset-3 col-md-2">
                            <asp:Button ID="btnYes" OnClick="btnYes_Click" CssClass="btn btn-info" runat="server" OnClientClick="this.disabled = true;this.value='Sending Mail Please Wait...';" UseSubmitBehavior="False" Text="Send Mail" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="POP_PDFView" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 60%;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Label ID="lblCertificateUserName" runat="server" Text=""></asp:Label></h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <asp:Literal ID="ltEmbed" runat="server" />
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
    <script type="text/javascript">
        function POP_PDFView() {
            $('#POP_PDFView').modal({
                show: true
            });
        }
    </script>

</asp:Content>

