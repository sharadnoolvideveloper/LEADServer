<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Developer/DeveloperConsole.master" AutoEventWireup="true" CodeFile="CollegeMaster.aspx.cs" Inherits="Pages_Developer_CollegeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>


     <script type="text/javascript">
         /*     $(document).ready(function () {
                  $("#txtCollegeName").keyup(function (e) {
                      var keyCode = e.keyCode || e.which;
                      $("#CollegeNameError").html("");
                      var collegeName = $("#txtCollegeName").val();
     
                      $.ajax({
                          type: "GET",
                          url: '@Url.Action("checkCollegeExistence")', // Replace with your actual server-side route
                          data: { collegeName: collegeName },
                          success: function (exists) {
                              if (exists) {
                                  $("#CollegeNameError").html("College name already exists.");
                              }
                          },
                          error: function (error) {
                              console.log(error);
                          }
                      });
                  });
              })*/
         function SearchCollegeDetail() {
             var input, filter, ul, li, a, i;
             input = document.getElementById("txtCollegeSearch");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel">
                <div class="panel-heading">
                    <h3>College Master &nbsp;
                 <span class="text-danger">
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label></span>
                    </h3>
                </div>

                <div class="panel-body">
                  
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-3">
                                    <label>State</label>
                                    <asp:DropDownList ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>District</label>
                                    <asp:DropDownList ID="ddlDistrict" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>Taluka</label>
                                    <asp:DropDownList ID="ddlTaluka" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>College Type</label>
                                    <asp:DropDownList ID="ddlCollegeType" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-6">
                                    <label>
                                        College Name
                        <asp:RequiredFieldValidator ID="rfv1" ControlToValidate="txtCollegeName" ForeColor="Red" ValidationGroup="Developer" runat="server" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox ID="txtCollegeName" CssClass="form-control" placeholder="College Name" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label>Manager Name</label>
                                    <asp:DropDownList ID="ddlManagerName" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <br />
                                    <asp:Button ID="btnSave" CssClass="btn btn-primary btn-block" ValidationGroup="Developer" OnClick="btnSave_Click" runat="server" Text="Save" />
                                </div>
                                <div class="col-md-1">
                                    <br />
                                    <asp:LinkButton ID="btnClear" CssClass="btn btn-danger btn-block" OnClick="btnClear_Click" runat="server"><span class="fa fa-remove"></span> </asp:LinkButton>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-search"></i>&nbsp; Search</span>
                                        <input type="text" id="txtCollegeSearch" onkeyup="SearchCollegeDetail()" placeholder="Search" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="height: 500px; overflow: auto;" id="SearchList">
                                    <asp:Repeater ID="rptCollege" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-mails">
                                                <thead>
                                                    <tr>
                                                        <td>Slno</td>
                                                        <td style="display: none">College Id
                                                        </td>


                                                        <td><strong><b>State </b></strong></td>
                                                        <td><strong><b>District</b></strong></td>
                                                        <td><strong><b>Taluaka</b></strong>
                                                        </td>
                                                        <td><strong><b>College_Name</b></strong>
                                                        </td>

                                                        <td><strong><b>College_Type</b></strong>
                                                        </td>
                                                        <td><strong><b>Manager</b></strong>
                                                        </td>


                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tbody>
                                                <tr>
                                                    <td class="text-center">  <%# Container.ItemIndex + 1 %></td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblCollegeId" runat="server" Text='<%# Eval("CollegeId") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblState" Font-Size="Small" runat="server" Text='<%# Eval("StateName") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDistrict" Font-Size="Small" runat="server" Text='<%# Eval("DistrictName") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTalukaName" Font-Size="Small" runat="server" Text='<%# Eval("TalukaName") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCollegeName" Font-Size="Small" runat="server" Text='<%# Eval("CollegeName") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCollegeType" Font-Size="Small" runat="server" Text='<%# Eval("CollegeType") %>' />
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblManager" Font-Size="Small" runat="server" Text='<%# Eval("ManagerName") %>' />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

