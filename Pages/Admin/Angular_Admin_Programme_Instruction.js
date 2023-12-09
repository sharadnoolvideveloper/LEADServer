var app = angular.module('Programme', []);

app.controller('ProgrammeCTL', function ($scope, $http) {
    $scope.ClearTextbox = function () {
       
        $scope.Event_Type = "NEW";
        $scope.txtContent.val("");
    };
});