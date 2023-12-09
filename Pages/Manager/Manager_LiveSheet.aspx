<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="Manager_LiveSheet.aspx.cs" Inherits="Pages_Manager_Manager_LiveSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .padder {
            box-shadow: 0 5px 5px 0 rgba(0, 0, 0, 0.16), 0 2px 10px 0 rgba(0, 0, 0, 0.12);
        }
    </style>

   <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/toster.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
     <ContentTemplate>
        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="1000"></asp:Timer>
         <asp:Repeater ID="Repeater1" runat="server">
              <ItemTemplate>
                 <ul>
                     <li>
                          <asp:Label ID="Label1" runat="server">
                             <%-- <%# Server.HtmlEncode(Eval("Lead_Id").ToString())%>--%>
                              <%#DataBinder.Eval(Container.DataItem, "Lead_Id")%>
                          </asp:Label>
                     </li>
                 <li>
                      <asp:Label ID="Label2" runat="server">
                        
                            <%#DataBinder.Eval(Container.DataItem, "Type")%>
                      </asp:Label>
                 </li>
                <li>
                     <asp:Label ID="Label3" runat="server" >
                        
                         
                            <%#DataBinder.Eval(Container.DataItem, "DeviceType")%>
                     </asp:Label>
                </li>
                      <li>
                     <asp:Label ID="Label4" runat="server">
                      
                         <%#DataBinder.Eval(Container.DataItem, "login_date")%>
                     </asp:Label>
                </li>
                     <li>
                         <asp:Label ID="lbl3" runat="server"></asp:Label>
                         <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("DeviceType") %>'></asp:TextBox>
                     </li>
                     <li>
                         <asp:LinkButton ID="btn" OnClick="btn_Click" runat="server">LinkButton</asp:LinkButton>
                     </li>
                     </ul>
             </ItemTemplate>
         </asp:Repeater>
   
     </ContentTemplate>
</asp:UpdatePanel>


     <div id="myModal" class="modal" role="dialog" style="width: auto; max-width: 90%; margin-top: 100px;">
        <div class="modal-dialog">
            <span class="close cursor" onkeydown="closeModal()" onclick="closeModal()">&times;</span>
           
         
        </div>
    </div>

 <script type="text/javascript">
     function myModal() {
            //$('#POP_RequestForCompletion').modal('show');
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: true,
                show: true
            });

        }
    </script>

</asp:Content>

