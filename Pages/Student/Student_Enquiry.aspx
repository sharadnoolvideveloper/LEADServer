<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="Student_Enquiry.aspx.cs" Inherits="Pages_Student_Student_Enquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/chat.css" rel="stylesheet" />
   

    <script>
      
        function resize() {
            if ($(window).width() < 514) {
                $("#tabs").removeClass("tabs").addClass("tabs-container tabs-vertical");
                
            }
            else { $("#tabs").removeClass("tabs-container tabs-vertical").addClass("tabs"); }
        }

        $(document).ready(function () {
            $(window).resize(resize);
            resize();
        });
    </script>

     <script type="text/javascript">
        function success(msg) {
            toastr.options.timeOut = 4500; //1.5 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.success(msg);
        }
        function warning(msg) {
            toastr.options.timeOut = 4500; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.warning(msg);
        }
        function info(msg) {
            toastr.options.timeOut = 4500; //1.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.info(msg);
        }
        function error(msg) {
            toastr.options.timeOut = 4500; //2.0 mili seconds 
            toastr.options.positionClass = "toast-top-right";
            toastr.error(msg);
        }
     </script>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
   <script type="text/javascript">
       function cnt(text) {
           var a = text.value;
           var type = $("#<%=ddlRequestType.ClientID%>  OPTION:selected").val();
           if (type == "1") {
               $("#Project").show();
           }
           else {
               $("#Project").hide();
           }
       }
    </script>
    <style>
         @media only screen and (max-width: 600px) {
            .received_msg {
                width: 96%;
            }

            .sent_msg {
                width: 96%;
            }
          
        }
         .panel-body
         {
             padding:14px;
         }
         .panel{
             margin-bottom:10px;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     
        <div class="row">
            <div class="col-md-2">
             <div class="panel">
                 <div class="panel-heading">
                      <h4><i class="fa fa-comment">
                 
                    </i>&nbsp; Request
                          </h4>
                 </div>
                 <div class="panel-body">
                    
                        <div class="row form-group">
                            <div class="col-md-12">
                               Request Type
                                <asp:DropDownList ID="ddlRequestType" OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                              
                        </div>
                    
                     <div class="row form-group" id="Project" runat="server">
                            <div class="col-md-12">
                                   Project Title
                                <asp:DropDownList ID="ddlProjectTitle" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                      
                        <div class="row form-group">
                            <div class="col-md-12">
                                Priority
                                <asp:DropDownList ID="ddlPriority" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                    <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                                    <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                      
                        <div class="row form-group">
                            <div class="col-md-12">
                                <label>Message &nbsp; <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtWriteRequest" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="true" ValidationGroup="Request"></asp:RequiredFieldValidator>
                               </label>
                                <asp:TextBox ID="txtWriteRequest" autocomplete="off" TextMode="MultiLine" Rows="4"   MaxLength="300" CssClass="form-control" placeholder="Type a message" runat="server"></asp:TextBox>
                            </div>
                        </div>
                     
                        <div class="row form-group">
                            <div class="col-md-12 text-center">
                                <asp:LinkButton ID="btnNewRequest" data-toggle="tooltip" OnClick="btnNewRequest_Click" title="New Request" ValidationGroup="Request" CssClass="btn btn-facebook" runat="server"> <span class="fa fa-paper-plane-o"> </span> &nbsp; Send Request </asp:LinkButton>
                            </div>
                        </div>
            <br />
                     <span class="hidden-xs">
                         <div style="height:150px;"></div>
                     </span>
        
                 </div>
             </div>
           
                   
            </div>
            <div class="col-md-10">
            
             
                <div class="row" >
                    <div class="col-md-12" >
                        <div class="panel" >
                            <div class="panel-heading"  >
                                <h4>Details
                                <span class="pull-right">
                                    <asp:TextBox ID="txtSearch" placeholder="Search" autocomplete="off" onkeyup="SearchResult()" BackColor="beige" CssClass="form-control" runat="server"></asp:TextBox>
                                </span>
                                    </h4>
                            </div>
                                  <div class="panel-body" id="MsgSearch">
                                        <asp:Repeater ID="rptRequest" OnItemDataBound="rptRequest_ItemDataBound" OnItemCommand="rptRequest_ItemCommand" runat="server">
                                            <HeaderTemplate>
                                                <div class="row" style="max-height: 550px; overflow: auto;">
                                                    <table class="table table-hover table-bordered" style="max-width: 100%" id="RequestList">
                                                        <thead>
                                                            <tr>
                                                                <td><strong><b>Ticket Details</b></strong>
                                                                </td>
                                                                <td class="hidden-xs"><strong><b>Last_Updated</b></strong>
                                                                </td>
                                                                   <td class="hidden-xs"><strong><b>Subject</b></strong>
                                                                </td>
                                                                <td><strong><b>Request_Message</b></strong>
                                                                </td>
                                                          
                                                            
                                                                <td class="hidden-xs"><strong><b>Response_Message</b></strong>
                                                                </td>
                                                                    <td class="hidden-xs"><strong><b>Project_Title</b></strong>
                                                                </td>
                                                              
                                                               
                                                                <td class="hidden-xs"><strong><b>Action</b></strong>
                                                                </td>
                                                            </tr>
                                                        </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                 
                                                    <tr>
                                                     
                                                        <td style="width: 15%;" >

                                                          TN - #<asp:Label ID="lblStudentRequestNo"  Font-Bold="true" runat="server" Text='<%# Eval("Request_Id") %>'></asp:Label> | <asp:Label ID="lblPriority" Font-Bold="true" runat="server" Text='<%# Eval("Request_Priority") %>'></asp:Label>
                                                              <span>  
                                                                <br />
                                        <asp:Label ID="lblStudentRequestTime" Font-Size="Smaller" runat="server" Text='<%# Eval("Request_Time")+" | "+Eval("Request_Date")  %>'></asp:Label>
                                          
                                                        </td>
                                                          <td style="width: 8%;" class="hidden-xs">
                                                            <span class="time_date">                                                           
                                                               <asp:Label ID="lblManagerResponseTime" Font-Size="Smaller" runat="server" Text='<%# Eval("Response_Time") %>'></asp:Label>
                                                                        <br />
                                            <asp:Label ID="lblManagerResponseDate" Font-Size="Smaller" runat="server" Text='<%# Eval("Response_Date") %>'></asp:Label>
                                                        </td>
                                         <td style="display:none;">
                                             <asp:Label ID="Label1" runat="server" Text='<%# Eval("Request_Id") %>'></asp:Label>
                                         </td>
                                                            <td style="width: 15%;" class="hidden-xs">
                                                             <asp:Label ID="lblStudentRequestHead" Font-Size="Smaller" runat="server" Text='<%# Eval("Head_Name") %>'></asp:Label>
                                                               
                                                        </td>
                                                        <td style="width: 20%;">
                                                               <asp:Label ID="lblRequestMessage" Font-Size="Smaller" runat="server" Text='<%# Eval("Request_Message") %>'></asp:Label>
                                                        </td>
                                                    
                                                        <td style="width: 20%;" class="hidden-xs">
                                                         <asp:Label ID="lblRespondMessage" Font-Size="Smaller" runat="server" Text='<%# Eval("Response_Message") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 20%;" >
                                                               <asp:Label ID="lblProjectTitle" Font-Size="Smaller" runat="server" Text='<%# Eval("ProjectTitle") %>'></asp:Label>
                                                        </td>
                                                      
                                         
                                                        <td style="min-width: 10px; text-align: center;">
                                                            <span style="display:none;">
                                                                   <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                <asp:Label ID="lblStudentPriority" runat="server" Text='<%# Eval("Request_Priority") %>'></asp:Label>
                                                                <asp:Label ID="lblManagerPriority" runat="server" Text='<%# Eval("Request_Priority") %>'></asp:Label>
                                                            </span>
                                                           <span>
                                                                    <asp:LinkButton ID="btnDeleteRequest" runat="server" ></asp:LinkButton>
                                                                  </span>
                                                            <span >
                                                                <asp:LinkButton ID="btnDownload" Visible="false" runat="server" data-toggle="tooltip" title="Download"><span class="fa fa-download" ></span></asp:LinkButton>
                                                            </span>
                                                                
                                                             
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

            <script type="text/javascript">
                function SearchResult() {
                    var input, filter, ul, li, a, i;
                    input = document.getElementById("<%= txtSearch.ClientID %>");
                    filter = input.value.toUpperCase();
                    ul = document.getElementById("RequestList");
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

