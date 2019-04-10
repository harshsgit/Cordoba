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
        $http.get(configurationService.basePath + "api/OrderApi/GetOrderDetail_Layout?order_id=" + $scope.order_id + "&store_id=" + $scope.StoreDetailInSession.store_id)
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