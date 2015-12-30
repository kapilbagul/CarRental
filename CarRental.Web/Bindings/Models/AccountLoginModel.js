(function (cr) {
    var AccountLoginModel = function () {
        var self = this;
        self.LoginEmail = '';
        self.RememberMe = false;
        self.Password=''
    }

   
    cr.AccountLoginModel = AccountLoginModel;
}(window.CarRental));
