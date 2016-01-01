/// <reference path="angular.min.js" />

var commonModule = angular.module('common', ['ngRoute']);
var appMainModule = angular.module('appMain', ['common']);

commonModule.factory('viewModelHelper', function ($http, $q) {
   
    return CarRental.viewModelHelper($http, $q);
});

commonModule.factory('validator', function () {
   
    return CarRental.validator();
});

(function (cr) {
    var viewModelHelper = function ($http, $q) {
        
        var self = this;
        self.modelIsValid = true;
        self.modelErrors = [];
        self.isLoading = false;

        self.apiGet = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.get(CarRental.rootPath + uri, data)
            .then(function (result) {
                success(result);
                if (always != null)
                    always();
                self.isLoading = false;
            }, function (result) {
                if (failure == null) {
                    if (result.status != 400)
                        self.modelErrors = [result.status + ':' + result.statusText];
                    else
                        self.modelErrors = [result.data.Message];
                }
                else
                    failure(result);
                if (always != null)
                    always();
                self.isLoading = false;

            });
        }

        self.apiPost = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.post(CarRental.rootPath + uri, data).then(function (result) {
                
                success(result);
                if (always != null)
                    always();
                self.isLoading = false;
            }, function (result) {
                alert(result.status);
                if (failure == null) {
                    self.modelIsValid = false;
                    alert(result.status);
                    if (result.status != 400)
                        self.modelErrors = [result.status + ':' + result.statusText];
                    else
                        self.modelErrors = [result.data.Message];
                }
                else
                    failure(result);
                if (always != null)
                    always();
                self.isLoading = false;

            });
        }
        return this;
    }
    cr.viewModelHelper = viewModelHelper;
   
    
}(window.CarRental));

(function (cr) {
    var mustEqual = function (value,other) {
        return value == other;
    }
    cr.mustEqual = mustEqual;
}(window.CarRental));

//window.valJs = {};

(function (cr) {
    var validator = function () {
       
        var self = this;
        
        self.PropertyRule = function (propertyName, rules) {
           
            var self = this;
            self.PropertyName = propertyName;
            self.Rules = rules;
            
        };

        self.ValidateModel = function (model, allPropertyRules) {
           
            var errors = [];
            var props = Object.keys(model);
            for (var i = 0; i < props.length; i++) {
                var prop = props[i];
                for (j = 0; j < allPropertyRules.length;j++){
                    var propertyRule = allPropertyRules[j];
                    if (prop == propertyRule.PropertyName) {
                        var propertyRules = propertyRule.Rules;

                        var propertyRuleProps = Object.keys(propertyRules);
                        for (k = 0; k < propertyRuleProps.length;k++){
                            var propertyRuleProp = propertyRuleProps[k];
                            if (propertyRuleProp != 'custom') {
                                var rule = rules[propertyRuleProp];
                                var params = null;
                                if (propertyRules[propertyRuleProp].hasOwnProperty('params'))
                                    params = propertyRules[propertyRuleProp].params;
                                var validationResult = rule.validator(model[prop], params);
                                if (!validationResult) {
                                    errors.push(getMessage(prop, propertyRuleProp, rule.message));
                                }
                            }
                            else {
                                var validator = propertyRules.custom.validator;
                                var value = null;
                                if (propertyRules.custom.hasOwnProperty('params')) {
                                    value = propertyRules.custom.params;
                                }
                                var result = validator(model[prop], value());
                                if (result != true) {
                                    errors.push(getMessage(prop,propertyRules.custom,'Invalid value'))
                                }
                            }
                        }

                    }
                }

            }
            model['errors'] = errors;
            model['isValid'] = (errors.length == 0);
        }
        var getMessage = function (prop,rule,defaultMessage) {
            var message = '';
            if (rule.hasOwnProperty('message'))
                message = rule.message;
            else
                message = prop + ': ' + defaultMessage;
            return message;
        }

        var rules = [];

        var setupRules = function () {
            rules['required'] = {
                validator: function (value,params) {
                    return !(value.trim() == '');
                },
                message:'value is required.'
            };

            rules['minLength'] = {
                validator: function (value, params) {
                    return !(value.trim().length < params);
                },
                message: 'value does not meet minimum length.'
            };

            rules['pattern'] = {
                validator: function (value, params) {
                    var regExp = new RegExp(params);
                    return !(regExp.exec(value.trim())==null)
                },
                message: 'value must match regular expression'
            };

            rules['email'] = {
                validator: function (value, params) {
                    var regExp = new RegExp('^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$');
                    return !(regExp.exec(value.trim()) == null)
                },
                message: 'value must be an email'
            };

        }

        setupRules();
        return this;
    }
    cr.validator = validator;

}(window.CarRental));
    