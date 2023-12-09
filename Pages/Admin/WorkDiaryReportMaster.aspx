<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="WorkDiaryReportMaster.aspx.cs" Inherits="Pages_Admin_WorkDiaryReportMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        function success(msg) {
            toastr.options.timeOut = 4500; //1.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.success(msg);
        }
        function warning(msg) {
            toastr.options.timeOut = 4000; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.warning(msg);
        }
        function info(msg) {
            toastr.options.timeOut = 4000; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.info(msg);
        }
        function error(msg) {
            toastr.options.timeOut = 4000; //2.0 mili seconds 
            toastr.options.positionClass = "toast-bottom-left";
            toastr.error(msg);
        }
    </script>
    <script type="text/javascript">

        jQuery(document).ready(function () {
            // Date Picker
            jQuery('.datepicker').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true,
                todayHighlight: true
            });
        });
    </script>


    <script type="text/javascript">
        function SearchManagerName() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtManagerNameSearch");
            filter = input.value.toUpperCase();
            ul = document.getElementById("ManagerList");
            li = ul.getElementsByClassName("list-group-item");
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
        function SearchCategoryManagerName() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtCategoryManagerNameSearch");
            filter = input.value.toUpperCase();
            ul = document.getElementById("CategoryList");
            li = ul.getElementsByClassName("list-group-item");
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-12">

            <div class="tabs">

                <ul class="nav nav-tabs">
                    <li id="tabManagerWise" runat="server">
                        <asp:LinkButton ID="btnManagerWise" OnClick="btnManagerWise_Click" runat="server"><span class="menu-active animated"><i class="fa fa-user-secret"></i></span>&nbsp; Manager Wise &nbsp;
                        </asp:LinkButton>
                    </li>
                    <li id="tabSubCatWise" runat="server">
                        <asp:LinkButton ID="btnSubCatWise" OnClick="btnSubCatWise_Click" runat="server"><span class="menu-active animated"><i class="fa fa-link"></i></span>&nbsp; Category Wise &nbsp;
                        </asp:LinkButton>
                    </li>
                </ul>

                <div class="tab-content">
                    <div id="ManagerWisetab" runat="server" class="brandFont">

                        <div class="container-fluid">

                            <div class="row">
                                <div class="col-md-12">

                                    <div class="panel">
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>
                                                        From Date
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="ManagerWise" ControlToValidate="txtFromDateManagerwise" SetFocusOnError="true" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtFromDateManagerwise" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>
                                                        To Date
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="ManagerWise" ControlToValidate="txtToDateManagerWise" SetFocusOnError="true" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtToDateManagerWise" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                </div>
                                                   <div class="col-md-2">
                                                     <label for="ddlProjectType"> Program</label>
                                                     <asp:DropDownList ID="ddlprogram"  OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                                   </div>
                                                <div class="col-md-2">
                                                    <label>Main Category</label>
                                                    <asp:DropDownList ID="ddlMainCategoryManagerWise" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Sort By</label>
                                                    <asp:DropDownList ID="ddlSortFileds" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SEC_TO_TIME(SUM(TIME_TO_SEC(wd.Spent_Time)))" Text="Spent Time"></asp:ListItem>
                                                        <asp:ListItem Value="Entry_Date" Text="Entry Date"></asp:ListItem>
                                                        <asp:ListItem Value="College_Name" Text="College Name"></asp:ListItem>

                                                        <asp:ListItem Value="Progress" Text="Status"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Order</label>
                                                    <asp:DropDownList ID="ddlAscdesc" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="desc" Text="Descending"></asp:ListItem>
                                                        <asp:ListItem Value="Asc" Text="Ascending"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>



                                        </div>

                                    </div>


                                </div>

                            </div>
                            <div class="row" id="ManagerList">


                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12" style="height: 550px; overflow: auto">
                                            <asp:Label ID="lblManagerId" Visible="false" runat="server" Text=""></asp:Label>
                                            <div class="panel">
                                                <div class="panel-heading">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="background-color: darkgreen; color: white;"><i class="fa fa-search"></i></span>
                                                        <input type="text" id="txtManagerNameSearch" onkeyup="SearchManagerName()" placeholder="Search" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="panel-body">

                                                    <ul class="list-group">

                                                        <asp:Repeater ID="rptManagerWiseManagerList" OnItemCommand="rptManagerWiseManagerList_ItemCommand" OnItemDataBound="rptManagerWiseManagerList_ItemDataBound" runat="server">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnManagerList" ValidationGroup="ManagerWise" CssClass="list-group-item brandFont" runat="server">
                                                                    <asp:Label ID="lblManagerId" runat="server" Visible="false" Text='<% #Eval("Manager_Id") %>'></asp:Label>
                                                                    <asp:Label ID="lblManagerName" Font-Size="Smaller" runat="server" Text='<% #Eval("ManagerName") %>'></asp:Label>
                                                                    <span class="pull-right">
                                                                        <asp:Label ID="lblTimeSpent" CssClass="text-success" runat="server" Text='<% #Eval("SpentTime") %>'></asp:Label>
                                                                    </span>
                                                                </asp:LinkButton>
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
                                                    <h5>
                                                        <asp:Label ID="lblManagerName" runat="server" Text=""></asp:Label>
                                                        <span class="pull-right">
                                                            <asp:LinkButton ID="btnManagerwiseExcel" OnClick="btnManagerwiseExcel_Click" CssClass="btn btn-floating btn-warning" runat="server"><span class="fa fa-file-excel-o"></span> </asp:LinkButton>
                                                        </span>
                                                    </h5>




                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12" style="height: 530px; overflow: auto">

                                                            <asp:Repeater ID="rptTaskListManagerWise" runat="server">
                                                                <HeaderTemplate>
                                                                    <table class="table table-hover table-bordered" style="width: 100%; overflow: auto;">
                                                                        <thead>
                                                                            <tr>
                                                                                <td><strong><b>Slno</b></strong>

                                                                                <td><strong><b>Entry_Date</b></strong>
                                                                                </td>
                                                                                <td><strong><b>Category</b></strong> </td>
                                                                                <td style="text-align: center"><strong><b>Spent_Time</b></strong> </td>
                                                                                <td><strong><b>College_Name</b></strong>
                                                                                </td>
                                                                                <td><strong><b>description</b></strong>
                                                                                </td>
                                                                                <td><strong><b>No_Of_Participants</b></strong>
                                                                                </td>
                                                                                <td><strong><b>Remark</b></strong>
                                                                                <td><strong><b>Progress</b></strong>
                                                                            </tr>
                                                                        </thead>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tbody>
                                                                        <tr>

                                                                            <td align="center">
                                                                                <%# Container.ItemIndex + 1 %>
                                                                                <asp:Label ID="lblSlno" Visible="false" runat="server" Text='<%# Eval("slno") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblEntry_Date" Font-Size="Small" runat="server" Text='<%# Eval("Entry_Date") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblSub_CategoryName" Font-Size="Small" runat="server" Text='<%# Eval("Sub_CategoryName") %>' />
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                <asp:Label ID="lblSpent_Time" Font-Size="Small" runat="server" Text='<%# Eval("Spent_Time") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblCollege_Name" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbldescription" Font-Size="Small" runat="server" Text='<%# Eval("description") %>' />
                                                                            </td>


                                                                            <td>
                                                                                <asp:Label ID="lblNo_Of_Participants" Font-Size="Smaller" runat="server" Text='<%# Eval("No_Of_Participants") %>' />
                                                                            </td>


                                                                            <td>
                                                                                <asp:Label ID="lblRemarks" Font-Size="Small" runat="server" Text='<%# Eval("Remarks") %>' />
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                <asp:Label ID="lblProgress" Font-Size="Small" runat="server" Text='<%# Eval("Progress") %>' />
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
                    <div id="SubCatWisetab" runat="server" class="tab-pane brandFont">

                        <div class="container-fluid">

                            <div class="row">
                                <div class="col-md-12">

                                    <div class="panel">
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>
                                                        From Date
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="CategoryWise" ControlToValidate="txtFromDateCategory" SetFocusOnError="true" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtFromDateCategory" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>
                                                        To Date
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="CategoryWise" ControlToValidate="txtToDateCategory" SetFocusOnError="true" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtToDateCategory" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                </div>
                                                  <div class="col-md-2"> 
                                                       <label for="ddlProgramtType"> Program</label>
                                                       <asp:DropDownList ID="ddlprogramID"  OnSelectedIndexChanged="ddlprogramID_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"> </asp:DropDownList>
                                                  </div>
                                                <div class="col-md-2">
                                                    <label>Manager Name</label>
                                                    <asp:DropDownList ID="ddlManagerName" AutoPostBack="true" OnSelectedIndexChanged="ddlManagerName_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Sort By</label>
                                                    <asp:DropDownList ID="ddlOrderByCategoryWise" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="SEC_TO_TIME(SUM(TIME_TO_SEC(wd.Spent_Time)))" Text="Spent Time"></asp:ListItem>
                                                        <asp:ListItem Value="Entry_Date" Text="Entry Date"></asp:ListItem>
                                                        <asp:ListItem Value="College_Name" Text="College Name"></asp:ListItem>

                                                        <asp:ListItem Value="Progress" Text="Status"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Order</label>
                                                    <asp:DropDownList ID="ddlOrderType" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="desc" Text="Descending"></asp:ListItem>
                                                        <asp:ListItem Value="Asc" Text="Ascending"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>



                                        </div>

                                    </div>


                                </div>

                            </div>
                            <div class="row" id="CategoryList">


                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12" style="height: 550px; overflow: auto">
                                            <asp:Label ID="lblSelectedCategoryId" Visible="false" runat="server" Text=""></asp:Label>
                                            <div class="panel">
                                                <div class="panel-heading">
                                                    <div class="input-group">
                                                        <span class="input-group-addon" style="background-color: darkgreen; color: white;"><i class="fa fa-search"></i></span>
                                                        <input type="text" id="txtCategoryManagerSearch" onkeyup="SearchCategoryManagerName()" placeholder="Search" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="panel-body">

                                                    <ul class="list-group brandFont">
                                                        <li class="list-group-item">
                                                            <asp:LinkButton ID="btnAllCategory" OnClick="btnAllCategory_Click" runat="server">
                                                                All
                                                                                    <span class="pull-right">
                                                                                        <asp:Label ID="lblCategoryWiseTotalSpentTime" CssClass="text-success" runat="server"></asp:Label>
                                                                                    </span>
                                                            </asp:LinkButton>
                                                        </li>
                                                        <asp:Repeater ID="rptCategoryWiseList" OnItemCommand="rptCategoryWiseList_ItemCommand" runat="server">
                                                            <ItemTemplate>
                                                                <li class="list-group-item ">

                                                                    <asp:LinkButton ID="btnCategoryList" ValidationGroup="CategoryWise" runat="server">
                                                                        <asp:Label ID="lblCategoryId" runat="server" Visible="false" Text='<% #Eval("slno") %>'></asp:Label>
                                                                        <asp:Label ID="lblMain_CategoryName" Font-Size="Smaller" runat="server" Text='<% #Eval("Main_CategoryName") %>'></asp:Label>
                                                                        <span class="pull-right">
                                                                            <asp:Label ID="lblTimeSpent" CssClass="text-success" runat="server" Text='<% #Eval("Spent_Time") %>'></asp:Label>
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
                                                    <h5>
                                                        <asp:Label ID="lblCategoryName" runat="server" Text=""></asp:Label>
                                                        <span class="pull-right">
                                                            <asp:LinkButton ID="btnCategoryWiseManagerExcelDetails" OnClick="btnCategoryWiseManagerExcelDetails_Click" CssClass="btn btn-floating btn-warning" runat="server"><span class="fa fa-file-excel-o"></span> </asp:LinkButton>
                                                        </span>
                                                    </h5>




                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12" style="height: 530px; overflow: auto">

                                                            <asp:Repeater ID="rptTaskCategoryWiseDetailedList" runat="server">
                                                                <HeaderTemplate>
                                                                    <table class="table table-hover table-bordered" style="width: 100%; overflow: auto;">
                                                                        <thead>
                                                                            <tr>
                                                                                <td><strong><b>Slno</b></strong>

                                                                                <td><strong><b>Entry_Date</b></strong>
                                                                                </td>
                                                                                <td><strong><b>Manager_Name</b></strong> </td>
                                                                                <td><strong><b>Category</b></strong> </td>
                                                                                <td style="text-align: center"><strong><b>Spent_Time</b></strong> </td>
                                                                                <td><strong><b>College_Name</b></strong>
                                                                                </td>
                                                                                <td><strong><b>description</b></strong>
                                                                                </td>
                                                                                <td><strong><b>No_Of_Participants</b></strong>
                                                                                </td>
                                                                                <td><strong><b>Remark</b></strong>
                                                                                <td><strong><b>Progress</b></strong>
                                                                            </tr>
                                                                        </thead>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tbody>
                                                                        <tr>

                                                                            <td align="center">
                                                                                <%# Container.ItemIndex + 1 %>
                                                                                <asp:Label ID="lblSlno" Visible="false" runat="server" Text='<%# Eval("slno") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblEntry_Date" Font-Size="Small" runat="server" Text='<%# Eval("Entry_Date") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblManagerName" Font-Size="Small" runat="server" Text='<%# Eval("ManagerName") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblSub_CategoryName" Font-Size="Small" runat="server" Text='<%# Eval("Sub_CategoryName") %>' />
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                <asp:Label ID="lblSpent_Time" Font-Size="Small" runat="server" Text='<%# Eval("Spent_Time") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblCollege_Name" Font-Size="Small" runat="server" Text='<%# Eval("College_Name") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbldescription" Font-Size="Small" runat="server" Text='<%# Eval("description") %>' />
                                                                            </td>


                                                                            <td>
                                                                                <asp:Label ID="lblNo_Of_Participants" Font-Size="Smaller" runat="server" Text='<%# Eval("No_Of_Participants") %>' />
                                                                            </td>


                                                                            <td>
                                                                                <asp:Label ID="lblRemarks" Font-Size="Small" runat="server" Text='<%# Eval("Remarks") %>' />
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                <asp:Label ID="lblProgress" Font-Size="Small" runat="server" Text='<%# Eval("Progress") %>' />
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
    </div>



</asp:Content>
