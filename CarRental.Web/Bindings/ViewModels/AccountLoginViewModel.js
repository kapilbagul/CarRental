﻿
/// <reference path="C:\My Projects\CarRental\CarRental.Web\Scripts/angular.min.js" />
/// <reference path="C:\My Projects\CarRental\CarRental.Web\Scripts/App.js" />

appMainModule.controller("AccountLoginViewModel", function ($scope, viewModelHelper, validator) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.accountModel = new CarRental.AccountLoginModel();
    $scope.returnUrl = '';

    var accountModelRules = [];
    var setupRules = function () {
        accountModelRules.push(new validator.PropertyRule("LoginEmail", {
            required: { message: "Login is required" }
            //email:{message:"Login must be in email format"}

        }));

        accountModelRules.push(new validator.PropertyRule("Password", {
            required: { message: "Password is required" },
            minLength:{message:"Password must be at least 6 characters",params:6}

        }));
    }
   
    $scope.login = function () {
        validator.ValidateModel($scope.accountModel, accountModelRules);
        viewModelHelper.modelIsValid = $scope.accountModel.isValid;
        viewModelHelper.modelErrors = $scope.accountModel.errors;
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost("api/account/login", $scope.accountModel,
                function (result) {
                if ($scope.returnUrl != '' && $scope.returnUrl.length > 1) {

                    window.location.href = CarRental.rootPath + $scope.returnUrl.substring(1);
                }
                else
                {
                   window.location.href = CarRental.rootPath;
                }
                
                
                });
        }
        
    };

    setupRules();
});

