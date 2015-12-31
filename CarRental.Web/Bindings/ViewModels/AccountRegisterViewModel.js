/// <reference path="C:\My Projects\CarRental\CarRental.Web\Scripts/angular.min.js" />

/// <reference path="C:\My Projects\CarRental\CarRental.Web\Scripts/App.js" />
var accountRegisterModule = angular.module("accountRegister", ['common']);

accountRegisterModule.controller("accountRegisterViewModel", function ($scope, $http, viewModelHelper,$location,$window) {
    $scope.viewModelHelper = viewModelHelper;

    $scope.accountModelStep1 = new CarRental.AccountRegisterModelStep1();
    $scope.accountModelStep2 = new CarRental.AccountRegisterModelStep2();
    $scope.accountModelStep3 = new CarRental.AccountRegisterModelStep3();

    $scope.previous = function () {
        $window.history.back();
    }
});