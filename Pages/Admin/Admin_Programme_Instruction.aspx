<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="Admin_Programme_Instruction.aspx.cs" Inherits="Pages_Admin_Admin_Programme_Instruction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script src="../../JS/CommonJS/jquery-1.9.1.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/CommonJS/toster.js"></script>
    <script src="../../JS/CommonJS/bootstrap-datepicker_fun.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/AngularJS.js"></script>
    <style>
        .list-group-item {
            padding: 1px 1px;
        }
    </style>
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
       
           $("#btnAdd").click(function () {
              //$scope.txtcontent.prop('disabled',false) 
               $("#txtContent").removeAttr("disabled");
               $("#Event_Type").text("NEW");           
               $("#txtContent").val("");
           });

           $("#btnSave").on('click', function () {
               var checkedValue;
               if ($('#Instruction').is(':checked')) {
                   checkedValue = "Instruction";
               }
               else if ($('#Benefit').is(':checked')) {
                   checkedValue = "Benefit";
               }
               else if ($('#Note').is(':checked')) {
                   checkedValue = "Note";
               }
               else if ($('#Links').is(':checked')) {
                   checkedValue = "Links";
               }

               var vmProgramme =
             {
                 Content: $("#txtContent").val(),
                 Event_Type: $("#Event_Type").text(),
                 type: checkedValue.toString(),

             };
               vmProgram = JSON.stringify({ 'vmProgram': vmProgramme });
               $.ajax({
                   type: "POST",
                   url: "Admin_Programme_Instruction.aspx/Admin_Save_Programme_Content",
                   
                   contentType: "application/json",
                   data: vmProgram,
                   success: function (data) {
                       $("#txtContent").val("");
                       $("#txtContent").prop('disabled', true);
                       $("#result").html(success(vmProgramme.type+" "+"Saved Successfully"));
                   },
                   error: function (x, y, z) {
                       alert(x.responseText.toString()+y.toString());
                   }
               });
               $("#Event_Type").text("SAVE");
               $("#txtContent").prop('disabled', true);
               $("#Event_Type").text("NEW");
               $("#txtContent").val("");
           });
       });
</script>

     <script>
         var app = angular.module('myApp', []);
        app.controller('GETProgrammeInstruction', function ($scope, $http) {
            $scope.getInstructions = function (value) {
                $scope.rdoSelected = value;
                 $("#txtContent").prop('disabled', true);
                $("#Event_Type").text("NEW");
                $("#txtContent").val("");
                var httpreq = {
                    method: 'POST',
                    url: 'Admin_Programme_Instruction.aspx/Admin_GET_Programme_List',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },

                    data: { Type: value }
                }
                $http(httpreq).success(function (response) {
                    $scope.Instructions = response.d;
                    //angular.forEach($scope.Instructions, function (value) {
                    //    if (value.Status == "1") {
                    //        $scope.btnStatusClass = "btn btn-info btn-floating";
                    //        $scope.btnSatausfaclass = "fa fa-thumbs-up";
                    //    }
                    //    else {
                    //        $scope.btnStatusClass = "btn btn-danger btn-floating";
                    //        $scope.btnSatausfaclass = "fa fa-thumbs-down";
                    //    }
                    //});
                })
            };           
            $scope.EnableDisable = function (slno, status) {
                var httpreq = {
                    method: 'POST',
                    url: 'Admin_Programme_Instruction.aspx/Admin_Update_Programme_Content_Status',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { slno: slno, Status:status }
                }
                $http(httpreq).success(function (response) {                   
                    $scope.getInstructions($scope.rdoSelected);
                })
            };
            $scope.EditInstruction = function (slno) {
                $("#Event_Type").text("EDIT");
                $("#txtContent").removeAttr("disabled");
                $("#txtContent").focus();
                $("#txtContent").val("");

                var http = {
                    method: 'POST',
                    url: 'Admin_Programme_Instruction.aspx/Admin_GET_Programme_ListBYId',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'dataType': 'json'
                    },
                    data: { Slno: slno }
                }
                $http(http).success(function (response) {
                    var vmProgramme =   {
                        Content: response.d[0].contents,
                        type: response.d[0].type,
                        type: checkedValue.toString(),

          };
                    vmProgram = JSON.stringify({ 'vmProgram': vmProgramme });


                    $scope.txtContent = response.d[0].contents;
                })
            };
        });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid" ng-app="myApp">
        <center><div id="result"></div></center>
        <div class="row" ng-controller="GETProgrammeInstruction">
            <label ng-model="rdoSelected" class="hidden"></label>
            <div class="col-md-6">
                <h3>
                    <label id="pnl_title">Page Title</label>
                    <span class="pull-right">
                        <label id="Event_Type" ng-model="Event_Type"></label>
                        <span class="btn btn-info btn-floating" id="btnAdd"><span class="fa fa-plus"></span></span>
                        <span class="btn btn-danger btn-floating" id="btnSave"><span class="fa fa-save"></span></span>
                    </span>
                </h3>
                <div class="row">
                    <div class="col-md-8">
                       
                        <label for="txtContent">Content</label>
                        <textarea rows="4" cols="50" id="txtContent" ng-model="txtContent" disabled="disabled" class="form-control" placeholder="Enter Content"></textarea>
                    </div>
                    <div class="col-md-4">
                        <label>Content Type</label>
                        <br />
                      <input type="radio" name="Content" id="Instruction" ng-model="Instruction" ng-click="getInstructions(Instruction)" value="Instruction"  ng-checked="true" /> Instruction<br>
                       <input type="radio" name="Content" id="Benefit" ng-model="Benefit" ng-click="getInstructions(Benefit)" value="Benefit" /> Benefit<br>
                        <input type="radio" name="Content" id="Note" ng-model="Note" ng-click="getInstructions(Note)" value="Note" /> Note<br>
                        <input type="radio" name="Content" id="Links" ng-model="Links" ng-click="getInstructions(Links)" value="Links" /> IMP Links<br>
                    </div>
                </div>


            </div>

            <div class="col-md-6" >
                <h3>
                    
                    <input type="text" ng-model="txtSearch" placeholder="Search for Contents" class="form-control" />
                  

                </h3>
                <div class="row" ng-init="getInstructions('Instruction')">
                    <div class="col-md-12">
                        <table class="table">
                            <thead>
                                <tr>
                                   <th>Slno</th>
                                    <th>Contents</th>
                                     <th>Edit</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr ng-repeat="x in Instructions  | filter:txtSearch | orderBy : 'slno'">
                                    <td>{{x.slno}}</td>
                                    <td>{{x.content }}</td>
                                     <td><a ng-click="EditInstruction(x.slno)" ng-model="btnEdit" class="btn btn-primary btn-floating"><span class="fa fa-pencil"></span> </a> </td>
                                    <td><a ng-click="EnableDisable(x.slno,x.Status)" ng-class="{'btn btn-info btn-floating':x.Status==1,'fa fa-thumbs-up':x.Status==1}" ><span  ng-class="{'btn btn-danger btn-floating':x.Status==0,'fa fa-thumbs-down':x.Status==0}"></span> </a> </td>
                                    
                                </tr>
                               
                            
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

