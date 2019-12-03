app.controller('ContactUsController', function (StoreSessionDetail, UserDetail, $timeout, $state, $http, $location, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q, vcRecaptchaService) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.googleresponse = '';
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.contactUsObj = {
        firstname: '',
        lastname: '',
        email: '',
        phone: '',
        description: ''
    }

    $scope.SendContactUsDetail = function (form) {
        $scope.captchaValid = true;
        //if (grecaptcha.getResponse() == undefined || grecaptcha.getResponse() == '' || grecaptcha.getResponse() == null) {  
        //    return false;

        //}
        //else {
        //    $scope.captchaValid = true;
        //}
        if (form.$valid && $scope.googleresponse.length > 0) {
            $http.post(configurationService.basePath + "API/ContactUsAPI/SendContactUsDetails?firstname=" + $scope.contactUsObj.firstname + "&lastname=" + $scope.contactUsObj.lastname + "&email=" + $scope.contactUsObj.email + "&phone=" + $scope.contactUsObj.phone + "&description=" + $scope.contactUsObj.description.replace(/#/g, '%23'), $scope.StoreDetailInSession)
                .then(function (response) {
                    $scope.contactUsObj = new Object();
                    toastr.success("Successfully Submitted.");
                    $scope.contactForm.$submitted = false;
                    vcRecaptchaService.reload($scope.widgetId);
                    $scope.googleresponse = '';
                })
                .catch(function (response) {

                })
                .finally(function () {

                });
        }
    }
    
    $scope.widgetId = null;
    $scope.model = {
        key: '6Lcab8IUAAAAAArxcfMunWISZAbIq90Gxwea2Utj' //6LdkjMUUAAAAAD_upY8uGXMUUGRYEVNZYrI9WRZs
    };
    $scope.setResponse = function (response) {
        $scope.googleresponse = response;
    };
    $scope.setWidgetId = function (widgetId) {
        $scope.widgetId = widgetId;
    };
    $scope.cbExpiration = function () {
        vcRecaptchaService.reload($scope.widgetId);
        $scope.googleresponse = null;
    };
})