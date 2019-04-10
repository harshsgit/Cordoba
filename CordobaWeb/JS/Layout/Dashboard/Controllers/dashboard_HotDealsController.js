app.controller('Dashboard_HotDealsController', function ($timeout,StoreSessionDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  
   

    $scope.HotDealsList = [];
    //#endregion  
    $scope.StoreDetailInSession = StoreSessionDetail;

    $scope.GetHotDealsListByStoreId = function () {

        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetHotDealsListByStoreId?StoreID=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + $rootScope.CustomerDetail.customer_id)
          .then(function (response) {           
              if (response.data.length > 0) {           
                  $scope.HotDealsList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetHotDealsListByStoreId();

});