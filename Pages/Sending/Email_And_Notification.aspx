<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="Email_And_Notification.aspx.cs" Inherits="Pages_Sending_Email_And_Notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

       <script type='text/javascript'>
        $(document).ready(function () {

            $("#<%=rdoSMS_Kannada.ClientID %>").click(function () {

                $("#pnlEng").attr("style", "display:none");
                $("#pnlKan").attr("style", "display:block");
                $("<%=txtSMS_Kannada_Message.ClientID%>").text("");
                $('#counter_Eng').text("");

            });
            $("#<%=rdoSMS_English.ClientID %>").click(function () {

                $("#pnlKan").attr("style", "display:none");
                $("#pnlEng").attr("style", "display:block");
                $("<%=txtSMS_English_Message.ClientID%>").text("");
                $('#counter_Kan').text("");

            });
        });
    </script>
       <script type="text/javascript" src="//tinymce.cachefly.net/4.0/tinymce.min.js"></script>
    <script type="text/javascript">
        tinymce.init({ selector: 'textarea#ContentPlaceHolder1_txtEmail_Message', });
    </script>
    <script src="../../JS/CommonJS/jsapi.js"></script>
       <script type="text/javascript">

        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                [google.elements.transliteration.LanguageCode.KANNADA],
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required
            // options.
            var control =
            new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.
            control.makeTransliteratable(['<%=txtSMS_Kannada_Message.ClientID%>']);
        }
        google.setOnLoadCallback(onLoad);
    </script>

    <script>
        function handleInput(control) {

            var count = $(control).val().length;
            $('#counter_Eng').text(count);
            if (count > 150) {
                $("#<%=txtSMS_English_Message.ClientID%>").css("color", "red");
                $("#<%=txtSMS_English_Message.ClientID%>").css("border-color", "red");
            }
            else {
                $("#<%=txtSMS_English_Message.ClientID%>").css("color", "black");
                $("#<%=txtSMS_English_Message.ClientID%>").css("border-color", "green");
            }
        }
    </script>
    <script>
        function handle_Kan(control) {

            var count = $(control).val().length;

            $('#counter_Kan').text(count);

            if (count > 150) {
                $("#<%=txtSMS_Kannada_Message.ClientID%>").css("color", "red");
                $("#<%=txtSMS_Kannada_Message.ClientID%>").css("border-color", "red");
            }
            else {
                $("#<%=txtSMS_Kannada_Message.ClientID%>").css("color", "black");
                $("#<%=txtSMS_Kannada_Message.ClientID%>").css("border-color", "green");
            }
        }
    </script>
    <script type="text/javascript">
        function GetSelectedCasePartyAmount() {

            var Count = 0;
            $("#<%=grdEntrepreneur.ClientID%> input[id*='ChkCollect']:checkbox").each(function (index) {

                if ($(this).is(':checked')) {
                    Count++;
                }
                document.getElementById("<%= lblSelected_Count.ClientID %>").innerHTML = Count;
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div class="container">
        <h3>Notify Sender
        </h3>

        <div class="manager-wrapper">
            <a id="toggle_Menu" href="#" class="contact-navicon"><i class="icon ion-navicon-round"></i></a>
            <div class="manager-right">

                <asp:MultiView ID="mvMain" runat="server">

                    <asp:View ID="vmDashboard" runat="server">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card bg1  color25">
                                    <div class="card-body">
                                        Total Sent<br />
                                        <asp:Label ID="lblTotal_Sent" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-4">
                                <div class="card  bg4  color25">
                                    <div class="card-body">
                                        Total Success<br />
                                        <asp:Label ID="lblTotal_Success" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card  bg6  color25">
                                    <div class="card-body">
                                        Total Failed<br />
                                        <asp:Label ID="lblTotal_Failed" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card  bg3  color25">
                                    <div class="card-body">
                                        Total Mails<br />
                                        <asp:Label ID="lblTotal_Mails" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card  bg10  color25">
                                    <div class="card-body">
                                        Total Notificaitons<br />
                                        <asp:Label ID="lblTotal_Notificaitons" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card  bg18  color25">
                                    <div class="card-body">
                                        Total SMS<br />
                                        <asp:Label ID="lblTotal_SMS" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vmCompose" runat="server">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card">
                                    <div class="card-header">
                                        <h4>
                                            <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label></h4>
                                    </div>
                                    <div class="card-body">
                                        <asp:Panel ID="pnlMail" runat="server">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>
                                                        Subject
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required"
                                                        ForeColor="DarkRed" ValidationGroup="Mail" Display="Dynamic" ControlToValidate="txtSubject"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtSubject" CssClass="form-control" placeholder="Subject" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>
                                                        Mail Body
                                                    
                                                    </label>
                                                    <asp:TextBox ID="txtEmail_Message" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                    <br />
                                                    <asp:LinkButton ID="btnSend_Mail" CssClass="btn btn-primary" ValidationGroup="Mail" OnClick="btnSend_Mail_Click" runat="server"><span class="fa fa-send"></span>&nbsp; Send Mail </asp:LinkButton>

                                                    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlNotification" runat="server">
                                            <div class="row" style="display: none;">
                                                <div class="col-md-12">
                                                    <asp:RadioButton ID="rdoNotification_Kannada" Text="Kannada" GroupName="N_Lang" runat="server" />
                                                    &nbsp;
                                                   <asp:RadioButton ID="rdoNotificaiton_English" Text="English" GroupName="N_Lang" Checked="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>
                                                        Title
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Required"
                                                          ForeColor="DarkRed" ValidationGroup="Notification" Display="Dynamic" ControlToValidate="txtNotificaiton_Title"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtNotificaiton_Title" placeholder="Title" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>
                                                        Message
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Required"
                                                              ForeColor="DarkRed" ValidationGroup="Notification" Display="Dynamic" ControlToValidate="txtNotificaiton_Message"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtNotificaiton_Message" placeholder="Message" CssClass="form-control" TextMode="MultiLine" runat="server" Rows="5"></asp:TextBox>
                                                    <br />
                                                    <asp:LinkButton ID="btnSend_Notification" ValidationGroup="Notification" CssClass="btn btn-primary" OnClick="btnSend_Notification_Click" runat="server"><span class="fa fa-send"></span>&nbsp; Send Notification </asp:LinkButton>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlSMS" runat="server">
                                            <div class="row">
                                                <div class="col-md-12">


                                                    <asp:RadioButton ID="rdoSMS_Kannada" Text="Kannada" GroupName="SMS_Lang" runat="server" />
                                                    &nbsp;
                                                   <asp:RadioButton ID="rdoSMS_English" Text="English" GroupName="SMS_Lang" Checked="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>
                                                        Message
                                                    
                                                         <span id="counter_Kan" style="color: red; font-weight: bold"></span>
                                                        <span id="counter_Eng" style="color: red; font-weight: bold"></span>
                                                    </label>
                                                    <span id="pnlKan" style="display: none;">
                                                        <asp:TextBox ID="txtSMS_Kannada_Message" onKeyPress="handle_Kan(this);" onKeyDown="handle_Kan(this);" CssClass="form-control" TextMode="MultiLine" runat="server" Rows="5"></asp:TextBox>
                                                    </span>
                                                    <span id="pnlEng">
                                                        <asp:TextBox ID="txtSMS_English_Message" onKeyPress="handleInput(this);" onKeyDown="handleInput(this);" CssClass="form-control" TextMode="MultiLine" runat="server" Rows="5"></asp:TextBox>
                                                    </span>
                                                    <br />
                                                    <asp:LinkButton ID="btnSend_SMS" ValidationGroup="SMS" CssClass="btn btn-primary" OnClick="btnSend_SMS_Click" runat="server"><span class="fa fa-send"></span>&nbsp; Send SMS </asp:LinkButton>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="input-group col-md-6">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <asp:RadioButton ID="rdoEntrepreneurs" OnCheckedChanged="rdoEntrepreneurs_CheckedChanged" AutoPostBack="true" GroupName="Top" Text="" runat="server" /></span>
                                                </div>
                                                <asp:TextBox ID="TextBox1" Text="Send to Entrepreneur" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="input-group col-md-6">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <asp:RadioButton ID="rdoInternal_Team" OnCheckedChanged="rdoInternal_Team_CheckedChanged" AutoPostBack="true" GroupName="Top" Text="" runat="server" /></span>
                                                </div>
                                                <asp:TextBox ID="TextBox2" Text="Send to Internal Team" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlEntrepreneur" runat="server">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-md-6" style="display: none;">
                                                            <asp:Panel ID="pnlParticipant" runat="server">
                                                                <label>
                                                                    Add Entreprenuer
                                                                </label>
                                                                <div class="input-group">
                                                                    <asp:ListBox ID="lstSector" CssClass="lstbox " runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                                    <div class="input-group-append">
                                                                        <asp:LinkButton ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-success" runat="server">
                                    <i class="fa fa-search"></i> 
                                                                        </asp:LinkButton>

                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label><small>Search any thing in grid</small></label>
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text"><span class="fa fa-search"></span></span>
                                                                </div>
                                                                <asp:TextBox ID="txtSearch" placeholder="Search" autocomplete="off" onkeyup="Search_Gridview(this)" BackColor="beige" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12" style="overflow: auto; max-height: 450px; min-height: 250px; height: auto;">

                                                            <asp:CheckBox ID="ChkSelectAll" AutoPostBack="true" OnCheckedChanged="ChkSelectAll_CheckedChanged"
                                                                Text="All" runat="server" />
                                                            &nbsp;&nbsp;&nbsp;
                                                  <span class="color7">Count &nbsp;<asp:Label ID="lblSelected_Count" runat="server" Text=""></asp:Label></span>
                                                            <asp:GridView ID="grdEntrepreneur" runat="server" CssClass="table table-hover"
                                                                AutoGenerateColumns="False" EmptyDataText="No Entrepreneur Exists">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ChkCollect" onclick="GetSelectedCasePartyAmount();" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="4px" Wrap="False" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Slno">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="4px" Wrap="False" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="App" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSlno" runat="server" Text='<%# Eval("slno") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Entreprenuer Id">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEntreprenuer_Id" runat="server" Text='<%# Eval("Entreprenuer_Id") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MobileNo">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMobile_No" runat="server" Text='<%# Eval("mobile_no") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Email id">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmail_Id" runat="server" Text='<%# Eval("Email_Id") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Submited_By">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSubmited_By" runat="server" Text='<%# Eval("Submitted_By") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Device_ID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDevice_Id" runat="server" Text='<%# Eval("Device_ID") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vmProcessing" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-body">

                                        <table class="table">
                                            <tr>
                                                <th colspan="2">
                                                    <b>Notify Status</b>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td style="width: 12%;">Notify Type :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNotify_Type" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lblNotify_Slno" Visible="false" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>User Type :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUser_Type" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Subject :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Message :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Language :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLanguage" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Status :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblProccess" runat="server" Text="Processing...."></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Delete :
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger" OnClick="btnDelete_Click" runat="server"><span class="fa fa-trash"></span>  </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vmSent" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12" style="overflow: auto; max-height: 450px; min-height: 250px; height: auto;">

                                                <asp:GridView ID="grdSent" runat="server" CssClass="table table-hover"
                                                    AutoGenerateColumns="False" EmptyDataText="No Notify Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Slno">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="4px" Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="App" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%# Eval("slno") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Entreprenuer Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEntreprenuer_Id" runat="server" Text='<%# Eval("Applicant_Id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MobileNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobile_No" runat="server" Text='<%# Eval("mobile_no") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Email Id">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmail_Id" runat="server" Text='<%# Eval("Email_Id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notify Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotify_Type" runat="server" Text='<%# Eval("Notify_Type") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Type" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUser_Type" runat="server" Text='<%# Eval("User_Type") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Subject" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Message" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMessage" runat="server" Text='<%# Eval("message") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vmFailed" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12" style="overflow: auto; max-height: 450px; min-height: 250px; height: auto;">

                                                <asp:GridView ID="grdFail" runat="server" CssClass="table table-hover"
                                                    AutoGenerateColumns="False" EmptyDataText="No Notify Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Slno">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="4px" Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="App" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%# Eval("slno") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Entreprenuer Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEntreprenuer_Id" runat="server" Text='<%# Eval("Applicant_Id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MobileNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobile_No" runat="server" Text='<%# Eval("mobile_no") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Email Id">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmail_Id" runat="server" Text='<%# Eval("Email_Id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notify Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotify_Type" runat="server" Text='<%# Eval("Notify_Type") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Type" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUser_Type" runat="server" Text='<%# Eval("User_Type") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Subject" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Message" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMessage" runat="server" Text='<%# Eval("message") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </asp:View>
                </asp:MultiView>
            </div>
            <div class="manager-left">
                <asp:LinkButton ID="btnExcel" Visible="false" CssClass="btn btn-contact-new" OnClick="btnExcel_Click" runat="server">
                <span class="fa fa-file-excel-o"></span>&nbsp;Excel Report
                </asp:LinkButton>
                <nav class="nav">
                    <asp:LinkButton ID="btnMail" CssClass="nav-link active" OnClick="btnMail_Click" runat="server">
                        <span class="fa fa-envelope faa-shake animated-hover"></span>&nbsp;Email
                    <span>
                        <asp:Label ID="lblLeft_Total_Mail" runat="server" Text=""></asp:Label></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnNotification" CssClass="nav-link active" OnClick="btnNotification_Click" runat="server">
                        <span class="fa fa-bell faa-ring animated-hover"></span>&nbsp; Notification
                    <span>
                        <asp:Label ID="lblLeft_Total_Notificaiton" runat="server" Text=""></asp:Label></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnSMS" CssClass="nav-link active" OnClick="btnSMS_Click" runat="server">
                        <span class="fa fa-phone faa-vertical animated-hover"></span>&nbsp; SMS
                    <span>
                        <asp:Label ID="lblLeft_Total_SMS" runat="server" Text=""></asp:Label></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnProcessing" CssClass="nav-link active" OnClick="btnProcessing_Click" runat="server">
                      <span class="fa fa-dot-circle-o faa-vertical animated-hover"></span>&nbsp; Processing
                    <span>..</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnSent" CssClass="nav-link" OnClick="btnSent_Click" runat="server">
                        <span class="fa fa-send tx-primary faa-passing animated-hover"></span>&nbsp;Sent
                    <span>
                        <asp:Label ID="lblLeft_Total_Success" runat="server" Text=""></asp:Label></span>
                    </asp:LinkButton>

                    <asp:LinkButton ID="btnFailed" CssClass="nav-link" OnClick="btnFailed_Click" runat="server">
                        <span class="fa fa-remove tx-danger"></span>&nbsp; Failed
                    <span>
                        <asp:Label ID="lblLeft_Total_Failed" runat="server" Text=""></asp:Label></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnDashboard" CssClass="nav-link  active" OnClick="btnDashboard_Click" runat="server">
                        <span class="fa fa-dashboard"></span>Dashboard
                    <span>
                        <asp:Label ID="lblLeft_Total_Sent" runat="server" Text=""></asp:Label></span>
                    </asp:LinkButton>
                </nav>
            </div>
        </div>
    </div>
    <div id="POP_Confirm" class="modal fade effect-newspaper" role="dialog">
        <div class="modal-dialog ">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body  tx-center ">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <i class="icon icon ion-ios-close-outline tx-100 tx-danger lh-1 mg-t-20 d-inline-block"></i>
                    <h4>
                        <span class="fa fa-warning text-warning"></span>&nbsp;
                            <asp:Label ID="lblConfirm_Text" CssClass="text-danger" Text="Are you sure you want to send sms it will cost effective" runat="server"></asp:Label>
                    </h4>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnSMS_No" CssClass="btn btn-secondary" runat="server"><span class="fa fa-remove"></span> &nbsp;No </asp:LinkButton>
                    <asp:LinkButton ID="btnSMS_Yes" OnClick="btnSMS_Yes_Click" CssClass="btn btn-primary" runat="server"><span class="fa fa-send"></span>&nbsp; Yes Send SMS</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div id="POP_ErrorMsg" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-body">
                    <h4>
                        <span class="fa fa-warning text-warning"></span>&nbsp;
                            <asp:Label ID="lblErroMessage" CssClass="text-danger" runat="server"></asp:Label>
                    </h4>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            $('#toggle_Menu').on('click', function (e) {
                e.preventDefault();
                $('.manager-left').toggleClass('d-block');
                $('.manager-right').toggleClass('d-none');
            });
        });
    </script>
    <script type="text/javascript">
        function POP_Confirm() {
            $('#POP_Confirm').modal({
                show: true
            });
        }
    </script>
    <script type="text/javascript">
        function POP_ErrorMsg() {
            $('#POP_ErrorMsg').modal({
                show: true
            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.lstbox').multiselect({
                includeSelectAllIfMoreThan: true,
                enableFiltering: true
            });
        });
    </script>
    <script type="text/javascript">
        function Search_Gridview(strKey) {
            var strGV = '<%= grdEntrepreneur.ClientID %>'
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>

</asp:Content>

