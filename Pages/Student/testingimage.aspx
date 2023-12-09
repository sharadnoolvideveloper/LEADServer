<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="testingimage.aspx.cs" Inherits="Pages_Student_testingimage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/CommonJS/croppie.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
    <link href="../../CSS/CommonCSS/croppie.css" rel="stylesheet" />
   <style>
    input[type="file"] {

     display:block;
    }
    .imageThumb {
     max-height: 75px;
     border: 2px solid;
     margin: 10px 10px 0 0;
     padding: 1px;
     }

    input[type="file"] {
  display: block;
}
.imageThumb {
  max-height: 75px;
  border: 2px solid;
  padding: 1px;
  cursor: pointer;
}
.pip {
  display: inline-block;
  margin: 10px 10px 0 0;
}
.remove {
  display: block;
  background: #444;
  border: 1px solid black;
  color: white;
  text-align: center;
  cursor: pointer;
}
.remove:hover {
  background: white;
  color: black;
}
    </style>
     <script type="text/javascript">
         $(document).ready(function () {
             if (window.File && window.FileList && window.FileReader) {
                 $("#files").on("change", function (e) {
                     var files = e.target.files,
                       filesLength = files.length;
                     for (var i = 0; i < filesLength; i++) {
                         var f = files[i]
                         var fileReader = new FileReader();
                         fileReader.onload = (function (e) {
                             var file = e.target;
                             $("<span class=\"pip\">" +
                               "<img class=\"imageThumb\" src=\"" + e.target.result + "\" title=\"" + file.name + "\"/>" +
                               "<br/><span class=\"remove\">Remove image</span>" +
                               "</span>").insertAfter("#files");
                             $(".remove").click(function () {
                                 $(this).parent(".pip").remove();
                                 $files.files.remove;
                                 $("#files")[0].files[0]
                             });

                             // Old code here
                             /*$("<img></img>", {
                               class: "imageThumb",
                               src: e.target.result,
                               title: file.name + " | Click to remove"
                             }).insertAfter("#files").click(function(){$(this).remove();});*/

                         });
                         fileReader.readAsDataURL(f);
                     }
                 });
             } else {
                 alert("Your browser doesn't support to File API")
             }
         });

    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div  class="nectar-video-wrap" data-bg-alignment="" style="opacity: 1; width: 100%; height: 100%;"><div class="nectar-youtube-bg" style="opacity: 1; width: 100%; height: 100%;"><div class="vc_video-bg"><iframe class="inner" frameborder="0" allowfullscreen="1" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" title="YouTube video player" width="100%" height="100%" src="https://www.youtube.com/embed/EC5ooEheKJ0?playlist=EC5ooEheKJ0&amp;iv_load_policy=3&amp;enablejsapi=1&amp;disablekb=1&amp;autoplay=1&amp;controls=0&amp;showinfo=0&amp;rel=0&amp;loop=1&amp;origin=https%3A%2F%2Fwww.kakatiyasandbox.org&amp;widgetid=1" id="widget2" style="max-width: 1000%; margin-left: 0px; margin-top: -53px; width: 1583px; height: 890.438px; opacity: 1;"></iframe></div></div></div>
</asp:Content>

