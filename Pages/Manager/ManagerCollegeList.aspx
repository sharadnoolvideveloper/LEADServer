<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="ManagerCollegeList.aspx.cs" Inherits="Pages_Manager_ManagerCollegeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="row">
        <div class="col-md-4">
            <div class="row">
                <div class="col-md-12">

                    <div class="panel">
                       

                        <div class="panel-body">
                             <h3>

                                    
                                <asp:Label ID="FromManagerName" runat="server" Text=""></asp:Label>
                            </h3>
                            &nbsp; the below colleges are allocated to you
                                            <asp:Label ID="lblFromManagerId" Visible="false" runat="server" Text=""></asp:Label>
                             <div style="height:5px;"></div>
                            <span>
                                &nbsp;  <span class="input-group">
                                    <span class="input-group-addon bg-info">
                                        <span class="input-group-text">Search</span>
                                    </span>
                                    <input type="text" id="txtCollege" onkeyup="CollegeSearch()" placeholder="Search for College etc." class="form-control" />
                                </span>
                            </span>
                         <div style="height:10px;"></div>
                            <ul class="list-group" style="overflow: auto; height: 600px;">

                                <asp:Repeater ID="rptFromManagerCollegeList" OnItemCommand="rptFromManagerCollegeList_ItemCommand" runat="server">
                                    <HeaderTemplate>
                                        <table class="table" id="CollegeList" >
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td class="list-group-item" style="padding: 4px;">
                                                    <asp:Label ID="lblCollegeId" Visible="false" Text='<% #Eval("CollegeId") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblTalukaId" Visible="false" runat="server" Text='<% #Eval("TalukId") %>'></asp:Label>
                                                    <asp:LinkButton ID="btnCollege" Text='<% #Eval("College_Name") %>' runat="server"></asp:LinkButton>
                                                    <%--<asp:CheckBox ID="ChkCollege" CssClass="checkbox checkbox-custom checkbox-primary" Font-Size="Small"  runat="server" />--%>
                                                    <span class="badge m-r bg-primary pull-right">
                                                        <asp:Label ID="lblTalukaName" runat="server" Text='<% #Eval("taluk_name") %>'></asp:Label>
                                                    </span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </ul>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-8">
            <div class="row">
                <div class="col-md-12">

                    <div class="panel">
                     
                        <div class="panel-body">
                             <h3>
                                <asp:Label ID="lblCollegeName" runat="server" Text=""></asp:Label>
                                  <asp:Label ID="lblCollegeId" Visible="false" runat="server" Text=""></asp:Label>
                                 <span class="pull-right">
                                      <asp:LinkButton ID="btnExcelReport" OnClick="btnExcelReport_Click" ValidationGroup="Report" CssClass="btn btn-success  btn-block" runat="server"><span class="fa  fa-file-excel-o"></span> &nbsp; Excel </asp:LinkButton>
                                 </span>
                                 <br />
                                &nbsp;  <span class="input-group pull-right">
                                    <span class="input-group-addon bg-info">
                                        <span class="input-group-text">Search</span>
                                    </span>
                                    <input type="text" id="txtStudentSearch" onkeyup="SearchStudentList()" placeholder="Search for LEAD ID / Name / Mobileno etc." class="form-control" />
                                </span>
                            </h3>
                            <div class="row">
                            <div class="col-md-12">
                                <asp:Repeater ID="rptStudentList"  runat="server">
                                    <HeaderTemplate>
                                        <div class="row" style="max-height: 600px; overflow: auto;">
                                            <table class="table table-hover " style="max-width: 98%" id="StudentList">
                                                <thead>
                                                    <tr>
                                                        <td class="text-center"><strong><b>Registration Date</b></strong>
                                                        </td>
                                                        <td><strong><b>LEAD ID</b></strong>
                                                        </td>
                                                        <td><strong><b>Name</b></strong>
                                                        </td>
                                                        <td><strong><b>Mobile No</b></strong>
                                                        </td>
                                                        <td><strong><b>eMailId</b></strong>
                                                        </td>
                                                        <td><strong><b>AadharNo</b></strong>
                                                        </td>
                                                        <td><strong><b>Student Type</b></strong>
                                                        </td>
                                                    </tr>
                                                </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblRegistrationDate" runat="server" Text='<%# Eval("RegistrationDate") %>' />
                                                </td>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lblLeadId" Font-Size="small" runat="server" Text='<%# Eval("Lead_Id") %>' />
                                                </td>
                                                <td style="width: 50%;">
                                                    <asp:Label ID="lblName" Font-Size="small" runat="server" Text='<%# Eval("StudentName") %>' />
                                                </td>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lblMobileNo" Font-Size="small" runat="server" Text='<%# Eval("MobileNo") %>' />
                                                </td>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lbleMail" Font-Size="small" runat="server" Text='<%# Eval("MailId") %>' />
                                                </td> 
                                                 <td style="width: 10%;">
                                                    <asp:Label ID="lblAadharNo" Font-Size="small" runat="server" Text='<%# Eval("AadharNo") %>' />
                                                </td> 
                                                 <td style="width: 10%;">
                                                    <asp:Label ID="lblStudentType" Font-Size="small" runat="server" Text='<%# Eval("Student_Type") %>' />
                                                </td> 
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                        </div>
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

     <script type="text/javascript">
         function SearchStudentList() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtStudentSearch");
            filter = input.value.toUpperCase();
            ul = document.getElementById("StudentList");
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
         function CollegeSearch() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtCollege");
            filter = input.value.toUpperCase();
            ul = document.getElementById("CollegeList");
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

