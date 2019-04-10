app.controller('dashboard_OrderCountController', function (StoreSessionDetail, $timeout, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, localStorageService, AdminUserDetail, OrderStatusEnum) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.AdminUserDetail = AdminUserDetail;

    $scope.OrderStatusEnum = [
   { Id: 1, Name: 'Processing' },
   { Id: 2, Name: 'Shipped' },
   { Id: 2, Name: 'Completed' },
   { Id: 3, Name: 'PartiallyShipped' },
   { Id: 4, Name: 'Returned' },
   { Id: 5, Name: 'Cancelled' },
   { Id: 6, Name: 'Delivered' }
    ];

    //$scope.ProcessingOrderStatusId = OrderStatusEnum.Processing;
    //$scope.DeliveredOrderStatusId = OrderStatusEnum.Delivered;
    //$scope.ReturnedOrderStatusId = OrderStatusEnum.Returned;

    $scope.GetOrderCountData = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetOrderDetailCount?Store_Id=" + $scope.AdminUserDetail.store_id)
        .then(function (response) {
            if (response.data.length > 0) {
                $scope.TopAlertStatusDropDown = response.data;
                //$scope.ProcessingOrder = $filter('filter')(response.data, { 'OrderStatusName': 'Processing' },true);
                //$scope.DeliveredOrder = $filter('filter')(response.data, { 'OrderStatusName': 'Delivered' }, true);
                //$scope.ReturnedOrder = $filter('filter')(response.data, { 'OrderStatusName': 'Returned' }, true);
                //$scope.ProcessingOrderCount = $scope.ProcessingOrder.length > 0 ? $scope.ProcessingOrder[0].OrderCount : 0;
                //$scope.DeliveredOrderCount = $scope.DeliveredOrder.length > 0 ? $scope.DeliveredOrder[0].OrderCount : 0;
                //$scope.ReturnedOrderCount = $scope.ReturnedOrder.length > 0 ? $scope.ReturnedOrder[0].OrderCount : 0;
            }
        })
    .catch(function (response) {

    })
    .finally(function () {

    });
    }

    $scope.GoToState = function (AlertObj) {
        if (AlertObj.NumberOfCount>0) {
            switch (AlertObj.type) {
                case 1:
                    var StatusId = $filter('filter')($scope.OrderStatusEnum, { 'Name': AlertObj.Name }, true);
                    $state.go('ShowOrders', ({ 'OrderStatusId': StatusId[0].Id }));
                    break;
                case 2:
                    $state.go('Customer', ({ 'CustomerApproved': 0 }));
                    break;
                case 3:
                    $state.go('Product', ({ 'Status': -3}));
                    break;
            }
        }
           
    }

    $scope.GetOrderCountData();
});