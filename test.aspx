<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            var mintext = 0;
            if ($("[id$=ddlSearchType]").val() == "Lead_Id") {
                mintext = 4;
            }
            else if ($("[id$=ddlSearchType]").val() == "Student_Name") {
                mintext = 3;
            }
            else if ($("[id$=ddlSearchType]").val() == "Projects") {
                mintext = 6;
            }
            else if ($("[id$=ddlSearchType]").val() == "Mobile_No") {
                mintext = 6;
            }
            $("[id$=txtSearch]").autocomplete({
                source: function (request, response) {
                    var searchtype = $("[id$=ddlSearchType]").val();
                  
                 
                    $.ajax({
                        url: '<%=ResolveUrl("~/test.aspx/GetSearchDetails") %>',
                        data: "{ 'prefix': '" + request.term + "', 'searchtype': '" + searchtype + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);
                },             
                minLength: mintext
            
            });
        });   
    </script>
        Enter search term:
        <asp:DropDownList ID="ddlSearchType"  runat="server">
            <asp:ListItem Text="Lead_Id" Value="Lead_Id"></asp:ListItem>
            <asp:ListItem Text="Student_Name" Value="Student_Name"></asp:ListItem>
            <asp:ListItem Text="Projects" Value="Projects"></asp:ListItem>
            <asp:ListItem Text="Mobile_No" Value="Mobile_No"></asp:ListItem>
        </asp:DropDownList>
    <asp:TextBox ID="txtSearch" runat="server" />
    <asp:HiddenField ID="hfCustomerId" runat="server" />
    <asp:Button ID="Button1" Text="Submit" runat="server" OnClick="Submit" />

        <br />

 <iframe src="https://calendar.google.com/calendar/embed?src=raghavendra.tech@dfmail.org&ctz=Asia%2FKolkata" style="border: 0" width="1000" height="700" frameborder="0" scrolling="yes"></iframe>

        <asp:TextBox ID="TextBox2" AutoPostBack="true"  OnTextChanged="TextBox2_TextChanged" runat="server"></asp:TextBox>
        <asp:DropDownList ID="ddlLead_Id" runat="server"></asp:DropDownList>

    </form>
  
  
</body>
</html>
