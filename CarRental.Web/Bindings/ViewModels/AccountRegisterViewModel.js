/// <reference path="C:\My Projects\CarRental\CarRental.Web\Scripts/angular.min.js" />


var accountRegisterModule = angular.module('accountRegister', ['common'])
                            .config(function ($routeProvider, $locationProvider) {
                                $routeProvider.when(CarRental.rootPath + 'account/register/step1', { templateUrl: CarRental.rootPath + 'Templates/RegisterStep1.html', controller: 'AccountRegisterStep1ViewModel' });
                                $routeProvider.when(CarRental.rootPath + 'account/register/step2', { templateUrl: CarRental.rootPath + 'Templates/RegisterStep2.html', controller: 'AccountRegisterStep2ViewModel' });
                                $routeProvider.when(CarRental.rootPath + 'account/register/step3', { templateUrl: CarRental.rootPath + 'Templates/RegisterStep3.html', controller: 'AccountRegisterStep3ViewModel' });
                                $routeProvider.when(CarRental.rootPath + 'account/register/confirm', { templateUrl: CarRental.rootPath + 'Templates/RegisterConfirm.html', controller: 'AccountRegisterConfirmViewModel' });
                                $routeProvider.otherwise({ redirectTo: CarRental.rootPath + 'account/register/step1' });
                                
                            });

accountRegisterModule.controller('AccountRegisterViewModelctl', function ($scope, $window, viewModelHelper) {
    $scope.viewModelHelper = viewModelHelper;

    $scope.accountModelStep1 = new CarRental.AccountRegisterModelStep1();
    $scope.accountModelStep2 = new CarRental.AccountRegisterModelStep2();
    $scope.accountModelStep3 = new CarRental.AccountRegisterModelStep3();

    $scope.previous = function () {
        $window.history.back();
    }
});



accountRegisterModule.controller("AccountRegisterStep1ViewModel", function ($scope, $http, viewModelHelper, $location, $window, validator) {
    
    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    var accountModelStep1Rules = [];

    var setupRules = function () {
        accountModelStep1Rules.push(new validator.PropertyRule("FirstName", {
            required: {message:"First Name is required."}
        }));
        accountModelStep1Rules.push(new validator.PropertyRule("LastName", {
            required: { message: "Last Name is required." }
        }));
        accountModelStep1Rules.push(new validator.PropertyRule("Address", {
            required: { message: "Address is required." }
        }));

        accountModelStep1Rules.push(new validator.PropertyRule("State", {
            required: { message: "State is required." }
        }));

        accountModelStep1Rules.push(new validator.PropertyRule("ZipCode", {
            required: { message: "Zip Code is required." },
            pattern: { message: "Zip code is in invalid format", params: /^\d{5}$/ }
            }));
    }

    $scope.step2 = function () {
        validator.ValidateModel($scope.accountModelStep1, accountModelStep1Rules);
        viewModelHelper.modelIsValid = $scope.accountModelStep1.isValid;
        viewModelHelper.modelErrors = $scope.accountModelStep1.errors;
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/account/register/validate1', $scope.accountModelStep1, function (result) {
                $scope.accountModelStep1.intitialized = true;
                $location.path(CarRental.rootPath + 'account/register/step2');
            });
        }
    }

    setupRules();
});

accountRegisterModule.controller("AccountRegisterStep2ViewModel", function ($scope, $http, viewModelHelper, $location, $window, validator) {
    if ($scope.accountModelStep1.intitialized != true) {
        $location.path(CarRental.rootPath + 'account/register/step1');
    }
    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    var accountModelStep2Rules = [];

    var setupRules = function () {
        accountModelStep2Rules.push(new validator.PropertyRule("LoginEmail", {
            required: { message: "Login Email is required." }
        }));
        accountModelStep2Rules.push(new validator.PropertyRule("Password", {
            required: { message: "Password is required." },
            minLength:{message:"Password must be of minimum 6 characters",params:6}
        }));
        accountModelStep2Rules.push(new validator.PropertyRule("PasswordConfirm", {
            required: { message: "Password Confirm is required." },
            custom: {
                validator: CarRental.mustEqual,
                message: "Password do not match",
                params: function () { return $scope.AccountRegisterModelStep2.Password;}
            }
        }));

       
    }

    $scope.step3 = function () {
        validator.ValidateModel($scope.accountModelStep2, accountModelStep2Rules);
        viewModelHelper.modelIsValid = $scope.accountModelStep2.isValid;
        viewModelHelper.modelErrors = $scope.accountModelStep2.errors;
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/account/register/validate2', $scope.accountModelStep2, function (result) {
                $scope.accountModelStep2.intitialized = true;
                $location.path(CarRental.rootPath + 'account/register/step3');
            });
        }
    }

    setupRules();
});

accountRegisterModule.controller("AccountRegisterStep3ViewModel", function ($scope, $http, viewModelHelper, $location, $window, validator) {

    if ($scope.accountModelStep2.intitialized != true) {
        $location.path(CarRental.rootPath + 'account/register/step1');
    }
    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    var accountModelStep3Rules = [];

    var setupRules = function () {
        accountModelStep3Rules.push(new validator.PropertyRule("CreditCard", {
            required: { message: "Credit Card # is required." },
            pattern: { message: "Credit Card is in invalid format(16 digit)", params: /^\d{16}$/ }
            }));
        accountModelStep3Rules.push(new validator.PropertyRule("ExpDate", {
            required: { message: "Expiration date is required." }
        }));
       
    }

    $scope.confirm = function () {
        validator.ValidateModel($scope.accountModelStep3, accountModelStep3Rules);
        viewModelHelper.modelIsValid = $scope.accountModelStep3.isValid;
        viewModelHelper.modelErrors = $scope.accountModelStep3.errors;
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/account/register/validate3', $scope.accountModelStep3, function (result) {
                $scope.accountModelStep3.intitialized = true;
                $location.path(CarRental.rootPath + 'account/register/confirm');
            });
        }
    }

    setupRules();
});

accountRegisterModule.controller("AccountRegisterConfirmViewModel", function ($scope, $http, viewModelHelper, $location, $window, validator) {
    if ($scope.accountModelStep3.intitialized != true) {
        $location.path(CarRental.rootPath + 'account/register/step1');
    }

    $scope.createAccount = function () {
        var accountModel;
        accountModel = $.extend(accountModel, $scope.accountModelStep1);
        accountModel = $.extend(accountModel, $scope.accountModelStep2);
        accountModel = $.extend(accountModel, $scope.accountModelStep3);

        viewModelHelper.apiPost('api/account/register', accountModel, function (result) {
            $window.location.href = CarRental.rootPath;
        });
    }
});