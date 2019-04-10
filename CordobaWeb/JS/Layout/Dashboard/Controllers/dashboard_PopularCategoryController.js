app.controller('Dashboard_PopularCategoryController', function (StoreSessionDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  

    $scope.LatestProductList = [];
    //#endregion  
    $scope.StoreDetailInSession = StoreSessionDetail;

    $scope.GetPopularCategoryListByStoreId = function () {

        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetPopularCategoryListByStoreId?StoreID=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + $rootScope.CustomerDetail.customer_id)
          .then(function (response) {
              if (response.data.length > 0) {        
                  $scope.PopularCategoryList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.GetPopularCategoryListByStoreId();

});