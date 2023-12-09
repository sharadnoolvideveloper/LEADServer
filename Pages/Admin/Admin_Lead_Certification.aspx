    <%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_Lead_Certification.aspx.cs" Inherits="Pages_Admin_Admin_Lead_Certification" %>

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
                        <h3 class="text-center text-success">Trainers Lead Certification</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Select Academic Year</label>
                                <asp:DropDownList ID="ddlAcademicYear" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-12">
                                <label>Level</label>
                                <asp:DropDownList ID="ddlLevel" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                    <asp:ListItem Text="Level-1" Value="Level_1"></asp:ListItem>
                                     <asp:ListItem Text="Level-2" Value="Level_2"></asp:ListItem>
                                     <asp:ListItem Text="Level-3" Value="Level_3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-6">
                                <label>Select .xls file</label>
                                <asp:FileUpload ID="Uploader" runat="server" />
                            </div>
                                   <div class="col-md-4">
                                      
                                       <asp:LinkButton ID="btnVerify" OnClick="btnVerify_Click" CssClass="btn btn-info" runat="server">Verify</asp:LinkButton>
                            </div>
                               <div class="col-md-1">
                                       
                                       <asp:LinkButton ID="btnReset" OnClick="btnReset_Click" CssClass="btn btn-danger" runat="server"> <span class="fa fa-refresh"></span> </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row" runat="server" id="pnlUpload">
                                <div class="col-md-12">
                                       
                                       <asp:LinkButton ID="btnUpload" OnClick="btnUpload_Click" CssClass="btn btn-primary form-control" runat="server">Final Upload</asp:LinkButton>
                            </div>
                        </div>
                        <br />
                          <div class="row">
                            <div class="col-md-12">
                                   <label for="txtFromDate">Certificate Date<asp:RequiredFieldValidator ValidationGroup="Certificate" ControlToValidate="txtCertificateDate"  ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="* Select Certificate Date" ForeColor="Red" SetFocusOnError="true" ></asp:RequiredFieldValidator> </label>
                                <asp:TextBox ID="txtCertificateDate" CssClass="form-control datepicker" placeholder="From Date" autocomplete="off" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
           <div class="row" runat="server" id="pnlGenerate">
               <div class="col-md-12">
                      <asp:LinkButton ID="btnGenerate" CssClass="btn btn-block btn-success" OnClick="btnGenerate_Click" runat="server" ValidationGroup="Certificate">Generate Vertificate</asp:LinkButton>
               </div>
           </div>
                
                      
                       
                          <div class="row">
                            <div class="col-md-12">
                                   <label for="txtFromDate">Mail Subject<asp:RequiredFieldValidator ValidationGroup="Mail" ControlToValidate="txtMail_Subject"  ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="* Required" ForeColor="Red" SetFocusOnError="true" ></asp:RequiredFieldValidator> </label>
                                <asp:TextBox ID="txtMail_Subject" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Enter the Mail subject" autocomplete="off" runat="server" MaxLength="200"></asp:TextBox>
                            </div>
                        </div>
                          <br />
                        <div class="row">
                            <div class="col-md-6">
                                <asp:LinkButton ID="btnSendMail" ValidationGroup="Mail" CssClass="btn btn-block btn-danger form-control" OnClick="btnSendMail_Click" runat="server">Send Mail </asp:LinkButton>
                            </div>
                            <div class="col-md-6">
                                <asp:LinkButton ID="btnReport" CssClass="btn btn-block btn-warning" OnClick="btnReport_Click" runat="server"><span class="fa fa-file-excel-o"></span>&nbsp; Report</asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <a class="btn btn-info form-control" href="../../Reports/Sample_lead_Certification.xls" target="_blank">Download Sample Sheet</a>
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
                                                            <td><strong><b>Trainer_Name</b></strong>
                                                            </td>
                                                            <td><strong><b>Lead_Id</b></strong>
                                                            </td>
                                                            <td><strong><b>Mail Id</b></strong></td>


                                                            <td><strong><b>Status</b></strong></td>
                                                            <td><strong><b>View</b></strong></td>

                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <%#Container.ItemIndex+1 %>
                                                        </td>
                                                        
                                                        <td style="letter-spacing: 2px;">
                                                            <asp:CheckBox ID="ChkCertificate" Font-Size="Smaller" ForeColor="Black" Text='<% #Eval("Trainer_Name") %>' runat="server" />
                                                        </td>
                                                            <td>
                                                                 <asp:Label ID="lblLead_Id" runat="server" Text='<% #Eval("lead_id") %>'></asp:Label>
                                                            
                                                        </td>
                                                        <td style="display: none;">

                                                            <asp:Label ID="lblSlno" runat="server" Text='<% #Eval("slno") %>'></asp:Label>
                                                        </td>
                                                        <td style="display: none;">

                                                            <asp:Label ID="lblStatus" runat="server" Text='<% #Eval("Progress") %>'></asp:Label>
                                                        </td>
                                                        <td>

                                                            <asp:Label ID="lblEmailId" runat="server" Text='<% #Eval("email_id") %>'></asp:Label>
                                                        </td>
                                                         <td style="display:none;">

                                                            <asp:Label ID="lblprogress" runat="server" Text='<% #Eval("progress") %>'></asp:Label>
                                                        </td>

                                                        <td >
                                                            <asp:Label ID="lblNewStatus" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="btnView" CssClass="btn btn-floating btn-info" CommandArgument='<%# Eval("Trainer_Name")+"_"+Eval("email_id")+"_"+Eval("progress")+"_"+Eval("slno") %>' runat="server"><span class="fa fa-eye"></span> </asp:LinkButton>
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

    <div id="POP_Confirm" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 40%;">
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
    
     <div id="POP_Existing" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 40%;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h2>Already Some Ids are Exists
                    </h2>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblExisting_Users" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="POP_Error" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 40%;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h2>Alert Message
                    </h2>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblError_Message" runat="server" Text=""></asp:Label>
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
                     todayHighlight: true,

                 });
             });

         </script>

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
      <script type="text/javascript">
          function POP_Existing() {
              $('#POP_Existing').modal({
                  show: true
              });
          }
      </script>
          <script type="text/javascript">
              function POP_Error() {
                  $('#POP_Error').modal({
                      show: true
                  });
              }
          </script>
    
</asp:Content>

