app.controller('OrderDetailController', function ($timeout, $state, StoreSessionDetail, UserDetail, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, OrderStatusEnum) {

    if (!(UserDetail.customer_id > 0)) {
        window.location.href = 'home/accessdenied';
    }
    decodeParams($stateParams);
    BindToolTip();
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.order_id = parseInt($stateParams.OrderId);

    $scope.OrderStatusEnum = OrderStatusEnum;
    $scope.GetOrderDetail_Layout = function () {
        var encryptOrderId = encryptedServerString($scope.order_id);
        var encryptStoreId = encryptedServerString($scope.StoreDetailInSession.store_id);

        $http.get(configurationService.basePath + "api/OrderApi/GetOrderDetail_Layout?order_id=" + encryptOrderId + "&store_id=" + encryptStoreId)
          .then(function (response) {             
              $scope.OrderdetailObj = response.data;
            
          })
        .catch(function (response) {

        })
         .finally(function () {

         });
    }

    $scope.GetOrderDetail_Layout();

});