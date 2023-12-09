<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Pages_Admin_Dashboard" %>

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
        .list-group-item {
            padding: 1px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-2">
                                <h4>Managers Details                         
                                </h4>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlAcademicYear" CssClass="form-control select2" runat="server">
                                </asp:DropDownList>
                            </div>
                             <div class="col-md-1">
                                <h4>Program                         
                                </h4>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlprogramtype"  OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                          <div class="row">
                           
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-3" style="height: 630px; overflow: auto">
                                <ul class="list-group">
                                    <asp:Repeater ID="rptManagerList" OnItemCommand="rptManagerList_ItemCommand" runat="server">
                                        <ItemTemplate>
                                            <li class="list-group-item">
                                                <h4>
                                                    <asp:Label ID="lblManagerId" runat="server" Visible="false" Text='<% #Eval("ManagerId") %>'></asp:Label>
                                                    <asp:Label ID="lblManagerName" Font-Size="Smaller" runat="server" Text='<% #Eval("ManagerName") %>'></asp:Label>
                                                    <span class="pull-right">
                                                        <asp:LinkButton ID="btnManagerList" runat="server"><span class="fa fa-arrow-right text-primary"></span>  </asp:LinkButton>
                                                    </span>
                                                </h4>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    
                                </ul>
                            </div>
                            <div class="col-md-9">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="quick-actions_homepage">
                                            <ul class="quick-actions text-center">

                                                <li class="bg_lg col-md-1" >
                                                
                                                        <a href="DashBoard.aspx?vwType=Proposed">
                                                            <i class="fa fa-list"></i><span class="label label-danger">
                                                                <asp:Label ID="lblProposedProjects" runat="server" Text="0"></asp:Label>
                                                            </span>
                                                            <br />
                                                            <br />
                                                            Proposed
                                                        </a>
                                                    
                                                </li>
                                                
                                                <li class="bg_ly  col-md-2"><a href="DashBoard.aspx?vwType=Approved"><i class="fa fa-list-ol "></i><span class="label label-success">
                                                    <asp:Label ID="lblApproved" runat="server" Text="0"></asp:Label>
                                                </span>
                                                    <br />
                                                    <br />
                                                    Approved </a></li>
                                                <li class="bg_lb col-md-1"><a href="DashBoard.aspx?vwType=RequestForModification"><i class="fa fa-users "></i><span class="label label-danger">
                                                    <asp:Label ID="lblRequestForModification" runat="server" Text="0"></asp:Label>
                                                </span>
                                                    <br />
                                                    Request for Modification</a></li>
                                                <li class="bg_ls col-md-2"><a href="#"><i class="fa fa-bell "></i><span class="label label-danger">
                                                    <asp:Label ID="lblAllProjects" runat="server" Text="0"></asp:Label>
                                                </span>
                                                    <br />

                                                    Request for Completion</a></li>
                                                <li class="bg_lo  col-md-2"><a href="#"><i class="fa fa-chain"></i><span class="label label-success">
                                                    <asp:Label ID="lblCompletedProjects" runat="server" Text="0"></asp:Label>
                                                </span>
                                                    <br />
                                                    <br />
                                                    Completed</a> </li>
                                                <%--     <li class="bg_ls  col-md-2"><a href="#"><i class="fa fa-eraser"></i><span class="label label-success">
                                                    <asp:Label ID="lblCancelledProjects" runat="server" Text="20"></asp:Label>
                                                </span>
                                                    <br />
                                                    Canceled</a> </li>--%>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                             <div class="row">
                                            <div class="col-md-12">
                                                <div class="panel">
                                                    <div class="panel-heading">
                                                        <div class="row">
                                                            <div class="col-md-2 ">
                                                                <asp:Label ID="lblManagerIdClicked" runat="server" Text=""></asp:Label>
                                                                <asp:DropDownList ID="ddlAllProjectAcademicYear"  CssClass="form-control" runat="server"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-10">
                                                                <div class="input-group">
                                                                    <div class="input-group-addon bg-info">
                                                                        <span class="input-group-text">Filteration</span>
                                                                    </div>
                                                                    <input type="text" id="txtStudentSearch" onkeyup="SearchStudentDetail()" placeholder="Search for Lead Id / Title / Institute / etc." class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="panel-body">
                                                      
                                                        <div class="row">
                                                            <div class="col-md-12" style="width: 100%; height: 550px; overflow: auto" id="SearchList">

                                                                <asp:Repeater ID="rptAllProjects" OnItemDataBound="rptAllProjects_ItemDataBound"  runat="server">
                                                                    <HeaderTemplate>
                                                                        <table class="table table-hover" style="width: 200%">
                                                                            <thead>
                                                                                <tr style="background-color:#27a9e3;color:#fff" >
                                                                                    <td align="center" style="display: none">PDId
                                                                                    </td>

                                                                                    <td style="text-align:center"><strong> <b>Pic</b><strong>
                                                                                    </td>
                                                                                    <td style="display:none"><strong> <b>Proposed Date</b><strong>
                                                                                    </td>
                                                                                    <td style="text-align:center"><strong> <b>Lead Id</b><strong>
                                                                                    </td>
                                                                                    <td><strong> <b>Student Name</b><strong>
                                                                                    </td>
                                                                                    <td><strong> <b>Title</b><strong>
                                                                                    </td>
                                                                                   
                                                                                    <td style="display:none">Institution
                                                                                    </td>
                                                                                    <td style="display:none">Location
                                                                                    </td>
                                                                                    <td style="display:none">Mobile No
                                                                                    </td>
                                                                                     <td><strong> <b>Req Amt</b><strong>
                                                                                    </td>
                                                                                    <td><strong> <b>Sanction Amt</b><strong>
                                                                                    </td>
                                                                                      <td><strong> <b>Disperse</b><strong>
                                                                                    </td>
                                                                                      <td><strong> <b>Bal</b><strong>
                                                                                    </td>
                                                                                    <td style="text-align:center"><strong> <b>Status</b><strong>
                                                                                    </td>
                                                                                    <td style="text-align:center"><strong> <b><span class="fa fa-rupee"></span></b><strong></td>
                                                                                </tr>
                                                                            </thead>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tbody>
                                                                            <tr >
                                                                                <td align="center" style="display: none" >
                                                                                    <asp:Label ID="lblPDId" runat="server" Text='<%# Eval("PDId") %>' />
                                                                                </td>
                                                                                <td style="width: 6%;">
                                                                                    <asp:Image ID="imgStudentImg" CssClass="img-circle" Width="40px" Height="40px" ImageUrl='<%# Eval("Image_path") %>' runat="server" />
                                                                                </td>
                                                                                <td style="min-width: 40px;display:none">
                                                                                    <asp:Label ID="lblProposedDate" Font-Size="Small" runat="server" Text='<%# Eval("ProposedDate") %>' />
                                                                                </td>
                                                                                <td style="min-width: 40px; text-align: center;">
                                                                                    <asp:Label ID="lblLeadId" Font-Size="Small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                                                </td>
                                                                                <td style="min-width: 50px;">
                                                                                    <asp:Label ID="lblStudentName" Font-Size="Small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                                                </td>
                                                                                <td style="width: 20%;">
                                                                                    <asp:Label ID="lblTitle" Font-Size="Small" runat="server" Text='<%# Eval("title") %>' />
                                                                                </td>

                                                                               
                                                                                <td style="min-width: 50px;display:none;">
                                                                                    <asp:Label ID="lblCollegeName" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                                </td>
                                                                                <td style="min-width: 50px; text-align: center;display:none">
                                                                                    <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("Taluk_Name") %>' />
                                                                                </td>
                                                                                <td style="min-width: 50px; text-align: center;display:none">
                                                                                    <asp:Label ID="lblMobileNo" Font-Size="Small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                                                </td>
                                                                                 <td style="min-width: 30px;">
                                                                                    <asp:Label ID="lblRequestedAmount" Font-Size="Small" runat="server" Text='<%# Eval("Amount") %>' />
                                                                                </td>
                                                                                <td style="min-width: 30px;">
                                                                                    <asp:Label ID="lblSacntionAmount" Font-Size="Small" runat="server" Text='<%# Eval("SanctionAmount") %>' />
                                                                                </td>
                                                                                  <td style="min-width: 30px;">
                                                                                    <asp:Label ID="lblDisperseAmt" Font-Size="Small" runat="server"  />
                                                                                </td>
                                                                                  <td style="min-width: 30px;">
                                                                                    <asp:Label ID="lblBalance" Font-Size="Small" runat="server"  />
                                                                                </td>
                                                                                <td style="min-width: 50px; text-align: center;">
                                                                                    <asp:Label ID="lblProjectStatus" Font-Size="Small" runat="server" Text='<%# Eval("ProjectStatus") %>' />
                                                                                </td>
                                                                                <td style="min-width: 10px; text-align: center;">
                                                                                    <asp:LinkButton ID="btnPayee" Font-Size="Small" CommandArgument='<%# Eval("PDId")+"_"+Eval("ProjectStatus")+"_"+("Payee") %>' runat="server"></asp:LinkButton>
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
        function ErrorModal() {
            $('#ErrorModal').modal('show');

        }
    </script>
    <div id="ErrorModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" style="margin-top: 0px">
        <div class="modal-dialog bg-danger">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h3>Message</h3>
                    <p>
                        <span id="lblErrorMsg"></span>
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

