<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_StudentWiseDocumentDownload.aspx.cs" Inherits="Pages_Admin_Admin_StudentWiseDocumentDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
      <script src="../../JS/StudentJS/bootstrap.min.js"></script> 
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
     <script type="text/javascript">

         function checkAll(objRef) {

             var GridView = objRef.parentNode.parentNode.parentNode;

             var inputList = GridView.getElementsByTagName("input");

             for (var i = 0; i < inputList.length; i++) {

                 //Get the Cell To find out ColumnIndex

                 var row = inputList[i].parentNode.parentNode;

                 if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                     if (objRef.checked) {

                         //If the header checkbox is checked

                         //check all checkboxes

                         //and highlight all rows

                         row.style.backgroundColor = "#f8f8f8";

                         inputList[i].checked = true;

                     }

                     else {

                         //If the header checkbox is checked

                         //uncheck all checkboxes

                         //and change rowcolor back to original

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
            <div class="col-md-2">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="txtFromDate">
                                    <asp:RequiredFieldValidator ValidationGroup="Report" ControlToValidate="txtFromDate" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="* Select From Date" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtFromDate" CssClass="form-control datepicker" autocomplete="off" placeholder="From Date" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label for="txtToDate">
                                    <asp:RequiredFieldValidator ValidationGroup="Report" ControlToValidate="txtToDate" ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="* Select To Date" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                <asp:TextBox ID="txtToDate" CssClass="form-control datepicker" autocomplete="off" placeholder="To Date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                          <div class="row form-group">
                            <div class="col-md-12">
                                <label for="ddlProjectType">Select Program</label>
                                <asp:DropDownList ID="ddlprogramId"  OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                  
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="row form-group">
                            <div class="col-md-12">
                                <label for="ddlProjectType">Select Manager</label>
                                <asp:DropDownList ID="ddlManagerId"  CssClass="form-control" runat="server">
                                  
                                </asp:DropDownList>
                            </div>
                        </div>
                       
                        <div class="row form-group">
                            <div class="col-md-12">
                                <label for="ddlProjectType">Select Project Type</label>
                                <asp:DropDownList ID="ddlProjectType" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                    <asp:ListItem Text="Completion Request" Value="RequestForCompletion"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="btnGenerate" CssClass="btn btn-primary btn-block" ValidationGroup="Report" OnClick="btnGenerate_Click" runat="server">Generate <span class="fa fa-arrow-right pull-right"></span> </asp:LinkButton>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="btnDownload" CssClass="btn btn-warning btn-block" ValidationGroup="Report" OnClick="btnDownload_Click" runat="server">Download <span class="fa fa-download pull-right"></span> </asp:LinkButton>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
            <div class="col-md-10">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel">
                            <div class="panel-heading">
                                <div class="row">

                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <div class="input-group-addon bg-info">
                                                <span class="input-group-text">Search &nbsp;(<asp:Label ID="lblCount" runat="server" Text=""></asp:Label>)</span>
                                            </div>
                                            <input type="text" id="txtStudentSearch" onkeyup="SearchStudentDetail()" placeholder="Search for LEAD ID / Title / Institute / etc." class="form-control" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-md-12" style="width: 100%; height: 650px; overflow: auto" id="SearchList">

                                        <asp:Repeater ID="rptAllProjects" OnItemCommand="rptAllProjects_ItemCommand" OnItemDataBound="rptAllProjects_ItemDataBound" runat="server">
                                            <HeaderTemplate>
                                                &nbsp; &nbsp; &nbsp;<asp:CheckBox ID="ChkSelectAll" Text="All" onclick="checkAll(this);" runat="server" />

                                                <table class="table table-hover" style="width: 200%">
                                                    <thead>
                                                        <tr style="background-color: #27a9e3; color: #fff">

                                                            <td style="text-align: center"><strong><b></b><strong></td>
                                                            <td style="text-align: center"><strong><b>LEAD ID</b><strong>
                                                            </td>
                                                            <td><strong><b>Student Name</b><strong>
                                                            </td>
                                                            <td><strong><b>College Name</b><strong>
                                                            </td>
                                                            <td><strong><b>Mobile No</b><strong>
                                                            </td>
                                                            <td><strong><b>Emailid</b><strong></td>
                                                            <td><strong><b>ManagerName</b><strong></td>
                                                            <td><strong><b>Download</b><strong></td>

                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>

                                                        <td style="min-width: 40px; text-align: center;">

                                                            <asp:CheckBox ID="ChkStudentSelect" Font-Size="Small" runat="server" />
                                                        </td>

                                                        <td style="min-width: 40px; text-align: center;">
                                                            <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                        </td>

                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblCollegeName" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                        </td>

                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblEmailId" Font-Size="Small" runat="server" Text='<%# Eval("mailid") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblManagerName" Font-Size="Small" runat="server" Text='<%# Eval("ManagerName") %>' />
                                                        </td>
                                                        <td style="min-width: 10px; text-align: center;">
                                                            <asp:LinkButton ID="btnDownload" Font-Size="Small" CommandArgument='<%# Eval("Lead_Id") %>' runat="server"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
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
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function SearchStudentDetail() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtStudentSearch");
            filter = input.value.toUpperCase();
            ul = document.getElementById("SearchList");
            li = ul.getElementsByTagName("tbody");
            for (i = 0; i < li.length; i++) {
                a = li[i];
                if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";
                }
            }
        }
    </script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // Date Picker

            jQuery('.datepicker').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true,
                todayHighlight: true,

            });
        });

    </script>
</asp:Content>

