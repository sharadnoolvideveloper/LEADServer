<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="Pages_Manager_TestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/df-style.css" rel="stylesheet" />
    <script src="../../JS/CommonJS/toster.js"></script>
    <script src="../../JS/CommonJS/AngularJS.js"></script>

    <script>
        var mainApp = angular.module("mainApp", []);
        
        main.controller('studentcontroller', function ($scope) {
            $scope.student = {
                firstname: "Mahesh",
                lastname: "noolvi",
                fees: 500,
                subjects: [
                    { name: 'Physics', marks: 70 },
                    { name: 'Chemestry', marks: 68 },
                    { name: 'Maths', marks: 60 }
                ],
                fullname: function () {
                    var studentObject;
                    studentObject = $scope.student;
                    return studentObject.firstname + "" + student.lastname;
                }
            };
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div ng-app="myapp" ng-controller="studentcontroller">
      <table class="table table-bordered">
          <tr>
              <td>Enter first name</td>
              <td>
                  <asp:TextBox ID="TextBox1" ng-model="student.firstname" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
              <td>Enter Last Name</td>
              <td>
                  <asp:TextBox ID="TextBox2" ng-model="student.lastname" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
              <td>Enter Fees</td>
              <td>
                  <asp:TextBox ID="TextBox3" ng-model="student.fees" runat="server"></asp:TextBox></td>
          </tr>
           <tr>
              <td>Enter Subjects</td>
              <td>
                  <asp:TextBox ID="TextBox4" ng-model="subjectName" runat="server"></asp:TextBox></td>
          </tr>
      </table>
      <br />
      <table class="table">
          <tr>
              <td>Name in Upper Case</td>
              <td>{{student.fullName() | uppercase}}</td>
          </tr>
           <tr>
              <td>Name in Lower Case</td>
              <td>{{student.fullName() | lowercase}}</td>
          </tr>
          <tr>
              <td>Name in Fees</td>
              <td>{{student.fees() | currency}}</td>
          </tr>

            <tr>
              <td>Subjects</td>
              <td>
                  <ul>
                      <li ng-repeat="subject in student.subjects | filter:SubjectName | orderBy:marks">
                          {{subject.name+',marks:'+subject.marks}}
                      </li>
                  </ul>
              </td>
          </tr>
      </table>
  </div>
</asp:Content>

