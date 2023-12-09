<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_Event_Programme.aspx.cs" Inherits="Pages_Admin_Admin_Event_Programme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/CommonJS/toster.js"></script>
    <script src="../../JS/CommonJS/bootstrap-datepicker_fun.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />

     <script>
        $(document).ready(function () {
          
            $("#<%=txtEventDescription.ClientID%>").on({
                focus: function () {
                    if (this.value.length > 450)
                        $("#<%=txtEventDescription.ClientID%>").css("border-color", "red");
                    else
                        $("#<%=txtEventDescription.ClientID%>").css("border-color", "#66afe9");
                    
                    $('#txtCount').text(this.value.length);
                    DisplayCharacterCount: false
                },
                keyup: function () {
                    if (this.value.length > 450)
                        $("#<%=txtEventDescription.ClientID%>").css("border-color", "red");
                     else
                        $("#<%=txtEventDescription.ClientID%>").css("border-color", "#66afe9");
                    $("#<%=txtEventDescription.ClientID%>").val(this.value);
                    $('#txtCount').text(this.value.length);
                    DisplayCharacterCount: false
                }
            })

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
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="col-md-12">
            <div class="row">

                <div class="col-md-12">
                    <div class="container-fluid">
                        <div class="panel">
                            <div class="panel-heading">
                                <asp:Label ID="lblEventButtonClick" Visible="false" runat="server" Text=""></asp:Label>
                                 <asp:Label ID="lblEditEventId" Visible="false" runat="server" Text=""></asp:Label>
                                <h3>Payment Gateway Events Creation
                                    <span class="pull-right">
                                        <asp:LinkButton ID="btnClear" OnClick="btnClear_Click" CssClass="btn btn-warning"  runat="server"><span class="fa fa-plus"></span> &nbsp; Add </asp:LinkButton>

                                        <asp:LinkButton ID="btnSaveEvent" OnClick="btnSaveEvent_Click" CssClass="btn btn-facebook" ValidationGroup="Events" runat="server"><span class="fa fa-check"></span> &nbsp; Save </asp:LinkButton>
                                    </span>
                                </h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label for="txtEventName">Event Name <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required" ForeColor="Red" ValidationGroup="Events" ControlToValidate="txtEventName"></asp:RequiredFieldValidator>   </label>
                                        <asp:TextBox ID="txtEventName" placeholder="Event Name" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                           
                                        <label for="txtEventName">
                                            Event Description
                                          <span id="txtCount"></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required" ForeColor="Red" ValidationGroup="Events" ControlToValidate="txtEventDescription"></asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtEventDescription" MaxLength="450" autocomplete="off" placeholder="Event Description" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <label for="txtEventName">Event From Date
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Required" ForeColor="Red" ValidationGroup="Events" ControlToValidate="txtEventFromDate"></asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtEventFromDate" placeholder="Event From Date" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="txtEventName">Event To Date
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Required" ForeColor="Red" ValidationGroup="Events" ControlToValidate="txtEventToDate"></asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtEventToDate" placeholder="Event To Date" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="txtEventFees">Event Fees
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Required" ForeColor="Red" ValidationGroup="Events" ControlToValidate="txtEventFees"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtEventFees" placeholder="Event Fees" autocomplete="off" CssClass="form-control" onkeypress="NumericOnly()" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="txtFirstPaymentFees">
                                            First Installment

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="* Required" ForeColor="Red" ValidationGroup="Events" ControlToValidate="txtFirstPaymentFees"></asp:RequiredFieldValidator>
                                            </label>
                                            <asp:TextBox ID="txtFirstPaymentFees" autocomplete="off" placeholder="First Installment" onkeypress="NumericOnly()" CssClass="form-control" runat="server"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                 <div class="row">
                                    <div class="col-md-4" style="height:200px;overflow:auto;">
                                        <label for="txtEventName">Select State</label>
                                        <asp:DataList ID="dlState" DataKeyField="code" CssClass="table table-hover"  RepeatDirection="Vertical" runat="server">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEventId" Visible="false"   runat="server" Text='<%# Eval("code") %>' />
                                                <asp:CheckBox ID="ChkState" runat="server" Text='<%# Eval("StateName") %>' />
                                                <br />
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                             
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:FileUpload ID="FileUpload1" onchange="Profile()" ToolTip="Select Event Image" runat="server" />

                                      
                                    </div>
                                    <div class="col-md-2 hidden">
                                      <h4>  <asp:CheckBox ID="ChkSendNotification" Text="Send Notification" CssClass="checkbox checkbox-info" runat="server" /></h4>
                                    </div>
                                    <div class="col-md-8">
                                        <span>
                                     <h4>   For Better Quality for Event use  &nbsp;<b class="text-danger">Width="1000px" Height="445px" </b></h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Image ID="imgEventImg" EnableTheming="True" runat="server" />
                                    </div>
                                    
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label for="txtStudentSearch">Search For Events</label>
                                         <input type="text" id="txtStudentSearch" onkeyup="SearchStudentDetail()" placeholder="Search for Events" class="form-control" />

                                        <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="width: 100%; height: 400px; overflow: auto" id="SearchList">

                                        <asp:Repeater ID="rptEvent" OnItemCommand="rptEvent_ItemCommand" OnItemDataBound="rptEvent_ItemDataBound" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-hover" style="width: 200%">
                                                    <thead>
                                                        <tr>
                                                            <td style="display: none">Event Id
                                                            </td>
                                                            <td>Event Name
                                                            </td>
                                                            <td >From Date
                                                            </td>
                                                            <td>To Date
                                                            </td>                                                         
                                                           <td>Fees
                                                            </td>  
                                                             <td>First Installment
                                                            </td>  
                                                            <td>Edit
                                                            </td>
                                                              <td>Delete
                                                            </td>
                                                             <td>Send Notification
                                                            </td>
                                                            
                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td align="center" style="display: none">
                                                            <asp:Label ID="lblEventId" runat="server" Text='<%# Eval("slno") %>' />
                                                        </td>
                                                       <td style="min-width: 40px;">
                                                            <asp:Label ID="lblEventName" Font-Size="Small" runat="server" Text='<%# Eval("event_name") %>' />
                                                        </td>
                                                        <td style="min-width: 40px;">
                                                            <asp:Label ID="lblEventFromDate" Font-Size="Small" runat="server" Text='<%# Eval("EventFromDate") %>' />
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:Label ID="lblEventToDate" Font-Size="Small" runat="server" Text='<%# Eval("EventToDate") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblStateName" Font-Size="Small" runat="server" Text='<%# Eval("Event_Fees") %>' />
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:Label ID="lblFistPayment" Font-Size="Small" runat="server" Text='<%# Eval("Event_FirstPayment") %>' />
                                                        </td>
                                                       <%--  <td style="min-width: 50px">
                                                              <asp:LinkButton ID="btnEditStatus" Text='<%# Eval("Status") %>' CommandArgument="StatusEdit" runat="server"></asp:LinkButton>
                                                           <asp:Label ID="lblStatus" Font-Size="Small" Visible="false" runat="server" Text='<%# Eval("Status") %>' />
                                                        </td>--%>
                                                        <td style="min-width: 50px;">                                                           
                                                            <asp:LinkButton ID="btnEdit" CssClass="btn btn-info btn-floating" CommandArgument="Edit" runat="server"><span class="fa fa-pencil"></span> </asp:LinkButton>
                                                        </td>
                                                         <td style="min-width: 50px;">                                                           
                                                            <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger btn-floating" CommandArgument="Delete" runat="server"><span class="fa fa-eraser"></span> </asp:LinkButton>
                                                        </td>
                                                        <td style="min-width: 50px;">
                                                            <asp:LinkButton ID="btnSendNotification" CssClass="btn btn-primary btn-floating" CommandArgument="Notification" runat="server"><span class="fa fa-bell"></span> </asp:LinkButton>
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
        function Profile() {
            var Avatar = document.querySelector('#<%=imgEventImg.ClientID %>');
            var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                Avatar.src = reader.result;
            }

            if (file) {

                reader.readAsDataURL(file);
            } else {
                Avatar.src = "";
            }
        }

    </script>

</asp:Content>

