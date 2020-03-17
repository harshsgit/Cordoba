

app.controller('AuditPointsController', function ($timeout, StoreSessionDetail, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder) {

    if (!(UserDetail.customer_id > 0)) {
        window.location.href = 'home/accessdenied';
    }
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.StoreDetailInSession = StoreSessionDetail;

    $scope.dtOptions = DTOptionsBuilder.newOptions()
        .withOption('bDestroy', true)
        .withOption("deferRender", true);



    $scope.AuditPoints = function () {
        var encryptCustomerId = encryptedServerString(UserDetail.customer_id);
        $http.get(configurationService.basePath + "API/PointsAuditApi/GetPointsAuditList?customer_id=" + encryptCustomerId)
            .then(function (response) {
                if (response && response.data) {
                    $scope.AuditPointsList = response.data;
                    var totalDeposit = 0;
                    var totalwithdraw = 0;
                    angular.forEach(response.data, function (item) {
                        if (item.Deposit != '-') {
                            totalDeposit += parseInt(item.Deposit);
                        }
                        else if (item.Withdrawal != '-') {
                            totalwithdraw += parseInt(item.Withdrawal);
                        }
                    });
                    $scope.totalBalance = totalDeposit + totalwithdraw;
                }
            })
            .catch(function (response) {

            })
            .finally(function () {

            });
    }

    $scope.AuditPoints();


});