<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="InternetSpeed.aspx.cs" Inherits="Pages_Student_InternetSpeed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="https://www.google.com/uds/?file=elements&amp;v=1&amp;packages=transliteration" type="text/javascript"></script>
    <link href="https://www.google.com/uds/api/elements/1.0/7ded0ef8ee68924d96a6f6b19df266a8/transliteration.css" type="text/css" rel="stylesheet">
    <script src="https://www.google.com/uds/api/elements/1.0/7ded0ef8ee68924d96a6f6b19df266a8/transliteration.I.js" type="text/javascript"></script>
    <script type="text/javascript">
        // Load the Google Transliteration API   

        google.load("elements", "1", {
            packages: "transliteration"
        });
        var ids = ""
        function onLoad() {
            $("#translControl").html("");
            var options = {
                sourceLanguage: 'en',
                destinationLanguage: ['hi', 'bn', 'gu', 'kn', 'ml', 'mr', 'pa', 'sa', 'ta', 'te', 'ur'],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };
            // Create an instance on TransliterationControl with the required         
            // options.         
            var control = new google.elements.transliteration.TransliterationControl(options);
            ids = ["txt_Search"];
            control.makeTransliteratable(ids);
            // Show the transliteration control which can be used to toggle between         
            // English and Hindi and also choose other destination language.         
            control.showControl('translControl');
        }
        google.setOnLoadCallback(onLoad);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblDownloadSpeed" runat="server" />
<asp:Button Text="Check Speed" runat="server" OnClick="CheckSpeed" />


    <div id="translControl">
        
    </div>
   <div class="inputapi-inline-block inputapi-transliterate-button" title="" role="button" tabindex="0" aria-haspopup="true" aria-expanded="false" aria-pressed="false" aria-activedescendant="" style="user-select: none;">
        <div class="inputapi-inline-block inputapi-transliterate-button-outer-box">
            <div class="inputapi-inline-block inputapi-transliterate-button-inner-box">
                <div class="inputapi-inline-block inputapi-transliterate-button-caption">
                    <div class="inputapi-transliterate-img-dropdown" style="height: 18px; width: 7px;"></div>
                </div>
            </div>
        </div>
    </div>

  <input name="txt_Search" type="text" id="txt_Search" style="color:#009999;border-color:#009999;border-style:Solid;font-size:Large;height:35px;width:40%;" dir="ltr">
  
   
</asp:Content>

