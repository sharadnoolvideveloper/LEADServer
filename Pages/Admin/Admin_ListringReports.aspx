<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_ListringReports.aspx.cs" Inherits="Pages_Admin_Admin_ListringReports" %>

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
      <script type="text/javascript">
          function Search() {
              var input, filter, ul, li, a, i;
              input = document.getElementById("txtSearch");
              filter = input.value.toUpperCase();
              ul = document.getElementById("List");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
  <div class="container-fluid">
        <div class="row">
            <div class="col-md-3">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-md-6">
                                  <label for="txtFromDate">From Date<asp:RequiredFieldValidator ValidationGroup="Report" ControlToValidate="txtFromDate"  ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="* Select From Date" ForeColor="Red" SetFocusOnError="true" ></asp:RequiredFieldValidator> </label>
                                <asp:TextBox ID="txtFromDate" CssClass="form-control datepicker" placeholder="From Date" autocomplete="off" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                  <label for="txtToDate">To Date <asp:RequiredFieldValidator ValidationGroup="Report" ControlToValidate="txtToDate" ID="RequiredFieldValidator2"  Display="Dynamic"  runat="server" ErrorMessage="* Select To Date" ForeColor="Red" SetFocusOnError="true" ></asp:RequiredFieldValidator></label>
                                <asp:TextBox ID="txtToDate" CssClass="form-control datepicker" placeholder="To Date" autocomplete="off" runat="server"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlManager" CssClass="form-control" runat="server">
                                          
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-12">
                                <label for="ddlProjectType">Select Project Type</label>
                                <asp:DropDownList ID="ddlProjectType" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="[All]" Value="[All]"></asp:ListItem>
                                            <asp:ListItem Text="Registration" Value="Registration"></asp:ListItem>
                                            <asp:ListItem Text="Proposed" Value="Proposed"></asp:ListItem>
                                            <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                            <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                            <asp:ListItem Text="Modification Request" Value="RequestForModification"></asp:ListItem>
                                            <asp:ListItem Text="Completion Request" Value="RequestForCompletion"></asp:ListItem>
                                            <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                           <asp:ListItem Text="Impact Project" Value="Impact"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-12">
                                <label for="ddlStudentType">Select Student Status</label>
                                <asp:DropDownList ID="ddlStudentType" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="[All]" Value="[All]"></asp:ListItem>
                                    <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                                    <asp:ListItem Text="Initiator" Value="Initiator"></asp:ListItem>
                                    <asp:ListItem Text="Change Maker" Value="Change Maker"></asp:ListItem>
                                    <asp:ListItem Text="LEADer" Value="LEADer"></asp:ListItem>
                                    <asp:ListItem Text="Master Leader" Value="Master Leader"></asp:ListItem>
                                    <asp:ListItem Text="Lead Ambassador" Value="Lead Ambassador"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                     
                         <div class="row form-group">
                            <div class="col-md-12">
                                <asp:LinkButton ID="btnGenerate" CssClass="btn btn-primary btn-block" ValidationGroup="Report" OnClick="btnGenerate_Click" runat="server">Generate <span class="fa fa-arrow-right pull-right"></span> </asp:LinkButton>

                            </div>
                         </div>
                       
                        <div class="row form-group">
                            <div class="col-md-6 form-group">
                                <asp:LinkButton ID="btnPDFReport" OnClick="btnPDFReport_Click" ValidationGroup="Report" CssClass="btn btn-danger  btn-block" runat="server"><span class="fa fa-file-pdf-o"></span> &nbsp; PDF </asp:LinkButton>
                            </div>
                            <div class="col-md-6">
                               <asp:LinkButton ID="btnExcelReport" OnClick="btnExcelReport_Click" ValidationGroup="Report" CssClass="btn btn-success  btn-block" runat="server"><span class="fa  fa-file-excel-o"></span> &nbsp; Excel </asp:LinkButton>
                            </div>
                        </div>
                       
                         <div class="row form-group">
                            <div class="col-md-12">
                                <asp:LinkButton ID="btnFolderWiseDownload" OnClick="btnFolderWiseDownload_Click" Visible="false" ValidationGroup="Report" CssClass="btn btn-danger  btn-block" runat="server"><span class="fa fa-folder"></span> &nbsp; Download Folderwise</asp:LinkButton>
                            </div>
                          
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="col-md-9">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h4>profile updated Student Listing&nbsp; <b><asp:Label ID="lblSelectedProjectType" runat="server" Text=""></asp:Label></b>  &nbsp; ( <asp:Label ID="lblCount" runat="server" Text=""></asp:Label> ) 
                            <span class="pull-right">
                                  <input type="text" id="txtSearch" style="display:none;"  onkeyup="Search()" placeholder="Search for Member"
                                class="form-control" />
                                Press Ctr+f to Search
                            </span>
                          
                        </h4>
                    </div>
                    <div class="panel-body" id="List">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12" id="main" runat="server" style="height:650px;overflow:auto;">
                                    <asp:GridView ID="grdReport" CssClass="table table-bordered" runat="server">

                                    </asp:GridView>
                                    <div id="Listing_table" class="table table-bordered" style="max-height:620px;overflow:auto;" runat="server">
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
                    todayHighlight: true,

                });
            });

        </script>
    
</asp:Content>
