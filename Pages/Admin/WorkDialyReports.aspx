<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="WorkDialyReports.aspx.cs" Inherits="Pages_Admin_WorkDialyReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://code.highcharts.com/modules/funnel.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
          <div class="col-md-3">
                                <label for="ddlProjectType">Select Program</label>
                                <asp:DropDownList ID="ddlprogram"  CssClass="form-control" runat="server">
                                  
                                </asp:DropDownList>
                            </div>
        <div class="row">
            
            <div class="col-md-3">
               <asp:Repeater ID="rptManagers" runat="server" OnItemDataBound="rptManagers_ItemDataBound">
    <HeaderTemplate>
        <table class="table">
            <tr>
                <th scope="col">
                    &nbsp;
                </th>
                <th scope="col" style="width: 150px">
                   Manager Name
                </th>
                <th scope="col" style="width: 150px">
                    Total_Spent_Time
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
           
            <td>
                <img alt="" style="cursor: pointer" src="../../CSS/Images/add.png" />
               
                <asp:Panel ID="pnlOrders" runat="server" Style="display: none;">
                    
                    <asp:Repeater ID="rptTask" runat="server">
                        <HeaderTemplate>
                            <table class="table table-bordered" cellspacing="0" rules="all" border="1">
                                <tr>
                                    <th scope="col" style="width: 150px">
                                       Category
                                    </th>
                                    <th scope="col" style="width: 150px">
                                        Spent_Time
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSubCategoryName" runat="server" Text='<%# Eval("Sub_CategoryName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblSpentTime" runat="server" Text='<%# Eval("Spent_Time") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:HiddenField ID="hfManagerId" runat="server" Value='<%# Eval("Manager_Id") %>' />
            </td>
             <td>
                <asp:Label ID="lblManagerName" runat="server" Text='<%# Eval("ManagerName") %>' />
            </td>
            <td>
                <asp:Label ID="lblTotalSpentTime" runat="server" Text='<%# Eval("SpentTime") %>' />
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


    <script type="text/javascript">
        $("body").on("click", "[src*=add]", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).removeAttr("src", "../../CSS/Images/add.png");
            $(this).attr("src", "../../CSS/Images/minus.png");
        });
        $("body").on("click", "[src*=minus]", function () {
            $(this).removeAttr("src", "../../CSS/Images/minus.png");
            $(this).attr("src", "../../CSS/Images/add.png");
            $(this).closest("tr").next().remove();
        });
    </script>
</asp:Content>

