(function (cr) {
    var ReserveCarModel = function () {
        var self = this;

        self.initialize = false;
        self.PickupDate = '';
        self.ReturnDate = '';
    }

    cr.ReserveCarModel = ReserveCarModel;
}(window.CarRental));