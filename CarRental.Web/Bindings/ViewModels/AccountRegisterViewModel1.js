
var accountRegisterModule = angular.module('accountRegister', ['common'])
                            .config(function ($routeProvider, $locationProvider) {
                                $routeProvider.when(CarRental.rootPath + 'account/register/step1', { templateUrl: CarRental.rootPath + 'Templates/RegisterStep1.html', controller: 'AccountRegisterStep1ViewModel' });
                                $routeProvider.when(CarRental.rootPath + 'account/register/step2', { templateUrl: CarRental.rootPath + 'Templates/RegisterStep2.html', controller: 'AccountRegisterStep2ViewModel' });
                                $routeProvider.when(CarRental.rootPath + 'account/register/step3', { templateUrl: CarRental.rootPath + 'Templates/RegisterStep3.html', controller: 'AccountRegisterStep3ViewModel' });
                                $routeProvider.when(CarRental.rootPath + 'account/register/confirm', { templateUrl: CarRental.rootPath + 'Templates/RegisterConfirm.html', controller: 'AccountRegisterConfirmViewModel' });
                                $routeProvider.otherwise({ redirectTo: CarRental.rootPath + 'account/register/step1' });
                            });
accountRegisterModule.controller("AccountRegisterViewModel", function ($scope,$http,$location,$window,viewModelHelper) {
    $scope.viewModelHelper = viewModelHelper;
    alert("Controller");
    $scope.accountModelStep1 = new CarRental.AccountRegisterModelStep1();
    $scope.accountModelStep2 = new CarRental.AccountRegisterModelStep2();
    $scope.accountModelStep3 = new CarRental.AccountRegisterModelStep3();

    $scope.previous = function () {
         $window.history.back();
    }
});

accountRegisterModule.controller("AccountRegisterStep1ViewModel", function ($scope,$http,$location,viewModelHelper,validator) {

});

accountRegisterModule.controller("AccountRegisterStep2ViewModel", function ($scope, $http, $location, viewModelHelper, validator) {

});
accountRegisterModule.controller("AccountRegisterStep3ViewModel", function ($scope, $http, $location, viewModelHelper, validator) {

});

accountRegisterModule.controller("AccountRegisterConfirmViewModel", function ($scope, $http, $location, viewModelHelper, validator) {

});