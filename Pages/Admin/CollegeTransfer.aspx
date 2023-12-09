<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="CollegeTransfer.aspx.cs" Inherits="Pages_Admin_CollegeTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
      <script src="../../JS/StudentJS/bootstrap.min.js"></script> 
     <script src="../../JS/CommonJS/toster.js"></script>
      
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
    <style>
        .list-group-item
        {
            padding:1px 10px;
        }
        .btn
        {
            text-align:left;
        }
        .checkbox
        {
            padding: 0px 0px 0px 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
            <div class="container-fluid">
                <div class="row">
                      <div class="col-md-1">
                                <h4>Program </h4>
                            </div>
                <div class="col-md-2"> 
                        <asp:DropDownList ID="ddlprogram"  OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading">
                                        <h4>Managers List</h4>
                                    </div>
                                    <div class="panel-body">
                                        <ul class="list-group" style="overflow: auto; height: 620px;">
                                            <asp:Repeater ID="rptManagersList" OnItemCommand="rptManagersList_ItemCommand" OnItemDataBound="rptManagersList_ItemDataBound" runat="server">
                                                <ItemTemplate>
                                                    <li class="list-group-item" style="padding: 0px;">
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
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="panel">
                                    <div class="panel-heading">
                                        <h4>
                                            <asp:Label ID="FromManagerName" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lblFromManagerId" Visible="false" runat="server" Text=""></asp:Label>

                                            <span class="pull-right">
                                                <asp:LinkButton ID="btnListCollegeToManager" OnClick="btnListCollegeToManager_Click" runat="server"><span class="fa fa-forward"></span> </asp:LinkButton>
                                            </span>

                                        </h4>
                                    </div>

                                    <div class="panel-body">
                                        
                                        <ul class="list-group" style="overflow: auto; height: 650px;">
                                            <asp:Repeater ID="rptFromManagerCollegeList" runat="server">
                                                <ItemTemplate>
                                                   
                                                        <li class="list-group-item"  style="padding: 0px;">
                                                            <asp:Label ID="lblCollegeId" Visible="false" Text='<% #Eval("CollegeId") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblTalukaId" Visible="false" runat="server" Text='<% #Eval("TalukId") %>'></asp:Label>
                                                            <asp:CheckBox ID="ChkCollege" CssClass="checkbox checkbox-custom checkbox-primary" Font-Size="Small" Text='<% #Eval("College_Name") %>' runat="server" />
                                                            <span class="badge m-r bg-primary">
                                                                <asp:Label ID="lblTalukaName" runat="server" Text='<% #Eval("taluk_name") %>'></asp:Label></span>

                                                        </li>
                                                 
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                             
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel">
                                    <div class="panel-heading">
                                        <h4>
                                           <span class="text-danger"> Select Manager to Transfer below Colleges

                                           </span>
                                            
                                            <div class="row"><br />
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlManagersList" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="pull-right">
                                                        <asp:LinkButton ID="btnUpdateCollege" CssClass="btn btn-primary" OnClick="btnUpdateCollege_Click" runat="server"><span class="fa fa-save"></span> &nbsp; Save </asp:LinkButton>
                                                    </span>
                                                </div>
                                            </div>
                                        </h4>
                                    </div>
                                    <div class="panel-body">
                                        <ul class="list-group" style="overflow: auto; height: 600px;">
                                            <asp:Repeater ID="rptToManager" runat="server">
                                                <ItemTemplate>

                                                    <li class="list-group-item " style="padding: 0px;">
                                                        <asp:Label ID="lblCollegeId" Visible="false" Text='<% #Eval("CollegeIdToManager") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblTalukaId" Visible="false" runat="server" Text='<% #Eval("TalukIdToManager") %>'></asp:Label>
                                                        <asp:Label ID="lblCollegeName" runat="server" Text='<% #Eval("CollegeNameToManager") %>'></asp:Label>

                                                        <span class="badge m-r bg-primary">
                                                            <asp:Label ID="lblTalukaName" runat="server" Text='<% #Eval("TalukaNameToManager") %>'></asp:Label></span>
                                                    </li>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>




            </div>
  
</asp:Content>

