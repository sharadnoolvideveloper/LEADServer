<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="LeadStories.aspx.cs" Inherits="Pages_Admin_LeadStories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
      <script src="../../JS/StudentJS/bootstrap.min.js"></script> 
     <script src="../../JS/CommonJS/toster.js"></script>
      <script src="../../JS/CommonJS/bootstrap-datepicker_fun.min.js"></script>
      <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/components.css" rel="stylesheet" />

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
    <script>
        $(document).ready(function () {
          
            $("#<%=txtLeadStory.ClientID%>").on({
                focus: function () {
                    if (this.value.length > 510)
                        $("#<%=txtLeadStory.ClientID%>").css("border-color", "red");
                   
                    else
                        $("#<%=txtLeadStory.ClientID%>").css("border-color", "#66afe9");
                    
                    $('#txtCount').text(this.value.length);
                    DisplayCharacterCount: false
                },
                keyup: function () {
                    if (this.value.length > 510)
                        $("#<%=txtLeadStory.ClientID%>").css("border-color", "red");
                     else
                        $("#<%=txtLeadStory.ClientID%>").css("border-color", "#66afe9");
                    $("#<%=txtLeadStory.ClientID%>").val(this.value);
                    $('#txtCount').text(this.value.length);
                    DisplayCharacterCount: false
                }
            })

        });
       
    </script>
    <script runat="server">
        protected void rdoContentAndImage_CheckedChanged(object sender, System.EventArgs e)
        {
            txtStoryURL.Visible = false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div class="container-fluid">
      <div class="panel" style="background-color:white;">
          <div class="panel-heading">
              <h3>LEAD Stories <span class="pull-right">
                   <asp:LinkButton ID="btnClear" CssClass="btn btn-primary" OnClick="btnClear_Click"  runat="server"><span class="fa fa-plus"></span> &nbsp; add </asp:LinkButton>
                  <asp:LinkButton ID="btnSaveStory" CssClass="btn btn-primary" OnClick="btnSaveStory_Click" ValidationGroup="Story" runat="server"><span class="fa fa-save"></span> &nbsp; Save Story </asp:LinkButton></span></h3>
         </div>
          <div class="panel-body">
              <asp:Label ID="lblEditStatus" Visible="false" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblEditSlno" Visible="false" runat="server" Text=""></asp:Label>
              <div class="row">
                 
                   <div class="col-md-12">
                       <div class="row ">
                           <div class="col-md-6">
                               <label for="txtStoryTitle">Enter Story Title <span class="text-danger"></span>  &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtStoryTitle" ForeColor="DarkRed" ValidationGroup="Story" runat="server" ErrorMessage="* Title Required"></asp:RequiredFieldValidator> </label>
                               <asp:TextBox ID="txtStoryTitle" CssClass="form-control"  placeholder="Enter Story Title In Short" runat="server"></asp:TextBox>
                           </div>
                            <div class="col-md-6">
                               <label for="txtStoryURL">Paste Story <span class="text-danger">URL</span>  </label>
                               <asp:TextBox ID="txtStoryURL" CssClass="form-control"  placeholder="Paste Story URL Here" runat="server"></asp:TextBox>
                           </div>
                       </div>
                       <div class="row">
                           <div class="col-md-9">
                               <label for="txtStoryTitle">Enter Story Description 
                                   <span id="txtCount"></span>
                               </label>
                               <asp:TextBox ID="txtLeadStory" CssClass="form-control" MaxLength="510" TextMode="MultiLine" Rows="6" placeholder="Enter Full Story" runat="server"></asp:TextBox>
                           </div>

                         <div class="col-md-3">
                               <br />
                               <asp:RadioButtonList ID="rdoStoryType" CssClass="radio radio-custom radio-info" OnSelectedIndexChanged="rdoStoryType_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                   <asp:ListItem Text="Content With Image" Value="Content With Image" Selected="True"></asp:ListItem>
                                   <asp:ListItem Text="Content With Image With Link" Value="Content With Image With Link"></asp:ListItem>
                                   <asp:ListItem Text="Story Card" Value="Story Card"></asp:ListItem>
                                   <asp:ListItem Text="Video Story" Value="Video Card"></asp:ListItem>
                               </asp:RadioButtonList>
                           </div>
                       </div>
                       <div class="row" id="VideoReadMore" runat="server">
                           <div class="col-md-9">
                                <label for="txtStoryTitle">Video Story Read More URL 
                                   
                               </label>
                               <asp:TextBox ID="txtVideoReadMoreURL" CssClass="form-control" placeholder="Video Story Read More URL" runat="server"></asp:TextBox>
                           </div>
                       </div>
                       <div class="row">
                            <div class="col-md-6" id="cover" runat="server">
                                <span class="text-danger">For Better Quality Upload Width=500px; height=350px;</span>

                                <div class="row">
                                    <div class="col-md-12">
                                         <label for="FileUpload1">Select Event Image</label>
                               <asp:FileUpload ID="FileUpload1" onchange="Profile()" ToolTip="Select Event Image" runat="server" /> 
                                    </div>
                                </div>
                           </div>
                            <div class="col-md-6" id="Card" runat="server">
                                <span class="text-danger">For Better Quality Upload Width=500px; height=700px;</span>
                                <div class="row">
                                    <div class="col-md-12">
                                         <label for="FileUpload1">Select Card Image</label>
                               <asp:FileUpload ID="FileUpload2" onchange="CoverImage()" ToolTip="Select Event Image" runat="server" /> 
                                    </div>
                                </div>

                           </div>
                       </div>
                 
                      
                       <div class="row">
                           <div class="col-md-5 ">
                               <asp:Image ID="imgStoryCover" CssClass="img-responsive img-thumbnail "  EnableTheming="true" runat="server" />
                           </div>
                           <div class="col-md-1">
                               <asp:LinkButton ID="btnUploadStoryCoverImage" CssClass="btn btn-rounded btn-primary" OnClick="btnUploadStoryCoverImage_Click"  runat="server"><span class="fa fa-upload"></span> </asp:LinkButton>
                           </div>
                           
                       </div>
                       <div class="row">
                            <div class="col-md-5">
                               <asp:Image ID="imgStoryCard"   CssClass="img-responsive img-thumbnail "  EnableTheming="true" runat="server" />
                           </div>
                           <div class="col-md-1">
                               <asp:LinkButton ID="btnUploadStoryCardImage" CssClass="btn btn-rounded btn-primary" OnClick="btnUploadStoryCardImage_Click"  runat="server"><span class="fa fa-upload"></span> </asp:LinkButton>
                           </div>
                       </div>
                  </div>
              </div>
            
                  <div class="row" style="height: 550px; overflow: auto;">
                          <div class="col-md-12">
                              <h4>Search a Stories to Edit</h4>
                              <div class="input-group">
                                  <div class="input-group-addon bg-info">
                                      <span class="input-group-text"><span class="fa fa-search"></span></span>
                                  </div>
                                  <input type="text" id="txtStorySearch" onkeyup="SearchLeadStory()" placeholder="Search for Stories" class="form-control" />
                              </div>
                              <br />
                              <ul class="list-group" id="SearchList">
                                  <asp:Repeater ID="rptLeadStory" OnItemCommand="rptLeadStory_ItemCommand" OnItemDataBound="rptLeadStory_ItemDataBound" runat="server">
                                      <ItemTemplate>
                                          <a>
                                              <li class="list-group-item">
                                                  <asp:Label ID="lblSlno" Visible="false" runat="server" Text='<% # Eval("slno") %>'></asp:Label>
                                                  <asp:LinkButton ID="btnStoryTitle" Text='<% # Eval("Story_Title") %>' CommandArgument='(StatusGet)+"_"+("StatusGet")' runat="server"></asp:LinkButton>
                                                   <asp:Label ID="lblCreatedDate" Font-Size="Small" runat="server" Text='<% # Eval("Created_Date") %>'></asp:Label>
                                                  


                                                  <span class="pull-right">
                                                      <asp:LinkButton ID="btnEditStatus" Text='<%# Eval("Status") %>' CommandArgument='<%# Eval("Status")+"_"+("StatusEdit") %>' runat="server"></asp:LinkButton>

                                                       <asp:LinkButton ID="btnNotification"  Text='<%# Eval("Status") %>'  CommandArgument='<%# Eval("Status")+"_"+("Notification") %>' runat="server"></asp:LinkButton>

                                                       <asp:LinkButton ID="btnDelete" CommandArgument='<%# Eval("Status")+"_"+("Delete") %>' runat="server"></asp:LinkButton>
                                                  </span>

                                              </li>
                                          </a>
                                      </ItemTemplate>
                                  </asp:Repeater>
                              </ul>
                          </div>
                      </div>
             
          </div>
      </div>
    </div>
    <script type="text/javascript">
        function SearchLeadStory() {
            var input, filter, ul, li, a, i;
            input = document.getElementById("txtStorySearch");
            filter = input.value.toUpperCase();
            ul = document.getElementById("SearchList");
            li = ul.getElementsByTagName("li");
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
            var Avatar = document.querySelector('#<%=imgStoryCover.ClientID %>');
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
    <script type="text/javascript">
        function CoverImage() {
            var Avatar = document.querySelector('#<%=imgStoryCard.ClientID %>');
            var file = document.querySelector('#<%=FileUpload2.ClientID %>').files[0];
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

