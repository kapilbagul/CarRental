
/// <reference path="C:\My Projects\CarRental\CarRental.Web\Scripts/angular.min.js" />
/// <reference path="C:\My Projects\CarRental\CarRental.Web\Scripts/App.js" />

appMainModule.controller("AccountLoginViewModel", function ($scope, viewModelHelper) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.accountModel = new CarRental.AccountLoginModel();
    $scope.returnUrl = '';
   
    $scope.login = function () {
        viewModelHelper.apiPost("api/account/login", $scope.accountModel, function (result) {
            if ($scope.returnUrl != '' && $scope.returnUrl.length > 1) {
                
                window.location.href = CarRental.rootPath + $scope.returnUrl.substring(1);
            }
            else
            {
                alert(CarRental.rootPath)
                window.location.href = CarRental.rootPath;
            }
        });
    };
});

