app.controller('Dashboard_StoreImageController', function (StoreSessionDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreDetailInSession = StoreSessionDetail;

    //$scope.Store_Id = StoreSessionDetail.store_id;
    //GetStoreImageList();
    //#endregion    
    $scope.GetStoreImageList = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetStoreImageList?Store_Id=" + $scope.StoreDetailInSession.store_id)
          .then(function (response) {
              if (response.data.length > 0) {              
                  $scope.StoreAdvertisementImageList = response.data;                
              }
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }

    $scope.GetStoreImageList();

});